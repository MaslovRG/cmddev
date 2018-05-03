using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileSyncSDK.Interfaces;
using FileSyncSDK.Implementations; 

namespace FileSyncTests
{
    [TestClass]    
    public class GroupTest
    {
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


    }
}
