using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MvcSandbox
{
    public class MobileRedirectActionFilter : Attribute, IActionFilter
    {
        public string Controller { get; set; }
        public string Action { get; set; }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Headers.Keys.Contains("x-mobile"))
            {
                context.Result = new RedirectToActionResult(Action, Controller, null);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Debug.WriteLine("Action Executed");
        }
    }
}
