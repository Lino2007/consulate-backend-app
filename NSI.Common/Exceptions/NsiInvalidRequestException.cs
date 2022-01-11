using NSI.Common.Enumerations;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NSI.Common.Exceptions
{
    [SerializableAttribute]
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
        protected NsiInvalidRequestException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
