using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SCM.Models;
using Microsoft.Reporting.WebForms;

namespace SCM
{
    public partial class Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                switch(Request.QueryString["r"])
                {
                    case "cached":
                        LoadRequestsReport();
                        break;
                    default:
                        break;
                }
            }
        }

        public void LoadRequestsReport()
        {
            if(Session[User.ToString() + "_Rpt_Requests"] == null)
            {
                return;
            }

            List<int> Ids = (List<int>)Session[User.ToString() + "_Rpt_Requests"];
            string filterSort = (string)Session[User.ToString() + "_Rpt_Requests_SortBy"];
            string filterSortDir = (string)Session[User.ToString() + "_Rpt_Requests_SortDir"];

            var requests = Utils.DataManager.Requests().Where(x => Ids.Contains(x.Id));
            if (filterSort == "last_update")
            {
                if (filterSortDir == "desc")
                    requests = requests.OrderByDescending(x => x.UpdatedOn).ToList();
                else
                    requests = requests.OrderBy(x => x.UpdatedOn).ToList();
            }
            else if (filterSort == "request_date")
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

            
            DSRequests.DSRequestDataTable ds = new DSRequests.DSRequestDataTable();
            // List<DSRequests.DSRequestRow> ds = new List<DSRequests.DSRequestRow>();
            DSRequests d = new DSRequests();
            
            foreach(var item in requests)
            {
                DSRequests.DSRequestRow row = ds.NewDSRequestRow();
                row.Id = item.Id;            
                row.Customer = item.Customer.Name;
                row.Phone = item.Customer.Phone ?? "";
                row.Mobile = item.Customer.Mobile ?? "";
                row.Engineer = item.EngineerId.HasValue ? item.Engineer.Name : "";
                row.IsDelayed = item.IsDelayed();
                row.Status = Utils.ListManager.GetStatus()[item.StatusId];

                ds.AddDSRequestRow(row);
            }

            reportViewer.LocalReport.ReportPath = Server.MapPath("~/Reports/Requests.rdlc");
            reportViewer.LocalReport.DisplayName = string.Format("{0}", AppViews.ServiceRequests);
            reportViewer.LocalReport.DataSources.Clear();
            ReportDataSource rs = new ReportDataSource("DSRequests", ds.ToList());
            reportViewer.LocalReport.DataSources.Add(rs);
            reportViewer.LocalReport.Refresh();
        }
    }
}