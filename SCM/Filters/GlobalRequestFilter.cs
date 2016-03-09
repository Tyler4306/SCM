using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCM.Filters
{
    public class GlobalRequestFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var context = filterContext.HttpContext;

            if (context.Request.UserLanguages != null && context.Request.UserLanguages.Length > 0)
            {
                string culture = context.Request.UserLanguages[0];
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(culture);
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(culture);
            }

            base.OnActionExecuting(filterContext);
        }
    }
}