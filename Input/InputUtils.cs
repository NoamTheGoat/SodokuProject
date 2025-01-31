using Sodoku.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sodoku.GlobalConstants;

namespace Sodoku
{
    public static class InputUtils
    {
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

        public static bool IsValidInput(string input)
        {
            if (input == "")
            {
                throw new EmptyInputException();
            }

            double len = Math.Sqrt(Math.Sqrt(input.Length));
            if (input != null && len == Math.Round(len) && len<=5)
            {
                UpdateConstants((int)len);
                return true; 
            } 
            return false;
        }

        public static void ShowMenu()
        {
            Console.WriteLine(" ──────────────────────────── \n" +
                              "│Enter c to write in console │\n" +
                              "│Enter f to read from file   │\n" +
                              "│Enter x to exit             │\n" +
                              " ──────────────────────────── ");
        }
        public static void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
