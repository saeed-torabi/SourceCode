using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;

namespace Board.Web
{
    /// <summary>
    /// All application level unhandled exceptions are caught here before an error page is displayed.
    /// </summary>
    public class ExceptionPublisherExceptionFilter : IExceptionFilter
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public void OnException(ExceptionContext exceptionContext)
        {
            var exception = exceptionContext.Exception;
            var request = exceptionContext.HttpContext.Request;
            if (!(exception is HttpException))
            {
                var logEvent = new LogEventInfo(LogLevel.Error, "", exception.Message);
                logEvent.Exception = exception;
                var userId = exceptionContext.HttpContext.Session["UserId"];
                if (userId != null)
                {
                    logEvent.Properties["UserId"] = ((int?)userId).Value;
                }

                logger.Log(logEvent);
            }
        }
    }
}