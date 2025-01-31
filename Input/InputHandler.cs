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

namespace Sodoku
{
    internal class InputHandler
    {
        public static void RunProgram()
        {
            Console.WriteLine();
            ShowMenu();
            Console.Write("\nEnter: ");
            string inputOption = Console.ReadLine();

            //read from file
            if (inputOption.Equals("f"))
            {
                Console.Write("\nEnter the file path or r to return to main menu: ");
                string filePath = Console.ReadLine();
                if (filePath.Equals("r"))
                {
                    return;
                }
                StreamReader sr = new StreamReader(filePath);
                string input = sr.ReadToEnd();
                sr.Close();

                ContinueWithInput(input, filePath);
            }

            else if (inputOption.Equals("c"))
            {
                Console.Write("\nEnter sodoku board: ");
                string input = Console.ReadLine();
                ContinueWithInput(input);
            }

            else if (inputOption.Equals("x"))
            {
                Console.WriteLine("Thanks for solving sodoku with me! Have a nice day");
                Environment.Exit(0);
            }
            //Invalid input
            else
            {
                throw new NotVaildInputException();
            }
        }
        private static void ContinueWithInput(string input, string filePath = "")
        {
            Console.WriteLine();

            if (!InputUtils.IsValidInput(input))
            {
                throw new NotVaildInputException();
            }

            int[] a = InputUtils.InputParser(input);

            SodokuSolver solver = new SodokuSolver(a);
            solver.PrintSodokuBoard();
            if (!solver.IsValidSodokuBoard())
            {
                throw new NotVaildBoardException();
            }

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            bool solved = solver.SolveSodoku();
            stopwatch.Stop();

            if (solved)
            {
                Console.WriteLine("The solved board is:");
                solver.PrintSodokuBoard();
                Console.WriteLine(solver.counter);
                if (filePath != "")
                {
                    using (StreamWriter sw = new StreamWriter(filePath, append: true))
                    {
                        sw.WriteLine("\n"+solver.ReturnBoardAsString());
                    }
                }
            }
            else
            {
                Console.WriteLine(solver.counter);

                Console.WriteLine("The board is not solvable");
            }

            Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} milliseconds");
        }
    }
}
