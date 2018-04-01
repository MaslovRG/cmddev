using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSync.Enums
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
    enum SyncStage
    {
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
        /// Отправка архива на облачное хранилище
        /// </summary>
        UploadingArchive,

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
