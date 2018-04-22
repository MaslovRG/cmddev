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
    internal interface IGroup : IGroupData, IXmlManagable
    {
        void UpdateLastSync(DateTime dateTime);
    }
}
