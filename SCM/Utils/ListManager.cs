using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCM.Utils
{
    public class ListManager
    {
        private static IDictionary<int, string> _statuses = null;

        public static IDictionary<int, string> GetCities()
        {
            return DataManager.Cities().ToDictionary(x => x.Id, y => y.Name);
        }
        public static IDictionary<int, string> GetRegions(int? cityId = null)
        {
            return DataManager.Regions().Where(x => cityId == null || !cityId.HasValue || x.CityId == cityId.Value).OrderBy(x => x.Name).ToDictionary(x => x.Id, y => y.Name);
        }
        public static IDictionary<int, string> GetDepartments()
        {
            return DataManager.Departments().ToDictionary(x => x.Id, y => y.Name);
        }
        public static IDictionary<string, string> GetProducts(string id = null)
        {
            return DataManager.Products().Where(x => x.Id == id || x.IsActive ).ToDictionary(x => x.Id, y => y.Name);
        }
        public static IDictionary<int, string> GetEngineers(int? departmentId = null, int? id = null)
        {
            return DataManager.Engineers().Where(x => x.Id == id || (x.IsActive && (departmentId == null || !departmentId.HasValue || x.Department.Id == departmentId.Value) ) ).ToDictionary(x => x.Id, y => y.Name);
        }
        public static IDictionary<int, string> GetPendingReasons()
        {
            var ctx = new SCMContext();
            return ctx.PendingReasons.OrderBy(x => x.Reason).ToDictionary(x => x.Id, y => y.Reason);
        }
        public static IDictionary<int, string> GetCancelReasons()
        {
            var ctx = new SCMContext();
            return ctx.CancelReasons.OrderBy(x => x.Reason).ToDictionary(x => x.Id, y => y.Reason);
        }
        public static IDictionary<int, string> GetStatus()
        {
            if(_statuses == null)
            {
                _statuses = new Dictionary<int, string>()
                    {
                        { 10 , "New" },
                        { 20 ,"Pending" },
                        { 90 ,"Canceled" },
                        { 100 ,"Closed" }
                    };
            }

            return _statuses;
        }
    }
}