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
        /// Returns a list of UNSOLVED cells in the specified row.
        /// </summary>
        /// <param name="row">The row number.</param>
        /// <returns>List of unsolved cells in the row.</returns>
        public List<UnsolvedCell> GetUnsolvedCellsInRow(int row)
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
        /// Returns a list of SOLVED cells in the specified row.
        /// </summary>
        /// <param name="row">The row number.</param>
        /// <returns>List of unsolved cells in the row.</returns>
        public List<SolvedCell> GetSolvedCellsInRow(int row)
        {
            List<SolvedCell> cells = new List<SolvedCell>();
            for (int i = 0; i < BoardLength; i++)
            {
                if (_board[row, i] is SolvedCell tempCell)
                {
                    cells.Add(tempCell);
                }
            }
            return cells;
        }

        /// <summary>
        /// Returns a list of UNSOLVED cells in the specified column.
        /// </summary>
        /// <param name="col">The column number.</param>
        /// <returns>List of unsolved cells in the column.</returns>
        public List<UnsolvedCell> GetUnsolvedCellsInCol(int col)
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
        /// Returns a list of SOLVED cells in the specified column.
        /// </summary>
        /// <param name="col">The column number.</param>
        /// <returns>List of unsolved cells in the column.</returns>
        public List<SolvedCell> GetSolvedCellsInCol(int col)
        {
            List<SolvedCell> cells = new List<SolvedCell>();
            for (int i = 0; i < BoardLength; i++)
            {
                if (_board[i, col] is SolvedCell tempCell)
                {
                    cells.Add(tempCell);
                }
            }
            return cells;
        }

        /// <summary>
        /// Returns a list of UNSOLVED cells in the specified box.
        /// </summary>
        /// <param name="box">The box number.</param>
        /// <returns>List of unsolved cells in the box.</returns>
        public List<UnsolvedCell> GetUnsolvedCellsInBox(int box)
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
        /// Returns a list of SOLVED cells in the specified box.
        /// </summary>
        /// <param name="box">The box number.</param>
        /// <returns>List of unsolved cells in the box.</returns>
        public List<SolvedCell> GetSolvedCellsInBox(int box)
        {
            List<SolvedCell> cells = new List<SolvedCell>();

            var (startRow, startCol) = CalculateCoordinateByBox(box);

            for (int i = 0; i < BoxLength; i++)
            {
                for (int j = 0; j < BoxLength; j++)
                {
                    if (_board[startRow + i, startCol + j] is SolvedCell tempCell)
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

            int cellWidth = 2; // Each cell is 2 characters wide
            int boxWidth = BoxLength * (cellWidth + 1); // Width of each box section
            int totalWidth = BoardLength * (cellWidth + 1) + (BoxLength - 1); // Full width including borders

            string topBorder = "┌" + string.Join("┬", Enumerable.Repeat(new string('─', boxWidth - (BoxLength-1)), BoxLength)) + "┐";
            string middleBorder = "├" + string.Join("┼", Enumerable.Repeat(new string('─', boxWidth - (BoxLength - 1)), BoxLength)) + "┤";
            string bottomBorder = "└" + string.Join("┴", Enumerable.Repeat(new string('─', boxWidth - (BoxLength - 1)), BoxLength)) + "┘";

            Console.WriteLine(topBorder);

            for (int row = 0; row < BoardLength; row++)
            {
                Console.Write("│"); // Left border

                for (int col = 0; col < BoardLength; col++)
                {
                    if (_board[row, col] is SolvedCell tempCell)
                    {
                        char displayChar = (char)(tempCell._value + '0'); 
                        Console.Write($" {displayChar}");
                    }
                    else
                    {
                        Console.Write(" ."); 
                    }

                    if ((col + 1) % BoxLength == 0) // Box separator
                    {
                        Console.Write(" │");
                    }
                }

                Console.WriteLine(); // End row

                if ((row + 1) % BoxLength == 0 && row + 1 < BoardLength)
                {
                    Console.WriteLine(middleBorder); // Middle separator between boxes
                }
            }

            Console.WriteLine(bottomBorder); // Bottom border
            Console.WriteLine();
        }

        /// <summary>
        /// Checks if the board is completely solved.
        /// </summary>
        /// <returns>True if the board is solved, otherwise false.</returns>
        public bool IsBoardSolved()
        {
            for(int i = 0; i < BoardLength; i++)
            {
                if (!IsValidSet(GetSolvedCellsInRow(i)))
                    return false;
                if (!IsValidSet(GetSolvedCellsInCol(i)))
                    return false;
                if (!IsValidSet(GetSolvedCellsInBox(i+1)))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Helper method to check if a set contains exactly {1, ..., BoardLength} options
        /// </summary>
        /// <param name="solvedCellGroup"></param>
        /// <returns>Ture if a set contains exactly {1, ..., BoardLength}, otherwise false</returns>
        private bool IsValidSet(List<SolvedCell> solvedCellGroup)
        {
            if(solvedCellGroup.Count != BoardLength)
            {
                return false;
            }

            var checkHashSet = new HashSet<int>();

            foreach (var cell in solvedCellGroup)
            {
                checkHashSet.Add(cell._value);
            }

            return checkHashSet.Count == BoardLength;
        }

    }
}
