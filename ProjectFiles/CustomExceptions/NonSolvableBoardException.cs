using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sodoku
{
    /// <summary>
    /// Exception thrown when the Sudoku board cannot be solved.
    /// </summary>
    public class NonSolvableBoardException : Exception
    {
        public NonSolvableBoardException()
        { }
        public NonSolvableBoardException(string message)
        : base(message)
        { }

        public NonSolvableBoardException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
