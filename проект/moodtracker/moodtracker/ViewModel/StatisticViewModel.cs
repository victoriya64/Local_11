using LiveCharts.Defaults;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows.Input;
using System.ComponentModel;
using System;
using System.Runtime.CompilerServices;
using LiveCharts.Wpf.Charts.Base;
using moodtracker.Repositories;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Windows.Documents;
using OxyPlot.Series;
using System.Globalization;
using System.Windows.Forms;
using OxyPlot.Axes;
using System.Runtime.InteropServices.ComTypes;
using moodtracker.Model;
using Day = moodtracker.Model.Day;

namespace moodtracker.ViewModel
{
    public class StatisticViewModel : ViewModelBase
    {
        public static Repositories.RepositoryBase dao;

        private int _radCount;
        public int RadCount
        {
            get { return _radCount; }
            set { _radCount = value; OnPropertyChanged(); }
        }

        private int _goodCount;
        public int GoodCount
        {
            get { return _goodCount; }
            set { _goodCount = value; OnPropertyChanged(); }
        }

        private int _neutralCount;
        public int NeutralCount
        {
            get { return _neutralCount; }
            set { _neutralCount = value; OnPropertyChanged(); }
        }

        private int _badCount;
        public int BadCount
        {
            get { return _badCount; }
            set { _badCount = value; OnPropertyChanged(); }
        }

        private int _awfulCount;
        public int AwfulCount
        {
            get { return _awfulCount; }
            set { _awfulCount = value; OnPropertyChanged(); }
        }

        private IChartValues _radCountP;
        public IChartValues RadCountP
        {
            get { return _radCountP; }
            set { _radCountP = value; OnPropertyChanged(); }
        }

        private IChartValues _goodCountP;
        public IChartValues GoodCountP
        {
            get { return _goodCountP; }
            set { _goodCountP = value; OnPropertyChanged(); }
        }

        private IChartValues _neutralCountP;
        public IChartValues NeutralCountP
        {
            get { return _neutralCountP; }
            set { _neutralCountP = value; OnPropertyChanged(); }
        }

        private IChartValues _badCountP;
        public IChartValues BadCountP
        {
            get { return _badCountP; }
            set { _badCountP = value; OnPropertyChanged(); }
        }

        private IChartValues _awfulCountP;
        public IChartValues AwfulCountP
        {
            get { return _awfulCountP; }
            set { _awfulCountP = value; OnPropertyChanged(); }
        }
        public Chart Chart { get; set; }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] LabelsX { get; set; }
        public string[] LabelsY { get; set; }
        public Func<double, string> DateLabelFormatter { get; set; }


        public StatisticViewModel()
        {
            CreateGraph();
            CreatePie();
        }


        public void CreateGraph()
        {
            dao = new Repositories.RepositoryBase();
            LabelsY = new string[] { "Rad", "Good", "Neutral", "Bad", "Awful" };
            List<Day> moodDataPoints = dao.ReadAll();
            // Заполнение коллекции точек графика
            var seriesCollection = new SeriesCollection();
            var allMoods = moodDataPoints.Select(p => p.Mood).Distinct().ToList();
            foreach (var moodID in allMoods)
            {
                var color = GetColorByMood(moodID);
                var series = new LiveCharts.Wpf.StepLineSeries
                {
                    Values = new ChartValues<ObservablePoint>(), // Initialize the Values property
                    Stroke = color, // Устанавливаем цвет контура
                };
                foreach (var dataPoint in moodDataPoints.Where(p => p.Mood == moodID))
                {
                    var date = dataPoint.Date;
                    series.Values.Add(new ObservablePoint(DateTimeAxis.ToDouble(date.Ticks), (int)dataPoint.Mood));
                }
                seriesCollection.Add(series);
            }
            var xAxis = new LiveCharts.Wpf.Axis { Title = "Date", Labels = new List<string>() };
            var allDates = moodDataPoints.Select(p => p.Date).Distinct().ToList(); // Использование свойства Date для удаления времени из даты
            allDates.Sort();
            foreach (var date in allDates)
            {
                xAxis.Labels.Add(date.ToString());
            }
            SeriesCollection = seriesCollection;
            DateLabelFormatter = value => new DateTime((long)value).ToString("dd.MM.yyyy");
        }


        private Brush GetColorByMood(int mood)
        {
            switch (mood)
            {
                case 0:
                    return new SolidColorBrush(Color.FromRgb(223, 193, 158));
                case 1:
                    return new SolidColorBrush(Color.FromRgb(147, 187, 141));
                case 2:
                    return new SolidColorBrush(Color.FromRgb(180, 148, 192));
                case 3:
                    return new SolidColorBrush(Color.FromRgb(143, 159, 191));
                case 4:
                    return new SolidColorBrush(Color.FromRgb(140, 140, 140));
                default:
                    return Brushes.Transparent;
            }
        }

        private void CreatePie()
        {
            dao = new Repositories.RepositoryBase();
            _radCount = 0;
            _goodCount = 0;
            _neutralCount = 0;
            _badCount = 0;
            _awfulCount = 0;
            _radCount = dao.CountMood(0);
            _goodCount = dao.CountMood(1);
            _neutralCount = dao.CountMood(2);
            _badCount = dao.CountMood(3);
            _awfulCount = dao.CountMood(4);
            _radCountP = new ChartValues<ObservableValue> { new ObservableValue(_radCount) };
            _goodCountP = new ChartValues<ObservableValue> { new ObservableValue(_goodCount) };
            _neutralCountP = new ChartValues<ObservableValue> { new ObservableValue(_neutralCount) };
            _badCountP = new ChartValues<ObservableValue> { new ObservableValue(_badCount) };
            _awfulCountP = new ChartValues<ObservableValue> { new ObservableValue(_awfulCount) };
        }
    }
}
