using Sodoku.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sodoku.IO.InputUtils;

namespace Sodoku.IO
{
    public static class SolveBoardWithInput
    {
        /// <summary>
        /// Solves a Sodoku puzzle from the provided input string. 
        /// Optionally writes the solution to a file.
        /// </summary>
        /// <param name="input">A string representing the Sodoku puzzle in a valid format.</param>
        /// <param name="filePath">An optional path to a file where the solved board will be saved. 
        /// If not provided, the solution will not be saved to a file.</param>
        /// <exception cref="NotVaildInputException">Thrown when the input is invalid.</exception>
        /// <exception cref="NotVaildBoardException">Thrown when the Sodoku board is invalid.</exception>
        public static void SolveWithInput(string input, string filePath = "")
        {
            Console.WriteLine();
            if (!InputUtils.IsValidInput(input))
            {
                throw new NotVaildInputException();
            }
            int[] inputAsArray = InputUtils.InputParser(input);


            SodokuSolver solver = new SodokuSolver(inputAsArray);
            Console.ForegroundColor = ConsoleColor.Red;
            solver.PrintSodokuBoard();
            Console.ResetColor();

            if (!solver.IsValidSodokuBoard())
            {
                throw new NotVaildBoardException();
            }

            bool boardSolved = solver.SolveSodoku();

            if (boardSolved)
            {
                
                Console.WriteLine("The solved board is:\n");
                Console.ForegroundColor = ConsoleColor.Green;
                solver.PrintSodokuBoard();
                Console.ResetColor();
                
                Console.WriteLine("The solved board as string:\n");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(solver.ReturnBoardAsString() + "\n");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Board solved in: {solver.sodokuSolverTimer} milliseconds\n");
                Console.ResetColor();

                if (filePath != "")
                {

                    using (StreamWriter sw = new StreamWriter(filePath, append: true))
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        ShowLoadingEffect("Copying the solution to the original file");
                        Console.ResetColor();
                        sw.WriteLine("\n\n" + solver.ReturnBoardAsString());
                    }
                }
            }

            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"The board is not solvable, elapsed time: " +
                    $"{solver.sodokuSolverTimer} milliseconds");
                Console.ResetColor();
            }
        }
    }
}
