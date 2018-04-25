using CG.Web.MegaApiClient;
using FileSyncSDK.Exceptions;
using FileSyncSDK.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
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
            try
            {
                this.progess = progess;
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
            GlobalSettings = new Settings(settingsPath, Enums.SettingsFileType.Global);
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
            if (group == null)
                throw new ArgumentNullException();
            if (!GlobalSettings.Groups.Contains(group))
                throw new ArgumentOutOfRangeException();

            GlobalSettings.Groups.Remove(group);
            GlobalSettings.Save();

            var nodes = client.GetNodes(workFolderNode);
            DeleteCloudFile(group.Name, nodes);
            UpdateCloudFile(GlobalSettings.FilePath, nodes);
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
            if (client.IsLoggedIn)
            {
                UploadSettings();
                client.Logout();
            }
            if (tempFolderPath != null && Directory.Exists(tempFolderPath))
            {
                Directory.Delete(tempFolderPath, true);
                tempFolderPath = null;
            }
        }

        private void UploadSettings()
        {
            throw new NotImplementedException();
        }

        public void SyncronizeGroups(IEnumerable<IGroup> localGroups)
        {
            throw new NotImplementedException();
        }
    }
}
