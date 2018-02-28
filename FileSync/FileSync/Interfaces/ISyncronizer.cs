using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FileSync.Interfaces
{
    /// <summary>
    /// Интерфейс для механизма синхронизации с одним облачным сервисом
    /// </summary>
    /// <remarks>
    /// В случае нескольких облачных сервисов подразумевается использование коллекции синхронизаторов
    /// </remarks>
    interface ISyncronizer : IXmlManagable
    {
        /// <summary>
        /// Имя облачного сервиса
        /// </summary>
        string CloudServiceName { get; set; }

        /// <summary>
        /// Путь к корневой папке синхронизатора в древе папок облачного хранилища
        /// </summary>
        string CloudServiceFolder { get; set; }

        /// <summary>
        /// Синхронизация группы файлов с облачным хранилищем
        /// </summary>
        /// <param name="group">
        /// Группа файлов для синхронизации
        /// </param>
        void SyncronizeGroup(IFileGroup group);
    }
}
