using NSI.Common.Enumerations;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NSI.Common.Exceptions
{
    [SerializableAttribute]
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
        protected NsiParsingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
