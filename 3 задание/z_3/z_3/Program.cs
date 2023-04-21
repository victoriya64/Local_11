using System.Data.SqlClient;
using System;

namespace z_3
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("1. Чтение из БД:");
            string connectionString = "Server=(local); Database=ivlieva; Integrated Security=true";
            string query = "SELECT * FROM Order1";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                Console.WriteLine("Таблица Order1:\n");
                while (reader.Read())
                {
                    int idOrd = (int)reader["IdOrd"];
                    int idCust = (int)reader["IdCust"];
                    DateTime ordDate = (DateTime)reader["OrdDate"];
                    Console.WriteLine("IdOrd: {0} IdCust: {1} OrdDate: {2}", idOrd, idCust, ordDate);
                }
                reader.Close();
            }
            query = "SELECT * FROM Customer";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                Console.WriteLine("\nТаблица Customer:\n");
                while (reader.Read())
                {
                    int id = (int)reader["IdCust"];
                    string FName = (string)reader["FName"];
                    string LName = (string)reader["LName"];
                    int idCity = (int)reader["IdCity"];
                    string address = (string)reader["Address"];
                    string zip = (string)reader["Zip"];
                    string phone = (string)reader["Phone"];
                    Console.WriteLine("IdCust: {0} FName: {1} LName: {2} IdCity: {3} Address: {4} Zip: {5} Phone: {6}", id, FName, LName, idCity, address, zip, phone);
                }
                reader.Close();
            }
            Console.WriteLine("\n2. Обработка и изменение данных из БД:");
            Console.WriteLine("Изменим клиенту №5 (Марина Морозова) имя на Вероника Морозова:");
            string updateQuery = "UPDATE Customer SET FName = @FName WHERE IdCust = @IdCust";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(updateQuery, connection);
                command.Parameters.AddWithValue("@FName", "Вероника");
                command.Parameters.AddWithValue("@IdCust", 5);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine("Rows affected: {0}", rowsAffected);
            }
            Console.WriteLine("\n3. Обработка и изменение данных из БД:");
            Console.WriteLine("Добавим в таблицу Order1 новый заказ клиенту №5 (Вероника Морозова):");
            string insertQuery = "INSERT INTO Order1 (IdCust) VALUES (@IdCust)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@IdCust", 5);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine("Rows affected: {0}", rowsAffected);
            }
            Console.WriteLine("\nИтог:");
            query = "SELECT * FROM Order1";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                Console.WriteLine("Таблица Order1:\n");
                while (reader.Read())
                {
                    int idOrd = (int)reader["IdOrd"];
                    int idCust = (int)reader["IdCust"];
                    DateTime ordDate = (DateTime)reader["OrdDate"];
                    Console.WriteLine("IdOrd: {0} IdCust: {1} OrdDate: {2}", idOrd, idCust, ordDate);
                }
                reader.Close();
            }
            query = "SELECT * FROM Customer";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                Console.WriteLine("\nТаблица Customer:\n");
                while (reader.Read())
                {
                    int id = (int)reader["IdCust"];
                    string FName = (string)reader["FName"];
                    string LName = (string)reader["LName"];
                    int idCity = (int)reader["IdCity"];
                    string address = (string)reader["Address"];
                    string zip = (string)reader["Zip"];
                    string phone = (string)reader["Phone"];
                    Console.WriteLine("IdCust: {0} FName: {1} LName: {2} IdCity: {3} Address: {4} Zip: {5} Phone: {6}", id, FName, LName, idCity, address, zip, phone);
                }
                reader.Close();
            }
        }
    }
} 