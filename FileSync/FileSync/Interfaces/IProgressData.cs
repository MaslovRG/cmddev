using FileSync.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSync.Interfaces
{
    /// <summary>
    /// Интерфейс для данных о прогрессе выполняемой операции
    /// </summary>
    interface IProgressData
    {
        /// <summary>
        /// Стадия операции
        /// </summary>
        SyncStage Stage { get; }

        /// <summary>
        /// Обрабатываемая группа файлов
        /// </summary>
        IGroup Group { get; }
    }
}
