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

        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}