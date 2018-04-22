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
    /// Интерфейс для изменения именованной группы файлов и папок.
    /// </summary>
    internal interface IGroup : IGroupData, IXmlManagable
    {
        /// <summary>
        /// Обновляет значение LastSync группы.
        /// </summary>
        /// <param name="dateTime"></param>
        void UpdateLastSync(DateTime dateTime);
    }
}
