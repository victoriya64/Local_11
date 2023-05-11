using Microsoft.VisualStudio.TestTools.UnitTesting;
using moodtracker.Model;
using moodtracker.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Command_Test
{
    [TestClass]
    public class DB_Test
    {
        [TestClass]
        public class DBTest
        {
            [TestClass]
            public class RepositoryBaseTests
            {
                private readonly string _connectionString = "Server=(local); Database=mood; Integrated Security=true";

                [AssemblyInitialize]
                public static void AssemblyInitialize(TestContext context)
                {
                    var repository = new RepositoryBase();
                    // Очистка таблицы перед каждым запуском модульных тестов
                    using (SqlConnection connection = new SqlConnection(repository._connectionString))
                    {
                        connection.Open();
                        var command = new SqlCommand("DELETE FROM [Notes]", connection);
                        command.ExecuteNonQuery();
                    }
                }


                [TestMethod]
                public void TestWriteAndReadAll()
                {
                    // Arrange
                    RepositoryBase repository = new RepositoryBase();
                    DateTime testDate = new DateTime(2023, 05, 06, 16, 48, 00);
                    Day testDay = new Day(testDate, 0, "Читала книгу");

                    // Act
                    repository.Write(testDay);
                    List<Day> result = repository.ReadAll();

                    // Assert
                    Assert.AreEqual(5, result.Count);
                    Assert.AreEqual(testDate, result[4].Date);
                    Assert.AreEqual(0, result[4].Mood);
                    Assert.AreEqual("Читала книгу", result[4].Note);
                }

                [TestMethod]
                public void TestUpdate()
                {
                    // Arrange
                    RepositoryBase repository = new RepositoryBase();
                    DateTime testDate = new DateTime(2023, 05, 07, 19, 50, 00);
                    Day testDay = new Day(testDate, 4, "Test note");
                    repository.Write(testDay);

                    // Act
                    testDay.Mood = 1;
                    testDay.Note = "Гуляла с друзьями";
                    repository.Update(testDay);
                    List<Day> result = repository.ReadAll();

                    // Assert
                    Assert.AreEqual(4, result.Count);
                    Assert.AreEqual(testDate, result[3].Date);
                    Assert.AreEqual(1, result[3].Mood);
                    Assert.AreEqual("Гуляла с друзьями", result[3].Note);
                }

                [TestMethod]
                public void TestCountMood()
                {
                    // Arrange
                    RepositoryBase repository = new RepositoryBase();
                    DateTime testDate1 = new DateTime(2023, 05, 08, 14, 05, 00);
                    Day testDay1 = new Day(testDate1, 2, "Ходила в магазин");
                    DateTime testDate2 = new DateTime(2023, 05, 09, 22, 55, 00);
                    Day testDay2 = new Day(testDate2, 3, "Опять ошибки в коде");
                    DateTime testDate3 = new DateTime(2023, 05, 10, 10, 48, 00);
                    Day testDay3 = new Day(testDate3, 3, "Делала домашнее задание");
                    repository.Write(testDay1);
                    repository.Write(testDay2);
                    repository.Write(testDay3);

                    // Act
                    int result1 = repository.CountMood(3);
                    int result2 = repository.CountMood(2);

                    // Assert
                    Assert.AreEqual(2, result1);
                    Assert.AreEqual(1, result2);
                }
            }
        }
    }
}
