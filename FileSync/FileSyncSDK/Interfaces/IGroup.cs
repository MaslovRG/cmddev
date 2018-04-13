using FileSyncSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FileSyncSDK.Interfaces
{
    /// <summary>
    /// Интерфейс для именованной группы файлов или папок
    /// </summary>
    public interface IGroup : IXmlManagable
    {
        /// <summary>
        /// Уникальное имя группы.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Дата последней синхронизации. Если группы нет в списке - null.
        /// </summary>
        DateTime? LastSync { get; }

        /// <summary>
        /// Список файлов.
        /// </summary>
        IReadOnlyList<INamePath> Files { get; }

        /// <summary>
        /// Список папок.
        /// </summary>
        IReadOnlyList<INamePath> Folders { get; }
    }
}
