using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCM.Utils
{
    public class PageUtils
    {
        public static bool IsArabic()
        {
            bool result = false;
            if (HttpContext.Current.Request.UserLanguages[0].Contains("ar"))
                result = true;
            return result;
        }

        public static string GetDirection()
        {
            if (IsArabic())
                return "rtl";
            else
                return "ltr";
        }

        public static string GetFloatDirection()
        {
            if (IsArabic())
                return "right";
            else
                return "left";
        }

        public static string GetPullDirection()
        {
            if (IsArabic())
                return "pull-right";
            else
                return "";
        }

        public static string GetLoginPullDirection()
        {
            if (IsArabic())
                return "navbar-left";
            else
                return "navbar-right";
        }
    }
}