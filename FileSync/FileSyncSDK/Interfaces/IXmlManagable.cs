using FileSyncSDK.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

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
        /// <param name="node">
        /// Исходный узел XML
        /// </param>
        /// <param name="settingsType">
        /// Тип файла настроек, которому принадлежит узел <paramref name="node"/>
        /// </param>
        void ImportFromXml(XmlNode node, SettingsFileType settingsType);

        /// <summary>
        /// Возвращает данные в виде XML узла
        /// </summary>
        /// <param name="settingsType">
        /// Тип файла настроек, для которого предназначается возвращаемый узел
        /// </param>
        /// <returns>XML узел соответствующего типа</returns>
        XmlNode ExportToXml(SettingsFileType settingsType);
    }
}
