using Sodoku.Heuristics;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sodoku.GlobalConstants;
using static Sodoku.Heuristics.HiddenSingleHeuristic;
using static Sodoku.Heuristics.NakedPairsHeuristic;


namespace Sodoku
{
    public class SodokuSolver
    {
        private IBoard board;
        public int counter= 0;

        public SodokuSolver(int[] input)
        {
            board = new Board(input);
        }
        public bool SolveSodoku()
        {
            SolveWithHeuristics();

            UnsolvedCell firstUnsolvedCell = board.FindCellWithMinOptions();
            if (firstUnsolvedCell == null)
            {
                firstUnsolvedCell = board.FindFirstUnsolvedCell();
                if (firstUnsolvedCell == null)
                {
                    return true;
                }
            }

            if (!SolveWithBackTracking(firstUnsolvedCell))
            {
                return false;
            }

            return true;
        }


        /// <summary>
        /// Solves the sodoku like a human, 
        /// if there is a cell with 1 option it turns it into a solved cell
        /// </summary>
        /// <param name="board"></param>
        private void SolveWithHeuristics()
        {
            bool isChanged = true;

            while (isChanged)
            {
                isChanged = false;
                // Check for an unsolved cell with exactly one option
                UnsolvedCell currentCell = board.FindCellWithMinOptions();

                if (currentCell != null && currentCell._options.Count == 1)
                {
                    SolvedCell tempCell = new SolvedCell(currentCell._row, currentCell._col, currentCell._box, currentCell._options.First());
                    board.ReplaceToSolvedCell(tempCell);
                    board.UpdateBoardOptions(tempCell);
                    isChanged = true;
                }

                isChanged |= HandleHiddenSingles(board);
                isChanged |= FindAndEliminateNakedPairs(board);
            }
        }

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

            UnsolvedCell nextCell = board.FindCellWithMinOptions();
            if (nextCell == null)
            {
                nextCell = board.FindFirstUnsolvedCell();
            }

            Board boardCopy = board.CloneBoard();
            if (board.GetCellInPosition(currentCell._row, currentCell._col) is SolvedCell)
            {
                return SolveWithBackTracking(nextCell);
            }

            foreach (int option in nextCell._options) 
            {
                SolvedCell possibleSolvedCell = new SolvedCell(currentCell._row, currentCell._col, currentCell._box, option);

                board.ReplaceToSolvedCell(possibleSolvedCell);
                board.UpdateBoardOptions(possibleSolvedCell);
                board.DecreseUnsolvedCellCount();

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

        public bool IsValidSodokuBoard()
        {
            return board.IsValidBoard();
        }
    }

}
