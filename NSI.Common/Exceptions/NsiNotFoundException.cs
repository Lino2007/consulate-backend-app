using NSI.Common.Enumerations;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NSI.Common.Exceptions
{
    [SerializableAttribute]
    public class NsiNotFoundException : NsiBaseException
    {
        public NsiNotFoundException(string message, Severity severity = Severity.Error)
            : base(message, severity)
        {
        }
        public NsiNotFoundException(string message, Exception inner, Severity severity)
            : base(message, inner, severity)
        {
        }
        protected NsiNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
