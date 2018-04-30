using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSyncSDK.Exceptions
{
    /// <summary>
    /// Исключение, выбрасываемое тогда, когда для выполнения операции требуется путь к локальному файлу настроек, который не был указан.
    /// </summary>
    public class NoLocalSettingsPathException : ArgumentNullException
    {
        private const string message = "Путь для файла настроек не выбран";

        public NoLocalSettingsPathException() : base(message)
        {
        }

        public NoLocalSettingsPathException(Exception inner) : base(message, inner)
        {
        }
    }
}
