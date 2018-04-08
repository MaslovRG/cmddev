using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileSyncTests
{
    /// <summary>
    /// Класс-пример с методами-заглушками для проверки работоспособности
    /// </summary>
    /// <description>
    /// Название класса и всех его методов иметь в начале название разрабатываемой вами фичи
    /// а также содержать информацию о том, что вы тестируете. 
    /// Если тестируете что-то сложное, добавьте комментарий перед методом.
    /// </description>
    [TestClass]
    public class Example1UnitTest
    {
        /// <summary>
        /// Тестируем сложную и важную вещь, проверяем равенство 1=1
        /// </summary>
        [TestMethod]
        public void Example1ImportantThing()
        {
            Assert.AreEqual(1, 1); 
        }
    }
}
