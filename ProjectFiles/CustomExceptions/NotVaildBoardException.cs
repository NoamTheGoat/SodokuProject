using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sodoku
{
    /// <summary>
    /// Exception thrown when the Sudoku board is invalid.
    /// </summary>
    public class NotVaildBoardException : Exception
    {
        public NotVaildBoardException()
        { }
        public NotVaildBoardException(string message)
        : base(message)
        { }

        public NotVaildBoardException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
