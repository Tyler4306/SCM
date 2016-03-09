using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCM.Models
{
    public static class ModelsExtension
    {
        public static string FullAddress(this Customer customer)
        {
            return string.Format("{0} {1} {2}", customer.Address, (customer.RegionId.HasValue) ? customer.Region.Name : "", (customer.CityId.HasValue) ? customer.City.Name:""  ).Trim();
        }
        public static string Topic(this ServiceRequest request)
        {

            return string.Format("{0} {1} {2}", request.Id, request.RQN, request.ReceiptNo ).Trim();
        }
    }
}