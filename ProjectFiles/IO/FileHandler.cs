﻿using System;
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
        /// Reads the Sodoku board input from a file specified by the user. 
        /// </summary>
        /// <param name="sodokuBoardInput">The Sodoku board input read from the file.</param>
        /// <param name="filePath">The path to the file containing the Sodoku board input.</param>
        /// <returns>
        /// Returns true if the file was successfully read otherwise, false.
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

            ReadBoardFromFile(out sodokuBoardInput, filePath);
            return true;
        }
        public static void ReadBoardFromFile(out string sodokuBoardInput, string filePath)
        {
            StreamReader sr = new StreamReader(filePath);
            sodokuBoardInput = sr.ReadLine();
            sr.Close();
        }
    }
}
