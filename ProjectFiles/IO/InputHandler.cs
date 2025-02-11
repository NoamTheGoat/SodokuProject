using Sodoku.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using static Sodoku.IO.InputUtils;
using static Sodoku.IO.FileHandler;
using static Sodoku.IO.SolveBoardWithInput;
using System.Web;
using Sodoku.ProjectFiles.CustomExceptions;

namespace Sodoku.IO
{
    internal class InputHandler
    {
        /// <summary>
        /// Runs a single iteration of the input handling process, where the user can select an action.
        /// </summary>
        public static void RunSingleIteration()
        {
            Console.WriteLine();
            ShowMenu();
            Console.Write("\nEnter: ");
            string inputOption = Console.ReadLine();

            switch (inputOption)
            {

                case "clr":
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    CoolFrontScreenText();
                    Console.ResetColor();
                    break;

                case "f":
                    if (ReadFromFile(out string input, out string filePath))
                    {
                        SolveWithInput(input, filePath);
                    }
                    break;

                case "c":
                    Console.Write("\nEnter sodoku board: ");
                    string sodokuBoardInput = Console.ReadLine();
                    SolveWithInput(sodokuBoardInput);
                    break;

                case "x":
                    Console.WriteLine("Thanks for solving sodoku with me! Have a nice day");
                    Environment.Exit(0);
                    break;

                default:
                    PrintError("Error: The input isnt valid, try again\n");
                    break;

            }
        }

        /// <summary>
        /// Runs the main program loop, repeatedly invoking the single iteration method and handling exceptions.
        /// </summary>
        public static void RunProgram()
        {
            Console.SetIn(new StreamReader(Console.OpenStandardInput(8192)));
            Console.ForegroundColor = ConsoleColor.Cyan;
            CoolFrontScreenText();
            Console.CancelKeyPress += (sender, e) =>
            {
                Console.WriteLine("Bye bye. have a nice day");
                Environment.Exit(0);
            };
            Console.ResetColor();
            while (true)
            {
                try
                {
                    RunSingleIteration();
                }
                catch (EmptyInputException)
                {
                    PrintError("Error: Enter an input that isnt empty!\n");
                }
                catch (NotVaildBoardException)
                {
                    PrintError("Error: The board is invalid.\n");
                }
                catch (NotVaildInputException)
                {
                    PrintError("Error: The input is invalid, try again\n");
                }
                catch (InvalidBoardLengthException)
                {
                    PrintError("Error: The board length is invalid, try again\n");
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
                catch (SecurityException)
                {
                    PrintError("Error: You do not have permission to access this file.\n");
                }
                catch (IOException ex)
                {
                    PrintError($"Error: I/O error occurred. Details: {ex.Message}\n");
                }
            }
        }
    }
}
