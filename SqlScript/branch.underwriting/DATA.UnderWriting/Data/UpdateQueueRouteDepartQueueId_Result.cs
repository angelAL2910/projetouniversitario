//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DATA.UnderWriting.Data
{
    using System;
    
    public partial class UpdateQueueRouteDepartQueueId_Result
    {
        public int Corp_Id { get; set; }
        public int Queue_Id { get; set; }
        public string Queue_Transactional_Code { get; set; }
        public int Queue_Type_Id { get; set; }
        public Nullable<int> Queue_Operator_Id { get; set; }
        public Nullable<int> Queue_Vendor_Id { get; set; }
        public Nullable<int> Distribution_Id { get; set; }
        public Nullable<int> Region_Id { get; set; }
        public Nullable<int> Country_Id { get; set; }
        public Nullable<int> DomesticReg_Id { get; set; }
        public Nullable<int> State_Prov_Id { get; set; }
        public Nullable<int> City_Id { get; set; }
        public Nullable<int> Office_Id { get; set; }
        public int Case_Seq_No { get; set; }
        public int Hist_Seq_No { get; set; }
        public string Policy_No { get; set; }
        public Nullable<int> Days_Late { get; set; }
        public string Periodic_Billing { get; set; }
        public Nullable<int> Policy_Cancellation_Reason_Id { get; set; }
        public string Policy_Cancellation_Reason_Comments { get; set; }
        public Nullable<System.DateTime> Action_Date { get; set; }
        public Nullable<int> Submitted_By_Department_Id { get; set; }
        public Nullable<int> Transferred_From_Department_Id { get; set; }
        public Nullable<int> Vendor_Agent_Id { get; set; }
        public Nullable<int> Queue_Status_Id { get; set; }
        public System.DateTime Create_Date { get; set; }
        public Nullable<System.DateTime> Modi_Date { get; set; }
        public int Create_UsrId { get; set; }
        public int Modi_UsrId { get; set; }
        public string Hostname { get; set; }
        public string Source_ID { get; set; }
        public Nullable<bool> IsOnHistory { get; set; }
    }
}
