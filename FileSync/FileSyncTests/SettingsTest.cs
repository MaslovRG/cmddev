using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileSyncSDK.Implementations;
using FileSyncSDK.Enums;
using FileSyncSDK.Exceptions; 
using System.IO;
using System.Xml; 

namespace FileSyncTests
{
    [TestClass]
    public class SettingsTest
    {     
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SettingsContruction()
        {
            Settings settings = new Settings(null, SettingsFileType.Local); 
        }

        [TestInitialize]
        public void Initialize()
        {
            if (!File.Exists("THE_END"))
                File.Create("THE_END");
        } 

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void SettingsLoad()
        {             
            Settings settings = new Settings("THE_END", (SettingsFileType)3);
            settings.Load();            
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void SettingsSave()
        {
            Settings settings = new Settings("THE_END", (SettingsFileType)3);
            settings.Save();
        }

        [TestMethod]
        [ExpectedException(typeof(SettingsDataCorruptedException))]
        public void SettingsLoadBadLocalFile()
        {
            Settings settings = new Settings("THE_END", SettingsFileType.Local);
            settings.Load(); 
        }

        [TestMethod]
        [ExpectedException(typeof(SettingsDataCorruptedException))]
        public void SettingsLoadBadGlobalFile()
        {
            Settings settings = new Settings("THE_END", SettingsFileType.Global);
            settings.Load(); 
        }
    }
}
