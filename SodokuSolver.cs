using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sodoku.GlobalConstants;

namespace Sodoku
{
    internal class SodokuSolver
    {
        private Board board;

        public SodokuSolver(int[] input)
        {
            board = new Board(input);
        }
        public bool SolveSodoku()
        {
            FillSolvedCells();
            UnsolvedCell firstUnsolvedCell = board.FindFirstUnsolvedCell();

            if (firstUnsolvedCell != null)
            {
                return true;
            }

            return SolveWithBackTracking(firstUnsolvedCell);
        }


        /// <summary>
        /// Solves the sodoku like a human, 
        /// if there is a cell with 1 option it turns it into a solved cell
        /// </summary>
        /// <param name="board"></param>
        private void FillSolvedCells()
        {
            UnsolvedCell currentCell = board.FindCellWithMinOptions();

            while (currentCell != null && currentCell._options.Count == 1)
            {
                SolvedCell tempCell = new SolvedCell(currentCell._row, currentCell._col, currentCell._box, currentCell._options.First());
                board.ReplaceToSolvedCell(tempCell);
                board.UpdateBoardOptions(tempCell);
                board.DecreseUnsolvedCellCount();
                currentCell = board.FindCellWithMinOptions();
            }
        }

        private bool SolveWithBackTracking(UnsolvedCell currentCell)
        {
            if (board.GetNumOfUnsolvedCells() == 0)
            {
                return true;
            }

            FillSolvedCells();

            UnsolvedCell nextCell = board.FindCellWithMinOptions();
            if (nextCell == null)
            {
                nextCell = board.FindFirstUnsolvedCell();
            }

            if (board.GetCellInPosition(currentCell._row, currentCell._col) is SolvedCell)
            {
                SolveWithBackTracking(nextCell);
            }

            Board boardCopy = board.CloneBoard();

            if (currentCell._options.Count == 0) 
            {
                return false;
            }

            
            foreach (int option in nextCell._options) 
            {
                SolvedCell possibleSolvedCell = new SolvedCell(currentCell._row, currentCell._col, currentCell._box, option);

                boardCopy.ReplaceToSolvedCell(possibleSolvedCell);
                boardCopy.UpdateBoardOptions(possibleSolvedCell);
                boardCopy.DecreseUnsolvedCellCount();

                if (SolveWithBackTracking(nextCell))
                {
                    return true;
                }

                boardCopy = board.CloneBoard();
            }

            return false;
        }
    }

}
