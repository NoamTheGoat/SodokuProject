using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sodoku.GlobalConstants;

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
            InitializeCellOptions();
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
            int boxRow = xCoordinate/3;
            int boxCol = yCoordinate/3;
            int boxNumber = boxRow * BoxLength + boxCol + 1;
            return boxNumber;
        }

        private void InitializeCellOptions()
        {
            for (int i = 0; i < BoardLength; i++)
            {
                for (int j = 0; j < BoardLength; j++)
                {
                    if (_board[i, j] is SolvedCell tempCell)
                    {
                        UpdateBoardOptions(tempCell);
                    }
                }
            }
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
            for(int col = 0; col < BoardLength; col++)
            {
                if (_board[cell._row, col] is UnsolvedCell tempCell)
                {
                    tempCell.RemoveOption(cell._value);
                    _board[cell._row, col] = tempCell; 
                }  
            }
        }
        /// <summary>
        /// Updates the option of the unsolved cell in the colum
        /// </summary>
        /// <param name="cell"></param>
        private void UpdateOptionsByCol(SolvedCell cell)
        {
            for (int row = 0; row < BoardLength; row++)
            {
                if (_board[row, cell._col] is UnsolvedCell tempCell)
                {
                    tempCell.RemoveOption(cell._value);
                    _board[row, cell._col] = tempCell;
                }
            }
        }
        /// <summary>
        /// Updates the option of the unsolved cell in the box
        /// </summary>
        /// <param name="cell"></param>
        private void UpdateOptionsByBox(SolvedCell cell)
        {
            int boxRow = (cell._box - 1) / 3;  
            int boxCol = (cell._box - 1) % 3;   

            int startRow = boxRow * BoxLength;  
            int startCol = boxCol * BoxLength;

            for (int i = 0; i < BoxLength; i++)
            {
                for (int j = 0; j < BoxLength; j++)
                {
                    if (_board[startRow + i, startCol + j] is UnsolvedCell tempCell)
                    {
                        tempCell.RemoveOption(cell._value);
                        _board[startRow + i, startCol + j] = tempCell;
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
                    var cell = _board[i, j];
                    if (cell is UnsolvedCell tempUnsolvedCell)
                    {
                        UnsolvedCell newUnsolvedCell = new UnsolvedCell(tempUnsolvedCell);
                        tempBoard.ReplaceToUnsolvedCell(newUnsolvedCell);
                    }
                    else if(cell is SolvedCell tempSolvedCell)
                    {
                        SolvedCell newSolvedCell = new SolvedCell(tempSolvedCell);
                        tempBoard.ReplaceToSolvedCell(newSolvedCell);
                    }
                }
            }
            tempBoard._numOfSolvedCells = _numOfSolvedCells;
            tempBoard._numOfUnsolvedCells = _numOfUnsolvedCells;
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

        /// <summary>
        /// Prints the board
        /// </summary>
        public void PrintBoard()
        {
            for (int row = 0; row < BoardLength; row++)
            {
                if(row % 3 == 0 && row != 0)
                {
                    Console.WriteLine("---------------------");
                }

                for (int col = 0; col < BoardLength; col++)
                {
                    if(col % 3 == 0 && col != 0)
                    {
                        Console.Write("| ");
                    }
                    if (_board[row, col] is SolvedCell tempCell)
                    {
                        Console.Write(tempCell._value + " ");
                    }
                    else
                    {
                        Console.Write(". ");
                    }
                }

                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
