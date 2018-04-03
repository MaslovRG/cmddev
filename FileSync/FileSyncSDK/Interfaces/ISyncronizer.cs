using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FileSyncSDK.Interfaces
{
    /// <summary>
    /// Интерфейс для механизма синхронизации с одним облачным сервисом
    /// </summary>
    /// <remarks>
    /// В случае нескольких облачных сервисов подразумевается использование коллекции синхронизаторов
    /// </remarks>
    internal interface ISyncronizer : IXmlManagable
    {
        /// <summary>
        /// Имя облачного сервиса
        /// </summary>
        string CloudServiceName { get; set; }

        /// <summary>
        /// Путь к корневой папке синхронизатора в древе папок облачного хранилища
        /// </summary>
        string[] CloudServiceFolder { get; set; }

        /// <summary>
        /// Имя пользователя на облачном сервисе
        /// </summary>
        string CloudServiceLogin { get; set; }

        /// <summary>
        /// Пароль пользователя на облачном сервисе
        /// </summary>
        string CloudServicePassword { get; set; }

        /// <summary>
        /// Синхронизация группы файлов с облачным хранилищем
        /// </summary>
        /// <param name="group">
        /// Группа файлов для синхронизации
        /// </param>
        void SyncronizeGroup(IGroup group);

        /// <summary>
        /// Загрузка глобального файла настроек на облако
        /// </summary>
        /// <param name="path">Путь к глобальному файлу настроек.</param>
        void UploadSettings(string path);

        /// <summary>
        /// Загрузка глобального файла настроек на локальный компьютер
        /// </summary>
        /// <param name="path">Путь к глобальному файлу настроек</param>
        void DownloadSettings(string path);
    }
}
