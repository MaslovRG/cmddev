using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace FileSyncSDK.Exceptions
{
    /// <summary>
    /// Исключение, выбрасываемое, когда данные в файле настроек не соответсвуют требованиям программы
    /// </summary>
    public class SettingsDataCorruptedException : Exception
    {
        private const string message = "Данные в файле настроек повреждены";

        public SettingsDataCorruptedException() : base(message)
        {
        }

        public SettingsDataCorruptedException(Exception inner) : base(message, inner)
        {
        }
    }
}
