using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sodoku.CustomExceptions;
using static Sodoku.IO.InputUtils;
using static Sodoku.GlobalConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sodoku.ProjectFiles.CustomExceptions;
using Sodoku;
using Sodoku.IO;

namespace SodokuTests.IOTests
{
    [TestClass]
    public class ValidationTests
    {
        [TestMethod]
        public void EmptyInputTest()
        {
            // ARRANGE
            string board = "";
            UpdateConstants((int)Math.Sqrt(Math.Sqrt(board.Length)));

            // ACT & ASSERT
            Assert.ThrowsException<EmptyInputException>(() => IsValidInput(board));
        }

        [TestMethod]
        public void InvalidBoardLengthTest()
        {
            // ARRANGE
            string board = "123456789101111213141516171819";
            UpdateConstants((int)Math.Sqrt(Math.Sqrt(board.Length)));

            // ACT & ASSERT
            Assert.ThrowsException<InvalidBoardLengthException>(() => IsValidInput(board));
        }

        /// <summary>
        /// In a 4x4 board the max value can be 4
        /// </summary>
        [TestMethod]
        public void InvalidCharactersInSodokuBoardTest()
        {
            // ARRANGE
            string board = "0080000A00000009";
            UpdateConstants((int)Math.Sqrt(Math.Sqrt(board.Length)));

            // ACT & ASSERT
            Assert.ThrowsException<NotVaildBoardException>(() => InputParser(board));
        }

        /// <summary>
        /// Entering a wrong solved board
        /// </summary>
        [TestMethod]
        public void WrongSolvedBoardTest()
        {
            // ARRANGE
            string unsolvedBoard = "123456789123456789123456789123456789123456789123456789123456789123456789123456789";
            UpdateConstants((int)Math.Sqrt(Math.Sqrt(unsolvedBoard.Length)));
            int[] unsolvedBoardAsArray = InputUtils.InputParser(unsolvedBoard);
            var solver = new SodokuSolver(unsolvedBoardAsArray);

            // ACT & ASSERT
            Assert.IsFalse(solver.SolveSodoku());
        }
    }
}
