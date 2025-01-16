using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sodoku.GlobalConstants;
using static Sodoku.BoxNumberGenerator;

namespace Sodoku
{ 
    internal class Board
    {
        private ICell[,] _board;
        public Board(int[] input) 
        {
            _board = new ICell[BoardLength, BoardLength];
            InitializeBoard(input);
        }

        /// <summary>
        /// Initializes the board with cells
        /// </summary>
        /// <param name="input"></param>
        public void InitializeBoard(int[] input)
        {
            int currentIndex;
            for (int i = 0; i < BoardLength; i++)
            {
                for (int j = 0; j < BoardLength; j++)
                {
                    currentIndex = i * BoardLength + j;
                    if (input[currentIndex] == 0)
                    {
                        _board[i, j] = new UnsolvedCell(i, j, CalculateBox(i, j));
                    }
                    else
                    {
                        _board[i, j] = new SolvedCell(i, j, CalculateBox(i, j), input[currentIndex]);
                    }
                }
            }
        }

        private int CalculateBox(int xCoordinate, int yCoordinate)
        {
            int newX = xCoordinate - xCoordinate % BoxLength;
            int newY = yCoordinate - yCoordinate % BoxLength;
            var newCoords = new Tuple<int, int>(newX, newY);
            return GetKeyByValue(BoxNumberDict, newCoords);
        }

        public void UpdateBoardOptions(SolvedCell cell)
        {
            UpdateOptionsByRow(cell);
            UpdateOptionsByCol(cell);
            UpdateOptionsByBox(cell);
        }
        private void UpdateOptionsByRow(SolvedCell cell)
        {
            for(int i = 0; i < BoardLength; i++)
            {
                if (_board[cell._row, i] is UnsolvedCell)
                {
                    UnsolvedCell temp = (UnsolvedCell)_board[cell._row, i];
                    temp.RemoveOption(cell._value);
                    _board[cell._row, i] = temp; 
                }  
            }
        }
        private void UpdateOptionsByCol(SolvedCell cell)
        {
            for (int i = 0; i < BoardLength; i++)
            {
                if (_board[i, cell._row] is UnsolvedCell)
                {
                    UnsolvedCell temp = (UnsolvedCell)_board[cell._row, i];
                    temp.RemoveOption(cell._value);
                    _board[cell._row, i] = temp;
                }
            }
        }
        private void UpdateOptionsByBox(SolvedCell cell)
        {
            Tuple<int, int> boxCoords = BoxNumberDict[cell._box];
            
            for (int i = 0; i < BoxLength; i++)
            {
                for (int j = 0; j < BoxLength; j++)
                {
                    if (_board[i + boxCoords.Item1, j + boxCoords.Item2] is UnsolvedCell)
                    {
                        UnsolvedCell tempCell = (UnsolvedCell)_board[i + boxCoords.Item1, j + boxCoords.Item2];
                        tempCell.RemoveOption(cell._value);
                        _board[cell._row, i] = tempCell;
                    }
                }
            }
        }
        public UnsolvedCell FindCellWithMinOptions()
        {
            int minOptions = BoardLength;
            UnsolvedCell minOptionsCell = null;
            for (int i = 0;i < BoardLength; i++)
            {
                for (int j = 0; j < BoardLength; j++)
                {
                    if (_board[i,j] is UnsolvedCell)
                    {
                        UnsolvedCell tempCell = (UnsolvedCell)_board[i, j];
                        if (tempCell._options.Count == 1) 
                            return tempCell; 

                        else
                        {
                            if(tempCell._options.Count < minOptions)
                            {
                                minOptions = tempCell._options.Count;
                                minOptionsCell = tempCell;
                            }
                        }
                    }
                }
            }
            return minOptionsCell;
        }
    }
}
