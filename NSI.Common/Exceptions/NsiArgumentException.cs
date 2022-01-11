using NSI.Common.Enumerations;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NSI.Common.Exceptions
{
    [SerializableAttribute]
    public class NsiArgumentException : NsiBaseException
    {
        public NsiArgumentException(string message, Severity severity = Severity.Error)
            : base(message, severity)
        {
        }
        public NsiArgumentException(string message, Exception inner, Severity severity)
            : base(message, inner, severity)
        {
        }
        protected NsiArgumentException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}