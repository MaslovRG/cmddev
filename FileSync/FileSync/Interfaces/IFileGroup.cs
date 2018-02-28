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
    interface IFileGroup : IXmlManagable
    {
        /// <summary>
        /// Имя группы на локальной машине
        /// </summary>
        string LocalName { get; set; }

        /// <summary>
        /// Имя группы на облачном хранилище
        /// </summary>
        string GlobalName { get; set; }

        /// <summary>
        /// Дата последней синхронизации на локальной машине
        /// </summary>
        DateTime LocalLastSync { get; set; }

        /// <summary>
        /// Дата последней синхронизации в облаке
        /// </summary>
        DateTime GlobalLastSync { get; set; }

        /// <summary>
        /// Список файлов
        /// </summary>
        List<INamePath> Files { get; set; }

        /// <summary>
        /// Список папок
        /// </summary>
        List<INamePath> Folders { get; set; }
    }
}
