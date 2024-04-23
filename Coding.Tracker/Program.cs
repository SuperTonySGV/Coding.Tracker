using System.Configuration;
using System.Collections.Specialized;
using System.Globalization;
using Microsoft.Data.Sqlite;
using static Coding.Tracker.Models.CodingSessionModel;
using Coding.Tracker.Controllers;

namespace CodingTracker
{
    class Program
    {
        static string connectionString = @"Data Source=coding-tracker.db";

        static void Main(string[] args)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText =
                    @"CREATE TABLE IF NOT EXISTS coding_tracker (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        StartTime TEXT,
                        EndTime TEXT,
                        Duration INTEGER
                        )";

                //tableCmd.CommandText +=
                //    @"INSERT INTO 'habit' ('name', 'date', 'uom', 'quantity') VALUES
                //      ('Water', '01-01-20','Ounces', '60'),
                //      ('Meditating', '01-01-21','Minutes', '30'),
                //      ('Healthy meals', '01-01-22','Count', '3'),
                //      ('Reading', '01-01-23','Pages', '22')";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }

            UserInput.GetUserInput();
        }
    }
}