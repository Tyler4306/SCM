using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCM.Models
{
    public static class ModelsExtension
    {

        #region Customer Extenions
        public static string FullAddress(this Customer customer)
        {
            return string.Format("{2} {1} {0}", (customer.RegionId.HasValue) ? customer.Region.Name : "", customer.Address, (customer.CityId.HasValue) ? customer.City.Name : "").Trim();
        }
        #endregion


        #region Service Request Extensions
        public static string Topic(this ServiceRequest request)
        {

            return string.Format("{0} {1} {2}", request.Id, request.RQN, request.ReceiptNo).Trim();
        }

        public static int DelayedFor(this ServiceRequest request)
        {
            var result = Convert.ToInt32(DateTime.Today.AddDays(1).Subtract(request.RequestDate.AddDays(3)).TotalDays);
            if (request.StatusId >= 90 || result < 0)
                result = 0;

            return result;
        }

        public static bool IsDelayed(this ServiceRequest request)
        {
            var result = request.DelayedFor() > 0;
            return result;
        }
        #endregion
    }
}