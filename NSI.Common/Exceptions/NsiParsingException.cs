using NSI.Common.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Common.Exceptions
{
    public class NsiParsingException : NsiBaseException
    {
        public NsiParsingException(string message, Severity severity = Severity.Error)
            : base(message, severity)
        {
        }
        public NsiParsingException(string message, Exception inner, Severity severity)
            : base(message, inner, severity)
        {
        }
    }
}
