using OxyPlot.Axes;
using OxyPlot.Wpf;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;
using System.Windows.Shapes;

namespace moodtracker.Windows
{
    /// <summary>
    /// Логика взаимодействия для StatisticWindow.xaml
    /// </summary>
    public partial class StatisticWindow : Window
    {
        public ObservableCollection<MoodCount> MoodCounts { get; set; }
        private enum Mood { Rad = 0, Good = 1, Neutral = 2, Bad = 3, Awful = 4 };
        public SeriesCollection seriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public StatisticWindow()
        {
            InitializeComponent();

            // Добавляем данные на график
            LoadData();
            AddDataToChart();
            
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            WindowsNavigation.BackToPreviousWindow();
        }

        private void LoadData()
        {
            int radCount = 0;
            int goodCount = 0;
            int neutralCount = 0;
            int badCount = 0;
            int awfulCount = 0;
            string _connectionString = "Server=(local); Database=mood; Integrated Security=true";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM [notes]";
                var reader = command.ExecuteReader();

                // Count records for each mood value
                while (reader.Read())
                {
                    int? mood = reader.GetInt32(reader.GetOrdinal("mood"));
                    if (mood==0) radCount++;
                    if (mood == 1) goodCount++;
                    if (mood == 2) neutralCount++;
                    if (mood == 3) badCount++;
                    if (mood == 4) awfulCount++;
                }
                reader.Close();
                connection.Close();
                Rad.Text = radCount.ToString();
                Good.Text = goodCount.ToString();
                Neutral.Text = neutralCount.ToString();
                Bad.Text = badCount.ToString();
                Awful.Text = awfulCount.ToString();
                IChartValues radCountP = new ChartValues<ObservableValue> { new ObservableValue(radCount) };
                IChartValues goodCountP = new ChartValues<ObservableValue> { new ObservableValue(goodCount) };
                IChartValues neutralCountP = new ChartValues<ObservableValue> { new ObservableValue(neutralCount) };
                IChartValues badCountP = new ChartValues<ObservableValue> { new ObservableValue(badCount) };
                IChartValues awfulCountP = new ChartValues<ObservableValue> { new ObservableValue(awfulCount) };
                RadP.Values = radCountP;
                GoodP.Values = goodCountP;
                NeutralP.Values = neutralCountP;
                BadP.Values = badCountP;
                AwfulP.Values = awfulCountP;
                //seriesCollection = new SeriesCollection
                //{
                //    new PieSeries
                //    {
                //        Title="Rad",
                //        Values=new ChartValues<ObservableValue> {new ObservableValue(radCount) },
                //        DataLabels = true,
                //    },
                //    new PieSeries
                //    {
                //        Title="Good",
                //        Values=new ChartValues<ObservableValue> {new ObservableValue(goodCount) },
                //        DataLabels = true
                //    },
                //    new PieSeries
                //    {
                //        Title="Neutral",
                //        Values=new ChartValues<ObservableValue> {new ObservableValue(neutralCount) },
                //        DataLabels = true
                //    },
                //    new PieSeries
                //    {
                //        Title="Bad",
                //        Values=new ChartValues<ObservableValue> {new ObservableValue(badCount) },
                //        DataLabels = true
                //    },
                //    new PieSeries
                //    {
                //        Title="Awful",
                //        Values=new ChartValues<ObservableValue> {new ObservableValue(awfulCount) },
                //        DataLabels = true
                //    },
                //};
                //DataContext = this;
            }
        }


            private void AddDataToChart()
        {
            int[] moodValues = { 0, 1, 2, 3, 4 };

            Dictionary<int, int> moodCounts = new Dictionary<int, int>();

            foreach (int moodD in moodValues)
            {
                moodCounts.Add(moodD, 0);
            }

            string _connectionString = "Server=(local); Database=mood; Integrated Security=true";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM [notes]";
                var reader = command.ExecuteReader();

                // Count records for each mood value
                while (reader.Read())
                {
                    int? mood = reader.GetInt32(reader.GetOrdinal("mood"));
                    DateTime date = reader.GetDateTime(reader.GetOrdinal("date"));
                    if (mood.HasValue && moodCounts.ContainsKey(mood.Value))
                    {
                        moodCounts[mood.Value]++;
                    }
                }
                reader.Close();
                connection.Close();
                //// Create collection of MoodCount from moodCounts dictionary
                ObservableCollection<MoodCount> moodCountCollection = new ObservableCollection<MoodCount>();
                foreach (var mood in moodValues)
                {
                    moodCountCollection.Add(new MoodCount { Mood = GetMoodImage(mood), Count = moodCounts[mood] });
                }
                for (int i = 0; i < moodCountCollection.Count; i++) { /*moodDataGrid.ItemSource = moodCountCollection;*/ }
            }
        }

        internal static ImageSource GetMoodImage(int mood)
        {
            switch (mood)
            {
                case (int)Mood.Rad:
                    return new BitmapImage(new Uri("/Image/rad.png", UriKind.Relative));
                case (int)Mood.Good:
                    return new BitmapImage(new Uri("/Image/good.png", UriKind.Relative));
                case (int)Mood.Neutral:
                    return new BitmapImage(new Uri("/Image/neutral.png", UriKind.Relative));
                case (int)Mood.Bad:
                    return new BitmapImage(new Uri("/Image/bad.png", UriKind.Relative));
                case (int)Mood.Awful:
                    return new BitmapImage(new Uri("/Image/awful.png", UriKind.Relative));
                default:
                    return null; // Если передано некорректное настроение, то возвращаем null
            }
        }

        private void Close_Button(object sender, RoutedEventArgs e)
        {
            WindowsNavigation.BackToPreviousWindow();
        }
    }
}

public class MoodData
{
    public DateTime Date { get; set; }
    public int Mood { get; set; }
    public ImageSource MoodImage { get; set; }
}
public class MoodCount
        {
            public ImageSource Mood { get; set; }
            public int Count { get; set; }
        }