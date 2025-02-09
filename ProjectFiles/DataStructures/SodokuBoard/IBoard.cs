using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sodoku
{
    public interface IBoard
    {
        void InitializeBoard(int[] input);
        bool IsValidBoard();
        Board CloneBoard();
        ICell GetCellInPosition(int x, int y);
        UnsolvedCell FindCellWithMinOptions();
        void PrintBoard();
        UnsolvedCell FindFirstUnsolvedCell();
        void ReplaceToSolvedCell(SolvedCell tempCell);
        void UpdateBoardOptions(SolvedCell tempCell);
        List<UnsolvedCell> GetRow(int i);
        List<UnsolvedCell> GetCol(int i);
        List<UnsolvedCell> GetBox(int v);
        void ReplaceToUnsolvedCell(UnsolvedCell cell);
        bool IsBoardSolved();
    }
}
