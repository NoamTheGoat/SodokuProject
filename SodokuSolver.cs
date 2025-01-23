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
        private void FillSolvedCells()
        {
            UnsolvedCell currentCell = board.FindCellWithMinOptions();
            if (currentCell == null)
            {
                currentCell = board.FindFirstUnsolvedCell();
            }

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
            if (board.FindFirstUnsolvedCell() == null)
            {
                return true;
            }

            if (currentCell._options.Count == 0)
            {
                return false;
            }

            FillSolvedCells();

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

                board = boardCopy.CloneBoard();
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
