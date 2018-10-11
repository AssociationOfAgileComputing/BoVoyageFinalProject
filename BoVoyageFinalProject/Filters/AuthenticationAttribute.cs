using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoVoyageFinalProject.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class AuthenticationAttribute : ActionFilterAttribute
    {
        public string Type { get; set; } = "BO";

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Type == "BO")
            {
                if (filterContext.HttpContext.Session["ADMINISTRATOR"] == null)
                {
                    filterContext.Result = new RedirectResult(@"\BackOffice\authentication\Login");
                }
            }

            if (Type == "CUSTOMER")
            {
                if (filterContext.HttpContext.Session["CUSTOMER"] == null)
                {
                    filterContext.Result = new RedirectResult(@"\customers\login");
                }
            }
        }
    }
}