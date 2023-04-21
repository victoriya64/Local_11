using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace moodtracker
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (DateTime.Now.TimeOfDay.Hours >= 12 && DateTime.Now.TimeOfDay.Hours < 18)
            {
                txtGreetings.Text = $"Добрый день, {Environment.UserName.ToString()}!";
            }
            else if (DateTime.Now.TimeOfDay.Hours > 18 && DateTime.Now.TimeOfDay.Hours < 22)
            {
                txtGreetings.Text = $"Добрый вечер, {Environment.UserName.ToString()}!";
            }
            else if (DateTime.Now.TimeOfDay.Hours >= 6 && DateTime.Now.TimeOfDay.Hours < 12)
            {
                txtGreetings.Text = $"Доброе утро, {Environment.UserName.ToString()}!";
            }
            else
            {
                txtGreetings.Text = $"Доброй ночи, {Environment.UserName.ToString()}!";
            }
        }


        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void pnlControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }
        private void pnlControlBar_MouseEnter(object sender, MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }


        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void NotesButton_Click(object sender, RoutedEventArgs e)
        {
            WindowsNavigation.GoToWindow(this, WindowsNavigation.newDayWindow);
        }

        private void StatisticButton_Click(object sender, RoutedEventArgs e)
        {
            WindowsNavigation.GoToWindow(this, WindowsNavigation.statisticWindow);
        }
    }
}
