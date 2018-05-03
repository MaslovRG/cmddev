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
    internal class Session : ISession
    {
        private const string settingsFileName = "settings.xml";
        private const string archivesFolderName = "Archives";
        private const string archiveFileNameSuffix = ".zip";

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
                ErrorCleanUp();
                throw new NoServiceSignInDataException(e);
            }
            catch (ApiException e)
            {
                ErrorCleanUp();
                throw new ServiceSignInFailedException(e);
            }
        }

        private void ErrorCleanUp()
        {
            if (client.IsLoggedIn)
                client.Logout();
            ReportProgress(SyncStage.Null);
        }

        private void SetupTempFolder()
        {
            string tempRoot = Path.GetTempPath();
            tempWorkFolderPath = Path.Combine(tempRoot, Path.GetRandomFileName());
            while (Directory.Exists(tempWorkFolderPath))
                tempWorkFolderPath = Path.Combine(tempRoot, Path.GetRandomFileName());
            Directory.CreateDirectory(tempWorkFolderPath);

            tempArchivesFolderPath = Path.Combine(tempWorkFolderPath, archivesFolderName);
            Directory.CreateDirectory(tempArchivesFolderPath);
        }

        private void SetupSettings()
        {
            string settingsPath = Path.Combine(tempWorkFolderPath, settingsFileName);
            INode node = GetFileNode(settingsFileName, workFolderNode);
            if (node != null)
                client.DownloadFile(node, settingsPath);
            GlobalSettings = new Settings(settingsPath, SettingsFileType.Global);
        }

        private INode GetFileNode(string name, INode parent)
        {
            return nodes.SingleOrDefault(n => n.Name == name && n.Type == NodeType.File && n.ParentId == parent.Id);
        }

        private void SetupWorkFolder(string path)
        {
            nodes = client.GetNodes();
            var pathFolders = path.Split('\\');
            var node = nodes.Single(n => n.Type == NodeType.Root);
            for (int i = 1; i < pathFolders.Length; ++i)
            {
                INode nextNode = nodes.SingleOrDefault(
                    n => n.ParentId == node.Id &&
                    n.Name == pathFolders[i] &&
                    n.Type == NodeType.Directory);

                if (nextNode == null)
                {
                    for (; i < pathFolders.Length; ++i)
                        node = client.CreateFolder(pathFolders[i], node);
                }
                else
                    node = nextNode;
            }

            workFolderNode = node;

            archivesFolderNode = nodes.SingleOrDefault(
                n => n.ParentId == workFolderNode.Id &&
                n.Name == archivesFolderName &&
                n.Type == NodeType.Directory);
            if (archivesFolderNode == null)
                archivesFolderNode = client.CreateFolder(archivesFolderName, workFolderNode);

            nodes = client.GetNodes();
        }

        public ISettings GlobalSettings { get; private set; }

        private MegaApiClient client = null;
        private IEnumerable<INode> nodes = null;
        private INode workFolderNode = null;
        private INode archivesFolderNode = null;
        private string tempWorkFolderPath = null;
        private string tempArchivesFolderPath = null;
        private IProgress<IProgressData> progess = null;

        public void DeleteGroup(IGroup group)
        {
            ReportProgress(SyncStage.UpdatingCloudData, group);

            if (group == null)
                throw new ArgumentNullException();
            if (!GlobalSettings.Groups.Contains(group))
                throw new ArgumentOutOfRangeException();

            GlobalSettings.Groups.Remove(group);            
            DeleteCloudFile(group.Name + archiveFileNameSuffix, archivesFolderNode);
            UpdateSettings();
        }

        void DeleteCloudFile(string name, INode parentNode)
        {
            INode node = GetFileNode(name, parentNode);
            if (node != null)
                client.Delete(node);
        }

        void UpdateCloudFile(string path, INode parentNode)
        {
            var name = Path.GetFileName(path);
            DeleteCloudFile(name, parentNode);
            client.UploadFile(path, parentNode);
        }

        public void Dispose()
        {
            ReportProgress(SyncStage.CleaningUp);

            if (client.IsLoggedIn)
                client.Logout();

            if (tempWorkFolderPath != null && Directory.Exists(tempWorkFolderPath))
            {
                Directory.Delete(tempWorkFolderPath, true);
                tempWorkFolderPath = null;
            }

            ReportProgress(SyncStage.Done);
        }

        public void SyncronizeGroups(IEnumerable<IGroup> localGroups)
        {
            foreach (IGroup localGroup in localGroups)
            {
                IGroup globalGroup = GlobalSettings.Groups.SingleOrDefault(g => g.Name == localGroup.Name);
                if (globalGroup == null || (localGroup.LastSync != null && localGroup.LastSync > globalGroup.LastSync))
                    UploadGroup(localGroup, globalGroup);
                else
                    DownloadGroup(localGroup, globalGroup);
            }
            UpdateSettings();
        }

        private void UpdateSettings()
        {
            ReportProgress(SyncStage.UpdatingCloudData);
            GlobalSettings.Save();
            UpdateCloudFile(GlobalSettings.FilePath, workFolderNode);
        }

        private void DownloadGroup(IGroup localGroup, IGroup globalGroup)
        {
            localGroup.UpdateLastSync(globalGroup.LastSync.Value);
            string archive = DownloadArchive(localGroup);
            ExtractArchive(localGroup, archive);
            DeleteArchive(localGroup, archive);

            ReportProgress(SyncStage.Done, localGroup);
        }

        private void DeleteArchive(IGroup localGroup, string archive)
        {
            ReportProgress(SyncStage.CleaningUp, localGroup);
            Directory.Delete(archive, true);
            File.Delete(archive + archiveFileNameSuffix);
        }

        private void ExtractArchive(IGroup localGroup, string archive)
        {
            ReportProgress(SyncStage.CopyingFiles, localGroup);
            Directory.CreateDirectory(archive);
            ZipFile.ExtractToDirectory(archive + archiveFileNameSuffix, archive);

            foreach (INamePath file in localGroup.Files)
                File.Copy(Path.Combine(archive, file.Name), file.Path, true);
            
            foreach (INamePath folder in localGroup.Folders)
            {
                if (Directory.Exists(folder.Path))
                    Directory.Delete(folder.Path, true);
                DirectoryCopy(Path.Combine(archive, folder.Name), folder.Path);
            }
        }

        private string DownloadArchive(IGroup group)
        {
            ReportProgress(SyncStage.DownloadingArchive, group);
            INode archiveNode = nodes
                .SingleOrDefault(n => n.Name == group.Name + archiveFileNameSuffix && n.Type == NodeType.File && n.ParentId == archivesFolderNode.Id);
            string archive = Path.Combine(tempArchivesFolderPath, group.Name);
            client.DownloadFile(archiveNode, archive + archiveFileNameSuffix);
            return archive;
        }

        private void UploadGroup(IGroup localGroup, IGroup globalGroup)
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

            UploadArchive(localGroup, archive);
            DeleteArchive(localGroup, archive);

            ReportProgress(SyncStage.Done, localGroup);
        }

        private void UploadArchive(IGroup localGroup, string archive)
        {
            ReportProgress(SyncStage.UpdatingCloudData, localGroup);
            UpdateCloudFile(archive + archiveFileNameSuffix, archivesFolderNode);
        }

        private string CreateArchive(IGroup localGroup)
        {
            ReportProgress(SyncStage.CopyingFiles, localGroup);
            string archive = Path.Combine(tempArchivesFolderPath, localGroup.Name);

            Directory.CreateDirectory(archive);
            foreach (INamePath file in localGroup.Files)
                File.Copy(file.Path, Path.Combine(archive, file.Name), true);
            foreach (INamePath folder in localGroup.Folders)
                DirectoryCopy(folder.Path, Path.Combine(archive, folder.Name));

            ReportProgress(SyncStage.CreatingArchive, localGroup);
            ZipFile.CreateFromDirectory(archive, archive + archiveFileNameSuffix);

            return archive;
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

        public void ReloadMetadata()
        {
            nodes = client.GetNodes(workFolderNode);
            archivesFolderNode = nodes.SingleOrDefault(
                n => n.Name == "Archives" &&
                n.Type == NodeType.Directory &&
                n.ParentId == workFolderNode.Id);
        }
    }
}
