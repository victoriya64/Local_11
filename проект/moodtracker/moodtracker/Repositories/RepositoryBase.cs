using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace moodtracker.Repositories
{
    internal class RepositoryBase
    {
        public readonly string _connectionString = "Server=(local); Database=mood; Integrated Security=true";
        public List<Day> data = new List<Day>();
        public Day currentDay = null;

        public enum ReadType { All, Month, Day, Next, Previous }

        public RepositoryBase()
        {
            string stringCommand = "";

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            var command = new SqlCommand();
            command.Connection = connection;
            if (stringCommand != "")
            {
                command.CommandText = stringCommand;
                command.ExecuteNonQuery();
            }
            connection.Close();
            Debug.WriteLine("INIT DB");
            Read(App.selectedDate, ReadType.Month);
        }

        public void Write(Day day)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            var command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM [notes] WHERE [date] = CONVERT(smalldatetime, '" + day.date + "', 120)";
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                var username = reader["username"];
                reader.Close();
                command.CommandText = "UPDATE [notes] SET [mood] = " + day.mood + ", [note] = '" + day.note + "' WHERE username = " + username;
            }
            else
            {
                reader.Close();
                command.CommandText = "INSERT INTO [notes] ([username], [date], [mood], [note]) VALUES ('" + day.username + "', CONVERT(smalldatetime, '" + day.date + "', 120), " + day.mood + ", '" + day.note + "')";
            }
            command.ExecuteNonQuery();
            connection.Close();
        }

        //public void GetMood()
        //{
        //    SqlConnection connection = new SqlConnection(_connectionString);
        //    connection.Open();
        //    var command = new SqlCommand();
        //    command.Connection = connection;
        //    command.CommandText = "SELECT * FROM [notes] WHERE [date] = CONVERT(smalldatetime, '" + day.date + "', 120)";
        //    SqlDataReader reader = command.ExecuteReader();
        //    if (reader.HasRows)
        //    {
        //        reader.Read();
        //        var username = reader["username"];
        //        reader.Close();
        //        command.CommandText = "UPDATE [notes] SET [mood] = " + day.mood + ", [note] = '" + day.note + "' WHERE username = " + username;
        //    }
        //    else
        //    {
        //        reader.Close();
        //        command.CommandText = "INSERT INTO [notes] ([username], [date], [mood], [note]) VALUES ('" + day.username + "', CONVERT(smalldatetime, '" + day.date + "', 120), " + day.mood + ", '" + day.note + "')";
        //    }
        //    command.ExecuteNonQuery();
        //    connection.Close();
        //}

        public bool Read(string selectedDate = null, ReadType readType = ReadType.All, bool check = false)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            var command = new SqlCommand();
            command.Connection = connection;

            switch (readType)
            {
                case ReadType.All:
                    ReadAll(command);
                    break;
                case ReadType.Month:
                    ReadMonth(command, selectedDate);
                    break;
                case ReadType.Day:
                    break;
                case ReadType.Next:
                case ReadType.Previous:
                    App.selectedDate = ReadNext(command, selectedDate, readType);
                    break;
            }
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (check)
                {
                    App.selectedDate = selectedDate;
                    return reader.HasRows;
                }
                else if (reader.HasRows)
                {
                    data.Clear();
                    while (reader.Read())
                    {
                        var username = (string)reader["username"];
                        var date = (string)reader["date"];
                        var mood = (int)(long)reader["mood"];
                        var note = (string)reader["note"];
                        var stringData = string.Format("{0} {1} {2} {3}", username, date, mood, note);
                        var day = new Day(date, mood, note, username);
                        if (App.selectedDate == (string)reader["date"])
                            currentDay = day;
                        data.Add(day);
                    }
                }
            }
            connection.Close();
            return data.Count != 0;
        }

        private void ReadAll(SqlCommand command)
        {
            command.CommandText = "SELECT * FROM [notes]";
        }

        private void ReadMonth(SqlCommand command, string selectedDate)
        {
            var ym = selectedDate.Split('-')[0] + "-" + selectedDate.Split('-')[1];
            command.CommandText = "SELECT * FROM [notes] WHERE ([date] BETWEEN '" + ym + "-01" + "' AND '" + ym + "-31" + "')";
        }

        private string ReadNext(SqlCommand command, string selectedDate, ReadType readType)
        {
            var ym = selectedDate.Split('-')[0] + "-" + selectedDate.Split('-')[1];

            if (readType == ReadType.Next)
                command.CommandText = "SELECT * FROM [notes] WHERE ([date] > '" + ym + "-31'" + ")";
            else
                command.CommandText = "SELECT * FROM [notes] WHERE ([date] < '" + ym + "-01'" + ")";

            bool findNextDate = false;
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    selectedDate = (string)reader["date"];
                    ym = selectedDate.Split('-')[0] + "-" + selectedDate.Split('-')[1];
                    findNextDate = true;
                }
            }

            if (findNextDate)
                command.CommandText = "SELECT * FROM [Tracker] WHERE ([date] BETWEEN '" + ym + "-01" + "' AND '" + ym + "-31" + "')";
            return selectedDate;
        }
    }
}
