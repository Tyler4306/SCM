using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCM.Models
{
    public class ServiceRequestRecord
    {
        public string No { get; set; }
        public string B2B_Flag { get; set; }
        public string Receipt_No { get; set; }
        public string Service_Product_Code { get; set; }
        public string SVC_Product { get; set; }
        public string Model { get; set; }
        public string Serial_No { get; set; }
        public string Status { get; set; }
        public string Customer_Name { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string Customer_Phone_No { get; set; }
        public string Cellular_No { get; set; }
        public string Postal_Code { get; set; }
        public string Customer_Type { get; set; }
        public string VIP_YN { get; set; }
        public string ASC_Claim_No { get; set; }
        public string EsnImei_No { get; set; }
        public string Svc_Type { get; set; }
        public string Out_Model { get; set; }
        public string Receipt_Date { get; set; }
        public string Request_Date { get; set; }
        public string Transfer_Send_Date { get; set; }
        public string Transfer_Receipt_Date { get; set; }
        public string First_Promise_Date { get; set; }
        public string Schedule { get; set; }
        public string Promise_Date { get; set; }
        public string Schedule1 { get; set; }
        public string Delay_from_Promise_Date { get; set; }
        public string Delay_from_Receipt_Date { get; set; }
        public string Transfer_Approval_Date { get; set; }
        public string SVC_Center { get; set; }
        public string SVC_Center_Name { get; set; }
        public string SVC_Center_Phone_No { get; set; }
        public string SVC_Engineer { get; set; }
        public string SVC_Engineer_Name { get; set; }
        public string Completion_Date { get; set; }
        public string Input_Date { get; set; }
        public string Closing_Date { get; set; }
        public string Pulling_Date { get; set; }
        public string Pending_Reason { get; set; }
        public string Cancel_Reason { get; set; }
        public string Warranty_Flag { get; set; }
        public string Dealer { get; set; }
        public string Dealer_Name { get; set; }
        public string Dealer_Receipt_No { get; set; }
        public string Asc_Remark { get; set; }
        public string Last_Update_User_ID { get; set; }
        public string Last_Update_Date { get; set; }
        public string Schedule_Complaint_Date { get; set; }
        public string Schedule_Complaint_Count { get; set; }
        public string Schedule_Complaint_Remark { get; set; }
        public string Receipt_Type { get; set; }
        public string City_Name { get; set; }
        public string State { get; set; }
        public string Offering_Type { get; set; }
        public string CIC_Remark { get; set; }

        public string ProductId { get; set; }
        public int? CityId { get; set; }
        public int CustomerId { get; set; }
        public int? EngineerId { get; set; }
        public int? PendingReasonId { get; set; }
        public int? CancelReasonId { get; set; }
        public int SysID { get; set; }
    }

    public sealed class ServiceRequestRecordMap : CsvClassMap<ServiceRequestRecord>
    {
        public ServiceRequestRecordMap()
        {
            Map(m => m.No).Name("No");
            Map(m => m.B2B_Flag).Name("B2B Flag");
            Map(m => m.Receipt_No).Name("Receipt No");
            Map(m => m.Service_Product_Code).Name("Service Product Code");
            Map(m => m.SVC_Product).Name("SVC Product");
            Map(m => m.Model).Name("Model");
            Map(m => m.Serial_No).Name("Serial No");
            Map(m => m.Status).Name("Status");
            Map(m => m.Customer_Name).Name("Customer Name");
            Map(m => m.Country).Name("Country");
            Map(m => m.Address).Name("Address");
            Map(m => m.Customer_Phone_No).Name("Customer Phone No");
            Map(m => m.Cellular_No).Name("Cellular No");
            Map(m => m.Postal_Code).Name("Postal Code");
            Map(m => m.Customer_Type).Name("Customer Type");
            Map(m => m.VIP_YN).Name("VIP Y/N");
            Map(m => m.ASC_Claim_No).Name("ASC Claim No");
            Map(m => m.EsnImei_No).Name("Esn/Imei No");
            Map(m => m.Svc_Type).Name("Svc Type");
            Map(m => m.Out_Model).Name("Out Model");
            Map(m => m.Receipt_Date).Name("Receipt Date");
            Map(m => m.Request_Date).Name("Request Date");
            Map(m => m.Transfer_Send_Date).Name("Transfer Send Date");
            Map(m => m.Transfer_Receipt_Date).Name("Transfer Receipt Date");
            Map(m => m.First_Promise_Date).Name("First Promise Date");
            Map(m => m.Schedule).Name("Schedule");
            Map(m => m.Promise_Date).Name("Promise Date");
            Map(m => m.Schedule1).Name("Schedule");
            Map(m => m.Delay_from_Promise_Date).Name("Delay from Promise Date");
            Map(m => m.Delay_from_Receipt_Date).Name("Delay from Receipt Date");
            Map(m => m.Transfer_Approval_Date).Name("Transfer Approval Date");
            Map(m => m.SVC_Center).Name("SVC Center");
            Map(m => m.SVC_Center_Name).Name("SVC Center Name");
            Map(m => m.SVC_Center_Phone_No).Name("SVC Center Phone No");
            Map(m => m.SVC_Engineer).Name("SVC Engineer");
            Map(m => m.SVC_Engineer_Name).Name("SVC Engineer Name");
            Map(m => m.Completion_Date).Name("Completion Date");
            Map(m => m.Input_Date).Name("Input Date");
            Map(m => m.Closing_Date).Name("Closing Date");
            Map(m => m.Pulling_Date).Name("Pulling Date");
            Map(m => m.Pending_Reason).Name("Pending Reason");
            Map(m => m.Cancel_Reason).Name("Cancel Reason");
            Map(m => m.Warranty_Flag).Name("Warranty Flag");
            Map(m => m.Dealer).Name("Dealer");
            Map(m => m.Dealer_Name).Name("Dealer Name");
            Map(m => m.Dealer_Receipt_No).Name("Dealer Receipt No");
            Map(m => m.Asc_Remark).Name("Asc Remark");
            Map(m => m.Last_Update_User_ID).Name("Last Update User ID");
            Map(m => m.Last_Update_Date).Name("Last Update Date");
            Map(m => m.Schedule_Complaint_Date).Name("Schedule Complaint Date");
            Map(m => m.Schedule_Complaint_Count).Name("Schedule Complaint Count");
            Map(m => m.Schedule_Complaint_Remark).Name("Schedule Complaint Remark");
            Map(m => m.Receipt_Type).Name("Receipt Type");
            Map(m => m.City_Name).Name("City Name");
            Map(m => m.State).Name("State");
            Map(m => m.Offering_Type).Name("Offering Type");
            Map(m => m.CIC_Remark).Name("CIC Remark");

        }
    }
}