using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using moodtracker.Repositories;
using Day = moodtracker.Repositories.Day;
using moodDataGrid = moodtracker.Windows.StatisticWindow;
using System.Collections.ObjectModel;
using static moodtracker.Repositories.MoodLists;
using System.Windows.Markup;

namespace moodtracker.Windows
{
    /// <summary>
    /// Логика взаимодействия для NewDayWindow.xaml
    /// </summary>
    public partial class NewDayWindow : Window
    {
        private int selectedMood;
        public NewDayWindow()
        {
            InitializeComponent();
            Activated += NewDayWindow_Activated;
        }

        private void NewDayWindow_Activated(object sender, EventArgs e)
        {
            DateText.Text = App.selectedDate;
        }

        #region ChoiceMood

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            WindowsNavigation.BackToPreviousWindow();
        }

        private void Mood_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.RadioButton;
            selectedMood = int.Parse((string)button.Tag);
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            App.db.Write(new Day(App.selectedDate, selectedMood, NoteText.Text, Environment.UserName.ToString()));
            WindowsNavigation.BackToPreviousWindow();
        }

        #endregion
    }
}
