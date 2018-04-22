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
            if (files != null && !IsAllDistinct(files))
                throw new ArgumentException("Array must have all distinct values.", "files");
            if (folders != null && !IsAllDistinct(folders))
                throw new ArgumentException("Array must have all distinct values.", "folders");

            Name = name;
            LastSync = null;
            Files = CreateNamePathList(files);
            Folders = CreateNamePathList(folders);
        }

        public Group(XElement element)
        {
            ImportFromXml(element);
        }

        private bool IsNullOrEmpty(string[] array)
        {
            return array == null || array.Length == 0;
        }

        private bool IsAllDistinct(string[] array)
        {
            return array.Distinct().Count() == array.Length;
        }

        private IReadOnlyList<INamePath> CreateNamePathList(string[] filenames)
        {
            if (filenames == null)
                return new List<INamePath>();

            var grouings = filenames.GroupBy(fn => Path.GetFileName(fn));
            var singles = grouings.Where(g => g.Count() == 1).ToList();
            var multiples = grouings.Where(g => g.Count() > 1).ToList();

            List<INamePath> result = singles.Select(g => new NamePath(g.Key, g.FirstOrDefault())).ToList<INamePath>();
            foreach (var grouping in multiples)
            {
                var list = grouping.ToList();
                for (int i = 0; i < list.Count; ++i)
                    result.Add(new NamePath(grouping.Key + "$" + i, list[i]));
            }
            result = result.OrderBy(np => np.Name).ToList();

            return result;
        }

        public string Name { get; private set; }

        public DateTime? LastSync { get; private set; }

        public IReadOnlyList<INamePath> Files { get; private set; }

        public IReadOnlyList<INamePath> Folders { get; private set; }

        public XElement ExportToXml()
        {
            throw new NotImplementedException();
        }

        public void ImportFromXml(XElement element)
        {
            throw new NotImplementedException();
        }

        public void UpdateLastSync(DateTime dateTime)
        {
            LastSync = dateTime;
        }
    }
}
