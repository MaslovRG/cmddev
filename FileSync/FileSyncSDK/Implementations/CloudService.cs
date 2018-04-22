using FileSyncSDK.Enums;
using FileSyncSDK.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace FileSyncSDK.Implementations
{
    internal class CloudService : ICloudService
    {
        public CloudService()
        {
            throw new NotImplementedException();
        }

        public string ServiceName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ServiceFolderPath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string UserLogin { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string UserPassword { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public XElement ExportToXml()
        {
            throw new NotImplementedException();
        }

        public void ImportFromXml(XElement element)
        {
            throw new NotImplementedException();
        }

        public ISession OpenSession(IProgress<IProgressData> progress = null)
        {
            throw new NotImplementedException();
        }
    }
}
