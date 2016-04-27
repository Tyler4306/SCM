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
    public class ProductsController : Controller
    {
        private SCMContext db = new SCMContext();

        public ActionResult Index(int? page = null)
        {
            var model = db.Products.Include(x => x.Department).ToList();
            int pageNumber = page ?? 1;
            int pageSize = 10;
            return View(model.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult List(int? page = null, string name = null, string sortBy = "Name", string direction = "ASC")
        {
            var model = db.Products.Include(x => x.Department).Where(x => string.IsNullOrEmpty(name) || x.Id.Contains(name) || x.Name.Contains(name) || x.Department.Name.Contains(name)).OrderBy(sortBy + " " + direction).ToList();
            int pageNumber = page ?? 1;
            int pageSize = 10;
            return PartialView(model.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = db.Products.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.DepartmentId = Utils.ListManager.GetDepartments();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,Name,DepartmentId,IsActive")] Product model)
        {
            ViewBag.DepartmentId = Utils.ListManager.GetDepartments();

            if (ModelState.IsValid)
            {
                db.Products.Add(model);
                db.SaveChanges();
                Utils.DataManager.ResetProducts();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = db.Products.Find(id);
            ViewBag.DepartmentId = Utils.ListManager.GetDepartments();
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,Name,DepartmentId,IsActive")] Product model)
        {
            ViewBag.DepartmentId = Utils.ListManager.GetDepartments();
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                Utils.DataManager.ResetProducts();

                return RedirectToAction("Index");
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = db.Products.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(string id)
        {
            var model = db.Products.Find(id);
            db.Products.Remove(model);
            db.SaveChanges();
            Utils.DataManager.ResetProducts();
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
