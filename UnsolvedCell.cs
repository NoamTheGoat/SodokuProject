using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sodoku.GlobalConstants;

namespace Sodoku
{
    internal class UnsolvedCell : ICell
    {
        private SortedSet<int> _options { get; }
        public int _row { get; }
        public int _col { get; }
        public int _box { get; }

        public UnsolvedCell(int row, int col, int box)
        {
            _row = row;
            _col = col;
            _box = box;
            _options = new SortedSet<int>();

            InitializeCell();
        }
        public UnsolvedCell(int row, int col, int box, int preFilledCellValue)
        {
            _row = row;
            _col = col;
            _box = box;
            _options = new SortedSet<int>{preFilledCellValue};
        }

        public void InitializeCell()
        {
            for (int i = 1; i < BoardLength+1; i++)
            {
                _options.Add(i);
            }
        }

        public void RemoveOption(int option)
        {
            _options.Remove(option);
        }
    }
}
