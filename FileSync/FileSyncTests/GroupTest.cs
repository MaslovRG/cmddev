using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileSyncSDK.Interfaces;
using FileSyncSDK.Implementations;

using System.Collections.Generic;

namespace FileSyncTests
{
    [TestClass]    
    public class GroupTest
    {
        /* тесты для файла Group */

            // тесты для инициализации Group
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GroupContruction1()
        {
            Group group = new Group(null, null, null);              
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GroupContruction2()
        {
            Group group = new Group("name", null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GroupContruction3()
        {
            Group group = new Group("name", new string[] { "aaa", "aaa" }, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GroupContruction4()
        {
            Group group = new Group("name", null, new string[] { "aaa", "aaa" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GroupContruction5()
        {
            Group group = new Group("name", new string[] { "aaa" }, new string[] { "aaa"});
        }

        [TestInitialize]
        public void Initialize()
        {
            new Group("name", new string[] { "aaa" }, new string[] { "bbb" });
        }

        // тест HaveIntersection

        [TestMethod]
        public void HaveIntersection1()
        {
            Group group = new Group("name", new string[] { "aaa" }, new string[] { "bbb" });
            Assert.AreEqual(false, group.HaveIntersection(null, null));
        }

        [TestMethod]
        public void HaveIntersection2()
        {
            Group group = new Group("name", new string[] { "aaa" }, new string[] { "bbb" });
            Assert.AreEqual(false, group.HaveIntersection(new List<string> { "aaa", "bbb" }, null));
        }

        [TestMethod]
        public void HaveIntersection3()
        {
            Group group = new Group("name", new string[] { "aaa" }, new string[] { "bbb" });
            Assert.AreEqual(false, group.HaveIntersection(null, new List<string> { "aaa", "bbb" }));
        }

        [TestMethod]
        public void HaveIntersection4()
        {
            Group group = new Group("name", new string[] { "aaa" }, new string[] { "bbb" });
            Assert.AreEqual(true, group.HaveIntersection(new List<string> { "aaa", "bbb" }, new List<string> { "aaa", "bbb" }));
        }

        [TestMethod]
        public void HaveIntersection5()
        {
            Group group = new Group("name", new string[] { "aaa" }, new string[] { "bbb" });
            Assert.AreEqual(false, group.HaveIntersection(new List<string> { "aaa", "bbb" }, new List<string> { "ccc" }));
        }


        // правильность ввода данных

        [TestMethod]
        public void Name()
        {
            Group group = new Group("name", new string[] { "aaa" }, new string[] { "bbb" });
            Assert.AreEqual("name", group.Name);
        }

        [TestMethod]
        public void LastSync()
        {
            Group group = new Group("name", new string[] { "aaa" }, new string[] { "bbb" });
            Assert.AreEqual(null, group.LastSync);
        }
    }
}

// IEnumerable<string> files = new List<string> { "aaa", "bbb" };
