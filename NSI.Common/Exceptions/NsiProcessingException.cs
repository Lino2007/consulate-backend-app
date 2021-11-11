using NSI.Common.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Common.Exceptions
{
    public class NsiProcessingException : NsiBaseException
    {
        public NsiProcessingException(string message, Severity severity = Severity.Error)
            : base(message, severity)
        {
        }
        public NsiProcessingException(string message, Exception inner, Severity severity)
            : base(message, inner, severity)
        {
        }
    }
}
