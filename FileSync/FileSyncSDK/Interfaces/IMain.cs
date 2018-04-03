﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSyncSDK.Interfaces
{
    public interface IMain
    {
        /// <summary>
        /// Инициализация синхронизатора.
        /// В случае, если какие-то параметры не были заданы или для них требуются уточнения - выбрасываются соответствующие исключения.
        /// </summary>
        /// <exception cref="Exceptions.NoLocalSettingsPathException">Не задан путь к локальному файлу настроек.</exception>
        /// <exception cref="Exceptions.NoCloudSignInDataException">Нет данных для авторизации на облачном сервисе.</exception>
        /// <exception cref="Exceptions.SignInFailedException">Авторизация на облачном сервисе не удалась.</exception>
        void GetData();

        /// <summary>
        /// Синхронизация групп файлов, отмеченных для синхронизации. Обновление файлов настроек.
        /// </summary>
        /// <exception cref="Exceptions.NoLocalSettingsPathException">Не задан путь к локальному файлу настроек.</exception>
        /// <exception cref="Exceptions.NoCloudSignInDataException">Нет данных для авторизации на облачном сервисе.</exception>
        /// <exception cref="Exceptions.SignInFailedException">Авторизация на облачном сервисе не удалась.</exception>
        void Syncronize();

        /// <summary>
        /// Путь к локальному файлу настроек.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        string LocalSettingsPath { get; set; }

        /// <summary>
        /// Имя пользователя на облачном сервисе.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        string CloudLogin { get; set; }

        /// <summary>
        /// Пароль пользователя на облачном сервисе.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        string CloudPassword { get; set; }

        /// <summary>
        /// Отображение прогресса операции.
        /// </summary>
        IProgress<IProgressData> ProgressView { get; set; }

        /// <summary>
        /// Список групп файлов из глобального файла настроек.
        /// </summary>
        IReadOnlyList<IGroup> GlobalGroups { get; }
        
        /// <summary>
        /// Список групп файлов из локального файла настроек.
        /// </summary>
        IReadOnlyList<IGroup> LocalGroups { get; }

        /// <summary>
        /// Создание новой группы.
        /// </summary>
        /// <param name="name">Имя новой группы. Должно быть уникально для обоих списков.</param>
        /// <param name="files">Локальные пути к файлам группы.</param>
        /// <param name="folders">Локальные пути к папкам группы.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        void NewGroup(string name, string[] files, string[] folders);

        /// <summary>
        /// Удаление группы.
        /// </summary>
        /// <param name="name">Имя группы. Должно соответствовать имени существующей группы из любого из списков.</param>
        /// <param name="local">Флаг того, что группа должна быть удалена из локального файла настроек.</param>
        /// <param name="global">Флаг того, что группа должна быть удалена из глобального файла настроек.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        void DeleteGroup(string name, bool local, bool global);
    }
}