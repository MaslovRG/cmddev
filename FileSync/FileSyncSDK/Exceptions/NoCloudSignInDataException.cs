using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSyncSDK.Exceptions
{
    /// <summary>
    /// Исключение, выбрасываемое тогда, когда для выполнения операции необходимы данные для авторизации на облачном сервисе, но они не были получены.
    /// </summary>
    public class NoCloudSignInDataException : ArgumentNullException
    {
        public NoCloudSignInDataException() : base()
        {
        }
    }
}
