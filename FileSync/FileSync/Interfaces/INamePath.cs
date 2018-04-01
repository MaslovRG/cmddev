using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSync.Interfaces
{
    /// <summary>
    /// Интерфейс для пары из имени объекта в архиве синхронизации и пути к нему (полного имени) в локальной файловой системе
    /// </summary>
    interface INamePath
    {
        /// <summary>
        /// Имя объекта в архиве синхронизации
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Путь (полное имя) к объекту в локальной файловой системе
        /// </summary>
        string Path { get; }
    }
}
