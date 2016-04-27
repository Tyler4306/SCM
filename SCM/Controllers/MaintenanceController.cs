using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using SCM.Utils;

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

        public ActionResult RequestsDepartmentModifier()
        {
            ViewBag.DepartmentId1 = new SelectList(DataManager.Departments(), "Id", "Name", 1);
            ViewBag.DepartmentId2 = new SelectList(DataManager.Departments(), "Id", "Name", 1);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RequestsDepartmentModifier([Bind(Include = "DepartmentId1,DepartmentId2")] RequestsDepartmentModifier model)
        {
           if(ModelState.IsValid)
            {
                SCMContext ctx = new SCMContext();
                var department1 = ctx.Departments.Find(model.DepartmentId1);
                var department2 = ctx.Departments.Find(model.DepartmentId2);

                var products = department1.Products.ToList();
                foreach(var p in products)
                {
                    p.DepartmentId = department2.Id;
                }

                var engineers = department1.Engineers.ToList();
                foreach (var e in engineers)
                {
                    e.DepartmentId = department2.Id;
                }

                var requests = department1.ServiceRequests.ToList();
                foreach (var r in requests)
                {
                    r.DepartmentId = department2.Id;
                }

                ctx.SaveChanges();
                Utils.DataManager.ResetDepartments();
                Utils.DataManager.ResetProducts();
                Utils.DataManager.ResetEngineers();
                Utils.DataManager.ResetRequests();

            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult RequestsProductModifier()
        {
            ViewBag.DepartmentId = new SelectList(DataManager.Departments(), "Id", "Name", 1);
            ViewBag.ProductId1 = new SelectList(DataManager.Products().Where(x => x.DepartmentId == 1), "Id", "Name", 1);
            ViewBag.ProductId2 = new SelectList(DataManager.Products().Where(x => x.DepartmentId == 1), "Id", "Name", 1);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RequestsProductModifier([Bind(Include = "DepartmentId,ProductId1,ProductId2")] RequestsProductModifier model)
        {
            if (ModelState.IsValid)
            {
                SCMContext ctx = new SCMContext();
                var department = ctx.Departments.Find(model.DepartmentId);
                var product1 = ctx.Products.Find(model.ProductId1);
                var product2 = ctx.Products.Find(model.ProductId2);

                var requests = product1.ServiceRequests.ToList();
                foreach (var r in requests)
                {
                    r.ProductId = product2.Id;
                }

                ctx.SaveChanges();
                Utils.DataManager.ResetRequests();
            }
            return RedirectToAction("Index", "Home");
        }

    }
}