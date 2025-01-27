using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sodoku.GlobalConstants;

namespace Sodoku.Heuristics
{
    public static class NakedPairsHeuristic
    {
        public static bool FindAndEliminateNakedPairs(IBoard board)
        {
            bool isChanged = false;
            var groupSet = new List<UnsolvedCell>();

            // Collect all unsolved cells with exactly 'pairSize' options
            for (int i = 0; i < BoardLength; i++)
            {
                for (int j = 0; j < BoardLength; j++)
                {
                    if (board.GetCellInPosition(i, j) is UnsolvedCell tempCell
                        && tempCell._options.Count == pairSize)
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
                        var nakedSet = new List<UnsolvedCell> { groupSet[i], groupSet[j] };
                        bool pairChanged = false;

                        if (groupSet[i]._box == groupSet[j]._box)
                        {
                            pairChanged = EliminateNakedSets(board, board.GetBox(groupSet[i]._box), nakedSet);
                        }
                        else if (groupSet[i]._col == groupSet[j]._col)
                        {
                            pairChanged = EliminateNakedSets(board, board.GetCol(groupSet[i]._col), nakedSet);
                        }
                        else if (groupSet[i]._row == groupSet[j]._row)
                        {
                            pairChanged = EliminateNakedSets(board, board.GetRow(groupSet[i]._row), nakedSet);
                        }

                        isChanged |= pairChanged; // Track if any change occurred
                    }
                }
            }

            return isChanged;
        }


        private static bool EliminateNakedSets(IBoard board, List<UnsolvedCell> group, List<UnsolvedCell> nakedSet)
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
        private static bool HasEqualOptions(UnsolvedCell Cell1, UnsolvedCell Cell2)
        {
            return Cell1._options.SetEquals(Cell2._options);
        }
    }
}
