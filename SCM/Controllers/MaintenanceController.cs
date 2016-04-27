using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace SCM.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MaintenanceController : Controller
    {
        // GET: Maintenance
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CustomerMerge()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CustomerMerge([Bind(Include = "CustomerId1,CustomerId2")] CustomerMerge model)
        {
            if(ModelState.IsValid)
            {
                SCMContext ctx = new SCMContext();
                var customer1 = ctx.Customers.Find(model.CustomerId1);
                var customer2 = ctx.Customers.Find(model.CustomerId2);
                
                if(string.IsNullOrEmpty(customer2.Phone))
                {
                    customer2.Phone = customer1.Phone;
                }
                if (string.IsNullOrEmpty(customer2.Mobile))
                {
                    customer2.Mobile = customer1.Mobile;
                }
                if (!customer2.CityId.HasValue)
                {
                    customer2.CityId = customer1.CityId;
                }
                if (!customer2.RegionId.HasValue)
                {
                    customer2.RegionId = customer1.RegionId;
                }
                if (string.IsNullOrEmpty(customer2.Address))
                {
                    customer2.Address = customer1.Address;
                }
                if (string.IsNullOrEmpty(customer2.Comments))
                {
                    customer2.Comments = customer1.Comments;
                }

                var requests = customer1.ServiceRequests.ToList();
                foreach(var r in requests)
                {
                    r.CustomerId = customer2.Id;
                }

                var tags = customer1.Tags.ToList();
                foreach(var t in tags)
                {
                    if (!t.Customers.Contains(customer2))
                        t.Customers.Add(customer2);
                }
                ctx.SaveChanges();

                Utils.DataManager.ResetCustomers();
                Utils.DataManager.ResetRequests();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}