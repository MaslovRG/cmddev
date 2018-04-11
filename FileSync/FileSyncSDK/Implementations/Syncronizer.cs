using FileSyncSDK.Enums;
using FileSyncSDK.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace FileSyncSDK.Implementations
{
    internal class Syncronizer : ISyncronizer
    {
        public Syncronizer(string name, string login, string password, string folderPath)
        {
            throw new NotImplementedException();
        }

        public string ServiceName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ServiceFolderPath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string UserLogin { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string UserPassword { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Dispose()
        {
            // Logout
            throw new NotImplementedException();
        }

        public void DownloadSettings(string path)
        {
            throw new NotImplementedException();
        }

        public void SyncronizeGroup(IGroup group)
        {
            throw new NotImplementedException();
        }

        public void UploadSettings(string path)
        {
            throw new NotImplementedException();
        }
    }
}
