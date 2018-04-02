using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSyncSDK.Exceptions
{
    /// <summary>
    /// Исключение, выбрасываемое тогда, когда авторизация на облачном сервисе не увенчалась успехом.
    /// </summary>
    public class SignInFailedException : Exception
    {
        public SignInFailedException() : base()
        {
        }

        public SignInFailedException(string message) : base(message)
        {
        }

        public SignInFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
