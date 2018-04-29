using CG.Web.MegaApiClient;
using FileSyncSDK.Enums;
using FileSyncSDK.Exceptions;
using FileSyncSDK.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace FileSyncSDK.Implementations
{
    // TODO - progress reports
    internal class Session : ISession
    {
        private const string settingsFileName = "settings.txt";

        public Session(string login, string password, string path, IProgress<IProgressData> progess = null)
        {
            this.progess = progess;
            ReportProgress(SyncStage.ProcessSetup);

            try
            {
                client = new MegaApiClient();
                client.Login(login, password);
                SetupWorkFolder(path);
                SetupTempFolder();
                SetupSettings();
            }
            catch (ArgumentNullException e)
            {
                throw new NoServiceSignInDataException(e);
            }
            catch (ApiException e)
            {
                throw new ServiceSignInFailedException(e);
            }
            finally
            {
                if (client.IsLoggedIn)
                    client.Logout();
                ReportProgress(SyncStage.Null);
            }
        }

        private void SetupTempFolder()
        {
            string tempRoot = Path.GetTempPath();
            tempFolderPath = Path.Combine(tempRoot, Path.GetRandomFileName());
            while (Directory.Exists(tempFolderPath))
                tempFolderPath = Path.Combine(tempRoot, Path.GetRandomFileName());
            Directory.CreateDirectory(tempFolderPath);
        }

        private void SetupSettings()
        {
            string settingsPath = Path.Combine(tempFolderPath, settingsFileName);
            INode node = GetFileNode(settingsFileName);
            if (node != null)
                client.DownloadFile(node, settingsPath);
            GlobalSettings = new Settings(settingsPath, SettingsFileType.Global);
        }

        private INode GetFileNode(string name, IEnumerable<INode> nodes = null)
        {
            if (nodes == null)
                nodes = client.GetNodes(workFolderNode);
            return nodes.SingleOrDefault(n => n.Name == name && n.Type == NodeType.File && n.ParentId == workFolderNode.Id);
        }

        private void SetupWorkFolder(string path)
        {
            var pathFolders = path.Split('\\');
            var nodes = client.GetNodes();
            var parentNode = nodes.Single(n => n.Type == NodeType.Root);
            for (int i = 1; i < pathFolders.Length; ++i)
            {
                var node = nodes.SingleOrDefault(n => n.ParentId == parentNode.Id && n.Name == pathFolders[i] && n.Type == NodeType.Directory);
                if (node == null)
                {
                    node = client.CreateFolder(pathFolders[i], parentNode);
                    nodes = client.GetNodes(node);
                }
                parentNode = node;
            }

            workFolderNode = parentNode;
        }

        public ISettings GlobalSettings { get; private set; }

        private MegaApiClient client = null;
        private INode workFolderNode = null;
        private IProgress<IProgressData> progess = null;
        private string tempFolderPath = null;

        public void DeleteGroup(IGroup group)
        {
            ReportProgress(SyncStage.UpdatingCloudData, group);

            if (group == null)
                throw new ArgumentNullException();
            if (!GlobalSettings.Groups.Contains(group))
                throw new ArgumentOutOfRangeException();

            GlobalSettings.Groups.Remove(group);            
            var nodes = client.GetNodes(workFolderNode);
            DeleteCloudFile(group.Name, nodes);
            UpdateSettings(nodes);
        }

        void DeleteCloudFile(string name, IEnumerable<INode> nodes = null)
        {
            INode node = GetFileNode(name, nodes);
            if (node != null)
                client.Delete(node);
        }

        void UpdateCloudFile(string path, IEnumerable<INode> nodes = null)
        {
            var name = Path.GetFileName(path);
            DeleteCloudFile(name, nodes);
            client.UploadFile(path, workFolderNode);
        }

        public void Dispose()
        {
            ReportProgress(SyncStage.CleaningUp);

            if (client.IsLoggedIn)
                client.Logout();

            if (tempFolderPath != null && Directory.Exists(tempFolderPath))
            {
                Directory.Delete(tempFolderPath, true);
                tempFolderPath = null;
            }

            ReportProgress(SyncStage.Done);
        }

        public void SyncronizeGroups(IEnumerable<IGroup> localGroups)
        {
            var nodes = client.GetNodes(workFolderNode);
            foreach (IGroup localGroup in localGroups)
            {
                IGroup globalGroup = GlobalSettings.Groups.SingleOrDefault(g => g.Name == localGroup.Name);
                if (globalGroup == null || (localGroup.LastSync != null && localGroup.LastSync > globalGroup.LastSync))
                    UploadGroup(localGroup, globalGroup, nodes);
                else
                    DownloadGroup(localGroup, globalGroup, nodes);
            }
            UpdateSettings(nodes);
        }

        private void UpdateSettings(IEnumerable<INode> nodes = null)
        {
            ReportProgress(SyncStage.UpdatingCloudData);

            if (nodes == null)
                nodes = client.GetNodes(workFolderNode);

            GlobalSettings.Save();
            UpdateCloudFile(GlobalSettings.FilePath, nodes);
        }

        private void DownloadGroup(IGroup localGroup, IGroup globalGroup, IEnumerable<INode> nodes)
        {
            localGroup.UpdateLastSync(globalGroup.LastSync.Value);

        }

        private void UploadGroup(IGroup localGroup, IGroup globalGroup, IEnumerable<INode> nodes)
        {
            string archive = CreateArchive(localGroup);

            localGroup.UpdateLastSync(DateTime.Now);
            if (globalGroup == null)
            {
                globalGroup = new Group(localGroup);
                GlobalSettings.Groups.Add(globalGroup);
            }
            else
                globalGroup.UpdateLastSync(localGroup.LastSync.Value);

            ReportProgress(SyncStage.UpdatingCloudData, localGroup);
            UpdateCloudFile(archive, nodes);
        }

        private string CreateArchive(IGroup localGroup)
        {
            ReportProgress(SyncStage.CreatingArchive, localGroup);
            string groupFolder = Directory.CreateDirectory(Path.Combine(tempFolderPath, localGroup.Name)).Name;
            foreach (INamePath file in localGroup.Files)
                File.Copy(file.Path, Path.Combine(groupFolder, file.Name));
            foreach (INamePath folder in localGroup.Files)
                DirectoryCopy(folder.Path, Path.Combine(groupFolder, folder.Name));
            ZipFile.CreateFromDirectory(groupFolder, groupFolder);

            ReportProgress(SyncStage.CleaningUp, localGroup);
            Directory.Delete(groupFolder, true);

            return groupFolder;
        }

        private void ReportProgress(SyncStage stage, IGroupData group = null, string info = null)
        {
            progess?.Report(new ProgressData(stage, group, info));
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);

            DirectoryInfo[] dirs = dir.GetDirectories();
            if (!Directory.Exists(destDirName))
                Directory.CreateDirectory(destDirName);

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            foreach (DirectoryInfo subdir in dirs)
            {
                string temppath = Path.Combine(destDirName, subdir.Name);
                DirectoryCopy(subdir.FullName, temppath);
            }
        }
    }
}
