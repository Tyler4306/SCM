using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Transactions;

namespace SCM.Utils
{
    public class ImportManager
    {
        public int Progress { get; set; }
        public string Phase { get; set; }
        int total = 0;
        int counter = 1;

        public void Import(IEnumerable<ServiceRequestRecord> data)
        {
            int i = 1;
            Progress = 0;


            foreach(var item in data)
            {
                item.SysID = i++;
            }
            
            using (SCMContext ctx = new SCMContext())
            {
                using (var tx = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        Phase = AppViews.DataImport_Phase1;
                        ImportProducts(ctx, ref data);
                        Phase = AppViews.DataImport_Phase2;
                        ImportCities(ctx, ref data);
                        Phase = AppViews.DataImport_Phase3;
                        ImportCancelReasons(ctx, ref data);
                        Phase = AppViews.DataImport_Phase4;
                        ImportPendingReasons(ctx, ref data);
                        Phase = AppViews.DataImport_Phase5;
                        ImportEngineers(ctx, ref data);
                        Phase = AppViews.DataImport_Phase6;
                        ImportCustomers(ctx, ref data);
                        Phase = AppViews.DataImport_Phase7;
                        ImportRequests(ctx, ref data);
                        Phase = "done";
                        tx.Commit();
                    }
                    catch(Exception ex)
                    {
                        tx.Rollback();
                        //throw ex;
                    }
                }
            }
        }

        private void ImportProducts(SCMContext ctx, ref IEnumerable<ServiceRequestRecord> data)
        {
            Progress = 0;
            counter = 1;
            total = data.Count();

            var curList = ctx.Products.Select(x => x.Id).ToList();
            var dic = new Dictionary<string, Product>();
            var objects = data.Select(x => new { Id = x.Service_Product_Code, Name = x.SVC_Product, IsActive = true }).Distinct().Where(x => !string.IsNullOrEmpty(x.Id)).ToList();
            foreach(var item in objects.Where(x => !curList.Contains(x.Id)))
            {
                var rec = new Product() { Id = item.Id, Name = item.Name, IsActive = item.IsActive };
                ctx.Products.Add(rec);
                ctx.SaveChanges();
                if(!dic.ContainsKey(rec.Id))
                {
                    dic.Add(rec.Id, rec);
                }
                counter++;
                if (total > 0 && counter <= total)
                {
                    Progress = Convert.ToInt32(counter * 100 / (double)total);
                }
            }
            foreach (var item in ctx.Products)
            {
                if(!dic.ContainsKey(item.Id))
                {
                    dic.Add(item.Id, item);
                }
            }
            foreach(var item in data)
            {
                item.ProductId = item.Service_Product_Code;
            }
        }

        private void ImportCities(SCMContext ctx, ref IEnumerable<ServiceRequestRecord> data)
        {
            Progress = 0;
            counter = 1;
            total = data.Count();

            var curList = ctx.Cities.Select(x => x.Name.Trim()).ToList();
            var dic = new Dictionary<string, City>();
            var objects = data.Select(x => new { Name = x.City_Name.Trim() }).Distinct().Where(x => !string.IsNullOrEmpty(x.Name)).ToList();
            foreach (var item in objects.Where(x => !curList.Contains(x.Name)))
            {
                var rec = new City() { Name = item.Name };
                ctx.Cities.Add(rec);
                ctx.SaveChanges();
                if (!dic.ContainsKey(rec.Name))
                {
                    dic.Add(rec.Name, rec);
                }
                counter++;
                if (total > 0 && counter <= total)
                {
                    Progress = Convert.ToInt32(counter * 100 / (double)total);
                }
            }
            foreach (var item in ctx.Cities)
            {
                if (!dic.ContainsKey(item.Name))
                {
                    dic.Add(item.Name, item);
                }
            }
            foreach (var item in data)
            {
                if (!string.IsNullOrEmpty(item.City_Name))
                {
                    item.CityId = dic[item.City_Name].Id;
                }
                else
                {
                    item.CityId = null;
                }
            }
        }

