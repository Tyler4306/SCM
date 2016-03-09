using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SCM;

namespace SCM.Controllers
{
    public class AAController : Controller
    {
        private SCMContext db = new SCMContext();

        // GET: AA
        public ActionResult Index()
        {
            var serviceRequests = Utils.DataManager.Requests();
            return View(serviceRequests.ToList());
        }

        // GET: AA/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceRequest serviceRequest = db.ServiceRequests.Find(id);
            if (serviceRequest == null)
            {
                return HttpNotFound();
            }
            return View(serviceRequest);
        }

        // GET: AA/Create
        public ActionResult Create()
        {
            ViewBag.CancelReasonId = new SelectList(db.CancelReasons, "Id", "Reason");
            ViewBag.CenterId = new SelectList(db.Centers, "Id", "Name");
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name");
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name");
            ViewBag.EngineerId = new SelectList(db.Engineers, "Id", "Name");
            ViewBag.PendingReasonId = new SelectList(db.PendingReasons, "Id", "Reason");
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");
            return View();
        }

        // POST: AA/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CustomerId,RequestDate,StatusId,StatusDate,CenterId,RQN,ReceiptNo,DepartmentId,ProductId,Model,SN,EngineerId,Description,Remarks,ClosingDate,PendingReasonId,CancelReasonId,CreatedBy,CreatedOn,UpdatedBy,UpdatedOn,IsDeleted")] ServiceRequest serviceRequest)
        {
            if (ModelState.IsValid)
            {
                db.ServiceRequests.Add(serviceRequest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CancelReasonId = new SelectList(db.CancelReasons, "Id", "Reason", serviceRequest.CancelReasonId);
            ViewBag.CenterId = new SelectList(db.Centers, "Id", "Name", serviceRequest.CenterId);
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", serviceRequest.CustomerId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", serviceRequest.DepartmentId);
            ViewBag.EngineerId = new SelectList(db.Engineers, "Id", "Name", serviceRequest.EngineerId);
            ViewBag.PendingReasonId = new SelectList(db.PendingReasons, "Id", "Reason", serviceRequest.PendingReasonId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", serviceRequest.ProductId);
            return View(serviceRequest);
        }

        // GET: AA/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceRequest serviceRequest = db.ServiceRequests.Find(id);
            if (serviceRequest == null)
            {
                return HttpNotFound();
            }
            ViewBag.CancelReasonId = new SelectList(db.CancelReasons, "Id", "Reason", serviceRequest.CancelReasonId);
            ViewBag.CenterId = new SelectList(db.Centers, "Id", "Name", serviceRequest.CenterId);
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", serviceRequest.CustomerId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", serviceRequest.DepartmentId);
            ViewBag.EngineerId = new SelectList(db.Engineers, "Id", "Name", serviceRequest.EngineerId);
            ViewBag.PendingReasonId = new SelectList(db.PendingReasons, "Id", "Reason", serviceRequest.PendingReasonId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", serviceRequest.ProductId);
            return View(serviceRequest);
        }

        // POST: AA/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CustomerId,RequestDate,StatusId,StatusDate,CenterId,RQN,ReceiptNo,DepartmentId,ProductId,Model,SN,EngineerId,Description,Remarks,ClosingDate,PendingReasonId,CancelReasonId,CreatedBy,CreatedOn,UpdatedBy,UpdatedOn,IsDeleted")] ServiceRequest serviceRequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serviceRequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CancelReasonId = new SelectList(db.CancelReasons, "Id", "Reason", serviceRequest.CancelReasonId);
            ViewBag.CenterId = new SelectList(db.Centers, "Id", "Name", serviceRequest.CenterId);
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", serviceRequest.CustomerId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", serviceRequest.DepartmentId);
            ViewBag.EngineerId = new SelectList(db.Engineers, "Id", "Name", serviceRequest.EngineerId);
            ViewBag.PendingReasonId = new SelectList(db.PendingReasons, "Id", "Reason", serviceRequest.PendingReasonId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", serviceRequest.ProductId);
            return View(serviceRequest);
        }

        // GET: AA/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceRequest serviceRequest = db.ServiceRequests.Find(id);
            if (serviceRequest == null)
            {
                return HttpNotFound();
            }
            return View(serviceRequest);
        }

        // POST: AA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiceRequest serviceRequest = db.ServiceRequests.Find(id);
            db.ServiceRequests.Remove(serviceRequest);
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
