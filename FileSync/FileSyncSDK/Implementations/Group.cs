using FileSyncSDK.Enums;
using FileSyncSDK.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FileSyncSDK.Implementations
{
    internal class Group : IGroup
    {
        public Group(string name, string[] files, string[] folders)
        {
            Name = string.IsNullOrEmpty(name) ? throw new ArgumentNullException("Name must not me null or empty.") : name;
            LastSync = null;
            Files = CreateNamePathList(files);
            Folders = CreateNamePathList(folders);
        }

        public Group(XmlNode node)
        {
            ImportFromXml(node);
        }

        private IReadOnlyList<INamePath> CreateNamePathList(string[] files)
        {
            throw new NotImplementedException();
        }

        public string Name { get; private set; }

        public DateTime? LastSync { get; private set; }

        public IReadOnlyList<INamePath> Files { get; private set; }

        public IReadOnlyList<INamePath> Folders { get; private set; }

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
