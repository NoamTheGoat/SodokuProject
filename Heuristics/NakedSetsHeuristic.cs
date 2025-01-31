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
        public static bool HandleNakedSets(IBoard board, int setSize)
        {
            bool isChanged = false;

            for (int i = 0; i < BoardLength; i++)
            {
                isChanged |= ProcessGroupForNakedSets(board, board.GetRow(i), setSize);
                isChanged |= ProcessGroupForNakedSets(board, board.GetCol(i), setSize);
                isChanged |= ProcessGroupForNakedSets(board, board.GetBox(i + 1), setSize);
            }

            return isChanged;
        }

        private static bool ProcessGroupForNakedSets(IBoard board, List<UnsolvedCell> unsolvedCells, int setSize)
        {
            bool isChanged = false;

            if (unsolvedCells.Count < setSize)
            {
                return false;
            }

            for (int i = 0; i < unsolvedCells.Count - setSize;  i++)
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
