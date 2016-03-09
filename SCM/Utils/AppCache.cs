using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCM.Utils
{
    public static class AppCache
    {
        

        public static T GetCached<T>(string name) where T : class
        {
            if (HttpContext.Current != null && HttpContext.Current.Cache[name] != null)
            {
                return HttpContext.Current.Cache[name] as T;
            }
            else
                return null;
        }

        public static void SetCached(string name, object value)
        {
            HttpContext.Current.Cache[name] = value;
        }
    }
}