using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using z_5;
using System.Linq;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void GetPrimesInRange_ReturnsExpectedResult()
        {
            //Этот тест проверяет, является ли GetPrimesInRange метод возвращает ожидаемый набор простых чисел для заданного диапазона ввода и количества потоков.
            // Arrange
            int lowerBound = 1;
            int upperBound = 100;
            int threadCount = 4;

            List<int> expectedPrimes = new List<int> { 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97 };

            // Act
            List<int> actualPrimes = PrimeFinder.GetPrimesInRange(lowerBound, upperBound, threadCount);

            // Assert
            CollectionAssert.AreEqual(expectedPrimes, actualPrimes);
        }
        [TestMethod]
        public void TestGetPrimesInRange_InvalidBounds_ThrowsException()
        {
            //Этот тест проверяет, что ArgumentException выдается, когда верхняя граница меньше нижней, что является недопустимым вводом.
            // Arrange
            int lowerBound = 0;
            int upperBound = 5;
            int threadCount = 11;

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => PrimeFinder.GetPrimesInRange(lowerBound, upperBound, threadCount));
        }
    }

}
