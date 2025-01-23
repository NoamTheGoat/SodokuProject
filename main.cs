using Sodoku.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sodoku
{
    internal class main
    {
        public static void Main(String[] args)
        {
            
            string input = Console.ReadLine();
            if (!InputHandler.IsValidInput(input)) 
            {
                throw new NotVaildInputException();
            }

            int[] a = InputHandler.StringToArray(input);

            SodokuSolver solver = new SodokuSolver(a);
            solver.PrintSodokuBoard();
            if (!solver.IsValidSodokuBoard())
            {
                throw new NotVaildBoardException();
            }
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //solver.PrintSodokuBoard();
            if (solver.SolveSodoku())
            {
                solver.PrintSodokuBoard();
            }
            else 
            {
                Console.WriteLine("not solved");
            }
            stopwatch.Stop();
            Console.WriteLine($"Elapsed time: {stopwatch.Elapsed.TotalSeconds} seconds");
        }
    }
}
