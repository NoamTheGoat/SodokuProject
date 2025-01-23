﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sodoku.CustomExceptions
{
    internal class NotVaildInputException : Exception
    {
        public NotVaildInputException()
        { }
        public NotVaildInputException(string message)
        : base(message)
        { }

        public NotVaildInputException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
