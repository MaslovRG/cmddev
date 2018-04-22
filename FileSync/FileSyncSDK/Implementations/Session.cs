using FileSyncSDK.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileSyncSDK.Implementations
{
    internal class Session : ISession
    {
        public Session(string login, string password, string path, IProgress<IProgressData> progess = null)
        {
            throw new NotImplementedException();
        }

        public ISettings GlobalSettings { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void DeleteGroup(IGroup group)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void SyncronizeGroups(IGroup local, IGroup global)
        {
            throw new NotImplementedException();
        }
    }
}
