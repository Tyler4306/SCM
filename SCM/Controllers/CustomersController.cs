using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SCM;
using PagedList;
using System.Linq.Dynamic;

namespace SCM.Controllers
{
    public class CustomersController : Controller
    {
        private SCMContext db = new SCMContext();

        public ActionResult GetRegions(int cityId)
        {
            var list = Utils.DataManager.Regions().Where(x => x.CityId == cityId).OrderBy(x => x.Name)
                .Select(x => new { Id = x.Id, Name = x.Name }).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index(int? page = null)
        {
            var model = Utils.DataManager.Customers();
            int pageNumber = page ?? 1;
            int pageSize = 10;
            return View(model.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult List(int? page = null, string name = null, string sortBy = "Name", string direction = "ASC")
        {
            var model = Utils.DataManager.Customers().Where(x => string.IsNullOrEmpty(name) || x.Name.Contains(name) || x.Phone == name || x.Mobile == name || (x.CityId.HasValue && x.City.Name.Contains(name)) || (x.RegionId.HasValue && x.Region.Name.Contains(name))); 
            // .OrderBy(sortBy + " " + direction).ToList();
            switch(sortBy)
            {
                case "City.Name":
                    if(direction == "DESC")
                    {
                        model = model.OrderByDescending(x => (x.CityId.HasValue ? x.City.Name : string.Empty));
                    }
                    else
                    {
                        model = model.OrderBy(x => (x.CityId.HasValue ? x.City.Name : string.Empty));
                    }
                    break;
                case "Region.Name":
                    if (direction == "DESC")
                    {
                        model = model.OrderByDescending(x => (x.RegionId.HasValue ? x.Region.Name : string.Empty));
                    }
                    else
                    {
                        model = model.OrderBy(x => (x.RegionId.HasValue ? x.Region.Name : string.Empty));
                    }
                    break;
                default:
                    if (direction == "DESC")
                    {
                        model = model.OrderByDescending(x => x.Name);
                    }
                    else
                    {
                        model = model.OrderBy(x => x.Name);
                    }
                    break;

            }
            model = model.ToList();
            int pageNumber = page ?? 1;
            int pageSize = 10;
            return PartialView(model.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult FindCustomer(int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 5;
            return PartialView("_FindCustomer", Utils.DataManager.Customers().ToPagedList(pageNumber, pageSize));
        }

        public JsonResult GetCustomer(int? id)
        {
            var cust = db.Customers.Find(id);
            var str = "[{" + string.Format("Id: {0}, Name: \"{1}\"", cust.Id, cust.Name) + "}]";
            var obj = new { Id = cust.Id, Name = cust.Name };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = db.Customers.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        public ActionResult Create(string phone = null)
        {
            ViewBag.CityId = Utils.ListManager.GetCities();
            ViewBag.RegionId = Utils.ListManager.GetRegions();
            if(!string.IsNullOrEmpty(phone))
            {
                var model = db.Customers.Create();
                model.Phone = phone;
                TempData["ForwardToRequests"] = true;
                return View(model);
            }
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Phone,Mobile,CityId,RegionId,Address,IsBlackListed")] Customer model)
        {
            ViewBag.CityId = Utils.ListManager.GetCities();
            ViewBag.RegionId = Utils.ListManager.GetRegions();

            if (ModelState.IsValid)
            {
                db.Customers.Add(model);
                db.SaveChanges();
                Utils.DataManager.AddCustomer(model.Id);
                if (TempData["ForwardToRequests"] != null && ((bool)TempData["ForwardToRequests"]) == true)
                {
                    return RedirectToAction("Create", "ServiceRequests", new { customerId = model.Id });
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = db.Customers.Find(id);
            ViewBag.CityId = Utils.ListManager.GetCities();
            ViewBag.RegionId = Utils.ListManager.GetRegions(model.CityId);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Phone,Mobile,CityId,RegionId,Address,IsBlackListed")] Customer model)
        {
            
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                Utils.DataManager.ChangeCustomer(model.Id);
                return RedirectToAction("Index");
            }
            ViewBag.CityId = Utils.ListManager.GetCities();
            ViewBag.RegionId = Utils.ListManager.GetRegions(model.CityId);
            return View(model);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = db.Customers.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            var model = db.Customers.Find(id);
            db.Customers.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
