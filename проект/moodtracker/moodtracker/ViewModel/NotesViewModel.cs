using System;
using System.Collections.Generic;
using System.Linq;
using Day = moodtracker.Model.Day;
using moodDataGrid = moodtracker.View.StatisticView;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using moodtracker.ViewModel;
using LiveCharts.Wpf.Charts.Base;

namespace moodtracker.ViewModel
{
    public class NotesViewModel : ViewModelBase
    {
        public int selectedMood = -1;

        public static Repositories.RepositoryBase dao;

        private DayBuilder _dayBuilder = new DayBuilder();
        public DayBuilder DayBuilder
        {
            get { return _dayBuilder; }
            set { _dayBuilder = value; OnPropertyChanged(); }
        }

        private string _currentDate = DateTime.Now.ToString("dd.MM.yyyy");
        public string CurrentDate
        {
            get { return _currentDate; }
            set { _currentDate = value; OnPropertyChanged(); }
        }

        private DateTime _selectedDate = DateTime.Parse(DateTime.Now.ToString("dd.MM.yyyy  HH:mm"));
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set { _selectedDate = value; OnPropertyChanged(); }
        }

        private string _note;

        public string Note
        {
            get { return _note; }
            set { _note = value; OnPropertyChanged("Note"); }
        }

        public ICommand MoodCommand { get; }
        public ICommand AcceptCommand { get; }

        public NotesViewModel()
        {
            dao = new Repositories.RepositoryBase();
            MoodCommand = new ViewModelCommand(OnMoodCommandExecute);
            AcceptCommand = new ViewModelCommand(OnAcceptCommandExecute, CanAcceptCommandExecute);
        }


        private void OnMoodCommandExecute(object parameter)
        {
            selectedMood = int.Parse((string)parameter);
        }

        private bool CanAcceptCommandExecute(object parameter)
        {
            return selectedMood != -1;
        }
        public void Return()
        {
            var mainWindow = Application.Current.MainWindow;
            if (mainWindow != null && mainWindow.DataContext is MainViewModel mainViewModel)
            {
                HistoryViewModel _historyViewModel = new HistoryViewModel();
                mainViewModel.CurrentChildView = _historyViewModel;
                mainViewModel.Check = true;
            }
        }
        private void OnAcceptCommandExecute(object parameter)
        {
            var day = _dayBuilder
            .SetSelectedDate(SelectedDate)
            .SetSelectedMood(selectedMood)
            .SetNote(Note)
            .Build();

            dao.Write(day);

            Return();
        }
    }
}
