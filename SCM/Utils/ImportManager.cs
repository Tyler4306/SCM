using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Transactions;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;

namespace SCM.Utils
{
    public class ImportManager
    {
        public int Progress { get; set; }
        public string Phase { get; set; }
        int total = 1;
        int counter = 0;

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
                        ex.Data["data"] = data;
                        tx.Rollback();
                        throw ex;
                    }
                }
            }
        }

        private void ResetProgress()
        {
            total = 1;
            Progress = 0;
            counter = 0;
        }

        private void UpdateProgress()
        {
            counter++;
            if (total > 0 && counter <= total)
            {
                Progress = (int)(((double)counter / total) * 100.0);
            }
            else
                Progress = 100;
        }
        
        private void ImportProducts(SCMContext ctx, ref IEnumerable<ServiceRequestRecord> data)
        {
            ResetProgress();            

            var curList = ctx.Products.Select(x => x.Id.ToUpper()).ToList();
            var dic = new Dictionary<string, Product>();
            var objects = data.Select(x => new { Id = x.Service_Product_Code, Name = x.SVC_Product.Trim(), IsActive = true }).Distinct().Where(x => !string.IsNullOrEmpty(x.Id)).ToList();
            var objectsToAdd = objects.Where(x => !curList.Contains(x.Id.ToUpper()));

            total = objectsToAdd.Count();
            foreach (var item in objectsToAdd)
            {
                var rec = new Product() { Id = item.Id, Name = item.Name.Trim(), IsActive = item.IsActive };
                ctx.Products.Add(rec);
                ctx.SaveChanges();
                if(!dic.ContainsKey(rec.Id.ToUpper()))
                {
                    dic.Add(rec.Id.ToUpper(), rec);
                }
                UpdateProgress();
            }
            foreach (var item in ctx.Products)
            {
                if(!dic.ContainsKey(item.Id.ToUpper()))
                {
                    dic.Add(item.Id.ToUpper(), item);
                }
            }
            foreach(var item in data)
            {
                item.ProductId = item.Service_Product_Code.ToUpper();
            }
            Progress = 100;
        }

        private void ImportCities(SCMContext ctx, ref IEnumerable<ServiceRequestRecord> data)
        {
            ResetProgress();

            var curList = ctx.Cities.Select(x => x.Name.ToUpper()).ToList();
            var dic = new Dictionary<string, City>();
            var objects = data.Select(x => new { Name = x.City_Name }).Distinct().Where(x => !string.IsNullOrEmpty(x.Name)).ToList();
            var objectsToAdd = objects.Where(x => !curList.Contains(x.Name.ToUpper()));

            total = objectsToAdd.Count();
            foreach (var item in objectsToAdd)
            {
                var rec = new City() { Name = item.Name.ToUpper() };
                ctx.Cities.Add(rec);
                ctx.SaveChanges();
                if (!dic.ContainsKey(rec.Name.ToUpper()))
                {
                    dic.Add(rec.Name.ToUpper(), rec);
                }

                UpdateProgress();
            }
            foreach (var item in ctx.Cities)
            {
                if (!dic.ContainsKey(item.Name.ToUpper()))
                {
                    dic.Add(item.Name.ToUpper(), item);
                }
            }
            foreach (var item in data)
            {
                if (!string.IsNullOrEmpty(item.City_Name))
                {
                    item.CityId = dic[item.City_Name.ToUpper()].Id;
                }
                else
                {
                    item.CityId = null;
                }
            }

            Progress = 100;
        }

        private void ImportPendingReasons(SCMContext ctx, ref IEnumerable<ServiceRequestRecord> data)
        {
            ResetProgress();

            var curList = ctx.PendingReasons.Select(x => x.Reason).ToList();
            var dic = new Dictionary<string, PendingReason>();
            var objects = data.Select(x => new { Reason = x.Pending_Reason }).Distinct().Where(x => !string.IsNullOrEmpty(x.Reason)).ToList();

            var objectsToAdd = objects.Where(x => !curList.Contains(x.Reason));
            total = objectsToAdd.Count();
            foreach (var item in objectsToAdd)
            {
                var rec = new PendingReason() { Reason = item.Reason };
                ctx.PendingReasons.Add(rec);
                ctx.SaveChanges();
                if (!dic.ContainsKey(rec.Reason.ToUpper()))
                {
                    dic.Add(rec.Reason.ToUpper(), rec);
                }

                UpdateProgress();
            }
            foreach (var item in ctx.PendingReasons)
            {
                if (!dic.ContainsKey(item.Reason.ToUpper()))
                {
                    dic.Add(item.Reason.ToUpper(), item);
                }
            }
            foreach (var item in data)
            {
                if (!string.IsNullOrEmpty(item.Pending_Reason))
                {
                    item.PendingReasonId = dic[item.Pending_Reason.ToUpper()].Id;
                }
                else
                {
                    item.PendingReasonId = null;
                }
            }

            Progress = 100;
        }

        private void ImportCancelReasons(SCMContext ctx, ref IEnumerable<ServiceRequestRecord> data)
        {
            ResetProgress();

            var curList = ctx.CancelReasons.Select(x => x.Reason).ToList();
            var dic = new Dictionary<string, CancelReason>();
            var objects = data.Select(x => new { Reason = x.Cancel_Reason }).Distinct().Where(x => !string.IsNullOrEmpty(x.Reason)).ToList();

            var objectToAdd = objects.Where(x => !curList.Contains(x.Reason));
            total = objectToAdd.Count();
            foreach (var item in objectToAdd)
            {
                var rec = new CancelReason() { Reason = item.Reason };
                ctx.CancelReasons.Add(rec);
                ctx.SaveChanges();
                if (!dic.ContainsKey(rec.Reason.ToUpper()))
                {
                    dic.Add(rec.Reason.ToUpper(), rec);
                }

                UpdateProgress();
            }
            foreach (var item in ctx.CancelReasons)
            {
                if (!dic.ContainsKey(item.Reason.ToUpper()))
                {
                    dic.Add(item.Reason.ToUpper(), item);
                }
            }
            foreach (var item in data)
            {
                if (!string.IsNullOrEmpty(item.Cancel_Reason))
                {
                    item.CancelReasonId = dic[item.Cancel_Reason.ToUpper()].Id;
                }
                else
                {
                    item.CancelReasonId = null;
                }
            }

            Progress = 100;
        }

        private void ImportCustomers(SCMContext ctx, ref IEnumerable<ServiceRequestRecord> data)
        {
            ResetProgress();

            var curList = ctx.Customers.Select(x => x.Name).ToList();
            var dic = new Dictionary<string, Customer>();
            var tags = ctx.Tags.Where(x => x.TagType == "C").ToDictionary(x=> x.Name, y => y);
            var objects = data.Select(x => new { Name = x.Customer_Name, Phone = x.Customer_Phone_No, Mobile = x.Cellular_No, City = x.CityId, Address = x.Address, CustomerType = x.Customer_Type }).Distinct().Where(x => !string.IsNullOrEmpty(x.Name)).ToList();

            var objectsToAdd = objects.Where(x => !curList.Contains(x.Name));
            total = objectsToAdd.Count();
            foreach (var item in objectsToAdd)
            {
                var rec = new Customer() { Name = item.Name, Phone = item.Phone, Mobile = item.Mobile, CityId = item.City, Address = item.Address  };
                if (string.IsNullOrEmpty(rec.Phone) && string.IsNullOrEmpty(rec.Mobile))
                    rec.Phone = "011XXXX";
                ctx.Customers.Add(rec);
                ctx.SaveChanges();
                if (!dic.ContainsKey(rec.Name.ToUpper()))
                {
                    dic.Add(rec.Name.ToUpper(), rec);
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

                UpdateProgress();
            }
            foreach (var item in ctx.Customers)
            {
                if (!dic.ContainsKey(item.Name.ToUpper()))
                {
                    dic.Add(item.Name.ToUpper(), item);
                }
            }
            ResetProgress();
            total = data.Count();
            foreach (var item in data)
            {
                if (!string.IsNullOrEmpty(item.Customer_Name) && dic.ContainsKey(item.Customer_Name))
                {
                    item.CustomerId = dic[item.Customer_Name.ToUpper()].Id;
                }
                UpdateProgress();
            }

            Progress = 100;
        }

        private void ImportEngineers(SCMContext ctx, ref IEnumerable<ServiceRequestRecord> data)
        {
            ResetProgress();

            var curList = ctx.Engineers.Select(x => x.Name).ToList();
            var dic = new Dictionary<string, Engineer>();
            var objects = data.Select(x => new { Name = x.SVC_Engineer_Name }).Distinct().Where(x => !string.IsNullOrEmpty(x.Name)).ToList();
            var objectsToAdd = objects.Where(x => !curList.Contains(x.Name));
            total = objectsToAdd.Count();

            foreach (var item in objectsToAdd)
            {
                var rec = new Engineer() { Name = item.Name, DepartmentId = 1, IsActive = true };
                ctx.Engineers.Add(rec);
                ctx.SaveChanges();
                if (!dic.ContainsKey(rec.Name.ToUpper()))
                {
                    dic.Add(rec.Name.ToUpper(), rec);
                }

                UpdateProgress();
            }
            foreach (var item in ctx.Engineers)
            {
                if (!dic.ContainsKey(item.Name.ToUpper()))
                {
                    dic.Add(item.Name.ToUpper(), item);
                }
            }
            foreach (var item in data)
            {
                if (!string.IsNullOrEmpty(item.SVC_Engineer_Name))
                {
                    item.EngineerId = dic[item.SVC_Engineer_Name.ToUpper()].Id;
                }
                else
                {
                    item.EngineerId = null;
                }
            }

            Progress = 100;
        }

        private void ImportRequests(SCMContext ctx, ref IEnumerable<ServiceRequestRecord> data)
        {
            ResetProgress();

            var list = data.Where(x => !string.IsNullOrEmpty(x.Receipt_No)).ToDictionary(x => x.Receipt_No, y => y.SysID);
            var dic = new Dictionary<string,int>();
            dic.Add("Repair Accepted",10);
            dic.Add("Repair Pending",20);
            dic.Add("Repair Canceled",90);
            dic.Add("Repair Completed",100);
            var tags = ctx.Tags.Where(x => x.TagType == "R").ToList();

            var rqns = list.Keys.ToArray();
            var ids = new List<int>();

            var objectsToUpdate = ctx.ServiceRequests.Where(x => !string.IsNullOrEmpty(x.RQN) && rqns.Contains(x.RQN));
            total = objectsToUpdate.Count();
            StringBuilder sb1 = new StringBuilder();
            int counter = 0;
            foreach (var item in objectsToUpdate)
            {
                if(counter >= 9)
                {
                    DbCommand cmd1 = ctx.Database.Connection.CreateCommand();
                    cmd1.CommandText = sb1.ToString();
                    
                    cmd1.Transaction = ctx.Database.CurrentTransaction.UnderlyingTransaction;
                    cmd1.ExecuteNonQuery();
                    sb1 = new StringBuilder();
                    counter = 0;

                }
                else
                {
                    counter++;
                }
                var sr = data.First(x => x.SysID == list[item.RQN]);

                string sql = @"UPDATE ServiceRequests SET [EngineerId] = {0}, [StatusId] = {1}, [StatusDate] = (case when [RequestDate] > getdate() then [RequestDate] else getdate() end), [ProductId] = {2}, [Model] = {3}, [SN] = {4}, [ClosingDate] = {5}, [Description] = {6}, UpdatedOn = getdate(), UpdatedBy = '{7}' Where [RQN] = '{8}'; ";
                var engId = (sr.EngineerId.HasValue && sr.EngineerId.Value > 0) ? sr.EngineerId.ToString() : "NULL";
                var model = string.IsNullOrEmpty(sr.Model) ? "NULL" : string.Format("N'{0}'", sr.Model.Replace("'", "''"));
                var rqn = item.RQN;
                var sn = string.IsNullOrEmpty(sr.Serial_No) ? "NULL" : string.Format("N'{0}'", sr.Serial_No.Replace("'", "''"));
                DateTime? closingDate = null;
                if (!string.IsNullOrEmpty(sr.Completion_Date))
                {
                    closingDate = Convert.ToDateTime(sr.Completion_Date);
                }
                var description = string.IsNullOrEmpty(sr.CIC_Remark) ? "NULL" : string.Format("N'{0}'", sr.CIC_Remark.Replace("'", "''"));
                var userName = HttpContext.Current.User.Identity.Name;
                if (!string.IsNullOrEmpty(sr.ProductId))
                    item.ProductId = "'" + sr.ProductId + "'";
                else
                    item.ProductId = "NULL";

                var valsql = string.Format(sql, engId, dic[sr.Status], item.ProductId, model, sn, !closingDate.HasValue ? "NULL" : "'" + closingDate.Value.ToString("dd/MMM/yyyy HH:mm:ss") + "'", description, userName, rqn);
                sb1.AppendLine(valsql);

                ids.Add(sr.SysID);


                UpdateProgress();

            }
            if (counter > 0)
            {
                DbCommand cmd1 = ctx.Database.Connection.CreateCommand();
                cmd1.CommandText = sb1.ToString();
                cmd1.CommandTimeout = 200;
                cmd1.Transaction = ctx.Database.CurrentTransaction.UnderlyingTransaction;
                cmd1.ExecuteNonQuery();
                counter = 0;
            }

            ResetProgress();
            var objectsToAdd = data.Where(x => !ids.Contains(x.SysID));
            total = objectsToAdd.Count();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("declare @id int;");
            try
            {
                counter = 0;
                foreach (var sr in objectsToAdd)
                {
                    if(counter >= 9)
                    {
                        DbCommand cmd = ctx.Database.Connection.CreateCommand();
                        cmd.CommandText = sb.ToString();
                        cmd.Transaction = ctx.Database.CurrentTransaction.UnderlyingTransaction;
                        cmd.ExecuteNonQuery();
                        sb = new StringBuilder();
                        sb.AppendLine("declare @id int;");
                        counter = 0;
                    }
                    else
                    {
                        counter++;
                    }
                    string sql = @"INSERT INTO ServiceRequests ([CenterId], [CustomerId], [DepartmentId], [RequestDate], [EngineerId], [RQN], [StatusId], [StatusDate], [Model], [SN], [ClosingDate], [Description], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy], [IsDeleted]) VALUES (1,{0}, 1, '{1}', {2}, '{3}', {4}, '{1}', {5}, {6}, {7}, {8}, getdate(), '{9}', getdate(), '{9}', 0 );";

                    var requestDate = !string.IsNullOrEmpty(sr.Request_Date) ? Convert.ToDateTime(sr.Request_Date) : DateTime.Now;
                    var engId = (sr.EngineerId.HasValue && sr.EngineerId.Value > 0) ? sr.EngineerId.ToString() : "NULL";
                    var model = string.IsNullOrEmpty(sr.Model) ? "NULL" : string.Format("N'{0}'", sr.Model.Replace("'","''"));
                    var sn = string.IsNullOrEmpty(sr.Serial_No) ? "NULL" : string.Format("N'{0}'", sr.Serial_No.Replace("'","''"));
                    DateTime? closingDate = null;
                    if (!string.IsNullOrEmpty(sr.Completion_Date))
                    {
                        closingDate = Convert.ToDateTime(sr.Completion_Date);
                    }
                    var description = string.IsNullOrEmpty(sr.CIC_Remark) ? "NULL" : string.Format("N'{0}'", sr.CIC_Remark.Replace("'","''"));
                    var userName = HttpContext.Current.User.Identity.Name;

                    string valsql = string.Format(sql, sr.CustomerId, requestDate.ToString("dd/MMM/yyyy HH:mm:ss"), engId
                        , sr.Receipt_No, dic[sr.Status], model, sn, !closingDate.HasValue ? "NULL" : "'" + closingDate.Value.ToString("dd/MMM/yyyy HH:mm:ss") + "'", description, userName);
                    sb.AppendLine(valsql);
                    sb.AppendLine("set @id = @@identity;");
                    if (!string.IsNullOrEmpty(sr.Svc_Type) || !string.IsNullOrEmpty(sr.Warranty_Flag))
                    {
                        var t = tags.FirstOrDefault(x => x.Name == sr.Svc_Type);
                        if (t != null)
                        {
                            sb.AppendFormat("INSERT INTO RequestTags (TagId, ServiceRequestId) VALUES ({0}, @id);", t.Id);
                            sb.AppendLine();
                        }
                        var t1 = tags.FirstOrDefault(x => x.Name == sr.Warranty_Flag);
                        if (t1 != null)
                        {
                            sb.AppendFormat("INSERT INTO RequestTags (TagId, ServiceRequestId) VALUES ({0}, @id);", t1.Id);
                            sb.AppendLine();
                        }
                    }


                    System.Diagnostics.Debug.Print(sr.SysID.ToString());
                    UpdateProgress();
                }
                if(counter > 0)
                {
                    DbCommand cmd = ctx.Database.Connection.CreateCommand();
                    cmd.CommandText = sb.ToString();
                    cmd.Transaction = ctx.Database.CurrentTransaction.UnderlyingTransaction;
                    cmd.ExecuteNonQuery();
                }
            }
        catch (Exception ex)
        {
            //ex.Data["item"] = item;
            //ex.Data["sr"] = sr;
            throw ex;
        }
    //ctx.SaveChanges();

    Progress = 100;
        }
    }
}