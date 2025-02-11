using Sodoku.CustomExceptions;
using Sodoku.ProjectFiles.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Sodoku.GlobalConstants;

namespace Sodoku.IO
{
    public static class InputUtils
    {
        /// <summary>
        /// Parses the input string into an integer array representing a Sodoku board.
        /// </summary>
        /// <param name="input">A string representing the Sodoku board input.</param>
        /// <returns>An integer array representing the parsed Sodoku board.</returns>
        /// <exception cref="NotVaildBoardException">
        /// Thrown when an invalid value is encountered in the input.</exception>
        public static int[] InputParser(string input)
        {
            int[] result = new int[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                result[i] = input[i] - '0';
                if (result[i] < -1 || result[i] > BoardLength)
                {
                    throw new NotVaildBoardException();
                }
            }
            return result;
        }

        /// <summary>
        /// Validates the input string to check if it represents a valid Sodoku board.
        /// </summary>
        /// <param name="input">A string representing the Sodoku board input.</param>
        /// <returns>True if the input is valid, otherwise false.</returns>
        /// <exception cref="EmptyInputException">Thrown when the input is empty.</exception>
        public static bool IsValidInput(string input)
        {

            if (input == "")
            {
                throw new EmptyInputException();
            }

            double len = Math.Sqrt(Math.Sqrt(input.Length));
            if (input.Length > Math.Pow(MaxBoxSize, 4))
            {
                PrintError("max board length: 625");
            }
            if (len != Math.Round(len))
            {
                throw new InvalidBoardLengthException();
            }
            if (input != null)
            {
                UpdateConstants((int)len);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Displays the main menu options to the user in the console.
        /// </summary>
        public static void ShowMenu()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(" ─────────────────────────────── \n" +
                              "│Enter c to write in console    │\n" +
                              "│Enter f to read from file      │\n" +
                              "│Enter x to exit                │\n" +
                              "│Enter clr to clear the console │\n" +
                              " ─────────────────────────────── ");
            Console.ResetColor();

        }

        /// <summary>
        /// Prints an error message to the console in red color.
        /// </summary>
        /// <param name="message">The error message to display.</param>
        public static void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void ShowLoadingEffect(string message)
        {
            Console.Write(message);
            for (int i = 0; i < 3; i++)
            {
                System.Threading.Thread.Sleep(750);
                Console.Write(".");
            }
            Console.WriteLine();
        }

        public static void CoolFrontScreenText()
        {
            Console.WriteLine(  "    _   __                     _          _____           __      __            _____       __               \r\n" +
                                "   / | / /___  ____ _____ ___ ( )_____   / ___/____  ____/ /___  / /____  __   / ___/____  / /   _____  _____\r\n" +
                                "  /  |/ / __ \\/ __ `/ __ `__ \\|// ___/   \\__ \\/ __ \\/ __  / __ \\/ //_/ / / /   \\__ \\/ __ \\/ / | / / _ \\/ ___/\r\n" +
                                " / /|  / /_/ / /_/ / / / / / / (__  )   ___/ / /_/ / /_/ / /_/ / ,< / /_/ /   ___/ / /_/ / /| |/ /  __/ /    \r\n" +
                                "/_/ |_/\\____/\\__,_/_/ /_/ /_/ /____/   /____/\\____/\\__,_/\\____/_/|_|\\__,_/   /____/\\____/_/ |___/\\___/_/");

        }

    }
}
