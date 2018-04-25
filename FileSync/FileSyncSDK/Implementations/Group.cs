using FileSyncSDK.Enums;
using FileSyncSDK.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace FileSyncSDK.Implementations
{
    internal class Group : IGroup
    {
        public Group(string name, string[] files, string[] folders)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("Group name must not be null or empty string.");
            if (IsNullOrEmpty(files) && IsNullOrEmpty(folders))
                throw new ArgumentNullException("Files and folders arrays cannot be both null or empty.");
            if (files != null)
            {
                if (!IsAllDistinct(files))
                    throw new ArgumentException("Array must have all distinct values.", "files");
                if (!IsAllDistinct(files.Select(s => Path.GetFileName(s))))
                    throw new ArgumentException("All files must have different names no matter of their path.");
            }
            if (folders != null)
            {
                if (!IsAllDistinct(folders))
                    throw new ArgumentException("Array must have all distinct values.", "folders");
                if (!IsAllDistinct(folders.Select(s => Path.GetFileName(s))))
                    throw new ArgumentException("All folders must have different names no matter of their path.");
            }
            if (HaveIntersection(files, folders))
                throw new ArgumentException("Arrays must not have intersection.");

            Name = name;
            LastSync = null;
            Files = CreateNamePathList(files);
            Folders = CreateNamePathList(folders);
        }

        public Group(XElement element)
        {
            ImportFromXml(element);
        }

        private bool IsNullOrEmpty(IEnumerable<string> data)
        {
            return data == null || !data.Any();
        }

        private bool IsAllDistinct(IEnumerable<string> data)
        {
            return data.Distinct().Count() == data.Count();
        }

        public bool HaveIntersection(IEnumerable<string> data1, IEnumerable<string> data2)
        {
            return data1 != null && data2 != null && data1.Intersect(data2).Any();
        }

        private IReadOnlyList<INamePath> CreateNamePathList(string[] filenames)
        {
            if (filenames == null)
                return new List<INamePath>();

            return filenames.Select(s => new NamePath(Path.GetFileName(s), s)).ToList<INamePath>();
        }

        public string Name { get; private set; }

        public DateTime? LastSync { get; private set; }

        public IReadOnlyList<INamePath> Files { get; private set; }

        public IReadOnlyList<INamePath> Folders { get; private set; }

        public XElement ExportToXml()
        {
            var group = new XElement("group");
            group.SetAttributeValue("name", Name);
            if (LastSync != null)
                group.SetAttributeValue("lastSync", LastSync.ToString());

            group.Add(Files.Select(np => new XElement("file", new XAttribute("name", np.Name), new XText(np.Path))));
            group.Add(Folders.Select(np => new XElement("folder", new XAttribute("name", np.Name), new XText(np.Path))));

            return group;
        }

        public void ImportFromXml(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException();
            if (element.Name != "group")
                throw new ArgumentException("XElement isn't \"group\" element.");
            if (element.Attribute("name") == null)
                throw new ArgumentException("XElement has no \"name\" attribute.");
            if (!element.HasElements || (!HasSpecificElements(element, "file") && !HasSpecificElements(element, "folder")))
                throw new ArgumentException("XElement has no \"file\" or \"folder\" child elements.");

            List<INamePath> files = ImportNamePath(element, "file");
            List<INamePath> folders = ImportNamePath(element, "folder");
            if (HaveIntersection(files.Select(np => np.Path), folders.Select(np => np.Path)))
                throw new ArgumentException("XElement data corrupted: files and folders path values intersection is not null.");

            if (element.Attribute("lastSync") == null)
                LastSync = null;
            else
                LastSync = DateTime.Parse(element.Attribute("lastSync").Value);

            Name = element.Attribute("name").Value;
            Files = files;
            Folders = folders;
        }

        private List<INamePath> ImportNamePath(XElement parentElement, string elementsName)
        {
            List<INamePath> list = null;

            try
            {
               list = parentElement.Elements()
                    .Where(e => e.Name == elementsName)
                    .Select(e => new NamePath(e.Attribute("name")?.Value, e.Value))
                    .ToList<INamePath>();
            }
            catch(ArgumentNullException e)
            {
                throw new ArgumentException("XElement data corrupted: NamePath pair creation failed.", e);
            }

            if (list.Select(np => np.Name).Distinct().Count() != list.Count)
                throw new ArgumentException("XElement data corrupted: Equal " + elementsName + " names found.");

            if (list.Select(np => np.Path).Distinct().Count() != list.Count)
                throw new ArgumentException("XElement data corrupted: Equal " + elementsName + " path data found.");

            list = list.OrderBy(np => np.Name).ToList();
            return list;
        }

        private bool HasSpecificElements(XElement element, string childElementName)
        {
            return element.Elements().Where(e => e.Name == childElementName).Any();
        }

        public void UpdateLastSync(DateTime dateTime)
        {
            LastSync = dateTime;
        }
    }
}
