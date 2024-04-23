using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace Coding.Tracker.Controllers
{
    internal class UserInput
    {
        internal static void GetUserInput()
        {
            Console.Clear();
            bool closeApp = false;
            while (closeApp == false)
            {
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("What would you like to do?")
                        .PageSize(10)
                        .AddChoices(new[] {
                            "Press 0 to close the application", "Press 1 to view all records", "Press 2 to add a new record",
                            "Press 3 to delete a record", "Press 4 to update a record"
                        }));

                string command = choice;

                switch (command)
                {
                    case "Press 0 to close the application":
                        Console.WriteLine("\nGoodbye!\n");
                        closeApp = true;
                        Environment.Exit(0);
                        break;
                    case "Press 1 to view all records":
                        CodingController.GetAllRecords();
                        break;
                    case "Press 2 to add a new record":
                        CodingController.Insert();
                        break;
                    case "Press 3 to delete a record":
                        CodingController.Delete();
                        break;
                    case "Press 4 to update a record":
                        CodingController.Update();
                        break;
                    default:
                        Console.WriteLine("\nInvalid command. Please press a number from 0 to 4.");
                        break;
                }
            }
        }
    }
}