        private void ImportPendingReasons(SCMContext ctx, ref IEnumerable<ServiceRequestRecord> data)
        {
            Progress = 0;
            counter = 1;
            total = data.Count();
            var curList = ctx.PendingReasons.Select(x => x.Reason.Trim()).ToList();
            var dic = new Dictionary<string, PendingReason>();
            var objects = data.Select(x => new { Reason = x.Pending_Reason.Trim() }).Distinct().Where(x => !string.IsNullOrEmpty(x.Reason)).ToList();
            foreach (var item in objects.Where(x => !curList.Contains(x.Reason)))
            {
                var rec = new PendingReason() { Reason = item.Reason };
                ctx.PendingReasons.Add(rec);
                ctx.SaveChanges();
                if (!dic.ContainsKey(rec.Reason))
                {
                    dic.Add(rec.Reason, rec);
                }
                counter++;
                if (total > 0 && counter <= total)
                {
                    Progress = Convert.ToInt32(counter * 100 / (double)total);
                }
            }
            foreach (var item in ctx.PendingReasons)
            {
                if (!dic.ContainsKey(item.Reason))
                {
                    dic.Add(item.Reason, item);
                }
            }
            foreach (var item in data)
            {
                if (!string.IsNullOrEmpty(item.Pending_Reason))
                {
                    item.PendingReasonId = dic[item.Pending_Reason].Id;
                }
                else
                {
                    item.PendingReasonId = null;
                }
            }
        }

        private void ImportCancelReasons(SCMContext ctx, ref IEnumerable<ServiceRequestRecord> data)
        {
            Progress = 0;
            counter = 1;
            total = data.Count();
            var curList = ctx.CancelReasons.Select(x => x.Reason.Trim()).ToList();
            var dic = new Dictionary<string, CancelReason>();
            var objects = data.Select(x => new { Reason = x.Cancel_Reason.Trim() }).Distinct().Where(x => !string.IsNullOrEmpty(x.Reason)).ToList();
            foreach (var item in objects.Where(x => !curList.Contains(x.Reason)))
            {
                var rec = new CancelReason() { Reason = item.Reason };
                ctx.CancelReasons.Add(rec);
                ctx.SaveChanges();
                if (!dic.ContainsKey(rec.Reason))
                {
                    dic.Add(rec.Reason, rec);
                }
                counter++;
                if (total > 0 && counter <= total)
                {
                    Progress = Convert.ToInt32(counter * 100 / (double)total);
                }
            }
            foreach (var item in ctx.CancelReasons)
            {
                if (!dic.ContainsKey(item.Reason))
                {
                    dic.Add(item.Reason, item);
                }
            }
            foreach (var item in data)
            {
                if (!string.IsNullOrEmpty(item.Cancel_Reason))
                {
                    item.CancelReasonId = dic[item.Cancel_Reason].Id;
                }
                else
                {
                    item.CancelReasonId = null;
                }
            }
        }

