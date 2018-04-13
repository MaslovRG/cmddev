using FileSyncSDK.Enums;
using FileSyncSDK.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace FileSyncSDK.Implementations
{
    internal class Group : IGroup
    {
        public string Name => throw new NotImplementedException();

        public DateTime? LastSync => throw new NotImplementedException();

        public IReadOnlyList<INamePath> Files => throw new NotImplementedException();

        public IReadOnlyList<INamePath> Folders => throw new NotImplementedException();

        public XmlNode ExportToXml()
        {
            throw new NotImplementedException();
        }

        public void ImportFromXml(XmlNode node)
        {
            throw new NotImplementedException();
        }
    }
}
