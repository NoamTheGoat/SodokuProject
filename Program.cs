using Sodoku.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using static Sodoku.InputUtils;
using static Sodoku.InputHandler;

namespace Sodoku
{
    internal class Program
    {
        public static void Main(String[] args)
        {
            Console.SetIn(new StreamReader(Console.OpenStandardInput(8192)));
            Console.WriteLine("Welcom to my sodoku solver!");
            while (true) 
            {
                try
                {
                    RunProgram();
                }
                catch (EmptyInputException)
                {
                    PrintError("Error: Enter an input that isnt empty!\n");
                }
                catch (NonSolvableBoardException)
                {
                    PrintError("Error: The board isnt solvable.\n");
                }
                catch (NotVaildBoardException)
                {
                    PrintError("Error: The board isnt valid.\n");
                }
                catch (NotVaildInputException)
                {
                    PrintError("Error: The input isnt valid, try again\nhint: try to check if the file only contains the sodoku board\n");
                }
                catch (ArgumentException)
                {
                    PrintError("Error: The file path is invalid.\n");
                }
                catch (FileNotFoundException)
                {
                    PrintError("Error: The file does not exist.\n");
                }
                catch (DirectoryNotFoundException)
                {
                    PrintError("Error: The specified directory was not found.\n");
                }
                catch (PathTooLongException)
                {
                    PrintError("Error: The file path is too long.\n");
                }
                catch (UnauthorizedAccessException)
                {
                    PrintError("Error: Access to the file is denied.\n");
                }
                catch (IOException ex)
                {
                    PrintError($"Error: I/O error occurred. Details: {ex.Message}\n");
                }
                catch (NotSupportedException)
                {
                    PrintError("Error: The file path format is not supported.\n");
                }
                catch (SecurityException)
                {
                    PrintError("Error: You do not have permission to access this file.\n");
                }
            }
            
            
        }   
    }
}
