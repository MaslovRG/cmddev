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
        public Session(string login, string password, string path, IProgress<IProgressData> progess = null)
        {
            try
            {
                this.progess = progess;
                client = new MegaApiClient();
                client.Login(login, password);
                SetupWorkFolder(path);
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

        public ISettings GlobalSettings { get => throw new NotImplementedException(); }

        private ISettings globalSettings = null;
        private MegaApiClient client = null;
        private INode workFolderNode = null;
        private IProgress<IProgressData> progess = null;
        private string tempFolderPath = null;

        public void DeleteGroup(IGroup group)
        {
            throw new NotImplementedException();
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
