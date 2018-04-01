using FileSync.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FileSync.Interfaces
{
    /// <summary>
    /// Интерфейс для именованной группы файлов или папок
    /// </summary>
    interface IGroup : IXmlManagable
    {
        /// <summary>
        /// Уникальное имя группы.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Дата последней синхронизации на локальной машине. Если группы нет в локальном списке - null.
        /// </summary>
        DateTime? LocalLastSync { get; }

        /// <summary>
        /// Дата последней синхронизации в облаке. Если группы нет в глобальном списке - null.
        /// </summary>
        DateTime? GlobalLastSync { get; }

        /// <summary>
        /// Список файлов.
        /// </summary>
        IReadOnlyList<INamePath> Files { get; }

        /// <summary>
        /// Список папок.
        /// </summary>
        IReadOnlyList<INamePath> Folders { get; }

        /// <summary>
        /// Флаг синхронизации - <code>true</code> если группа синхронизируется между локальным и облачным хранилищем
        /// </summary>
        bool Syncronize { get; set; }
    }
}