        private void ImportCustomers(SCMContext ctx, ref IEnumerable<ServiceRequestRecord> data)
        {
            Progress = 0;
            counter = 1;
            total = data.Count();
            var curList = ctx.Customers.Select(x => x.Name.Trim()).ToList();
            var dic = new Dictionary<string, Customer>();
            var tags = ctx.Tags.Where(x => x.TagType == "C").ToDictionary(x=> x.Name, y => y);
            var objects = data.Select(x => new { Name = x.Customer_Name.Trim(), Phone = x.Customer_Phone_No, Mobile = x.Cellular_No, City = x.CityId, Address = x.Address, CustomerType = x.Customer_Type }).Distinct().Where(x => !string.IsNullOrEmpty(x.Name)).ToList();
            foreach (var item in objects.Where(x => !curList.Contains(x.Name)))
            {
                var rec = new Customer() { Name = item.Name, Phone = item.Phone, Mobile = item.Mobile, CityId = item.City, Address = item.Address  };
                if (string.IsNullOrEmpty(rec.Phone) && string.IsNullOrEmpty(rec.Mobile))
                    rec.Phone = "011XXXX";
                ctx.Customers.Add(rec);
                ctx.SaveChanges();
                if (!dic.ContainsKey(rec.Name))
                {
                    dic.Add(rec.Name, rec);
                }

                // Add Customer Tags
                string t1 = item.CustomerType;
                if(t1 == "General Enduser")
                        t1 = "General End User";
                Tag t = null;
                if(!tags.ContainsKey(t1))
                {
                    t = new Tag() { Name = t1, TagType = "C", Format = "label-default" };
                    ctx.Tags.Add(t);
                    ctx.SaveChanges();
                    tags.Add(t.Name, t);
                }
                else
                {
                    t = tags[t1];
                }
                if(t != null)
                {
                    rec.Tags.Add(t);
                    ctx.SaveChanges();
                }

                counter++;
                if (total > 0 && counter <= total)
                {
                    Progress = Convert.ToInt32(counter * 100 / (double)total);
                }
            }
            foreach (var item in ctx.Customers)
            {
                if (!dic.ContainsKey(item.Name))
                {
                    dic.Add(item.Name, item);
                }
            }
            foreach (var item in data)
            {
                if (dic.ContainsKey(item.Customer_Name.Trim()))
                {
                    item.CustomerId = dic[item.Customer_Name.Trim()].Id;
                }
            }
        }

        private void ImportEngineers(SCMContext ctx, ref IEnumerable<ServiceRequestRecord> data)
        {
            Progress = 0;
            counter = 1;
            total = data.Count();

            var curList = ctx.Engineers.Select(x => x.Name.Trim()).ToList();
            var dic = new Dictionary<string, Engineer>();
            var objects = data.Select(x => new { Name = x.SVC_Engineer_Name.Trim() }).Distinct().Where(x => !string.IsNullOrEmpty(x.Name)).ToList();
            foreach (var item in objects.Where(x => !curList.Contains(x.Name)))
            {
                var rec = new Engineer() { Name = item.Name, DepartmentId = 1, IsActive = true };
                ctx.Engineers.Add(rec);
                ctx.SaveChanges();
                if (!dic.ContainsKey(rec.Name))
                {
                    dic.Add(rec.Name, rec);
                }

                counter++;
                if (total > 0 && counter <= total)
                {
                    Progress = Convert.ToInt32(counter * 100 / (double)total);
                }
            }
            foreach (var item in ctx.Engineers)
            {
                if (!dic.ContainsKey(item.Name))
                {
                    dic.Add(item.Name, item);
                }
            }
            foreach (var item in data)
            {
                if (!string.IsNullOrEmpty(item.SVC_Engineer_Name.Trim()))
                {
                    item.EngineerId = dic[item.SVC_Engineer_Name.Trim()].Id;
                }
                else
                {
                    item.EngineerId = null;
                }
            }
        }

