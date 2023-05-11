using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Day = moodtracker.Model.Day;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Data.Common;

namespace moodtracker.Repositories
{
   public class RepositoryBase
    {
        public readonly string _connectionString = "Server=(local); Database=mood; Integrated Security=true";
        private SqlConnection _connection;

        public RepositoryBase()
        {
            string stringCommand = "";
            try
            {
                _connection = new SqlConnection(_connectionString);
                _connection.Open();
                var command = new SqlCommand();
                command.Connection = _connection;
                if (stringCommand != "")
                {
                    command.CommandText = stringCommand;
                    command.ExecuteNonQuery();
                }
            }
            finally
            {
                if (_connection != null)
                    _connection.Close();
            }
        }
        public void Write(Day day)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM [Notes] WHERE [date] = @date", connection);
                command.Parameters.AddWithValue("@date", day.Date);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Close();
                    Update(day);
                }
                else
                {
                    reader.Close();
                    command = new SqlCommand("INSERT INTO [Notes] ([date], [mood], [note]) VALUES (@date, @mood, @note)", connection);
                    command.Parameters.AddWithValue("@date", day.Date);
                    command.Parameters.AddWithValue("@mood", day.Mood);
                    command.Parameters.AddWithValue("@note", (object)day.Note ?? DBNull.Value);
                    command.ExecuteNonQuery();
                }
                reader.Close();
                connection.Close();
            }
        }

        public void Update(Day day)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand();
                command.Connection = connection;
                connection.Open();
                command.CommandText = "UPDATE [Notes] SET [mood] = @mood, [note] = @note WHERE [date] = CONVERT(smalldatetime, @date, 120)";
                command.Parameters.AddWithValue("@mood", day.Mood);
                command.Parameters.AddWithValue("@note", day.Note);
                command.Parameters.AddWithValue("@date", day.Date);
                command.ExecuteNonQuery();
                connection.Close();
            }            
        }
            
        public List<Day> ReadAll()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand();
                command.Connection = connection;
                connection.Open();
                command.CommandText = "SELECT * FROM [Notes] ORDER BY date DESC";
                List<Day> notes = new List<Day>();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var date = (DateTime)reader["date"];
                    var mood = (int)reader["mood"];
                    var note = reader["note"].ToString();
                    notes.Add(new Day(date, mood, note));
                }
                reader.Close();
                connection.Close();
                return notes;
            }
        }
        public int CountMood(int mood)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand();
                command.Connection = connection;
                connection.Open();
                command.CommandText = "SELECT COUNT(*) FROM [Notes] WHERE [mood] = @mood";
                command.Parameters.AddWithValue("@mood", mood);
                var result = (int)command.ExecuteScalar();
                return result;
            }
        }
    }
}
