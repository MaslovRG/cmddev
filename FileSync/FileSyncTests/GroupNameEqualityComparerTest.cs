using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileSyncSDK.Interfaces;
using FileSyncSDK.Implementations;

namespace FileSyncTests
{
    class GroupNameEqualityComparerTest
    {
        /// <summary>
        /// Тестирование класса GroupNameEqualityComparer
        /// </summary>
        [TestMethod]
        public void Equals1()
        {
            GroupNameEqualityComparer G = new GroupNameEqualityComparer();
            Assert.AreEqual(true, G.Equals(new Group("name", new string[] { "aaa" }, new string[] { "bbb" }), new Group("name", new string[] { "aaa" }, new string[] { "bbb" })));
        }

        [TestMethod]
        public void Equals2()
        {
            GroupNameEqualityComparer G = new GroupNameEqualityComparer();
            Assert.AreEqual(false, G.Equals(new Group("ame", new string[] { "aaa" }, new string[] { "bbb" }), new Group("name", new string[] { "aaa" }, new string[] { "bbb" })));
        }

        [TestMethod]
        public void GetHashCode()
        {
            GroupNameEqualityComparer G = new GroupNameEqualityComparer();
            Group Gr = new Group("ame", new string[] { "aaa" }, new string[] { "bbb" });
            Assert.AreEqual(Gr.Name.GetHashCode(), G.GetHashCode(Gr));
        }
    }
}
