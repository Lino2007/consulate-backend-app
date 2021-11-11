using NSI.Common.Enumerations;
using NSI.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Logger.Interfaces
{
    public interface ILoggerAdapter
    {
        void LogException<T>(Exception ex, T request, Severity severity = Severity.Error);
        void LogException<T>(Exception ex, Severity severity = Severity.Error);
        void LogException<T>(NsiBaseException ex, T request);
        void LogException<T>(NsiBaseException ex);
        void LogDebug(string message);
    }
}
