using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSyncSDK.Enums
{
    /// <summary>
    /// Тип выполняемой операции
    /// </summary>
    /// <remarks>
    /// 
    /// Процесс отправки группы:
    /// ProcessSetup -> CopyingFiles -> CreatingArchive -> UploadingArchive -> CleaningUp
    /// 
    /// Процесс приема группы:
    /// ProcessSetup -> DownloadingArchive -> ExtractingFiles -> CopyingFiles -> CleaningUp
    /// 
    /// </remarks>
    public enum SyncStage
    {
        /// <summary>
        /// Ничего не происходит
        /// </summary>
        Null,

        /// <summary>
        /// Группа задач выполнена
        /// </summary>
        Done,

        /// <summary>
        /// Подготовка к началу синхронизациии
        /// </summary>
        ProcessSetup,

        /// <summary>
        /// Очистка - удаление временных файлов
        /// </summary>
        CleaningUp,

        /// <summary>
        /// Копирование файлов группы в рабочую папку
        /// </summary>
        CopyingFiles,

        /// <summary>
        /// Создание архива
        /// </summary>
        CreatingArchive,

        /// <summary>
        /// Обновление данных облачного хранилиза
        /// </summary>
        UpdatingCloudData,

        /// <summary>
        /// Загрузка архива из облачного хранилища
        /// </summary>
        DownloadingArchive,

        /// <summary>
        /// Извлечнение файлов из архива
        /// </summary>
        ExtractingFiles,
    }
}
