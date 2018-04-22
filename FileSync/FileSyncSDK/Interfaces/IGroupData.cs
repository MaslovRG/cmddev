using System;
using System.Collections.Generic;

namespace FileSyncSDK.Interfaces
{
    /// <summary>
    /// Интерфейс для доступных только для чтения данных об именованной группе файлов и папок.
    /// </summary>
    public interface IGroupData
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
