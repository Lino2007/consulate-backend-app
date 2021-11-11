using NSI.Common.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Common.Exceptions
{
    public class NsiArgumentNullException : NsiBaseException
    {
        public NsiArgumentNullException(string message, Severity severity = Severity.Error)
            : base(message, severity)
        {
        }
        public NsiArgumentNullException(string message, Exception inner, Severity severity)
            : base(message, inner, severity)
        {
        }
    }
}
