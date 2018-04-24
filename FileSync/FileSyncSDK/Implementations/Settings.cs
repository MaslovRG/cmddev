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
        /// <param name="filePath"></param>
        /// <exception cref="ArgumentNullException"><paramref name="filePath"/> равен null.</exception>
        /// <exception cref="FileNotFoundException">Глобальный файл настроек не найден по указанному пути.</exception>
        public Settings(string filePath)
        {
            FilePath = filePath;
            CloudService = null; 
            Load(); 
        }

        public Settings(string filePath, SettingsFileType type) : this(filePath)
        {
            Type = type;
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
            
            if (xElement.Name == "localSettings")
            {
                Type = SettingsFileType.Local;
                if (xElement.Element("services") != null)
                    if (xElement.Element("services").Element("service") != null)
                        CloudService = new CloudService(xElement.Element("services").Element("service"));
            }
            else
                Type = SettingsFileType.Global; 

            IEnumerable<XElement> groups = xElement.Element("groups").Elements("group");
            Groups = new List<IGroup>(); 

            foreach (XElement group in groups)
            {
                Groups.Add(new Group(group)); 
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