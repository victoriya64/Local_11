using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace moodtracker
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal static Repositories.RepositoryBase db;
        internal static string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
        internal static string selectedDate = DateTime.Now.ToString("yyyy-MM-dd  hh:mm");

        public App()
        {
            db = new Repositories.RepositoryBase();
        }
    }
}
