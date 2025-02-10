using Sodoku.Heuristics;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sodoku.GlobalConstants;
using static Sodoku.Heuristics.HiddenSingleHeuristic;
using static Sodoku.Heuristics.NakedPairsHeuristic;
using static Sodoku.Heuristics.NakedSetsHeuristic;


namespace Sodoku
{
    public class SodokuSolver
    {
        private IBoard board;

        //Used for counting the number of recurive calls
        public int counter = 0;

        //Used to store how much time it took the 
        public long sodokuSolverTimer { get; set; }

        public SodokuSolver(int[] input)
        {
            board = new Board(input);
        }

        /// <summary>
        /// integrates all the solving techniques into one method
        /// </summary>
        /// <returns>true if the board was solved, otherwise false</returns>
        public bool SolveSodoku()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            SolveWithHeuristics();

            UnsolvedCell firstUnsolvedCell = board.FindCellWithMinOptions();
            if (firstUnsolvedCell == null)
            {
                firstUnsolvedCell = board.FindFirstUnsolvedCell();
            }

            if (!SolveWithBackTracking(firstUnsolvedCell))
            {
                return false;
            }

            stopwatch.Stop();
            this.sodokuSolverTimer = stopwatch.ElapsedMilliseconds;

            /*for cases when the input is a full wrong solved board*/
            if (!board.IsBoardSolved())
            {
                return false;  
            }
            return true;
        }


        /// <summary>
        ///  Solves the Sodoku board using human-like heuristics, 
        ///  such as naked singles, hidden singles, and naked sets.
        ///  if the board is 9x9 or smaller it uses naked sets, 
        ///  and if the boards is bigger it uses naked pairs because it is more efficient for bigger boards
        /// </summary>
        /// <param name="board"></param>
        private void SolveWithHeuristics()
        {
            bool isChanged = true;

            while (isChanged)
            {
                isChanged = false;
                UnsolvedCell currentCell = board.FindCellWithMinOptions();

                //Naked single heuristic
                if (currentCell != null && currentCell._options.Count == 1)
                {
                    SolvedCell tempCell = new SolvedCell
                        (currentCell._row, currentCell._col, currentCell._box, currentCell._options.First());
                    board.ReplaceToSolvedCell(tempCell);
                    board.UpdateBoardOptions(tempCell);
                    isChanged = true;
                }

                if (BoardLength <= StandardBoardSize)
                {
                    isChanged |= HandleHiddenSingles(board);

                    for (int i = 2; i < BoardLength; i++)
                    {
                        isChanged |= HandleNakedSets(board, i);
                    }
                }
                else
                {
                    isChanged |= HandleHiddenSingles(board);
                    isChanged |= HandleNakedPairs(board);
                }
            }
        }

        /// <summary>
        /// A recursive algorithm that attempts to solve the Sodoku puzzle,
        /// using backtracking from a given unsolved cell.
        /// </summary>
        /// <param name="currentCell">The current unsolved cell from which to start backtracking.</param>
        /// <returns>True if a solution is found, otherwise, false</returns>
        private bool SolveWithBackTracking(UnsolvedCell currentCell)
        {
            counter++;
            if (board.FindFirstUnsolvedCell() == null)
            {
                return true;
            }

            if (!board.IsValidBoard())
            {
                return false;
            }

            SolveWithHeuristics();
            Board boardCopy = board.CloneBoard();

            UnsolvedCell nextCell = board.FindCellWithMinOptions();
            if (nextCell == null)
            {
                nextCell = board.FindFirstUnsolvedCell();
            }

            //the current cell has been solved by the heuristics
            if (board.GetCellInPosition(currentCell._row, currentCell._col) is SolvedCell)
            {
                return SolveWithBackTracking(nextCell);
            }

            foreach (int option in nextCell._options)
            {
                var possibleSolvedCell = new SolvedCell(currentCell._row, currentCell._col, currentCell._box, option);

                board.ReplaceToSolvedCell(possibleSolvedCell);
                board.UpdateBoardOptions(possibleSolvedCell);

                if (SolveWithBackTracking(nextCell))
                {
                    return true;
                }
                board = boardCopy;
            }

            return false;
        }

        public void PrintSodokuBoard()
        {
            board.PrintBoard();
        }

        /// <summary>
        /// Converts the board back to string for exporting
        /// </summary>
        /// <returns>A string representing the Sodoku board, 
        /// or null if the board is not fully solved.</returns>
        public string ReturnBoardAsString()
        {
            string result = "";
            for (int i = 0; i < BoardLength; i++)
            {
                for (int j = 0; j < BoardLength; j++)
                {
                    if (board.GetCellInPosition(i, j) is SolvedCell currentCell)
                    {
                        result += ((char)(currentCell._value + '0')).ToString();
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            return result;
        }

        public bool IsValidSodokuBoard()
        {
            return board.IsValidBoard();
        }

        public bool IsSodokuBoardSolved()
        {
            return board.IsBoardSolved();
        }
    }
}
