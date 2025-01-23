using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sodoku.GlobalConstants;

namespace Sodoku
{
    internal static class InputHandler
    {
        public static int[] StringToArray(string input)
        {
            int[] result = new int[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                result[i] = input[i] - '0';
            }
            return result;
        }
        public static bool IsValidInput(string input)
        {
            if (input != null && input.Length == BoardLength*BoardLength && input.All(char.IsDigit))
            {
                return true; 
            } 
            return false;
        }

        private static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }
    }
}
