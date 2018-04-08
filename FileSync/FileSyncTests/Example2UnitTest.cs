using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileSyncTests
{
    /// <summary>
    /// Второй пустой класс для проверки работоспособности 
    /// </summary>
    [TestClass]
    public class Example2UnitTest
    {
        [TestMethod]
        public void Example2OneIsOne()
        {
            Assert.AreEqual(1, 1); 
        }
    }
}
