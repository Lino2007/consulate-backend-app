using Microsoft.AspNetCore.Http;
using NSI.Common.DataContracts.Base;
using NSI.Common.DataContracts.Enumerations;
using NSI.Common.Enumerations;
using NSI.Common.Exceptions;
using NSI.Common.Resources;
using NSI.Common.Serialization;
using NSI.Logger.Interfaces;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NSI.REST.Middlewares
{
    [ExcludeFromCodeCoverage]
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context, ILoggerAdapter logger)
        {
            try
            {
                logger.LogDebug(await FormatRequest(context.Request));
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, logger);
            }
        }

        #region << Private Methods >>

        /// <summary>
        /// Returns exception message.
        /// </summary>
        /// <returns>Exception message</returns>
        private static async Task HandleExceptionAsync(HttpContext context, Exception ex, ILoggerAdapter logger)
        {
            var exceptionType = ex.GetType();
            var severity = (ex as NsiBaseException)?.Severity ?? Severity.Fatal;
            logger.LogException(ex, context.Request, severity);
            context.Response.ContentType = "application/json";

            switch (exceptionType)
            {
                case Type _ when exceptionType == typeof(NsiValidationException):
                case Type _ when exceptionType == typeof(NsiInvalidRequestException):
                case Type _ when exceptionType == typeof(NsiArgumentNullException):
                case Type _ when exceptionType == typeof(NsiArgumentException):
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    await context.Response.WriteAsync(CreateResponseOnException(ex.Message, severity));
                    break;
                case Type _ when exceptionType == typeof(NsiNotFoundException):
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    await context.Response.WriteAsync(CreateResponseOnException(ex.Message, severity));
                    break;
                case Type _ when exceptionType == typeof(NsiNotAuthorizedException):
                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    await context.Response.WriteAsync(CreateResponseOnException(ex.Message, severity));
                    break;
                case Type _ when exceptionType == typeof(NsiProcessingException):
                    context.Response.StatusCode = (int)HttpStatusCode.NoContent;
                    await context.Response.WriteAsync(CreateResponseOnException(ex.Message, severity));
                    break;
                case Type _ when exceptionType == typeof(NsiBaseException):
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    await context.Response.WriteAsync(CreateResponseOnException(ex.Message, severity));
                    break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    await context.Response.WriteAsync(CreateResponseOnException(ExceptionMessages.AnErrorOccured, severity));
                    break;
            }
        }

        /// <summary>
        ///  Creates appropriate response for exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="severity"></param>
        /// <returns>string</returns>

        private static string CreateResponseOnException(string message, Severity severity)
        {
            return JsonHelper.Serialize(new BaseResponse<string>
            {
                Success = ResponseStatus.Failed,
                Error = new Error
                {
                    Severity = severity,
                    Message = message,
                }
            });
        }

        /// <summary>
        /// Formats request as string
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Request string</returns>
        private async Task<string> FormatRequest(HttpRequest request)
        {
            var bodyStr = "";
            var req = request;

            // Allows using stream several times in ASP.Net Core
            req.EnableBuffering();

            using (StreamReader reader = new StreamReader(req.Body, Encoding.UTF8, true, 1024, true))
            {
                bodyStr = await reader.ReadToEndAsync();
            }

            req.Body.Position = 0;

            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyStr}";
        }

        #endregion
    }
}
