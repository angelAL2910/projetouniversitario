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
    
    public partial class SP_Get_SRVC_QUEUES_INITIAL_PAYMENTS_VEHICLE_Result
    {
        public int Corp_Id { get; set; }
        public string Queue_Transactional_Code { get; set; }
        public Nullable<int> Queue_Type_Id { get; set; }
        public Nullable<int> Queue_Operator_Id { get; set; }
        public Nullable<int> Queue_Vendor_Id { get; set; }
        public Nullable<int> Distribution_Id { get; set; }
        public int Region_Id { get; set; }
        public int Country_Id { get; set; }
        public int DomesticReg_Id { get; set; }
        public int State_Prov_Id { get; set; }
        public int City_Id { get; set; }
        public int Office_Id { get; set; }
        public int Case_Seq_No { get; set; }
        public int Hist_Seq_No { get; set; }
        public string Policy_No { get; set; }
        public Nullable<decimal> Initial_Premium { get; set; }
        public Nullable<decimal> Initial_Premium_GP { get; set; }
        public Nullable<decimal> Initial_Premium_Percentage { get; set; }
        public int Days_Late { get; set; }
        public Nullable<int> Periodic_Billing { get; set; }
        public Nullable<int> Policy_Cancellation_Reason_Id { get; set; }
        public Nullable<int> Policy_Cancellation_Reason_Comments { get; set; }
        public System.DateTime Action_Date { get; set; }
        public Nullable<int> Submitted_By_Department_Id { get; set; }
        public Nullable<int> Transferred_From_Department_Id { get; set; }
        public Nullable<int> Vendor_Agent_Id { get; set; }
        public int Queue_Status_Id { get; set; }
        public System.DateTime Create_Date { get; set; }
        public Nullable<short> Create_UsrId { get; set; }
        public Nullable<short> Create_UsrId1 { get; set; }
        public string Hostname { get; set; }
        public string Source_ID { get; set; }
    }
}
