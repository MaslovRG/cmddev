using FileSyncSDK.Enums;
using FileSyncSDK.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace FileSyncSDK.Implementations
{
    internal class Settings : ISettings
    {
        /// <summary>
        /// Основной конструктор класса настроек
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filePath"></param>
        /// <exception cref="ArgumentNullException"><paramref name="filePath"/> равен null.</exception>
        /// <exception cref="FileNotFoundException">Глобальный файл настроек не найден по указанному пути.</exception>
        public Settings(SettingsFileType type, string filePath)
        {
            Type = type;
            FilePath = filePath;
            Load(); 
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
                filePath = value;
                Load(); 
            }
        }

        public SettingsFileType Type { get; set; }

        public IList<IGroup> Groups { get; set; }

        public ICloudService CloudService { get; set; }

        public void Load()
        {
            if (!File.Exists(FilePath))
                throw new ArgumentNullException();
            string file = File.ReadAllText(FilePath);
            XElement xElement = XElement.Parse(file);

            IEnumerable<XElement> groups = xElement.Element("groups").Elements("group");
            Groups = new List<IGroup>(); 

            foreach (XElement group in groups)
            {
                Groups.Add(new Group(group)); 
            }

            if (Type == SettingsFileType.Local)
            {
                CloudService = new CloudService(xElement.Element("services").Element("service")); 
            }
        }

        public void Save()
        {
            XElement settings = new XElement("globalSettings"); 
            XElement groups = new XElement("groups"); 
            foreach (IGroup group in Groups)
            {
                groups.Add(group.ExportToXml()); 
            }
            settings.Add(groups); 

            if (Type == SettingsFileType.Local)
            {
                settings.Name = "localSettings"; 
                XElement services = new XElement("services");
                services.Add(CloudService.ExportToXml());
                settings.AddFirst(services); 
            }

            XDocument document = new XDocument(settings);            
            document.Save(FilePath); 
        }
    }
}