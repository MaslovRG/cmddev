using FileSyncSDK.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileSyncSDK.Interfaces
{
    /// <summary>
    /// Интерфейс для работы с файлом настроек.
    /// </summary>
    internal interface ISettings
    {
        /// <summary>
        /// Путь к файлу настроек.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        string FilePath { get; set; }

        /// <summary>
        /// Тип файла настроек.
        /// </summary>
        SettingsFileType Type { get; set; }

        /// <summary>
        /// Список групп синхронизации, загруженный из файла настроек или ожидающий записи.
        /// </summary>
        IList<IGroup> Groups { get; }

        /// <summary>
        /// Имя пользователя для облачного сервиса. При работе с глобальным файлом настроек - null.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        string UserLogin { get; set; }

        /// <summary>
        /// Имя облачного сервиса. При работе с глобальным файлом настроек - null.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        string ServiceName { get; set; }

        /// <summary>
        /// Путь (в виде массива названий узлов) к рабочей директории в облачном сервисе.
        /// При работе с глобальным файлом настроек - null.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        string ServiceFolderPath { get; set; }

        /// <summary>
        /// Сохранение текущего списка групп в файл.
        /// </summary>
        void Save();

        /// <summary>
        /// Загрузка текущего списка групп из файла.
        /// </summary>
        void Load();
    }
}
