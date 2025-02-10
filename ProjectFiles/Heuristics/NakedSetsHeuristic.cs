using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sodoku.GlobalConstants;


namespace Sodoku.Heuristics
{
    public static class NakedSetsHeuristic
    {
        /// <summary>
        /// Iterates over each row, column, and box of the Sodoku board to process naked sets.
        /// A naked set is a group of unsolved cells that together contain a number of options equal to the set size.
        /// It eliminates those options from the other cells in the group that are not part of the naked set.
        /// further explination in github readme.
        /// </summary>
        /// <param name="board">The Sodoku board where the naked sets are to be handled.</param>
        /// <param name="setSize">The size of the naked set to be processed.</param>
        /// <returns>
        /// Returns true if any changes were made to the board, otherwise, false.
        /// </returns>
        public static bool HandleNakedSets(IBoard board, int setSize)
        {
            bool isChanged = false;

            for (int i = 0; i < BoardLength; i++)
            {
                isChanged |= ProcessGroupForNakedSets(board, board.GetUnsolvedCellsInRow(i), setSize);
                isChanged |= ProcessGroupForNakedSets(board, board.GetUnsolvedCellsInCol(i), setSize);
                isChanged |= ProcessGroupForNakedSets(board, board.GetUnsolvedCellsInBox(i + 1), setSize);
            }

            return isChanged;
        }

        /// <summary>
        /// Processes a group (row, column, or box) of unsolved cells to detect and handle naked sets.
        /// if the union of the options is equal to the set size,
        /// it eliminates those options from other unsolved cells in the group.
        /// </summary>
        /// <param name="board">The Sodoku board where the naked sets are to be processed.</param>
        /// <param name="unsolvedCells">A list of unsolved cells in the current group (row, column, or box).</param>
        /// <param name="setSize">The size of the naked set to be processed.</param>
        /// <returns>
        /// Returns true if any changes were made, otherwise, false.
        /// </returns>
        private static bool ProcessGroupForNakedSets(IBoard board, List<UnsolvedCell> unsolvedCells, int setSize)
        {
            bool isChanged = false;

            if (unsolvedCells.Count < setSize)
            {
                return false;
            }

            for (int i = 0; i < unsolvedCells.Count - setSize; i++)
            {
                var subset = unsolvedCells.GetRange(i, setSize);
                var optionsUnion = new HashSet<int>();

                foreach (var cell in subset)
                {
                    optionsUnion.UnionWith(cell._options);
                }

                if (optionsUnion.Count == setSize)
                {
                    isChanged |= EliminateNakedSets(board, unsolvedCells, subset);
                }
            }

            return isChanged;
        }

        /// <summary>
        /// Eliminates options from unsolved cells in a group (row, column, or box) based on a detected naked set.
        /// It removes the options of the naked set from other cells that do not belong to the naked set.
        /// </summary>
        /// <param name="board">The Sodoku board where the naked set is to be eliminated.</param>
        /// <param name="group">The group (row, column, or box) of unsolved cells to process.</param>
        /// <param name="nakedSet">The set of unsolved cells identified as a naked set.</param>
        /// <returns>
        /// Returns true if any changes were made, otherwise, false.
        /// </returns>
        public static bool EliminateNakedSets(IBoard board, List<UnsolvedCell> group, List<UnsolvedCell> nakedSet)
        {
            bool isChanged = false;
            HashSet<int> nakedOptions = new HashSet<int>(nakedSet.First()._options);

            foreach (var cell in group)
            {
                if (!nakedSet.Contains(cell))
                {
                    int initialOptionCount = cell._options.Count;
                    cell._options.ExceptWith(nakedOptions);

                    if (cell._options.Count != initialOptionCount) // Check if options were modified
                    {
                        isChanged = true;
                    }
                }
            }

            return isChanged;
        }
    }
}
