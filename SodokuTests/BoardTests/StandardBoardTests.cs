using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sodoku;
using Sodoku.IO;
using static Sodoku.GlobalConstants;
using Sodoku.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodokuTests.BoardTests
{
    /// <summary>
    /// Tests on 9x9 sodoku boards
    /// </summary>
    [TestClass]
    public class StandardBoardTests
    {
        [TestMethod]
        public void EasyBoard1Test()
        {
            // ARRANGE
            string unsolvedBoard = "000000015020060000000000408003000900000100000000008000150400000000070300800000060";
            string solvedBoard = "379284615428561739561793428213657984786149253945328176157436892692875341834912567";
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
        public void HardBoard1Test()
        {
            // ARRANGE
            string unsolvedBoard = "300090002020104000000300700603500080870000014010007605002001000000905020900030006";
            string solvedBoard = "381796542726154398594328761643519287875263914219847635432671859167985423958432176";
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
        public void HardBoard2Test()
        {
            // ARRANGE
            string unsolvedBoard = "700040006000759000900801003000524000030000020400000007501000208080000010209000304";
            string solvedBoard = "715243986863759142924861753697524831138697425452318697541936278386472519279185364"; UpdateConstants((int)Math.Sqrt(Math.Sqrt(unsolvedBoard.Length)));
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
        public void UnsovlableBoard1Test()
        {
            // ARRANGE
            string unsolvedBoard = "000030000060000400007050800000406000000900000050010300400000020000300000000000000";
            UpdateConstants((int)Math.Sqrt(Math.Sqrt(unsolvedBoard.Length)));
            int[] unsolvedBoardAsArray = InputUtils.InputParser(unsolvedBoard);
            var solver = new SodokuSolver(unsolvedBoardAsArray);

            // ACT & ASSERT
            Assert.IsFalse(solver.SolveSodoku());
        }

        [TestMethod]
        public void UnsovlableBoard2Test()
        {
            // ARRANGE
            string unsolvedBoard = "000005080000601043000000000010500000000106000300000005530000061000000004000000000";
            UpdateConstants((int)Math.Sqrt(Math.Sqrt(unsolvedBoard.Length)));
            int[] unsolvedBoardAsArray = InputUtils.InputParser(unsolvedBoard);
            var solver = new SodokuSolver(unsolvedBoardAsArray);

            // ACT & ASSERT
            Assert.IsFalse(solver.SolveSodoku());
        }

        [TestMethod]
        public void UnsovlableBoard3Test()
        {
            // ARRANGE
            string unsolvedBoard = "100006080000700000090050000000560030300000000000003801500001060000020400802005010";
            UpdateConstants((int)Math.Sqrt(Math.Sqrt(unsolvedBoard.Length)));
            int[] unsolvedBoardAsArray = InputUtils.InputParser(unsolvedBoard);
            var solver = new SodokuSolver(unsolvedBoardAsArray);

            // ACT & ASSERT
            Assert.IsFalse(solver.SolveSodoku());
        }

        [TestMethod]
        public void EmptyBoard()
        {
            // ARRANGE
            string unsolvedBoard = "000000000000000000000000000000000000000000000000000000000000000000000000000000000";
            string solvedBoard = "123456789456789123789123456231674895875912364694538217317265948542897631968341572";
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
