using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sodoku.GlobalConstants;

namespace Sodoku.Heuristics
{
    public static class HiddenSingleHeuristic
    {
        public static bool HandleHiddenSingles(IBoard board)
        {
            bool isChanged = false;

            for (int i = 0; i < BoardLength; i++)
            {
                isChanged |= ProcessGroupForNakedSingles(board, board.GetRow(i));
                isChanged |= ProcessGroupForNakedSingles(board, board.GetCol(i));
                isChanged |= ProcessGroupForNakedSingles(board, board.GetBox(i + 1));
            }

            return isChanged;
        }


        private static bool ProcessGroupForNakedSingles(IBoard board, List<UnsolvedCell> unsolvedCells)
        {
            bool isChanged = false;
            var optionDictionary = new Dictionary<int, List<UnsolvedCell>>();

            // Step 1: Map each option to the cells it appears in
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

            // Step 2: Apply naked singles
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
                        isChanged = true; // A change has been made
                    }
                }
            }

            return isChanged;
        }
    }
}
