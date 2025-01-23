using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sodoku
{
    internal interface IBoard
    {
        void InitializeBoard(int[] input);
        bool IsValidBoard();
        Board CloneBoard();
        int GetNumOfUnsolvedCells();
        ICell GetCellInPosition(int x, int y);
        UnsolvedCell FindCellWithMinOptions();
        void PrintBoard();
    }
}
