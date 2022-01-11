using NSI.Common.Enumerations;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NSI.Common.Exceptions
{
    [SerializableAttribute]
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
        protected NsiArgumentOutOfRangeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
