using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Sodoku.GlobalConstants;

namespace Sodoku
{
    public class Board : IBoard
    {
        private ICell[,] _board;
        public Board(int[] input)
        {
            _board = new ICell[BoardLength, BoardLength];
            InitializeBoard(input);
            InitializeCellOptions();
        }
        public Board()
        {
            _board = new ICell[BoardLength, BoardLength];
        }

        /// <summary>
        /// Initializes the board using a given array of integers.
        /// </summary>
        /// <param name="input">Array of integers representing the board's initial state.</param>
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

        /// <summary>
        /// Calculates the box number for a cell based on its coordinates.
        /// </summary>
        /// <param name="xCoordinate">The row coordinate of the cell.</param>
        /// <param name="yCoordinate">The column coordinate of the cell.</param>
        /// <returns>The box number (1-9).</returns>
        private int CalculateBox(int xCoordinate, int yCoordinate)
        {
            int boxRow = xCoordinate / BoxLength;
            int boxCol = yCoordinate / BoxLength;
            int boxNumber = boxRow * BoxLength + boxCol + 1;
            return boxNumber;
        }

        /// <summary>
        /// Calculates the top-left coordinate of the box.
        /// </summary>
        /// <param name="box">The box number (1-9).</param>
        /// <returns>The top-left coordinates of the box.</returns>
        private (int, int) CalculateCoordinateByBox(int box)
        {
            int boxRow = (box - 1) / BoxLength;
            int boxCol = (box - 1) % BoxLength;

            int startRow = boxRow * BoxLength;
            int startCol = boxCol * BoxLength;
            return (startRow, startCol);
        }

        /// <summary>
        /// Updates the unsolved cells options acccording to the solved cells options
        /// </summary>
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
        /// Updates the options of unsolved cells based on a solved cell's value.
        /// </summary>
        /// <param name="cell">The solved cell.</param>
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
            for (int col = 0; col < BoardLength; col++)
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
            var (startRow, startCol) = CalculateCoordinateByBox(cell._box);

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
        /// Finds the unsolved cell with the least options.
        /// </summary>
        /// <returns>The unsolved cell with the fewest options.</returns>
        public UnsolvedCell FindCellWithMinOptions()
        {
            int minOptions = BoardLength;
            UnsolvedCell minOptionsCell = null;
            for (int i = 0; i < BoardLength; i++)
            {
                for (int j = 0; j < BoardLength; j++)
                {
                    if (_board[i, j] is UnsolvedCell tempCell)
                    {
                        if (tempCell._options.Count == 1)
                        {
                            return tempCell;
                        }
                        else if (tempCell._options.Count < minOptions)
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
        /// Checks if the board is valid (no cells with zero options).
        /// </summary>
        /// <returns>True if the board is valid, otherwise false.</returns>
        public bool IsValidBoard()
        {
            for (int i = 0; i < BoardLength; i++)
            {
                for (int j = 0; j < BoardLength; j++)
                {
                    if (_board[i, j] is UnsolvedCell tempCell && tempCell._options.Count == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Returns a list of unsolved cells in the specified row.
        /// </summary>
        /// <param name="row">The row number.</param>
        /// <returns>List of unsolved cells in the row.</returns>
        public List<UnsolvedCell> GetRow(int row)
        {
            List<UnsolvedCell> cells = new List<UnsolvedCell>();
            for (int i = 0; i < BoardLength; i++)
            {
                if (_board[row, i] is UnsolvedCell tempCell)
                {
                    cells.Add(tempCell);
                }
            }
            return cells;
        }

        /// <summary>
        /// Returns a list of unsolved cells in the specified column.
        /// </summary>
        /// <param name="col">The column number.</param>
        /// <returns>List of unsolved cells in the column.</returns>
        public List<UnsolvedCell> GetCol(int col)
        {
            List<UnsolvedCell> cells = new List<UnsolvedCell>();
            for (int i = 0; i < BoardLength; i++)
            {
                if (_board[i, col] is UnsolvedCell tempCell)
                {
                    cells.Add(tempCell);
                }
            }
            return cells;
        }

        /// <summary>
        /// Returns a list of unsolved cells in the specified box.
        /// </summary>
        /// <param name="box">The box number.</param>
        /// <returns>List of unsolved cells in the box.</returns>
        public List<UnsolvedCell> GetBox(int box)
        {
            List<UnsolvedCell> cells = new List<UnsolvedCell>();

            var (startRow, startCol) = CalculateCoordinateByBox(box);

            for (int i = 0; i < BoxLength; i++)
            {
                for (int j = 0; j < BoxLength; j++)
                {
                    if (_board[startRow + i, startCol + j] is UnsolvedCell tempCell)
                    {
                        cells.Add(tempCell);
                    }
                }
            }
            return cells;
        }

        /// <summary>
        /// Gets the cell at the specified coordinates.
        /// </summary>
        /// <param name="x">The row coordinate.</param>
        /// <param name="y">The column coordinate.</param>
        /// <returns>The cell at the specified position.</returns>
        public ICell GetCellInPosition(int x, int y)
        {
            return _board[x, y];
        }

        /// <summary>
        /// Replaces a cell with a solved cell.
        /// </summary>
        /// <param name="cell">The solved cell.</param>
        public void ReplaceToSolvedCell(SolvedCell cell)
        {
            _board[cell._row, cell._col] = cell;
        }

        /// <summary>
        /// Replaces a cell with an unsolved cell.
        /// </summary>
        /// <param name="cell">The unsolved cell.</param>
        public void ReplaceToUnsolvedCell(UnsolvedCell cell)
        {
            _board[cell._row, cell._col] = cell;
        }

        /// <summary>
        /// Creates a deep copy of the board.
        /// </summary>
        /// <returns>A new Board instance with the same state.</returns>
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
                    else if (cell is SolvedCell tempSolvedCell)
                    {
                        SolvedCell newSolvedCell = new SolvedCell(tempSolvedCell);
                        tempBoard.ReplaceToSolvedCell(newSolvedCell);
                    }
                }
            }
            return tempBoard;
        }

        /// <summary>
        /// Finds the first unsolved cell in the board.
        /// </summary>
        /// <returns>The first unsolved cell, or null if none exist.</returns>
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
            int boxSize = (int)Math.Sqrt(BoardLength);

            for (int row = 0; row < BoardLength; row++)
            {
                // Print horizontal separators between boxes
                if (row % boxSize == 0 && row != 0)
                {
                    Console.WriteLine(new string('─', BoardLength * 3 + (boxSize - 1) * 2 - 1));
                }

                for (int col = 0; col < BoardLength; col++)
                {
                    // Print vertical separators between boxes
                    if (col % boxSize == 0 && col != 0)
                    {
                        Console.Write("│ ");
                    }

                    if (_board[row, col] is SolvedCell tempCell)
                    {
                        Console.Write(((char)(tempCell._value + '0')).ToString().PadLeft(2) + " ");
                    }
                    else
                    {
                        Console.Write(". ".PadLeft(3));
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Checks if the board is completely solved.
        /// </summary>
        /// <returns>True if the board is solved, otherwise false.</returns>
        public bool IsBoardSolved()
        {
            var checkHashSet = new HashSet<int>();
            for (int row = 0; row < BoardLength; row++)
            {
                for (int i = 1; i < BoardLength + 1; i++)
                {
                    checkHashSet.Add(i);
                }

                for (int col = 0; col < BoardLength; col++)
                {
                    if (_board[row, col] is SolvedCell tempCell)
                    {
                        checkHashSet.Remove(tempCell._value);
                    }
                    else
                    {
                        return false;
                    }
                }
                if (checkHashSet.Count != 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
