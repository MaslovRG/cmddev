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
        ISettings GlobalSettings { get; }

        /// <summary>
        /// Синхронизировать группы
        /// </summary>
        /// <param name="localGroups">Локальные группы.</param>
        void SyncronizeGroups(IEnumerable<IGroup> localGroups);

        /// <summary>
        /// Удалить группу из облачного хранилища
        /// </summary>
        /// <param name="group">Удаляемая группа</param>
        void DeleteGroup(IGroup group);

        /// <summary>
        /// Обновить метаданные с облачного хранилища
        /// </summary>
        void ReloadMetadata();
    }
}
