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

        public ActionResult Index(int? page = null)
        {
            var model = Utils.DataManager.Customers();
            int pageNumber = page ?? 1;
            int pageSize = 10;
            return View(model.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult List(int? page = null, string name = null, string sortBy = "Name", string direction = "ASC")
        {
            var model = Utils.DataManager.Customers().Where(x => string.IsNullOrEmpty(name) || x.Name.Contains(name) || x.Phone == name || x.Mobile == name || x.City.Name.Contains(name) || x.Region.Name.Contains(name)).OrderBy(sortBy + " " + direction).ToList();
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

        public ActionResult Create()
        {
            ViewBag.CityId = Utils.ListManager.GetCities();
            ViewBag.RegionId = Utils.ListManager.GetRegions();
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
                return RedirectToAction("Index");
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
            ViewBag.RegionId = Utils.ListManager.GetRegions();
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
            ViewBag.CityId = Utils.ListManager.GetCities();
            ViewBag.RegionId = Utils.ListManager.GetRegions();
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
