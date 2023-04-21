using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using moodtracker.Windows;

namespace moodtracker
{
    internal class WindowsNavigation
    {
        private static LinkedList<Window> _previous = new LinkedList<Window>();

        public static MainWindow mainWindow = new MainWindow();
        public static NewDayWindow newDayWindow = new NewDayWindow();
        public static StatisticWindow statisticWindow = new StatisticWindow();
        //public static DateChoiceWindow dateChoiceWindow = new DateChoiceWindow();

        public static void Startup(Window startWindow)
        {
            startWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            App.Current.MainWindow = startWindow;
            //if (startWindow == newDayWindow)
            //    _previous.AddLast(mainWindow);
        }

        public static void GoToWindow(Window from, Window to)
        {
            App.Current.MainWindow.Hide();
            _previous.AddLast(from);
            to.Left = from.Left;
            to.Top = from.Top;
            App.Current.MainWindow = to;
            App.Current.MainWindow.Show();
        }

        public static void BackToPreviousWindow()
        {
            App.Current.MainWindow.Hide();
            _previous.Last.Value.Left = App.Current.MainWindow.Left;
            _previous.Last.Value.Top = App.Current.MainWindow.Top;
            App.Current.MainWindow = _previous.Last.Value;
            _previous.RemoveLast();
            App.Current.MainWindow.Show();
        }

        public static void CloseWindows()
        {
            Environment.Exit(0);
        }
    }
}
