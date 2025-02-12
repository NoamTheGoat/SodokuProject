using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sodoku.IO;
using Sodoku;
using static Sodoku.GlobalConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodokuTests.BoardTests
{
    [TestClass]
    public class SmallBoardsTests
    {
        [TestMethod]
        public void FourOnFourEmptyBoardTest()
        {
            // ARRANGE
            string unsolvedBoard = "0000000000000000";
            string solvedBoard = "1234341221434321";
            UpdateConstants((int)Math.Sqrt(Math.Sqrt(unsolvedBoard.Length)));
            int[] unsolvedBoardAsArray = InputUtils.InputParser(unsolvedBoard);
            var solver = new SodokuSolver(unsolvedBoardAsArray);

            // ACT
            solver.SolveSodoku();
            string result = solver.ReturnBoardAsString();

            // ASSERT
            Assert.AreEqual(solvedBoard, result);
        }

        [TestMethod]
        public void FourOnFourBoardTest()
        {
            // ARRANGE
            string unsolvedBoard = "0002000000030000";
            string solvedBoard = "1432234141233214";
            UpdateConstants((int)Math.Sqrt(Math.Sqrt(unsolvedBoard.Length)));
            int[] unsolvedBoardAsArray = InputUtils.InputParser(unsolvedBoard);
            var solver = new SodokuSolver(unsolvedBoardAsArray);
            
            // ACT
            solver.SolveSodoku();
            string result = solver.ReturnBoardAsString();

            // ASSERT
            Assert.AreEqual(solvedBoard, result);
        }

        [TestMethod]
        public void FourOnFourUnsolvableBoardTest()
        {
            // ARRANGE
            string unsolvedBoard = "1000100010001000";
            UpdateConstants((int)Math.Sqrt(Math.Sqrt(unsolvedBoard.Length)));
            int[] unsolvedBoardAsArray = InputUtils.InputParser(unsolvedBoard);
            var solver = new SodokuSolver(unsolvedBoardAsArray);

            // ACT & ASSERT
            Assert.IsFalse(solver.SolveSodoku());
        }

        [TestMethod]
        public void OneOnOneBoardTest()
        {
            // ARRANGE
            string unsolvedBoard = "0";
            string solvedBoard = "1";
            UpdateConstants((int)Math.Sqrt(Math.Sqrt(unsolvedBoard.Length)));
            int[] unsolvedBoardAsArray = InputUtils.InputParser(unsolvedBoard);
            var solver = new SodokuSolver(unsolvedBoardAsArray);

            // ACT
            solver.SolveSodoku();
            string result = solver.ReturnBoardAsString();

            // ASSERT
            Assert.AreEqual(solvedBoard, result);
        }
    }
}
