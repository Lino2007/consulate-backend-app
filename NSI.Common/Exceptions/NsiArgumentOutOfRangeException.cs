using NSI.Common.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Common.Exceptions
{
    public class NsiArgumentOutOfRangeException : NsiBaseException
    {
        public NsiArgumentOutOfRangeException(string message, Severity severity = Severity.Error)
            : base(message, severity)
        {
        }
        public NsiArgumentOutOfRangeException(string message, Exception inner, Severity severity)
            : base(message, inner, severity)
        {
        }
    }
}
