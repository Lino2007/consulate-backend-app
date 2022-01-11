using NSI.Common.Enumerations;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NSI.Common.Exceptions
{
    [SerializableAttribute]
    public class NsiValidationException : NsiBaseException
    {
        public NsiValidationException(string message, Severity severity = Severity.Error)
            : base(message, severity)
        {
        }
        public NsiValidationException(string message, Exception inner, Severity severity)
            : base(message, inner, severity)
        {
        }
        protected NsiValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
