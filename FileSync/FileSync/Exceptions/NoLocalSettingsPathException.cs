using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSync.Exceptions
{
    /// <summary>
    /// Исключение, выбрасываемое тогда, когда для выполнения операции требуется путь к локальному файлу настроек, который не был указан.
    /// </summary>
    class NoLocalSettingsPathException : ArgumentNullException
    {
        public NoLocalSettingsPathException() : base()
        {
        }
    }
}
