using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sodoku.GlobalConstants;

namespace Sodoku
{
    public static class InputHandler
    {
        public static int[] StringToArray(string input)
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
            double len = Math.Sqrt(Math.Sqrt(input.Length));
            if (input != null && len == Math.Round(len))
            {
                UpdateConstants((int)len);
                return true; 
            } 
            return false;
        }
    }
}
