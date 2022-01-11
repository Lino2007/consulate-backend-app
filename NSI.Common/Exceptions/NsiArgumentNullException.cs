using NSI.Common.Enumerations;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NSI.Common.Exceptions
{
    [SerializableAttribute]
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
        protected NsiArgumentNullException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
