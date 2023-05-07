using moodtracker.ViewModel;
using moodtracker.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace moodtracker.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly MainViewModelFacade _mainViewModelFacade = new MainViewModelFacade();

        public readonly HistoryViewModel _historyViewModel;
        public ICommand HistoryCommand { get; }
        public ICommand NotesCommand { get; }
        public ICommand StatisticCommand { get; }

        public ICommand ExitCommand { get; }

        private ViewModelBase _currentChildView = new HistoryViewModel();
        public ViewModelBase CurrentChildView
        {
            get
            {
                return _currentChildView;
            }
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }

        private string _greetings;
        public string Greetings
        {
            get
            {
                return _greetings;
            }
            set
            {
                _greetings = value;
                OnPropertyChanged(nameof(Greetings));
            }
        }

        public bool _check;
        public bool Check
        {
            get
            {
                return _check;
            }
            set
            {
                _check = value;
                OnPropertyChanged(nameof(Check));
            }
        }

        public MainViewModel()
        {
            _historyViewModel = new HistoryViewModel();
            _check = true;
            SystemGreeting();
            HistoryCommand = new ViewModelCommand(OnHistoryCommandExecute);
            NotesCommand = new ViewModelCommand(OnNotesCommandExecute);
            StatisticCommand = new ViewModelCommand(OnStatisticCommandExecute);
            ExitCommand = new ViewModelCommand(OnExitCommandExecute);
        }

        public void SystemGreeting()
        {
            if (DateTime.Now.TimeOfDay.Hours >= 12 && DateTime.Now.TimeOfDay.Hours < 18)
            {
                Greetings = $"Добрый день, {Environment.UserName.ToString()}!";
            }
            else if (DateTime.Now.TimeOfDay.Hours > 18 && DateTime.Now.TimeOfDay.Hours < 22)
            {
                Greetings = $"Добрый вечер, {Environment.UserName.ToString()}!";
            }
            else if (DateTime.Now.TimeOfDay.Hours >= 6 && DateTime.Now.TimeOfDay.Hours < 12)
            {
                Greetings = $"Доброе утро, {Environment.UserName.ToString()}!";
            }
            else
            {
                Greetings = $"Доброй ночи, {Environment.UserName.ToString()}!";
            }
        }

        public virtual void Exit()
        {
            Environment.Exit(0);
        }

        private void OnHistoryCommandExecute(object parameter)
        {
            CurrentChildView = new HistoryViewModel();
        }

        private void OnNotesCommandExecute(object parameter)
        {
            CurrentChildView = new NotesViewModel();
        }

        private void OnStatisticCommandExecute(object parameter)
        {
            CurrentChildView = new StatisticViewModel();
        }

        private void OnExitCommandExecute(object parameter)
        {
             Exit();
        }
    }
}
