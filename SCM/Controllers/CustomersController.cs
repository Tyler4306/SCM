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

        public ActionResult FindCustomer(int? page, string search = null)
        {
            var list = Utils.DataManager.Customers().Where(x => 
            (string.IsNullOrEmpty(search) || 
            x.Name.Contains(search) || 
            (x.Phone == search) || 
            (x.Mobile == search))).ToList();
            int pageNumber = page ?? 1;
            int pageSize = 5;
            return PartialView("_FindCustomer", list.ToPagedList(pageNumber, pageSize));
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
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,Name,Phone,Mobile,CityId,RegionId,Address,IsBlackListed,Comments")] Customer model)
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

        [Authorize]
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
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,Name,Phone,Mobile,CityId,RegionId,Address,IsBlackListed,Comments")] Customer model)
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

        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int? id)
        {
            var model = db.Customers.Find(id);
            db.Customers.Remove(model);
            db.SaveChanges();
            Utils.DataManager.ResetCustomers();
            return RedirectToAction("Index");
        }

        public ActionResult CustomerTags(int customerId)
        {
            var ctx = new SCMContext();
            var customer = ctx.Customers.Find(customerId);
            ViewBag.EditTags = Utils.DataManager.Tags().Where(x => x.TagType == "C").ToList();
            return PartialView("CustomerTags", customer.Tags.Select(x => x.Id).ToList());
        }

        [HttpPost]
        [Authorize]
        public void CustomerTags(int customerId, string tags)
        {
            var ctx = new SCMContext();
            var customer = ctx.Customers.Find(customerId);
            List<int> tagList = tags.Split(',').Select(x => Convert.ToInt32(x)).ToList();
            List<int> customerTags = customer.Tags.Select(x => x.Id).ToList();
            List<int> deletedTags = customerTags.Where(x => !tagList.Contains(x)).ToList();
            if (deletedTags.Count > 0)
            {
                foreach (int i in deletedTags)
                {
                    customer.Tags.Remove(ctx.Tags.Find(i));
                    customerTags.Remove(i);
                }
                ctx.SaveChanges();
            }
            tagList.RemoveAll(x => customerTags.Contains(x));
            if (tagList.Count > 0)
            {
                foreach (int id in tagList)
                {
                    customer.Tags.Add(ctx.Tags.Find(id));
                }
                ctx.SaveChanges();
            }
            Utils.DataManager.ChangeCustomer(customerId);

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
