using FileSyncSDK.Enums;
using FileSyncSDK.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

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
        }

        public string FilePath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public SettingsFileType Type { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IList<IGroup> Groups => throw new NotImplementedException();

        public string UserLogin { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ServiceName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ServiceFolderPath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Load()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}