using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSyncSDK.Enums
{
    /// <summary>
    /// Тип файла настроек
    /// </summary>
    public enum SettingsFileType
    {
        /// <summary>
        /// Файл локальных настроек
        /// </summary>
        Local,

        /// <summary>
        /// Файл глобальных настроек (в облаке)
        /// </summary>
        Global
    }
}
