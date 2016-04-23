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
    public class ExServiceRequestsController : Controller
    {
        private SCMContext db = new SCMContext();

        // GET: ExServiceRequests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExServiceRequest exServiceRequest = db.ExServiceRequests.Find(id);
            if (exServiceRequest == null)
            {
                return HttpNotFound();
            }
            return View(exServiceRequest);
        }

        // GET: ExServiceRequests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExServiceRequest exServiceRequest = db.ExServiceRequests.Find(id);
            if (exServiceRequest == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.ServiceRequests, "Id", "RQN", exServiceRequest.Id);
            return View(exServiceRequest);
        }

        // POST: ExServiceRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,B2BFlag,CompletionDate,InputDate,PullingDate,Dealer,DealerName,DealerReceiptNo,ASCRemarks,SchComplaintDate,SchComplaintCount,SchComplaintRemarks,ASCClaimNo,EsnImeiNo,OutModel,ReceiptDate,TransferSendDate,TransferReceiptDate,FirstPromiseDate,Schedule,PromiseDate,Schedule1,DelayFromPromiseDate,DelayFromReceiptDate,TransferApprovalDate")] ExServiceRequest exServiceRequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(exServiceRequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", "ServiceRequests", new { id = exServiceRequest.Id });
            }
            ViewBag.Id = new SelectList(db.ServiceRequests, "Id", "RQN", exServiceRequest.Id);
            return View(exServiceRequest);
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
