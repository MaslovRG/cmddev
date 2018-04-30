using FileSyncSDK.Enums;
using FileSyncSDK.Exceptions;
using System.Collections.Generic;

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
        /// Облачный сервис
        /// </summary>
        ICloudService CloudService { get; set; }

        /// <summary>
        /// Сохранение текущего списка групп в файл.
        /// </summary>
        void Save();

        /// <summary>
        /// Загрузка текущего списка групп из файла.
        /// </summary>
        /// <exception cref="SettingsDataCorruptedException">Данные в файле не соответствуют ожидаемому формату.</exception>
        void Load();
    }
}
