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
        private int _numOfSolvedCells;
        private int _numOfUnsolvedCells;
        public Board(int[] input) 
        {
            _board = new ICell[BoardLength, BoardLength];
            _numOfSolvedCells = 0;
            _numOfUnsolvedCells = 0;
            InitializeBoard(input);
        }
        public Board()
        {
            _board = new ICell[BoardLength, BoardLength];
            _numOfSolvedCells = 0;
            _numOfUnsolvedCells = 0;
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
                        _numOfUnsolvedCells++;
                    }
                    else
                    {
                        _board[i, j] = new SolvedCell(i, j, CalculateBox(i, j), input[currentIndex]);
                        _numOfSolvedCells++;
                    }
                }
            }
        }
        /// <summary>
        /// Calculates the the box of the cell
        /// if the board is 9x9 a box is 3x3 and there are 9 of them
        /// </summary>
        /// <param name="xCoordinate"></param>
        /// <param name="yCoordinate"></param>
        /// <returns></returns>
        private int CalculateBox(int xCoordinate, int yCoordinate)
        {
            int newX = xCoordinate - xCoordinate % BoxLength;
            int newY = yCoordinate - yCoordinate % BoxLength;
            var newCoords = new Tuple<int, int>(newX, newY);
            return GetKeyByValue(BoxNumberDict, newCoords);
        }

        /// <summary>
        /// Gets the number of unsolved cells
        /// </summary>
        /// <returns></returns>
        public int GetNumOfUnsolvedCells()
        {
            return _numOfUnsolvedCells;
        }

        /// <summary>
        /// Gets the number of solved cells
        /// </summary>
        /// <returns></returns>
        public int GetNumOfSolvedCells()
        {
            return _numOfSolvedCells;
        }

        /// <summary>
        /// Updates the option of the unsolved cell in the row, colum and box
        /// </summary>
        /// <param name="cell"></param>
        public void UpdateBoardOptions(SolvedCell cell)
        {
            UpdateOptionsByRow(cell);
            UpdateOptionsByCol(cell);
            UpdateOptionsByBox(cell);
        }
        /// <summary>
        /// Updates the option of the unsolved cell in the row
        /// </summary>
        /// <param name="cell"></param>
        private void UpdateOptionsByRow(SolvedCell cell)
        {
            for(int i = 0; i < BoardLength; i++)
            {
                if (_board[cell._row, i] is UnsolvedCell tempCell)
                {
                    tempCell.RemoveOption(cell._value);
                    _board[cell._row, i] = tempCell; 
                }  
            }
        }
        /// <summary>
        /// Updates the option of the unsolved cell in the colum
        /// </summary>
        /// <param name="cell"></param>
        private void UpdateOptionsByCol(SolvedCell cell)
        {
            for (int i = 0; i < BoardLength; i++)
            {
                if (_board[i, cell._row] is UnsolvedCell tempCell)
                {
                    tempCell.RemoveOption(cell._value);
                    _board[cell._row, i] = tempCell;
                }
            }
        }
        /// <summary>
        /// Updates the option of the unsolved cell in the box
        /// </summary>
        /// <param name="cell"></param>
        private void UpdateOptionsByBox(SolvedCell cell)
        {
            Tuple<int, int> boxCoords = BoxNumberDict[cell._box];
            
            for (int i = 0; i < BoxLength; i++)
            {
                for (int j = 0; j < BoxLength; j++)
                {
                    if (_board[i + boxCoords.Item1, j + boxCoords.Item2] is UnsolvedCell tempCell)
                    {
                        tempCell.RemoveOption(cell._value);
                        _board[cell._row, i] = tempCell;
                    }
                }
            }
        }
        /// <summary>
        /// Finds the unsolved cells with the minimun options
        /// </summary>
        /// <returns></returns>
        public UnsolvedCell FindCellWithMinOptions()
        {
            int minOptions = BoardLength;
            UnsolvedCell minOptionsCell = null;
            for (int i = 0;i < BoardLength; i++)
            {
                for (int j = 0; j < BoardLength; j++)
                {
                    if (_board[i,j] is UnsolvedCell tempCell)
                    {
                        if (tempCell._options.Count == 1)
                        {
                            return tempCell;
                        }
                        else if(tempCell._options.Count < minOptions)
                        {
                            minOptions = tempCell._options.Count;
                            minOptionsCell = tempCell;
                        }
                    }
                }
            }
            return minOptionsCell;
        }
        /// <summary>
        /// Checks if the board is valid
        /// the board is unvalid if there is a cell with 0 options
        /// </summary>
        /// <returns></returns>
        public bool IsValidBoard()
        {
            for(int i = 0; i < BoardLength; i++)
            {
                for (int j = 0; j < BoardLength; j++)
                {
                    if (_board[i,j] is UnsolvedCell tempCell && tempCell._options.Count == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// Gets a cell in a position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public ICell GetCellInPosition(int x, int y)
        {
            return _board[x,y];
        }

        /// <summary>
        /// Replaces a cell with an unsolved cell
        /// </summary>
        /// <param name="cell"></param>
        public void ReplaceToSolvedCell(SolvedCell cell)
        {
            _board[cell._row, cell._col] = cell;
        }

        /// <summary>
        /// Updates the option of the unsolved cell in the row
        /// </summary>
        /// <param name="cell"></param>
        public void ReplaceToUnsolvedCell(UnsolvedCell cell)
        {
            _board[cell._row, cell._col] = cell;
        }

        /// <summary>
        /// Decreases the amount of unsolved cells
        /// used when replacing cells
        /// </summary>
        public void DecreseUnsolvedCellCount()
        {
            this._numOfUnsolvedCells--;
            this._numOfSolvedCells++;
        }

        /// <summary>
        /// Does a deep copy of a board
        /// </summary>
        /// <returns></returns>
        public Board CloneBoard()
        {
            Board tempBoard = new Board();
            for (int i = 0; i < BoardLength; i++)
            {
                for (int j = 0; j < BoardLength; j++)
                {
                    if (_board[i, j] is UnsolvedCell tempUnsolvedCell)
                    {
                        UnsolvedCell newUnsolvedCell = new UnsolvedCell(tempUnsolvedCell);
                        tempBoard.ReplaceToUnsolvedCell(newUnsolvedCell);
                        tempBoard._numOfUnsolvedCells++;
                    }
                    else
                    {
                        SolvedCell tempSolvedCell = (SolvedCell)tempBoard.GetCellInPosition(i, j);
                        SolvedCell newSolvedCell = new SolvedCell(tempSolvedCell);
                        tempBoard.ReplaceToSolvedCell(newSolvedCell);
                        tempBoard._numOfSolvedCells++;
                    }
                }
            }
            return tempBoard;
        }

        /// <summary>
        /// finds the first unsolved cell in the board
        /// </summary>
        /// <returns></returns>
        public UnsolvedCell FindFirstUnsolvedCell()
        {
            for (int i = 0; i < BoardLength; i++)
            {
                for (int j = 0; j < BoardLength; j++)
                {
                    if (_board[i, j] is UnsolvedCell cell)
                    {
                        return cell;
                    }
                }
            }
            return null;
        }
    }
}
