using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sodoku.GlobalConstants;

namespace Sodoku.Heuristics
{
    /// <summary>
    /// A hidden single occurs when a number can only appear in one unsolved cell 
    /// within a row, column, or box, even if other numbers are also options for that cell.
    /// </summary>
    /// <param name="board">The Sudoku board</param>
    /// <returns>
    /// Returns true if any changes were made, otherwise, false.
    /// </returns>

    public static class HiddenSingleHeuristic
    {
        public static bool HandleHiddenSingles(IBoard board)
        {
            bool isChanged = false;

            for (int i = 0; i < BoardLength; i++)
            {
                isChanged |= ProcessGroupForNakedSingles(board, board.GetUnsolvedCellsInRow(i));
                isChanged |= ProcessGroupForNakedSingles(board, board.GetUnsolvedCellsInCol(i));
                isChanged |= ProcessGroupForNakedSingles(board, board.GetUnsolvedCellsInBox(i + 1));
            }

            return isChanged;
        }

        /// <summary>
        /// Applies the naked singles technique to a group of unsolved cells. 
        /// If an option is unique to one cell, it removes all the other options from the cell.
        /// </summary>
        /// <param name="board">The Sudoku board.</param>
        /// <param name="unsolvedCells">A list of unsolved cells in a row, column, or box.</param>
        /// Returns true if any changes were made, otherwise, false.

        private static bool ProcessGroupForNakedSingles(IBoard board, List<UnsolvedCell> unsolvedCells)
        {
            bool isChanged = false;
            var optionDictionary = new Dictionary<int, List<UnsolvedCell>>();

            // Map each option to the cells it appears in
            foreach (var cell in unsolvedCells)
            {
                foreach (int option in cell._options)
                {
                    if (!optionDictionary.ContainsKey(option))
                    {
                        optionDictionary[option] = new List<UnsolvedCell>();
                    }
                    optionDictionary[option].Add(cell);
                }
            }

            // Apply naked singles
            foreach (var kvp in optionDictionary)
            {
                int option = kvp.Key;
                List<UnsolvedCell> cells = kvp.Value;

                if (cells.Count == 1)
                {
                    var cell = cells[0];
                    if (cell._options.Count != 1 || !cell._options.Contains(option))
                    {
                        cell._options.Clear();
                        cell._options.Add(option);
                        isChanged = true;
                    }
                }
            }

            return isChanged;
        }
    }
}

