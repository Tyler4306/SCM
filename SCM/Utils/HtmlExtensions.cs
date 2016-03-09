using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SCM
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString MyDropDown(string name, object data, object value)
        {
            StringBuilder sb = new StringBuilder(512);
            int? Id = (int?)value;
            if (data != null)
            {
                sb.AppendFormat("<select name='{0}' id='{0}' class = 'form-control'>", name);

                if (!Id.HasValue || Id.Value == 0)
                        sb.AppendFormat("<option value=''>{0}</option>", AppViews.SelectValue);

                foreach (var x in (data as IDictionary<int, string>))
                {
                    if (x.Key == Id)
                        sb.AppendFormat("<option selected='selected' value='{0}'>{1}</option>", x.Key, x.Value);
                    else
                        sb.AppendFormat("<option value='{0}'>{1}</option>", x.Key, x.Value);
                }
                sb.AppendFormat("</select>");
            }
            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString MyDropDownStr(string name, object data, object value)
        {
            StringBuilder sb = new StringBuilder(512);
            string Id = (string) value;
            if (data != null)
            {
                sb.AppendFormat("<select name='{0}' id='{0}' class = 'form-control'>", name);

                if (string.IsNullOrEmpty(Id))
                    sb.AppendFormat("<option value=''>{0}</option>", AppViews.SelectValue);

                foreach (var x in (data as IDictionary<string, string>))
                {
                    if (x.Key == Id)
                        sb.AppendFormat("<option selected='selected' value='{0}'>{1}</option>", x.Key, x.Value);
                    else
                        sb.AppendFormat("<option value='{0}'>{1}</option>", x.Key, x.Value);
                }
                sb.AppendFormat("</select>");
            }
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}