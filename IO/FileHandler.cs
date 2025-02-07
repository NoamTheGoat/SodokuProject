using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using static Sodoku.IO.InputUtils;

namespace Sodoku.IO
{
    public static class FileHandler
    {
        /// <summary>
        /// Reads the Sudoku board input from a file specified by the user. 
        /// </summary>
        /// <param name="sodokuBoardInput">The Sudoku board input read from the file.</param>
        /// <param name="filePath">The path to the file containing the Sudoku board input.</param>
        /// <returns>
        /// Returns true if the file was successfully read and the input was retrieved, 
        /// otherwise, false if the user chose to return to the main menu.
        /// </returns>
        public static bool ReadFromFile(out string sodokuBoardInput, out string filePath)
        {
            filePath = null;
            sodokuBoardInput = null;
            Console.Write("\nEnter the file path or r to return to main menu: ");

            filePath = Console.ReadLine();

            if (filePath.Equals("r"))
            {
                return false;
            }

            StreamReader sr = new StreamReader(filePath);
            sodokuBoardInput = sr.ReadLine(); //ReadLine
            sr.Close();   
            return true;
        }
    }
}
