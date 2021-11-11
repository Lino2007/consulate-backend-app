using NSI.Common.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Common.Exceptions
{
    public class NsiInvalidRequestException : NsiBaseException
    {
        public NsiInvalidRequestException(string message, Severity severity = Severity.Error)
            : base(message, severity)
        {
        }
        public NsiInvalidRequestException(string message, Exception inner, Severity severity)
            : base(message, inner, severity)
        {
        }
    }
}
