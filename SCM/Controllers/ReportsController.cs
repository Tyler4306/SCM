using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCM.Controllers
{ 

    public class ReportsController : Controller
    {
        // GET: Reports
        [ChildActionOnly]
        public ActionResult Index()
        {
            var scm = new SCMContext();
            return PartialView("Index", scm.Reports.ToList());
        }

        [Authorize]
        public ActionResult GetReport(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index", "ServiceRequests");

            var model = new ReportViewModel(id.Value);

            ViewBag.DepartmentId = Utils.ListManager.GetDepartments();
            ViewBag.EngineerId = Utils.ListManager.GetEngineers();
            ViewBag.ProductId = Utils.ListManager.GetProducts();
            ViewBag.CityId = Utils.ListManager.GetCities();
            ViewBag.RegionId = Utils.ListManager.GetRegions();
            ViewBag.Tags = Utils.DataManager.Tags();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult GetReport(ReportViewModel model)
        {
            // [Bind(Include ="Id,Title,SubTitle,FromDate,ToDate,CustomerId,DepartmentId,EngineerId,ProductId,IsStatusActive,IsStatusPending,IsStatusCancelled,IsStatusClosed,CityId,RegionId,TagsFilter,Text1,Text2,Number1,Number2,Date1,Date2")]

            Session[User.Identity.Name + "ReportViewModel"] = model;
            
            return Redirect("~/ReportPage.aspx?r=g");
        }
    }
}