using System;
using System.Data;
using System.Data.SqlClient;

namespace DbIsYourBff
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString;
            connectionString = "Server=localhost;Database=hotel;Trusted_Connection=True;";
            //                 "Host=localhost;Initial Catalog=hotel;Integrated_Security=SSPI"

            SqlConnection connection;
            connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = @"
                    insert into locationtype(name)
                    values
                    ('motor inn'),
                    ('hotel'),
                    ('lodge')
                ";
                int value = command.ExecuteNonQuery();
                Console.WriteLine($"{value} row(s) affected.");


                Console.WriteLine("What is the franchise?");
                string franchise = Console.ReadLine();

                Console.WriteLine("In what state?");
                string state = Console.ReadLine();

                command = connection.CreateCommand();
                command.CommandText = @"
                    insert into franchise(name, state)
                    values(@franchise, @state)
                ";
                command.Parameters.AddWithValue("@franchise", franchise);
                command.Parameters.AddWithValue("@state", state);
                command.ExecuteNonQuery();

                command = connection.CreateCommand();
                command.CommandText = "SELECT franchiseid, name, state FROM franchise";
                SqlDataReader reader = command.ExecuteReader();
                
                while (reader.Read())
                {
                    int rowId = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    state = reader.GetString(2);
                    Console.WriteLine($"{rowId},{name},{state}");
                }
            }
            finally
            {
                connection.Dispose();
            }
        }
    }
}
