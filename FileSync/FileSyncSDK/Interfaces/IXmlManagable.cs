using System.Xml.Linq;

namespace FileSyncSDK.Interfaces
{
    /// <summary>
    /// Интерфейс для методов импорта и экспорта данных в XML узел файла настроек
    /// </summary>
    public interface IXmlManagable
    {
        /// <summary>
        /// Заполнение данных из узла XML
        /// </summary>
        /// <param name="element">
        /// Исходный элемент XML
        /// </param>
        void ImportFromXml(XElement element);

        /// <summary>
        /// Возвращает данные в виде XML узла
        /// </summary>
        /// <param name="settingsType">
        /// Тип файла настроек, для которого предназначается возвращаемый узел
        /// </param>
        /// <returns>XML узел соответствующего типа</returns>
        XElement ExportToXml();
    }
}
