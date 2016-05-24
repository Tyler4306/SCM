using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SCM.Models;
using Microsoft.Reporting.WebForms;
using System.Data.SqlClient;

namespace SCM
{
    public partial class ReportPage : System.Web.UI.Page
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
                    case "g":
                        LoadGeneralReport();
                        break;
                    default:
                        break;
                }
            }
        }

        public void LoadRequestsReport()
        {
            if(Session[User.Identity.Name + "_Rpt_Requests"] == null)
            {
                return;
            }

            List<int> Ids = (List<int>)Session[User.Identity.Name + "_Rpt_Requests"];
            string filterSort = (string)Session[User.Identity.Name + "_Rpt_Requests_SortBy"];
            string filterSortDir = (string)Session[User.Identity.Name + "_Rpt_Requests_SortDir"];

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
                row.Address = item.Customer.FullAddress();
                row.Product = string.IsNullOrEmpty(item.ProductId) ? "" : item.Product.Name;
                row.Remarks = (string.IsNullOrEmpty(item.Description) ? "" : item.Description) + "\r\n" + (string.IsNullOrEmpty(item.Remarks) ? "" : item.Remarks);
                ds.AddDSRequestRow(row);
            }

            reportViewer.LocalReport.ReportPath = Server.MapPath("~/Content/Reports/Requests.rdlc");
            reportViewer.LocalReport.DisplayName = string.Format("{0}", AppViews.ServiceRequests);
            reportViewer.LocalReport.DataSources.Clear();
            ReportDataSource rs = new ReportDataSource("DSRequests", ds.ToList());
            reportViewer.LocalReport.DataSources.Add(rs);
            reportViewer.LocalReport.Refresh();
        }

        public void LoadGeneralReport()
        {
            if (Session[User.Identity.Name + "ReportViewModel"] == null)                
                return;

            var ctx = new SCMContext();

            ReportViewModel rvm = (ReportViewModel)Session[User.Identity.Name + "ReportViewModel"];
            rvm.GetReportObject(rvm.Id);
            Report report = rvm.Report;
             
            DSGeneral.ReportParametersDataTable reportParams = new DSGeneral.ReportParametersDataTable();
            int parameterId = 1;
            bool parametersVisible = false;
            if(!parametersVisible)
            {
                parametersVisible = true;
                DSGeneral.ReportParametersRow para = reportParams.NewReportParametersRow();
                para.Id = parameterId;
                para.Name = "Report Date";
                para.Description = DateTime.Today.ToString("dd/MM/yyyy");
                reportParams.AddReportParametersRow(para);
                parameterId++;
            }

            if(report.FilterByCustomer)
            {
                parametersVisible = true;
                DSGeneral.ReportParametersRow para = reportParams.NewReportParametersRow();
                para.Id = parameterId;
                para.Name = "Customer";
                if (rvm.CustomerId.HasValue && rvm.CustomerId.Value > 0)
                {
                    para.Description = ctx.Customers.Find(rvm.CustomerId.Value).Name;
                    
                    report.Command = report.Command.Replace("$CustomerId", rvm.CustomerId.Value.ToString());
                }
                else
                {
                    para.Description = "All";
                    report.Command = report.Command.Replace("$CustomerId", "");
                }
                reportParams.AddReportParametersRow(para);
                parameterId++;
            }
            if (report.FilterByDate)
            {
                parametersVisible = true;
                DSGeneral.ReportParametersRow para = reportParams.NewReportParametersRow();
                para.Id = parameterId;
                para.Name = "From Date";
                para.Description = rvm.FromDate.ToString("dd/MM/yyyy");
                reportParams.AddReportParametersRow(para);
                parameterId++;
                report.Command = report.Command.Replace("$FromDate", string.Format("'{0}'", rvm.FromDate.ToString("yyyy-MM-dd")));


                DSGeneral.ReportParametersRow para1 = reportParams.NewReportParametersRow();
                para1.Id = parameterId;
                para1.Name = "To Date";
                para1.Description = rvm.ToDate.ToString("dd/MM/yyyy");
                reportParams.AddReportParametersRow(para1);
                parameterId++;
                report.Command = report.Command.Replace("$ToDate", string.Format("'{0}'", rvm.ToDate.ToString("yyyy-MM-dd")));
            }
            if (report.FilterByDepartment)
            {
                parametersVisible = true;
                DSGeneral.ReportParametersRow para = reportParams.NewReportParametersRow();
                para.Id = parameterId;
                para.Name = "Department";
                if (rvm.DepartmentId.HasValue && rvm.DepartmentId.Value > 0)
                {
                    para.Description = ctx.Departments.Find(rvm.DepartmentId.Value).Name;
                    report.Command = report.Command.Replace("$DepartmentId", rvm.DepartmentId.Value.ToString());
                }
                else
                {
                    para.Description = "All";
                    report.Command = report.Command.Replace("$DepartmentId","");
                }
                reportParams.AddReportParametersRow(para);
                parameterId++;
            }

            if (report.FilterByEngineer)
            {
                parametersVisible = true;
                DSGeneral.ReportParametersRow para = reportParams.NewReportParametersRow();
                para.Id = parameterId;
                para.Name = "Engineer";
                if (rvm.EngineerId.HasValue && rvm.EngineerId.Value > 0)
                {
                    para.Description = ctx.Engineers.Find(rvm.EngineerId.Value).Name;
                    report.Command = report.Command.Replace("$EngineerId", rvm.EngineerId.Value.ToString());

                }
                else
                {
                    para.Description = "All";
                    report.Command = report.Command.Replace("$EngineerId", "");
                }
                reportParams.AddReportParametersRow(para);
                parameterId++;
            }
            if (report.FilterByProduct)
            {
                parametersVisible = true;
                DSGeneral.ReportParametersRow para = reportParams.NewReportParametersRow();
                para.Id = parameterId;
                para.Name = "Product";
                if (!string.IsNullOrEmpty(rvm.ProductId))
                {
                    para.Description = ctx.Products.Find(rvm.ProductId).Name;
                    report.Command = report.Command.Replace("$ProductId", string.Format("{0}", rvm.ProductId));
                }
                else
                {
                    para.Description = "All";
                    report.Command = report.Command.Replace("$ProductId", "");
                }
                reportParams.AddReportParametersRow(para);
                parameterId++;
            }
            if (report.FilterByCity)
            {
                parametersVisible = true;
                DSGeneral.ReportParametersRow para = reportParams.NewReportParametersRow();
                para.Id = parameterId;
                para.Name = "City";
                if (rvm.CityId.HasValue && rvm.CityId.Value > 0)
                {
                    para.Description = ctx.Cities.Find(rvm.CityId.Value).Name;
                    report.Command = report.Command.Replace("$CityId", rvm.CityId.Value.ToString());
                }
                else
                {
                    para.Description = "All";
                    report.Command = report.Command.Replace("$CityId", "");
                }
                reportParams.AddReportParametersRow(para);
                parameterId++;
            }
            if (report.FilterByRegion)
            {
                parametersVisible = true;
                DSGeneral.ReportParametersRow para = reportParams.NewReportParametersRow();
                para.Id = parameterId;
                para.Name = "Region";
                if (rvm.RegionId.HasValue && rvm.RegionId.Value > 0)
                {
                    para.Description = ctx.Regions.Find(rvm.RegionId.Value).Name;
                    report.Command = report.Command.Replace("$RegionId", rvm.RegionId.Value.ToString());
                }
                else
                {
                    para.Description = "All";
                    report.Command = report.Command.Replace("$RegionId", "");
                }
                reportParams.AddReportParametersRow(para);
                parameterId++;
            }
            if (report.FilterByStatus)
            {
                parametersVisible = true;
                DSGeneral.ReportParametersRow para = reportParams.NewReportParametersRow();
                para.Id = parameterId;
                para.Name = "Status";
                para.Description = string.Format("{0}{1}{2}{3}", rvm.IsStatusActive ? "Open," : "", rvm.IsStatusPending ? "Pending," : "", rvm.IsStatusCancelled ? "Cancelled," : "", rvm.IsStatusClosed ? "Closed," : "");
                para.Description = para.Description.TrimEnd(',');
                para.Description = para.Description.Replace(",", ", ");
                reportParams.AddReportParametersRow(para);
                parameterId++;
                string statList = string.Format("0,{0}{1}{2}{3}", rvm.IsStatusActive ? "10," : "", rvm.IsStatusPending ? "20," : "", rvm.IsStatusCancelled ? "90," : "", rvm.IsStatusClosed ? "100," : "");
                statList = statList.TrimEnd(',');
                statList = statList.Replace(",", ", ");

                report.Command = report.Command.Replace("$Status", string.Format("({0})", statList));

            }
            if (report.FilterByTag)
            {
                parametersVisible = true;
                DSGeneral.ReportParametersRow para = reportParams.NewReportParametersRow();
                para.Id = parameterId;
                para.Name = "Tags";
                List<int> tags = new List<int>();
                tags.Add(0);
                string[] tagIds = (rvm.TagsFilter ?? "").Split(',');
                foreach(var t in tagIds)
                {
                    int tg = 0;
                    if (int.TryParse(t, out tg))
                        tags.Add(tg);
                }
                para.Description = "";
                if (ctx.Tags.Count() == tagIds.Count())
                {
                    para.Description = "All";
                }
                else
                {
                    var tagObjects = ctx.Tags.Where(x => tags.Contains(x.Id)).ToList();
                    foreach (var t in tagObjects)
                        para.Description += t.Name + ",";

                    para.Description = para.Description.TrimEnd(',');
                    para.Description = para.Description.Replace(",", ", ");
                }
                reportParams.AddReportParametersRow(para);
                parameterId++;

                if(tags.Count > 0)
                {
                    string tagsList = "";
                    foreach(var t in tags)
                    {
                        tagsList += t.ToString() + ",";
                    }
                    tagsList = tagsList.TrimEnd(',');
                    tagsList = tagsList.Replace(",", ", ");
                    report.Command = report.Command.Replace("$Tags", string.Format("({0})", tagsList));
                }
                else
                {
                    report.Command = report.Command.Replace("$Tags", "(0)");

                }
            }
            if (report.FilterByText1)
            {
                parametersVisible = true;
                DSGeneral.ReportParametersRow para = reportParams.NewReportParametersRow();
                para.Id = parameterId;
                para.Name = report.Text1Title??"Text1";
                para.Description = rvm.Text1;
                reportParams.AddReportParametersRow(para);
                parameterId++;
                report.Command = report.Command.Replace("$Text1", string.Format("{0}", rvm.Text1??""));

            }
            if (report.FilterByText2)
            {
                parametersVisible = true;
                DSGeneral.ReportParametersRow para = reportParams.NewReportParametersRow();
                para.Id = parameterId;
                para.Name = report.Text2Title ?? "Text2";
                para.Description = rvm.Text2;
                reportParams.AddReportParametersRow(para);
                parameterId++;
                report.Command = report.Command.Replace("$Text2", string.Format("{0}", rvm.Text2 ?? ""));
            }
            if (report.FilterByNumber1)
            {
                parametersVisible = true;
                DSGeneral.ReportParametersRow para = reportParams.NewReportParametersRow();
                para.Id = parameterId;
                para.Name = report.Number1Title ?? "Number1";
                para.Description = rvm.Number1.ToString();
                reportParams.AddReportParametersRow(para);
                parameterId++;
                report.Command = report.Command.Replace("$Number1", string.Format("{0}", rvm.Number1));
            }
            if (report.FilterByNumber2)
            {
                parametersVisible = true;
                DSGeneral.ReportParametersRow para = reportParams.NewReportParametersRow();
                para.Id = parameterId;
                para.Name = report.Number2Title ?? "Number2";
                para.Description = rvm.Number2.ToString();
                reportParams.AddReportParametersRow(para);
                parameterId++;
                report.Command = report.Command.Replace("$Number2", string.Format("{0}", rvm.Number2));
            }
            if (report.FilterByDate1)
            {
                parametersVisible = true;
                DSGeneral.ReportParametersRow para = reportParams.NewReportParametersRow();
                para.Id = parameterId;
                para.Name = report.Date1Title ?? "Date1";
                para.Description = rvm.Date1.HasValue ? rvm.Date1.Value.ToString("dd/MM/yyyy") : "";
                reportParams.AddReportParametersRow(para);
                parameterId++;
                if(rvm.Date1.HasValue)
                    report.Command = report.Command.Replace("$Date1", string.Format("'{0}'", rvm.Date1.Value.ToString("yyyy-MM-dd")));
                else
                    report.Command = report.Command.Replace("$Date1", string.Format("'{0}'", DateTime.Today.ToString("yyyy-MM-dd")));

            }
            if (report.FilterByDate2)
            {
                parametersVisible = true;
                DSGeneral.ReportParametersRow para = reportParams.NewReportParametersRow();
                para.Id = parameterId;
                para.Name = report.Date2Title ?? "Date2";
                para.Description = rvm.Date2.HasValue ? rvm.Date2.Value.ToString("dd/MM/yyyy") : "";
                reportParams.AddReportParametersRow(para);
                parameterId++;

                if (rvm.Date2.HasValue)
                    report.Command = report.Command.Replace("$Date2", string.Format("'{0}'", rvm.Date2.Value.ToString("yyyy-MM-dd")));
                else
                    report.Command = report.Command.Replace("$Date2", string.Format("'{0}'", DateTime.Today.ToString("yyyy-MM-dd")));
            }
            if (report.GVisible)
            {
                parametersVisible = true;
                DSGeneral.ReportParametersRow para = reportParams.NewReportParametersRow();
                para.Id = parameterId;
                para.Name = "Group By";
                para.Description = report.GHeader??"";
                reportParams.AddReportParametersRow(para);
                parameterId++;
            }



            DSGeneral.ReportBodyDataTable body = new DSGeneral.ReportBodyDataTable();
            DSGeneral.ReportHeaderDataTable header = new DSGeneral.ReportHeaderDataTable();

            string titles = "T1,T2,T3,T4,T5,T6,T7,T8,N1,N2,N3,N4,N5,D1,D2,D3,D4,D5";
            foreach(var s in titles.Split(','))
            {
                var h = header.NewReportHeaderRow();
                h.Code = s;
                if(report.F1Map == s)
                {
                    h.Header = report.F1Header;
                    h.Color = report.F1Color;
                    h.Visible = report.F1Visible.HasValue ? report.F1Visible.Value : false;
                }
                else if(report.F2Map == s)
                {
                    h.Header = report.F2Header;
                    h.Color = report.F2Color;
                    h.Visible = report.F2Visible.HasValue ? report.F2Visible.Value : false;
                }
                else if (report.F3Map == s)
                {
                    h.Header = report.F3Header;
                    h.Color = report.F3Color;
                    h.Visible = report.F3Visible.HasValue ? report.F3Visible.Value : false;
                }
                else if (report.F4Map == s)
                {
                    h.Header = report.F4Header;
                    h.Color = report.F4Color;
                    h.Visible = report.F4Visible.HasValue ? report.F4Visible.Value : false;
                }
                else if (report.F5Map == s)
                {
                    h.Header = report.F5Header;
                    h.Color = report.F5Color;
                    h.Visible = report.F5Visible.HasValue ? report.F5Visible.Value : false;
                }
                else if (report.F6Map == s)
                {
                    h.Header = report.F6Header;
                    h.Color = report.F6Color;
                    h.Visible = report.F6Visible.HasValue ? report.F6Visible.Value : false;
                }
                else if (report.F7Map == s)
                {
                    h.Header = report.F7Header;
                    h.Color = report.F7Color;
                    h.Visible = report.F7Visible.HasValue ? report.F7Visible.Value : false;
                }
                else if (report.F8Map == s)
                {
                    h.Header = report.F8Header;
                    h.Color = report.F8Color;
                    h.Visible = report.F8Visible.HasValue ? report.F8Visible.Value : false;
                }

                header.AddReportHeaderRow(h);
            }

            using (SqlConnection conn = new SqlConnection(ctx.Database.Connection.ConnectionString))
            {
                
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandTimeout = 60;
                cmd.CommandText = report.Command;
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.Default);
                while(reader.Read())
                {
                    DSGeneral.ReportBodyRow row = body.NewReportBodyRow();

                    for(int i = 0; i < reader.FieldCount; i++)
                    {
                        string colName = reader.GetName(i);
                        row[colName] = reader[colName];
                    }
                    body.AddReportBodyRow(row);
                }
                reader.NextResult();
                // da.Fill(body);
            }

            string reportFile = string.Format("~/Content/Reports/{0}.rdlc", report.TemplateName);
            reportViewer.LocalReport.ReportPath = Server.MapPath(reportFile);
            reportViewer.LocalReport.DataSources.Clear();
            ReportDataSource rsParameter = new ReportDataSource("ReportParameters", reportParams.ToList());
            reportViewer.LocalReport.DataSources.Add(rsParameter);
            ReportDataSource rsBody = new ReportDataSource("ReportBody", body.ToList());
            reportViewer.LocalReport.DataSources.Add(rsBody);
            ReportDataSource rsHeader = new ReportDataSource("ReportHeader", header.ToList());
            reportViewer.LocalReport.DataSources.Add(rsHeader);

            List<ReportParameter> p = new List<ReportParameter>();
            p.Add(new ReportParameter("ParametersVisible", parametersVisible ? "1" : "0" ));
            p.Add(new ReportParameter("Title", rvm.Title));
            p.Add(new ReportParameter("SubTitle", rvm.SubTitle));
            p.Add(new ReportParameter("GVisible", report.GVisible ? "1" : "0"));
            p.Add(new ReportParameter("GHeader", report.GHeader));

            reportViewer.LocalReport.SetParameters(p);
            reportViewer.LocalReport.Refresh();
            
        }
    }
}