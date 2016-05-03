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
using SCM.Utils;
using System.Data.Entity.Core.Objects;
using SCM.Models;

namespace SCM.Controllers
{
    public class ServiceRequestsController : Controller
    {
        private SCMContext db = new SCMContext();
        private static object locker = new Object();

        public ActionResult GetEngineers(int departmentId)
        {
            var list = DataManager.Engineers().Where(x => x.DepartmentId == departmentId && x.IsActive).OrderBy(x => x.Name)
                .Select(x => new { Id = x.Id, Name = x.Name }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        // GET: Requests
        public ActionResult Index(int? page)
        {
            var requests = DataManager.Requests();

            ViewBag.CityId = Utils.ListManager.GetCities();
            ViewBag.RegionId = Utils.ListManager.GetRegions();

            ViewBag.TotalAll = requests.Count();
            ViewBag.TotalActive = requests.Count(x => x.StatusId < 90);
            ViewBag.TotalPending = requests.Count(x => x.StatusId == 20);
            ViewBag.TotalDelayed = requests.Count(x => x.StatusId < 90 && x.RequestDate.AddDays(3) < DateTime.Today);
            int c = requests.Count(x => x.StatusId >= 90);
            ViewBag.TotalClosed = c;
            ViewBag.Status = "all";

            var dic = new Dictionary<string, int>();
            foreach (var tag in DataManager.Tags().Where(x => x.TagType == "C"))
            {
                var cc = requests.Select(x => x.Customer).Distinct().SelectMany(x => x.Tags).Where(x => x.Id == tag.Id);
                dic[tag.Name] = cc.Count();
            }
            foreach (var tag in DataManager.Tags().Where(x => x.TagType == "R"))
            {
                var cc = requests.SelectMany(x => x.Tags).Where(x => x.Id == tag.Id);
                if (dic.ContainsKey(tag.Name))
                {
                    dic[tag.Name] = dic[tag.Name] + cc.Count();
                }
                else
                {
                    dic[tag.Name] = cc.Count();
                }
            }
            ViewBag.RelatedTags = dic;
            // ViewBag.RelatedTags = requests.SelectMany(x => x.Tags.Union(x.Customer.Tags)).OrderBy(y => y.TagType).ThenBy(y => y.Name).GroupBy(x => x.Name).ToDictionary(x => x.Key, y => y.Count());
            Session[User.ToString() + "_Rpt_Requests"] = requests.Select(x => x.Id).ToList();
            Session[User.ToString() + "_Rpt_Requests_SortBy"] = "last_update";
            Session[User.ToString() + "_Rpt_Requests_SortDir"] = "desc";

            int pageSize = 30;
            int pageNumber = (page ?? 1);
            return View(requests.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Requests(int? page = null, string status = "all", string filterDuration = null, string customerName = null, string phone = null, string code = null, string receipt = null, string product = null,string engineer = null, string model = null, string sn = null, string tags = null, string filterDepartment = null, string filterSort = "last_update", string filterSortDir = "desc", int? city = null, int? region = null)
        {
            var requests = DataManager.Requests();

            ViewBag.CityId = Utils.ListManager.GetCities();
            ViewBag.RegionId = Utils.ListManager.GetRegions();

            var departments = new List<int>();
            if (!string.IsNullOrEmpty(filterDepartment))
            {
                departments.AddRange(filterDepartment.Split(',').Select(x => Convert.ToInt32(x)));
            }
            var filterTags = new List<int>();
            if (!string.IsNullOrEmpty(tags))
            {
                filterTags.AddRange(tags.Split(',').Select(x => Convert.ToInt32(x)));
            }

            requests = (from x in requests
                        where (string.IsNullOrEmpty(customerName) || x.Customer.Name.Contains(customerName))
                        && (string.IsNullOrEmpty(phone) || x.Customer.Phone == phone || x.Customer.Mobile == phone)
                        && (!city.HasValue || x.Customer.CityId == city.Value)
                        && (!region.HasValue || x.Customer.RegionId == region.Value)
                        && (string.IsNullOrEmpty(code) || x.RQN == code)
                        && (string.IsNullOrEmpty(receipt) || x.ReceiptNo == receipt)
                        && (string.IsNullOrEmpty(product) || (x.Product != null && x.Product.Name.Contains(product)))
                         && (string.IsNullOrEmpty(engineer) || (x.Engineer != null && x.Engineer.Name.Contains(engineer)))
                        && (string.IsNullOrEmpty(model) || x.Model == model)
                        && (string.IsNullOrEmpty(sn) || x.SN == sn)
                        && (string.IsNullOrEmpty(filterDepartment) || (x.DepartmentId.HasValue && departments.Contains(x.DepartmentId.Value)))
                        && (string.IsNullOrEmpty(tags) || x.Tags.Any(t => filterTags.Contains(t.Id)) || x.Customer.Tags.Any(t => filterTags.Contains(t.Id)))
                        select x);

            switch(filterDuration)
            {
                case "today":
                    requests = requests.Where(x => x.RequestDate.Year == DateTime.Today.Year && x.RequestDate.Month == DateTime.Today.Month && x.RequestDate.Day == DateTime.Today.Day);
                    break;
                case "week":
                    int span = 0;
                    DateTime startDate;
                    DateTime endDate;
                    switch(DateTime.Today.DayOfWeek)
                    {
                        case DayOfWeek.Sunday:
                            span = 0;
                            startDate = Convert.ToDateTime(DateTime.Today.ToString("dd/MMM/yyyy") + " 00:00:00");
                            endDate = Convert.ToDateTime(DateTime.Today.AddDays(7).ToString("dd/MMM/yyyy") + " 23:59:59");
                            requests = requests.Where(x => x.RequestDate >= startDate && x.RequestDate <= endDate).ToList();
                            break;
                        case DayOfWeek.Monday:
                            span = 1;
                            startDate = Convert.ToDateTime(DateTime.Today.AddDays(-span).ToString("dd/MMM/yyyy") + " 00:00:00");
                            endDate = Convert.ToDateTime(DateTime.Today.AddDays(7-span).ToString("dd/MMM/yyyy") + " 23:59:59");
                            requests = requests.Where(x => x.RequestDate >= startDate && x.RequestDate <= endDate).ToList();
                            break;
                        case DayOfWeek.Tuesday:
                            span = 2;
                            startDate = Convert.ToDateTime(DateTime.Today.AddDays(-span).ToString("dd/MMM/yyyy") + " 00:00:00");
                            endDate = Convert.ToDateTime(DateTime.Today.AddDays(7 - span).ToString("dd/MMM/yyyy") + " 23:59:59");
                            requests = requests.Where(x => x.RequestDate >= startDate && x.RequestDate <= endDate).ToList();
                            break;
                        case DayOfWeek.Wednesday:
                            span = 3;
                            startDate = Convert.ToDateTime(DateTime.Today.AddDays(-span).ToString("dd/MMM/yyyy") + " 00:00:00");
                            endDate = Convert.ToDateTime(DateTime.Today.AddDays(7 - span).ToString("dd/MMM/yyyy") + " 23:59:59");
                            requests = requests.Where(x => x.RequestDate >= startDate && x.RequestDate <= endDate).ToList();
                            break;
                        case DayOfWeek.Thursday:
                            span = 4;
                            startDate = Convert.ToDateTime(DateTime.Today.AddDays(-span).ToString("dd/MMM/yyyy") + " 00:00:00");
                            endDate = Convert.ToDateTime(DateTime.Today.AddDays(7 - span).ToString("dd/MMM/yyyy") + " 23:59:59");
                            requests = requests.Where(x => x.RequestDate >= startDate && x.RequestDate <= endDate).ToList();
                            break;
                        case DayOfWeek.Friday:
                            span = 5;
                            startDate = Convert.ToDateTime(DateTime.Today.AddDays(-span).ToString("dd/MMM/yyyy") + " 00:00:00");
                            endDate = Convert.ToDateTime(DateTime.Today.AddDays(7 - span).ToString("dd/MMM/yyyy") + " 23:59:59");
                            requests = requests.Where(x => x.RequestDate >= startDate && x.RequestDate <= endDate).ToList();
                            break;
                        case DayOfWeek.Saturday:
                            span = 6;
                            startDate = Convert.ToDateTime(DateTime.Today.AddDays(-span).ToString("dd/MMM/yyyy") + " 00:00:00");
                            endDate = Convert.ToDateTime(DateTime.Today.AddDays(7 - span).ToString("dd/MMM/yyyy") + " 23:59:59");
                            requests = requests.Where(x => x.RequestDate >= startDate && x.RequestDate <= endDate).ToList();
                            break;
                        default:
                            span = 0;
                            startDate = Convert.ToDateTime(DateTime.Today.ToString("dd/MMM/yyyy") + " 00:00:00");
                            endDate = Convert.ToDateTime(DateTime.Today.AddDays(7).ToString("dd/MMM/yyyy") + " 23:59:59");
                            requests = requests.Where(x => x.RequestDate >= startDate && x.RequestDate <= endDate).ToList();
                            break;
                    }
                    break;
                case "month":
                    requests = requests.Where(x => x.RequestDate.Year == DateTime.Today.Year && x.RequestDate.Month == DateTime.Today.Month );
                    break;
                default:
                    break;
            }

            ViewBag.TotalAll = requests.Count();
            ViewBag.TotalActive = requests.Count(x => x.StatusId < 90);
            ViewBag.TotalPending = requests.Count(x => x.StatusId == 20);
            ViewBag.TotalDelayed = requests.Count(x => x.StatusId < 90 && x.RequestDate.AddDays(3) < DateTime.Today);
            int c = requests.Count(x => x.StatusId >= 90);
            ViewBag.TotalClosed = c;

            switch (status)
            {
                case "all":
                    break;
                case "pending":
                    requests = requests.Where(x => x.StatusId == 20);
                    break;
                case "closed":
                    requests = requests.Where(x => x.StatusId >= 90);
                    break;
                case "active":
                    requests = requests.Where(x => x.StatusId < 90);
                    break;
                case "delayed":
                    requests = requests.Where(x => x.StatusId < 90 && x.RequestDate.AddDays(3) < DateTime.Today);
                    break;
                default:
                    break;
            }

            if(filterSort == "last_update")
            {
                if(filterSortDir == "desc")
                    requests = requests.OrderByDescending(x => x.UpdatedOn).ToList();
                else
                    requests = requests.OrderBy(x => x.UpdatedOn).ToList();
            }
            else if(filterSort == "request_date")
            {
                if (filterSortDir == "desc")
                    requests = requests.OrderByDescending(x => x.RequestDate).ToList();
                else
                    requests = requests.OrderBy(x => x.RequestDate).ToList();
            }
            else if (filterSort == "request_delay")
            {
                if (filterSortDir == "desc")
                    requests = requests.OrderByDescending(x => x.DelayedFor()).ToList();
                    
                else
                    requests = requests.OrderBy(x => x.DelayedFor()).ToList();
            }
            else
                requests = requests.OrderByDescending(x => x.UpdatedOn).ToList();


            ViewBag.Status = status;

            var dic = new Dictionary<string, int>();
            foreach (var tag in DataManager.Tags().Where(x => x.TagType == "C"))
            {
                var cc = requests.Select(x => x.Customer).Distinct().SelectMany(x => x.Tags).Where(x => x.Id == tag.Id);
                dic[tag.Name] = cc.Count();
            }
            foreach (var tag in DataManager.Tags().Where(x => x.TagType == "R"))
            {
                var cc = requests.SelectMany(x => x.Tags).Where(x => x.Id == tag.Id);
                if (dic.ContainsKey(tag.Name))
                {
                    dic[tag.Name] = dic[tag.Name] + cc.Count();
                }
                else
                {
                    dic[tag.Name] = cc.Count();
                }
            }
            ViewBag.RelatedTags = dic;
            // ViewBag.RelatedTags = requests.SelectMany(x => x.Tags.Union(x.Customer.Tags)).OrderBy(y => y.TagType).ThenBy(y => y.Name).GroupBy(x => x.Name).ToDictionary(x => x.Key, y => y.Count());
            Session[User.ToString() +  "_Rpt_Requests"] = requests.Select(x => x.Id).ToList();
            Session[User.ToString() + "_Rpt_Requests_SortBy"] = filterSort;
            Session[User.ToString() + "_Rpt_Requests_SortDir"] = filterSortDir;

            int pageSize = 30;
            int pageNumber = (page ?? 1);
            return PartialView("_Requests", requests.ToPagedList(pageNumber, pageSize));
        }

        [Authorize]
        public JsonResult TakeCall()
        {
            q_take_call_Result result = null;
            SecurityManager sm = new SecurityManager();
            string name = User.Identity.Name;
            string ext = sm.GetUserExt(name);

            lock(locker)
            {
                result = db.q_take_call(User.Identity.Name, ext).ToList().FirstOrDefault();

            }
            if (result == null)
            {
                return null;
            }            
            //var cust = db.Customers.Find(id);
            //var str = "[{" + string.Format("Id: {0}, Name: \"{1}\"", cust.Id, cust.Name) + "}]";
            var obj = new { Id = result.Id, CallerId = result.TNo};
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = db.ServiceRequests.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: AA/Create
        public ActionResult Create(int? customerId = null, int? qin = null, string phone = null)
        {
            ViewBag.CenterId = new SelectList(DataManager.Centers(), "Id", "Name");
            ViewBag.DepartmentId = new SelectList(DataManager.Departments(), "Id", "Name", 1);
            // get active products
            var products = DataManager.Products().Where(x => x.IsActive);
            ViewBag.ProductId = new SelectList(products, "Id", "Name");

            // get active engineers
            var engineers = DataManager.Engineers().Where(x => (x.DepartmentId == 1 && x.IsActive));
            ViewBag.EngineerId = new SelectList(engineers, "Id", "Name");


            var model = db.ServiceRequests.Create();
            model.RequestDate = DateTime.Now ;
            
            if(customerId.HasValue)
            {
                var customer = db.Customers.Find(customerId);
                if(customer != null)
                {
                    model.Customer = customer;
                    model.CustomerId = customerId.Value;
                }
                
            }
            else if(!string.IsNullOrEmpty(phone))
            {
                var customer = db.Customers.FirstOrDefault(x => x.Phone == phone.Trim() || x.Mobile == phone.Trim());
                if(customer != null)
                {
                    model.Customer = customer;
                    model.CustomerId = customer.Id;
                }
                else
                {
                    return RedirectToAction("Create", "Customers", new { phone = phone });
                }
            }
            else if (qin.HasValue)
            {
                var q = db.QINs.Find(qin);
                if (q != null)
                {
                    var qcustomer = db.Customers.FirstOrDefault(x => x.Phone == q.TNo || x.Mobile == q.TNo);
                    if (qcustomer != null)
                    {
                        model.Customer = qcustomer;
                        model.CustomerId = qcustomer.Id;
                    }
                    else
                    {
                        // First redirect to create customer
                        return RedirectToAction("Create", "Customers", new { phone = q.TNo });
                    }

                }
            }

            return View(model);
        }

        // POST: AA/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,CustomerId,RequestDate,StatusId,StatusDate,CenterId,RQN,ReceiptNo,DepartmentId,ProductId,Model,SN,EngineerId,Description,Remarks,ClosingDate,PendingReasonId,CancelReasonId,CreatedBy,CreatedOn,UpdatedBy,UpdatedOn,IsDeleted")] ServiceRequest model)
        {
            //Set default values
            model.CenterId = 1;
            model.StatusId = 10;
            if (model.RequestDate >= DateTime.Now)
                model.StatusDate = model.RequestDate;
            else
                model.StatusDate = DateTime.Now;

            if (string.IsNullOrEmpty(User.Identity.Name))
            {
                model.CreatedBy = "SYSTEM";
                model.UpdatedBy = "SYSTEM";
            }
            else
            {
                model.CreatedBy = User.Identity.Name;
                model.UpdatedBy = User.Identity.Name;
            }
            model.CreatedOn = DateTime.Now;
            model.UpdatedOn = DateTime.Now;
                       

            if (ModelState.IsValid)
            {

                try
                {
                    db.ServiceRequests.Add(model);
                    db.SaveChanges();
                    var exRequest = db.ExServiceRequests.Create();
                    exRequest.Id = model.Id;
                    db.ExServiceRequests.Add(exRequest);
                    db.SaveChanges();
                    DataManager.AddRequest(model.Id);
                }
                catch (Exception e)
                {

                    throw e;
                }

                return RedirectToAction("Edit", new { id = model.Id});
            }

            ViewBag.CenterId = new SelectList(DataManager.Centers(), "Id", "Name");
            ViewBag.DepartmentId = new SelectList(DataManager.Departments(), "Id", "Name");
            // get active products
            var products = DataManager.Products().Where(x => x.Id == model.ProductId || x.IsActive);
            ViewBag.ProductId = new SelectList(products, "Id", "Name", model.ProductId);

            // get active engineers
            var engineers = DataManager.Engineers().Where(x => x.Id == model.EngineerId || (x.DepartmentId == model.DepartmentId && x.IsActive));
            ViewBag.EngineerId = new SelectList(engineers, "Id", "Name", model.EngineerId);

            return View(model);
        }

        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ////var model = db.ServiceRequests.Include(x => x.Customer).Include(x => x.Center).Include(x => x.Department)
            ////    .Include(x => x.Product).Include(x => x.Engineer).Include(x => x.Customer.City)
            ////    .Include(x => x.Customer.Region).Include(x => x.PendingReason).Include(x => x.CancelReason)
            ////    .Include(x => x.Tags).Where(x => x.Id == id).First();

            var model = db.ServiceRequests.Find(id);
            
            ViewBag.CenterId = new SelectList(DataManager.Centers(), "Id", "Name", model.CenterId);
            ViewBag.DepartmentId = new SelectList(DataManager.Departments(), "Id", "Name", model.DepartmentId);

            // get active products
            var products = DataManager.Products().Where(x => x.Id == model.ProductId || x.IsActive);
            ViewBag.ProductId = new SelectList(products, "Id", "Name", model.ProductId);

            // get active engineers
            var engineers = DataManager.Engineers().Where(x => x.Id == model.EngineerId || (x.DepartmentId == model.DepartmentId && x.IsActive));
            ViewBag.EngineerId = new SelectList(engineers, "Id", "Name", model.EngineerId);

            ViewBag.PendingReasonId = new SelectList(db.PendingReasons, "Id", "Reason", model.PendingReasonId);
            ViewBag.CancelReasonId = new SelectList(db.CancelReasons, "Id", "Reason", model.CancelReasonId);
            ViewBag.StatusId = Utils.ListManager.GetStatus();

            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,CustomerId,RequestDate,StatusId,StatusDate,CenterId,RQN,ReceiptNo,DepartmentId,ProductId,Model,SN,EngineerId,Description,Remarks,ClosingDate,PendingReasonId,CancelReasonId,CreatedBy,CreatedOn,UpdatedBy,UpdatedOn,IsDeleted")] ServiceRequest serviceRequest)
        {

            ViewBag.CenterId = new SelectList(DataManager.Centers(), "Id", "Name", serviceRequest.CenterId);
            ViewBag.DepartmentId = new SelectList(DataManager.Departments(), "Id", "Name", serviceRequest.DepartmentId);

            // get active products
            var products = DataManager.Products().Where(x => x.Id == serviceRequest.ProductId ||x.IsActive);
            ViewBag.ProductId = new SelectList(products, "Id", "Name", serviceRequest.ProductId);

            // get active engineers
            var engineers = DataManager.Engineers().Where(x => x.Id == serviceRequest.EngineerId || (x.DepartmentId == serviceRequest.DepartmentId && x.IsActive));
            ViewBag.EngineerId = new SelectList(engineers, "Id", "Name", serviceRequest.EngineerId);

            ViewBag.PendingReasonId = new SelectList(db.PendingReasons, "Id", "Reason", serviceRequest.PendingReasonId);
            ViewBag.CancelReasonId = new SelectList(db.CancelReasons, "Id", "Reason", serviceRequest.CancelReasonId);
            ViewBag.StatusId = Utils.ListManager.GetStatus();

            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(User.Identity.Name))
                {
                    serviceRequest.UpdatedBy = "SYSTEM";
                }
                else
                {
                    serviceRequest.UpdatedBy = User.Identity.Name;
                }
                serviceRequest.UpdatedOn = DateTime.Now;

                if(Request["OldStatus"] != serviceRequest.StatusId.ToString())
                {
                    serviceRequest.StatusDate = DateTime.Now;
                    if (serviceRequest.StatusId >= 90)
                    {
                        serviceRequest.ClosingDate = DateTime.Now;
                    }
                }

                

                db.Entry(serviceRequest).State = EntityState.Modified;
                db.SaveChanges();
                ModelState.Clear();
                DataManager.ChangeRequest(serviceRequest.Id);
                return RedirectToAction("Index");
            }

            return View(serviceRequest);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = db.ServiceRequests.Find(id);
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
            var model = db.ServiceRequests.Find(id);
            db.ServiceRequests.Remove(model);
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

        public ActionResult ForceReload()
        {
            DataManager.ResetRequests();
            return RedirectToAction("Index");
        }

        public ActionResult ServiceRequestTags(int requestId)
        {
            var ctx = new SCMContext();
            var request = ctx.ServiceRequests.Find(requestId);
            ViewBag.EditTags = Utils.DataManager.Tags().Where(x => x.TagType == "R").ToList();
            return PartialView("RequestTags", request.Tags.Select(x => x.Id).ToList());
        }

        [HttpPost]
        [Authorize]
        public void ServiceRequestTags(int requestId, string tags)
        {
            var ctx = new SCMContext();
            var request = ctx.ServiceRequests.Find(requestId);
            List<int> tagList = tags.Split(',').Select(x => Convert.ToInt32(x)).ToList();
            List<int> requestTags = request.Tags.Select(x => x.Id).ToList();
            List<int> deletedTags = requestTags.Where(x => !tagList.Contains(x)).ToList();
            if (deletedTags.Count > 0)
            {
                foreach (int i in deletedTags)
                {
                    request.Tags.Remove(ctx.Tags.Find(i));
                    requestTags.Remove(i);
                }
                ctx.SaveChanges();
            }
            tagList.RemoveAll(x => requestTags.Contains(x));
            if (tagList.Count > 0)
            {
                foreach (int id in tagList)
                {
                    request.Tags.Add(ctx.Tags.Find(id));
                }
                ctx.SaveChanges();
            }
            Utils.DataManager.ChangeRequest(requestId);

        }
    }
}