        private void ImportRequests(SCMContext ctx, ref IEnumerable<ServiceRequestRecord> data)
        {
            Progress = 0;
            counter = 1;
            total = data.Count();

            var list = data.Where(x => !string.IsNullOrEmpty(x.Receipt_No)).ToDictionary(x => x.Receipt_No, y => y.SysID);
            var dic = new Dictionary<string,int>();
            dic.Add("Repair Accepted",10);
            dic.Add("Repair Pending",20);
            dic.Add("Repair Canceled",90);
            dic.Add("Repair Completed",100);
            var tags = ctx.Tags.Where(x => x.TagType == "R").ToList();

            var rqns = list.Keys.ToArray();
            var ids = new List<int>();
            

            foreach (var item in ctx.ServiceRequests.Where(x => !string.IsNullOrEmpty(x.RQN) && rqns.Contains(x.RQN)))
            {

                var sr = data.First(x => x.SysID == list[item.RQN]);
                if (sr.EngineerId.HasValue && sr.EngineerId.Value > 0)
                    item.EngineerId = sr.EngineerId;
                if (dic.ContainsKey(sr.Status))
                {
                    item.StatusId = dic[sr.Status];
                }
                item.StatusDate = DateTime.Now;
                if(!string.IsNullOrEmpty(sr.ProductId))
                    item.ProductId = sr.ProductId;
                item.Model = sr.Model;
                item.SN = sr.Serial_No;
                item.ClosingDate = string.IsNullOrEmpty(sr.Closing_Date) ? DateTime.Now : Convert.ToDateTime(sr.Closing_Date);
                item.Description = sr.CIC_Remark;
                item.UpdatedOn = DateTime.Now;
                item.UpdatedBy = HttpContext.Current.User.Identity.Name;
                ids.Add(sr.SysID);

                counter++;
                if(total > 0 && counter <= total)
                {
                    Progress = Convert.ToInt32(counter * 100 / (double) total);
                }
            }
            ctx.SaveChanges();
            foreach (var sr in data.Where(x => !ids.Contains(x.SysID)))
            {
                var item = ctx.ServiceRequests.Create();
                System.Diagnostics.Debug.Print(sr.SysID.ToString());
                try
                {
                    item.CenterId = 1;
                    item.CustomerId = sr.CustomerId;
                    item.DepartmentId = 1;
                    if (!string.IsNullOrEmpty(sr.Request_Date))
                        item.RequestDate = Convert.ToDateTime(sr.Request_Date);
                    else if (!string.IsNullOrEmpty(sr.Receipt_Date))
                        item.RequestDate = Convert.ToDateTime(sr.Receipt_Date);
                    else
                        item.RequestDate = DateTime.Now;

                    if(sr.EngineerId.HasValue && sr.EngineerId.Value > 0)
                        item.EngineerId = sr.EngineerId;
                    item.RQN = sr.Receipt_No;

                    if (dic.ContainsKey(sr.Status))
                    {
                        item.StatusId = dic[sr.Status];
                    }
                    item.StatusDate = DateTime.Now;
                    item.Model = sr.Model;
                    item.SN = sr.Serial_No;
                    item.ClosingDate = string.IsNullOrEmpty(sr.Closing_Date) ? DateTime.Now : Convert.ToDateTime(sr.Closing_Date);
                    item.Description = sr.CIC_Remark;
                    item.CreatedOn = DateTime.Now;
                    item.CreatedBy = HttpContext.Current.User.Identity.Name;
                    item.UpdatedOn = DateTime.Now;
                    item.UpdatedBy = HttpContext.Current.User.Identity.Name;
                    item.IsDeleted = false;
                    ctx.ServiceRequests.Add(item);
                    ctx.SaveChanges();
                    var exItem = ctx.ExServiceRequests.Create();
                    exItem.Id = item.Id;
                    ctx.ExServiceRequests.Add(exItem);
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    ex.Data["item"] = item;
                    ex.Data["sr"] = sr;
                    throw;
                }
                if (!string.IsNullOrEmpty(sr.Svc_Type) || !string.IsNullOrEmpty(sr.Warranty_Flag))
                {
                    var t = tags.FirstOrDefault(x => x.Name == sr.Svc_Type);
                    if (t != null)
                        item.Tags.Add(t);
                    var t1 = tags.FirstOrDefault(x => x.Name == sr.Warranty_Flag);
                    if (t1 != null)
                        item.Tags.Add(t1);

                    ctx.SaveChanges();
                }
                counter++;
                if (total > 0 && counter <= total)
                {
                    Progress = Convert.ToInt32(counter * 100 / (double) total);
                }

            }
            ctx.SaveChanges();
        }
    }
}