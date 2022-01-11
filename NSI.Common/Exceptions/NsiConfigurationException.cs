using NSI.Common.Enumerations;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NSI.Common.Exceptions
{
    [SerializableAttribute]
    public class NsiConfigurationException : NsiBaseException
    {
        public NsiConfigurationException(string message, Severity severity = Severity.Error)
            : base(message, severity)
        {
        }
        public NsiConfigurationException(string message, Exception inner, Severity severity)
            : base(message, inner, severity)
        {
        }
        protected NsiConfigurationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
