using System;
using System.Collections.Generic;
using System.Text;

namespace FileSyncSDK.Exceptions
{
    /// <summary>
    /// Исключение, выбрасываемое при вводе некорректного значения для рабочей папки облачного сервиса
    /// </summary>
    public class InvalidServiceFolderPathException : ArgumentException
    {
        private const string message = "Некорректное значение пути";

        public InvalidServiceFolderPathException() : base(message)
        {
        }

        public InvalidServiceFolderPathException(Exception inner) : base(message, inner)
        {
        }
    }
}
