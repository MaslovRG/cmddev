using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FileSyncSDK.Interfaces
{
    /// <summary>
    /// Интерфейс для работы с облачным сервисом
    /// </summary>
    /// <remarks>
    /// В случае нескольких облачных сервисов подразумевается использование коллекции синхронизаторов
    /// </remarks>
    internal interface ICloudService : IXmlManagable
    {
        /// <summary>
        /// Имя облачного сервиса
        /// </summary>
        string ServiceName { get; set; }

        /// <summary>
        /// Путь к корневой папке синхронизатора в древе папок облачного хранилища
        /// </summary>
        string ServiceFolderPath { get; set; }

        /// <summary>
        /// Имя пользователя на облачном сервисе
        /// </summary>
        string UserLogin { get; set; }

        /// <summary>
        /// Пароль пользователя на облачном сервисе
        /// </summary>
        string UserPassword { get; set; }

        /// <summary>
        /// Открывает сессию работы с облачным сервисом
        /// </summary>
        /// <returns>Объект сессии взаимодействия с облачным сервисом</returns>
        ISession OpenSession(IProgress<IProgressData> progress = null);

        /// <summary>
        /// Устанавливает все значения на значения по-умолчанию
        /// </summary>
        void Clear();
    }
}
