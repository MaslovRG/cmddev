using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileSyncSDK.Implementations;
using FileSyncSDK.Enums;

namespace FileSyncTests
{
    [TestClass]
    public class ProgressDataTest
    {
        /// <summary>
        /// Тестирование класса NamePath
        /// </summary>
        [TestMethod]
        public void Stage1()
        {
            Assert.AreEqual(SyncStage.CleaningUp, (new ProgressData(SyncStage.CleaningUp, new Group("name", new string[] { "aaa" }, new string[] { "bbb" }), "aaa")).Stage);
        }

        [TestMethod]
        public void Stage2()
        {
            Assert.AreNotEqual(SyncStage.CopyingFiles, (new ProgressData(SyncStage.CleaningUp, new Group("name", new string[] { "aaa" }, new string[] { "bbb" }), "aaa")).Stage);
        }

        [TestMethod]
        public void Group1()
        {
            Group Gr = new Group("name", new string[] { "aaa" }, new string[] { "bbb" });
            Assert.AreEqual(Gr, (new ProgressData(SyncStage.CleaningUp, Gr, "aaa")).Group);
        }

        [TestMethod]
        public void Group2()
        {
            Group Gr1 = new Group("name", new string[] { "aaa" }, new string[] { "bbb" });
            Group Gr2 = new Group("name", new string[] { "aaa" }, new string[] { "bbb" });
            Assert.AreNotEqual(Gr2, (new ProgressData(SyncStage.CleaningUp, Gr1, "aaa")).Group);
        }

        [TestMethod]
        public void Info1()
        {
            Assert.AreEqual("aaa", (new ProgressData(SyncStage.CleaningUp, new Group("name", new string[] { "aaa" }, new string[] { "bbb" }), "aaa")).Info);
        }

        [TestMethod]
        public void Info2()
        {
            Assert.AreNotEqual("bbb", (new ProgressData(SyncStage.CleaningUp, new Group("name", new string[] { "aaa" }, new string[] { "bbb" }), "aaa")).Info);
        }
    }
}
