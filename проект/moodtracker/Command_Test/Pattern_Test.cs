using Microsoft.VisualStudio.TestTools.UnitTesting;
using moodtracker.Model;
using moodtracker.View;
using moodtracker.Repositories;
using moodtracker.ViewModel;
using Moq;
using LiveCharts.Defaults;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using System.Net.Mime;
using System;
using System.Linq;
using System.Collections.Generic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Day = moodtracker.Model.Day;
using System.Windows;
using Window = System.Windows.Window;
using System.Windows.Forms;
using moodtracker;

namespace Command_Test
{
    [TestClass]
    public class Pattern_Test
    {

        [TestMethod]
        public void TestHistoryCommand() //проверяет паттерн "Команда" и "Фасад"
        {
            // Arrange
            var viewModel = new MainViewModel();
            var historyViewModel = new HistoryViewModel();

            // Act
            viewModel.HistoryCommand.Execute(null);

            // Assert
            Assert.AreEqual(historyViewModel.GetType(), viewModel.CurrentChildView.GetType());
        }

        [TestMethod]
        public void TestNotesCommand() //проверяет паттерн "Команда" и "Фасад"
        {
            // Arrange
            var viewModel = new MainViewModel();
            var notesViewModel = new NotesViewModel();

            // Act
            viewModel.NotesCommand.Execute(null);

            // Assert
            Assert.AreEqual(notesViewModel.GetType(), viewModel.CurrentChildView.GetType());
        }

        [TestMethod]
        public void TestStatisticCommand() //проверяет паттерн "Команда" и "Фасад"
        {
            // Arrange
            var viewModel = new MainViewModel();
            var statisticViewModel = new StatisticViewModel();

            // Act
            viewModel.StatisticCommand.Execute(null);

            // Assert
            Assert.AreEqual(statisticViewModel.GetType(), viewModel.CurrentChildView.GetType());
        }

        [TestMethod]
        public void ExitCommand_Should_ExitApplication() //проверяет паттерн "Команда"
        {
            // Arrange
            var viewModel = new MockMainViewModel();

            // Act
            viewModel.ExitCommand.Execute(new object());

            // Assert
            Assert.IsTrue(MockEnvironment.HasShutdownStarted);
        }

        private class MockMainViewModel : MainViewModel
        {
            public override void Exit()
            {
                MockEnvironment.Exit(0);
            }
        }

        private class MockEnvironment
        {
            public static bool HasShutdownStarted { get; private set; }

            public static void Exit(int exitCode)
            {
                HasShutdownStarted = true;
            }
        }

        [TestMethod]
        public void MoodCommand_Executed_SelectedMoodIsSet()
        {
            // Arrange
            var viewModel = new NotesViewModel();
            var parameter = "3"; // Selected mood parameter
            var expectedMood = 3;

            // Act
            viewModel.MoodCommand.Execute(parameter);

            // Assert
            Assert.AreEqual(expectedMood, viewModel.selectedMood);
        }

        [TestMethod]
        public void AcceptCommand_CanExecute_SelectedMoodIsNotNegativeOne()
        {
            // Arrange
            var viewModel = new NotesViewModel();
            viewModel.selectedMood = 3;
            var parameter = new object();
            var expectedResult = true;

            // Act
            var result = viewModel.AcceptCommand.CanExecute(parameter);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void AcceptCommandExecute_SavesDayAndReturns() //проверяет паттерн "Команда" и "Билдер"
        {
            // Arrange
            var viewModel = new NotesViewModel();
            var mainViewModel = new MainViewModel();

            var expectedDate = new DateTime(2023, 5, 12);
            var expectedMood = 4;
            var expectedNote = "На улице опять похолодало";

            var mainWindow = new Window()
            {
                DataContext = mainViewModel
            };
            mainWindow = System.Windows.Application.Current?.MainWindow;
            if (mainWindow != null)
            {
                // Act
                viewModel.AcceptCommand.Execute(null);
                var builder = new DayBuilder();
                var result = builder
                    .SetSelectedDate(expectedDate)
                    .SetSelectedMood(expectedMood)
                    .SetNote(expectedNote)
                    .Build();

                // Assert
                Assert.AreEqual(expectedDate, result.Date);
                Assert.AreEqual(expectedMood, result.Mood);
                Assert.AreEqual(expectedNote, result.Note);
            }
        }
    }
}
