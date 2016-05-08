using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCM.Models
{
    public class ReportViewModel
    {
        public ReportViewModel()
        {
            FromDate = DateTime.Today;
            ToDate = DateTime.Today;
        }
        public ReportViewModel(int id)
        {
            GetReportObject(id);
        }

        public void GetReportObject(int id)
        {
            var ctx = new SCMContext();
            var report = ctx.Reports.Find(id);
            Report = report;
            if(string.IsNullOrEmpty(Title))
                Title = Report.Title;
            if(string.IsNullOrEmpty(SubTitle))
                SubTitle = Report.SubTitle;
            Id = Report.Id;
        }

        public Report Report { get; set; }
        public int Id { get; set; }

        [Display(Name = "Report_Title", ResourceType = typeof(AppViews))]
        public string Title { get; set; }
        [Display(Name = "Report_SubTitle", ResourceType = typeof(AppViews))]
        public string SubTitle { get; set; }
        [Display(Name = "Report_FromDate", ResourceType = typeof(AppViews))]
        public DateTime FromDate { get; set; }
        [Display(Name = "Report_ToDate", ResourceType = typeof(AppViews))]
        public DateTime ToDate { get; set; }
        [Display(Name = "Customer", ResourceType = typeof(AppViews))]
        public int? CustomerId { get; set; }
        [Display(Name = "Department", ResourceType = typeof(AppViews))]
        public int? DepartmentId { get; set; }
        [Display(Name = "Engineer", ResourceType = typeof(AppViews))]
        public int? EngineerId { get; set; }
        [Display(Name = "Product", ResourceType = typeof(AppViews))]
        public string ProductId { get; set; }
        public bool IsStatusActive { get; set; }
        public bool IsStatusPending { get; set; }
        public bool IsStatusCancelled { get; set; }
        public bool IsStatusClosed { get; set; }
        [Display(Name = "City", ResourceType = typeof(AppViews))]
        public int? CityId { get; set;}
        [Display(Name = "Region", ResourceType = typeof(AppViews))]
        public int? RegionId { get; set; }
        [Display(Name = "Tags", ResourceType = typeof(AppViews))]
        public string TagsFilter { get; set; }
        public string Text1 { get; set; }
        public string Text2 { get; set; }
        public int? Number1 { get; set; }
        public int? Number2 { get; set; }
        public DateTime? Date1 { get; set; }
        public DateTime? Date2 { get; set; }
    }
}