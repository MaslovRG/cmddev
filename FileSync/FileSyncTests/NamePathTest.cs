using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileSyncSDK.Implementations;

namespace FileSyncTests
{
    [TestClass]
    public class NamePathTest
    {

        /// <summary>
        /// Тестирование класса NamePath
        /// </summary>
        [TestMethod]
        public void Name1()
        {
            Assert.AreEqual("name", (new NamePath("name", "path")).Name);
        }

        [TestMethod]
        public void Name2()
        {
            Assert.AreNotEqual("ame", (new NamePath("name", "path")).Name);
        }

        [TestMethod]
        public void Path1()
        {
            Assert.AreEqual("path", (new NamePath("name", "path")).Path);
        }

        [TestMethod]
        public void Path2()
        {
            Assert.AreNotEqual("ath", (new NamePath("name", "path")).Path);
        }
    }
}
