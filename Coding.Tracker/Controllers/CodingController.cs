using Dapper;
using Microsoft.Data.Sqlite;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using static Coding.Tracker.Models.CodingSessionModel;

namespace Coding.Tracker.Controllers
{
    internal class CodingController
    {
        static string connectionString = @"Data Source=coding-tracker.db";
        internal static void GetAllRecords()
        {
            //var choice = GetNumberInput("\n\nType 1 for all records or 2 for a more advanced search. Type 0 to return to the main menu. \n\n");

            var sql = "SELECT * FROM coding_tracker";
            var tableData = new List<CodingSession>();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                tableData = connection.Query<CodingSession>(sql).ToList();

                Console.WriteLine("------------------------------------------------------------------------------");
                foreach (var dw in tableData)
                {
                    AnsiConsole.Markup($"[cyan3]{dw.Id} - {dw.StartTime.ToString("yyyy-MM-dd HH:mm:ss")} - {dw.EndTime.ToString("yyyy-MM-dd HH:mm:ss")} - Duration: {dw.Duration}[/]\n");
                }
                Console.WriteLine("------------------------------------------------------------------------------\n");

                connection.Close();
            }

            //using (var connection = new SqliteConnection(connectionString))
            //{
            //    connection.Open();
            //    var tableCmd = connection.CreateCommand();

            //    tableCmd.CommandText =
            //        $"SELECT * FROM coding_tracker";

            //    List<CodingSession> tableData = new();

            //    SqliteDataReader reader = tableCmd.ExecuteReader();

            //    if (reader.HasRows)
            //    {
            //        while (reader.Read())
            //        {
            //            tableData.Add(new CodingSession()
            //            {
            //                Id = reader.GetInt32(0),
            //                StartTime = DateTime.ParseExact(reader.GetString(1), "yyyy-MM-dd HH:mm:ss", new CultureInfo("en-US")),
            //                EndTime = DateTime.ParseExact(reader.GetString(2), "yyyy-MM-dd HH:mm:ss", new CultureInfo("en-US")),
            //                Duration = reader.GetInt32(3)
            //            });
            //        }
            //    }
            //    else
            //    {
            //        Console.WriteLine("No rows found.");
            //    }

            //    connection.Close();

            //    Console.WriteLine("------------------------------------------------------------------------------");
            //    foreach (var dw in tableData)
            //    {
            //        Console.WriteLine($"{dw.Id} - {dw.StartTime.ToString("yyyy-MM-dd HH:mm:ss")} - {dw.EndTime.ToString("yyyy-MM-dd HH:mm:ss")} - Duration: {dw.Duration}");
            //    }
            //    Console.WriteLine("------------------------------------------------------------------------------\n");
            //}

        }
        internal static void Insert()
        {
            string startTime = Helpers.GetTimeInput("start");
            string endTime = Helpers.GetTimeInput("end");
            int duration = Helpers.CalculateDuration(startTime, endTime);

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $"INSERT INTO coding_tracker(startTime, endTime, duration) VALUES('{startTime}', '{endTime}',{duration})";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }
        }
        internal static void Update()
        {
            GetAllRecords();

            var recordId = Helpers.GetNumberInput("\n\nPlease type Id of the record you would like to update. Type 0 to return to the main menu. \n\n");

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var checkCmd = connection.CreateCommand();
                checkCmd.CommandText = $"SELECT EXISTS(SELECT 1 FROM coding_tracker WHERE Id = {recordId})";
                int checkQuery = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (checkQuery == 0)
                {
                    Console.WriteLine($"\n\nRecord with Id {recordId} doesn't exist. \n\n");
                    connection.Close();
                    Update();
                }

                string startTime = Helpers.GetTimeInput("start");
                string endTime = Helpers.GetTimeInput("end");
                int duration = Helpers.CalculateDuration(startTime, endTime);

                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = $"UPDATE coding_tracker SET StartTime = '{startTime}', EndTime = '{endTime}', duration = {duration} WHERE Id = {recordId}";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }
        }
        internal static void Delete()
        {
            Console.Clear();
            GetAllRecords();

            var recordId = Helpers.GetNumberInput("\n\nPlease type the Id of the record you want to delete or type 0 to go back to the Main Menu \n\n");

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $"DELETE from coding_tracker WHERE Id = '{recordId}'";

                int rowCount = tableCmd.ExecuteNonQuery();

                if (rowCount == 0)
                {
                    Console.WriteLine($"\n\nRecord with an Id {recordId} doesn't exist. \n\n");
                    Delete();
                }
            }

            Console.WriteLine($"\n\nRecord with Id {recordId} was deleted. \n\n");

            UserInput.GetUserInput();
        }
    }
}
