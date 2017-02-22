using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Board.Web
{
    public static class WebHelper
    {
        public static List<string> GetErrorListFromModelState(ModelStateDictionary modelState)
        {
            var query = from state in modelState.Values
                        from error in state.Errors
                        where !string.IsNullOrWhiteSpace(error.ErrorMessage)
                        select error.ErrorMessage;

            var errorList = query.ToList();
            return errorList;
        }

        public static string GetUserIpAddress()
        {
            return HttpContext.Current.Request.Params["HTTP_CLIENT_IP"] ?? HttpContext.Current.Request.UserHostAddress;

            //string strIPAddress = null;
            //var context = new HttpContextWrapper(HttpContext.Current);
            //HttpRequestBase request = context.Request;
            //if (request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            //{
            //    strIPAddress = request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(',').FirstOrDefault();
            //}
            //else
            //{
            //    strIPAddress = request.ServerVariables["REMOTE_ADDR"] ?? "NULL";
            //}
            //return strIPAddress;
        }
    }
}