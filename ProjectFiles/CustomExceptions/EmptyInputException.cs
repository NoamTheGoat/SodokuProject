﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sodoku.CustomExceptions
{
    /// <summary>
    /// Exception thrown when the input is empty.
    /// </summary>
    public class EmptyInputException : Exception
    {
        public EmptyInputException()
        { }
        public EmptyInputException(string message)
        : base(message)
        { }

        public EmptyInputException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
