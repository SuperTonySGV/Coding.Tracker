using Coding.Tracker.Controllers;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding.Tracker
{
    internal class Helpers
    {
        internal static string GetTimeInput(string message)
        {
            AnsiConsole.Write(new Markup($"[bold yellow]Please insert your {message} time: (Format: yyyy-MM-dd HH:mm:ss). Type 0 to return to the main menu.[/]\n\n"));

            string dateInput = Console.ReadLine();

            if (dateInput == "0") UserInput.GetUserInput();

            while (!DateTime.TryParseExact(dateInput, "yyyy-MM-dd HH:mm:ss", new CultureInfo("en-US"), DateTimeStyles.None, out _))
            {
                AnsiConsole.Write(new Markup($"[red]Invalid {message} time. Format is yyyy-MM-dd HH:mm:ss. Type 0 to return to the main menu or try again.[/] \n\n"));
                dateInput = Console.ReadLine();
            }

            return dateInput;
        }

        internal static int GetNumberInput(string message)
        {
            Console.WriteLine(message);

            string numberInput = Console.ReadLine();

            if (numberInput == "0") UserInput.GetUserInput();

            while (!Int32.TryParse(numberInput, out _) || Convert.ToInt32(numberInput) < 0)
            {
                Console.WriteLine("\n\nInvalid number. Try again. \n\n");
                numberInput = Console.ReadLine();
            }

            int finalInput = Convert.ToInt32(numberInput);

            return finalInput;
        }

        internal static int CalculateDuration(string startTime, string endTime)
        {
            DateTime st;
            DateTime et;
            if (DateTime.TryParseExact(startTime, "yyyy-MM-dd HH:mm:ss", new CultureInfo("en-US"), DateTimeStyles.None, out st) && DateTime.TryParseExact(endTime, "yyyy-MM-dd HH:mm:ss", new CultureInfo("en-US"), DateTimeStyles.None, out et))
            {
                var difference = et - st;
                int minutesDifference = (int)difference.TotalMinutes;
                return minutesDifference;
            }
            else
            {
                Console.WriteLine("\n\nCould not caluclate duration. \n\n");
                return -1;
            }
        }
    }
}
