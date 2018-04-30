using FileSyncSDK.Enums;
using FileSyncSDK.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace FileSyncSDK.Implementations
{
    internal class CloudService : ICloudService
    {
        private const string defaultServiceName = "Mega.nz";
        private const string defaultServiceFolderPath = "Root\\FileSync";

        public CloudService()
        {
            ServiceName = defaultServiceName;
            ServiceFolderPath = defaultServiceFolderPath;
        }

        public CloudService(XElement element)
        {
            ImportFromXml(element);
        }

        public string ServiceName { get; set; }
        public string UserLogin { get; set; }
        public string UserPassword { get; set; }
        public string ServiceFolderPath
        {
            get
            {
                return serviceFolderPath;
            }

            set
            {
                if (value == null)
                    throw new ArgumentNullException();

                if (!IsFolderValueCorrect(value))
                    throw new ArgumentException("Invalid path.");

                serviceFolderPath = value;
            }
        }

        private string serviceFolderPath;

        public XElement ExportToXml()
        {
            var service = new XElement("service", new XAttribute("name", ServiceName));
            service.Add(new XElement("login", new XText(UserLogin)));
            service.Add(new XElement("folder", new XText(ServiceFolderPath))); 
            return service;
        }

        public void ImportFromXml(XElement element)
        {
            if (element == null)
                throw new ArgumentNullException();
            if (element.Name != "service")
                throw new ArgumentException("XElement is not \"service\" elements.");
            if (element.Attribute("name") == null)
                throw new ArgumentException("XElement has no \"name\" attribute.");
            if (element.Element("login") == null)
                throw new ArgumentException("XElement has no \"login\" child element.");
            if (element.Element("folder") == null)
                throw new ArgumentException("XElement has no \"folder\" child element.");

            ServiceFolderPath = element.Element("folder").Value;
            ServiceName = element.Attribute("name").Value;
            UserLogin = element.Element("login").Value;
        }

        private bool IsFolderValueCorrect(string folderPath)
        {
            string[] pathArray = folderPath.Split('\\');
            char[] forbidden = Path.GetInvalidPathChars();
            return pathArray[0] == "Root" &&
                !pathArray.Any(p => string.IsNullOrEmpty(p)) &&
                !folderPath.ToCharArray().Distinct().Any(c => forbidden.Contains(c));
        }

        public ISession OpenSession(IProgress<IProgressData> progress = null)
        {
            return new Session(UserLogin, UserPassword, ServiceFolderPath, progress);
        }

        public void Clear()
        {
            ServiceName = defaultServiceName;
            ServiceFolderPath = defaultServiceFolderPath;
            UserLogin = null;
            UserPassword = null;
        }
    }
}
