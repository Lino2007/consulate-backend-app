using NLog;
using NSI.Common.Enumerations;
using NSI.Common.Exceptions;
using NSI.Logger.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Logger.Implementations
{
    public class NLogAdapter : ILoggerAdapter
    {
        private static readonly NLog.Logger _logger = LogManager.GetCurrentClassLogger();

        public void LogException<T>(Exception ex, T request, Severity severity = Severity.Error)
        {
            _logger.Log(ConvertToNLogLevel(severity), ex, ex.Message, null);
        }

        public void LogException<T>(Exception ex, Severity severity = Severity.Error)
        {
            _logger.Log(ConvertToNLogLevel(severity), ex, ex.Message, null);
        }

        public void LogException<T>(NsiBaseException ex, T request)
        {
            _logger.Log(ConvertToNLogLevel(ex.Severity), ex, ex.Message, request);
        }

        public void LogException<T>(NsiBaseException ex)
        {
            _logger.Log(ConvertToNLogLevel(ex.Severity), ex, ex.Message, null);
        }

        /// <summary>
        /// Wrapper around NLog. Use this to log debug level messages. 
        /// </summary>
        /// <param name="message">Message to log</param>
        public void LogDebug(String message)
        {
            _logger.Debug(message);
        }

        private LogLevel ConvertToNLogLevel(Severity severity)
        {
            switch (severity)
            {
                case Severity.Info:
                    return LogLevel.Info;
                case Severity.Fatal:
                    return LogLevel.Fatal;
                case Severity.Warning:
                    return LogLevel.Warn;
                case Severity.Debug:
                    return LogLevel.Debug;
                default:
                    return LogLevel.Error;
            }
        }
    }
}
