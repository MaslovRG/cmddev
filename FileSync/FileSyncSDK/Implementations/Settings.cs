using FileSyncSDK.Enums;
using FileSyncSDK.Exceptions;
using FileSyncSDK.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace FileSyncSDK.Implementations
{
    internal class Settings : ISettings
    {
        /// <summary>
        /// Основной конструктор класса настроек
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="filePath"/> равен null.</exception>
        public Settings(string filePath, SettingsFileType type)
        {
            InitData();
            Type = type;
            FilePath = filePath;
        }

        private string filePath; 

        public string FilePath
        {
            get
            {
                return filePath; 
            }
            set
            {
                filePath = value ?? throw new ArgumentNullException();
                Load(); 
            }
        }

        public SettingsFileType Type { get; set; }

        public IList<IGroup> Groups { get; private set; }

        public ICloudService CloudService { get; set; }

        public void Load()
        {
            if (File.Exists(filePath))
            {
                switch (Type)
                {
                    case SettingsFileType.Global:
                        LoadGlobalSettings();
                        return;
                    case SettingsFileType.Local:
                        LoadLocalSettings();
                        return;
                    default:
                        throw new NotImplementedException();
                }
            }
            else
                SetNullData();
        }

        private void SetNullData()
        {
            Groups.Clear();
            CloudService.Clear();
        }

        private void LoadLocalSettings()
        {
            try
            {
                XElement root = LoadXmlRoot("localSettings");
                LoadGroups(root);
                LoadCloudService(root);
            }
            catch (Exception e)
            {
                throw new SettingsDataCorruptedException(e);
            }
        }

        private void LoadCloudService(XElement root)
        {
            try
            {
                var services = root.Elements().SingleOrDefault(n => n.Name == "services");
                if (services == null)
                    CloudService.Clear();
                else
                    CloudService.ImportFromXml(services.Elements().FirstOrDefault(el => el.Name == "service"));
            }
            catch (Exception e)
            {
                throw new SettingsDataCorruptedException(e);
            }
        }

        private void LoadGroups(XElement root)
        {
            Groups.Clear();
            var groups = root.Elements().SingleOrDefault(n => n.Name == "groups");
            if (groups != null)
            {
                var groupList = groups.Elements().Where(el => el.Name == "group").ToList();
                foreach (var group in groupList)
                    Groups.Add(new Group(group));
            }
        }

        private XElement LoadXmlRoot(string rootName)
        {
            XDocument document = XDocument.Load(filePath);
            XElement root = document.Root;
            if (root.Name == rootName)
                return root;
            else
                throw new ArgumentException("Incorrect root element name.");
        }

        private void LoadGlobalSettings()
        {
            XElement root = LoadXmlRoot("globalSettings");
            LoadGroups(root);
            LoadCloudService(root);
        }

        private void InitData()
        {
            Groups = new List<IGroup>();
            CloudService = new CloudService();
        }

        public void Save()
        {
            switch (Type)
            {
                case SettingsFileType.Global:
                    SaveGlobalSettings();
                    return;
                case SettingsFileType.Local:
                    SaveLocalSettings();
                    return;
                default:
                    throw new NotImplementedException();
            }
        }

        private void SaveLocalSettings()
        {
            var root = new XElement("localSettings");
            if (CloudService != null)
                root.Add(new XElement("services", CloudService.ExportToXml()));
            if (Groups.Count > 0)
                root.Add(ExportGroupsToXml());
            SaveDocument(root);
        }

        private void SaveDocument(XElement root)
        {
            var document = new XDocument(root);
            document.Save(filePath);
        }

        private XElement ExportGroupsToXml()
        {
            var groups = new XElement("groups");
            foreach (IGroup group in Groups)
                groups.Add(group.ExportToXml());
            return groups;
        }

        private void SaveGlobalSettings()
        {
            var root = new XElement("globalSettings");
            if (Groups.Count > 0)
                root.Add(ExportGroupsToXml());
            SaveDocument(root);
        }
    }
}