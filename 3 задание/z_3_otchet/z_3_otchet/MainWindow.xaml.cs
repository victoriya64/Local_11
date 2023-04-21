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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace z_3_otchet
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
            if (dataTable == null)
            {
                LoadData();
            }
        }

        private DataTable dataTable;
        private void LoadData()
        {
            // подключение к базе данных
            string connectionString = "Data Source=(local);Database=ivlieva;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);

            // запрос для выборки данных из таблиц Order1 и Customer
            string query = "SELECT o.IdOrd, c.FName, c.LName, c.IdCity, c.Address, c.Zip, c.Phone, o.OrdDate " +
                           "FROM Order1 o " +
                           "JOIN Customer c ON o.IdCust = c.IdCust " +
                           "GROUP BY YEAR(o.OrdDate), MONTH(o.OrdDate), c.IdCust, c.FName, c.LName, c.IdCity, c.Address, c.Zip, c.Phone, o.IdOrd, o.OrdDate " +
                           "ORDER BY YEAR(o.OrdDate), MONTH(o.OrdDate), c.FName, c.LName";

            // создание объекта SqlDataAdapter и заполнение DataGrid данными из запроса SQL
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGrid.ItemsSource = dataTable.DefaultView;
        }
    }
}
