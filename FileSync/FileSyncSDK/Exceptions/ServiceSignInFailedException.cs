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
    public class ServiceSignInFailedException : Exception
    {
        private const string message = "Введенные данные пользователя облачного сервиса не верны";

        public ServiceSignInFailedException() : base(message)
        {
        }

        public ServiceSignInFailedException(Exception innerException) : base(message, innerException)
        {
        }
    }
}
