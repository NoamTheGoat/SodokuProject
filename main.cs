using Sodoku.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sodoku
{
    internal class main
    {
        public static void Main(String[] args)
        {

            Console.SetIn(new StreamReader(Console.OpenStandardInput(8192)));
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
            bool solved = solver.SolveSodoku();
            stopwatch.Stop();

            if (solved)
            {
                solver.PrintSodokuBoard();
                Console.WriteLine(solver.counter);
            }
            else 
            {
                Console.WriteLine(solver.counter);

                Console.WriteLine("not solved");
            }
            
            Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} milliseconds");
        }   
    }
}
