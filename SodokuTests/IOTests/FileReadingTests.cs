using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Sodoku.IO.FileHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security;

namespace SodokuTests.IOTests
{
    [TestClass]
    public class FileReadingTests
    {
        [TestMethod]
        public void EmptyPathFileTest()
        {
            // ARRANGE
            string filePath = "";

            // ACT & ASSERT
            Assert.ThrowsException<ArgumentException>(() => ReadBoardFromFile(out string board, filePath));
        }

        [TestMethod]
        public void InvalidFilePathTest()
        {
            // ARRANGE
            string filePath = "c:\\Very\\Real\\File\\Path\\";

            // ACT & ASSERT
            Assert.ThrowsException<DirectoryNotFoundException>(() => ReadBoardFromFile(out string board, filePath));
        }

        [TestMethod]
        public void FileNotExistTest()
        {
            // ARRANGE
            string filePath = "c:\\Very\\Real\\File.txt";

            // ACT & ASSERT
            Assert.ThrowsException<DirectoryNotFoundException>(() => ReadBoardFromFile(out string board, filePath));
        }

        [TestMethod]
        public void UnauthorizedAccessToFileTest()
        {
            // ARRANGE
            string filePath = "c:\\";

            // ACT & ASSERT
            Assert.ThrowsException<UnauthorizedAccessException>(() => ReadBoardFromFile(out string board, filePath));
        }

        [TestMethod]
        public void FilePathTooLongTest()
        {
            // ARRANGE
            string filePath = "c:\\OmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmegaOmega";

            // ACT & ASSERT
            Assert.ThrowsException<PathTooLongException>(() => ReadBoardFromFile(out string board, filePath));
        }

        [TestMethod]
        public void InvalidCharactersInFilePathTest()
        {
            // ARRANGE
            string filePath = "c:\\Invalid!|\\Characters.,txt";
            // ACT & ASSERT
            Assert.ThrowsException<ArgumentException>(() => ReadBoardFromFile(out string board, filePath));
        }
    }
}
