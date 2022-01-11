using NSI.Common.Enumerations;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NSI.Common.Exceptions
{
    [SerializableAttribute]
    public class NsiNotAuthorizedException : NsiBaseException
    {
        public NsiNotAuthorizedException(string message, Severity severity = Severity.Error)
            : base(message, severity)
        {
        }
        public NsiNotAuthorizedException(string message, Exception inner, Severity severity)
            : base(message, inner, severity)
        {
        }
        protected NsiNotAuthorizedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
