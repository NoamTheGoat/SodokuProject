using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sodoku.GlobalConstants;
using static Sodoku.Heuristics.NakedSetsHeuristic;

namespace Sodoku.Heuristics
{
    public static class NakedPairsHeuristic
    {
        /// <summary>
        /// checks if two unsolved cells in a row, column, or box contain exactly the same two options.
        /// These options can be eliminated from other cells in the same group (row, column, or box).
        /// This naked pairs is more efficient than naked sets with a set size of 2 so it is used for bigger boards.
        /// </summary>
        /// <param name="board">The Sudoku board on which naked pairs are to be detected and processed.</param>
        /// <returns>
        /// Returns true if any changes were made, otherwise, false.
        /// </returns>
        public static bool HandleNakedPairs(IBoard board)
        {
            bool isChanged = false;
            var groupSet = new List<UnsolvedCell>();

            // Collect all unsolved cells with exactly 2 options
            for (int i = 0; i < BoardLength; i++)
            {
                for (int j = 0; j < BoardLength; j++)
                {
                    if (board.GetCellInPosition(i, j) is UnsolvedCell tempCell
                        && tempCell._options.Count == PairSize)
                    {
                        groupSet.Add(tempCell);
                    }
                }
            }

            // Check for naked pairs within the collected group
            for (int i = 0; i < groupSet.Count; i++)
            {
                for (int j = i + 1; j < groupSet.Count; j++)
                {
                    if (HasEqualOptions(groupSet[i], groupSet[j]))
                    {
                        var nakedPair = new List<UnsolvedCell> { groupSet[i], groupSet[j] };
                        bool pairChanged = false;

                        if (groupSet[i]._box == groupSet[j]._box)
                        {
                            pairChanged = EliminateNakedSets(board, board.GetBox(groupSet[i]._box), nakedPair);
                        }
                        else if (groupSet[i]._col == groupSet[j]._col)
                        {
                            pairChanged = EliminateNakedSets(board, board.GetCol(groupSet[i]._col), nakedPair);
                        }
                        else if (groupSet[i]._row == groupSet[j]._row)
                        {
                            pairChanged = EliminateNakedSets(board, board.GetRow(groupSet[i]._row), nakedPair);
                        }

                        isChanged |= pairChanged;
                    }
                }
            }

            return isChanged;
        }

        /// <summary>
        /// checks if 2 cells have equal options
        /// </summary>
        private static bool HasEqualOptions(UnsolvedCell Cell1, UnsolvedCell Cell2)
        {
            return Cell1._options.SetEquals(Cell2._options);
        }
    }
}
