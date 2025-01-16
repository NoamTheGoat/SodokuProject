using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sodoku
{
    internal class SolvedCell : ICell
    {
        public int _value { get; }
        public int _row { get; }
        public int _col { get; }
        public int _box { get; }

        public SolvedCell(int row, int col, int box, int value)
        {
            _row = row;
            _col = col;
            _box = box;
            _value = value;
        }
    }
}
