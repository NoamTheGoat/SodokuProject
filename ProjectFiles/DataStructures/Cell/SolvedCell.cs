using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sodoku
{
    /// <summary>
    /// Represents a cell in the Sodoku board that has been solved. 
    /// It contains the fixed value of the cell along with its row, column, and box positions.
    /// </summary>
    public class SolvedCell : ICell
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
        public SolvedCell(SolvedCell cell)
        {
            _row = cell._row;
            _col = cell._col;
            _box = cell._box;
            _value = cell._value;
        }
    }
}
