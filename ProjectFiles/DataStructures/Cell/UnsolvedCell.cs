﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sodoku.GlobalConstants;

namespace Sodoku
{
    /// <summary>
    /// Represents a cell in the Sodoku board that has not yet been solved. 
    /// It maintains a set of possible values (options) that can fill the cell.
    /// </summary>
    public class UnsolvedCell : ICell
    {
        public HashSet<int> _options { get; }
        public int _row { get; }
        public int _col { get; }
        public int _box { get; }

        public UnsolvedCell(int row, int col, int box)
        {
            _row = row;
            _col = col;
            _box = box;
            _options = new HashSet<int>();

            InitializeCell();
        }

        /// <summary>
        /// for deep cloning
        /// </summary>
        /// <param name="cell"></param>
        public UnsolvedCell(UnsolvedCell cell)
        {
            _row = cell._row;
            _col = cell._col;
            _box = cell._box;
            _options = new HashSet<int> (cell._options);
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
            if (_options.Contains(option))
            {
                _options.Remove(option);
            }
        }
    }
}
