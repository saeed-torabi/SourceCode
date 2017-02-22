using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Board.Web.Controllers
{
    public abstract class DefaultController : Controller
    {
        protected static readonly Logger logger = LogManager.GetCurrentClassLogger();

        protected ActionResult GetModelErrors()
        {
            var errors = WebHelper.GetErrorListFromModelState(ModelState);
            return Json(errors, JsonRequestBehavior.AllowGet);
        }

        protected void LogException(string message, Exception exception, string fileName = null)
        {
            var logEvent = new LogEventInfo(LogLevel.Error, "", string.Format("{0} {1}", message, exception.Message));
            logEvent.Exception = exception;

            if (Session["UserId"] != null)
            {
                logEvent.Properties["UserId"] = ((int?)Session["UserId"]).Value;
            }

            if (!string.IsNullOrWhiteSpace(fileName))
                logEvent.Properties["FileName"] = fileName;

            logger.Log(logEvent);

        }

        protected void LogInfo(string message, string fileName = null)
        {
            var logEvent = new LogEventInfo(LogLevel.Info, "", message);

            if (Session["UserId"] != null)
            {
                logEvent.Properties["UserId"] = ((int?)Session["UserId"]).Value;
            }
            if (!string.IsNullOrWhiteSpace(fileName))
                logEvent.Properties["FileName"] = fileName;

            logger.Log(logEvent);
        }

        protected ActionResult CatchException(Exception ex, string message)
        {
            LogException(message, ex);
            ModelState.AddModelError("", message);
            return GetModelErrors();
        }
    }

    [AllowAnonymous]
    public class ErrorsController : Controller
    {
        public ActionResult Error()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }
    }
}