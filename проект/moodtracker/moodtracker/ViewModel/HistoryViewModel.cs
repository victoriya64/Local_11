using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Runtime.CompilerServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Forms;
using moodtracker.Model;
using Day = moodtracker.Model.Day;
using moodtracker.Repositories;

namespace moodtracker.ViewModel
{

    public class HistoryViewModel : ViewModelBase
    {

        // Введенный текст
        public string InputText
        {
            get => _inputText;
            set
            {
                if (_inputText != value)
                {
                    _inputText = value;
                    OnPropertyChanged(nameof(InputText));
                }
            }
        }
        private string _inputText;

        // Флаг, указывающий, что текстовое поле доступно только для чтения
        public bool IsInputTextReadOnly
        {
            get => _isInputTextReadOnly;
            set
            {
                if (_isInputTextReadOnly != value)
                {
                    _isInputTextReadOnly = value;
                    OnPropertyChanged(nameof(IsInputTextReadOnly));
                }
            }
        }
        private bool _isInputTextReadOnly;

        private ObservableCollection<Day> _notes;

        public static Repositories.RepositoryBase dao;
        public ObservableCollection<Day> Notes
        {
            get { return _notes; }
            set
            {
                if (_notes != value)
                {
                    _notes = value;
                    OnPropertyChanged(nameof(Notes));
                }
            }
        }

        public HistoryViewModel()
        {
            dao = new Repositories.RepositoryBase();
            _notes = FillNotes(dao);
        }

        public ObservableCollection<Day> FillNotes(RepositoryBase dao)
        {
            if (dao == null)
            {
                dao = new Repositories.RepositoryBase();
            }
            List<Day> notes = dao.ReadAll();
            // Сортировка по убыванию даты
            notes.Sort((d1, d2) => -d1.Date.CompareTo(d2.Date));
            return new ObservableCollection<Day>(notes);
        }

    }

}
