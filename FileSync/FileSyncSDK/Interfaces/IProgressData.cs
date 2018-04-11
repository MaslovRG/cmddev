using FileSyncSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSyncSDK.Interfaces
{
    /// <summary>
    /// Интерфейс для данных о прогрессе выполняемой операции
    /// </summary>
    public interface IProgressData
    {
        /// <summary>
        /// Стадия операции
        /// </summary>
        SyncStage Stage { get; }

        /// <summary>
        /// Обрабатываемая группа файлов, может быть null
        /// </summary>
        IGroup Group { get; }

        /// <summary>
        /// Строка с дополнительной информацией, может быть null
        /// </summary>
        string Info { get; }
    }
}
