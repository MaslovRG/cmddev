using System;
using System.Collections.Generic;
using System.Text;

namespace FileSyncSDK.Interfaces
{
    /// <summary>
    /// Интерфейс для сессии работы с облачным сервисом
    /// </summary>
    internal interface ISession : IDisposable
    {
        /// <summary>
        /// Получение/загрузка глобальных настроек
        /// </summary>
        ISettings GlobalSettings { get; set; }

        /// <summary>
        /// Синхронизировать группы
        /// </summary>
        /// <param name="group">Локальная группа.</param>
        void SyncronizeGroup(IGroup group);

        /// <summary>
        /// Удалить группу из облачного хранилища
        /// </summary>
        /// <param name="group">Удаляемая группа</param>
        void DeleteGroup(IGroup group);
    }
}
