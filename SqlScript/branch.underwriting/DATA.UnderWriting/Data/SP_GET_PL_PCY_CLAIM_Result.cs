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
    
    public partial class SP_GET_PL_PCY_CLAIM_Result
    {
        public int Corp_Id { get; set; }
        public int Region_id { get; set; }
        public int Country_Id { get; set; }
        public int Domesticreg_Id { get; set; }
        public int State_Prov_Id { get; set; }
        public int City_Id { get; set; }
        public int Office_Id { get; set; }
        public int Case_Seq_No { get; set; }
        public int Hist_Seq_No { get; set; }
        public int Case_Type_Id { get; set; }
        public int Case_Id { get; set; }
        public int Case_Sequence { get; set; }
        public int Case_No { get; set; }
        public string Claim_Description { get; set; }
        public Nullable<int> DiagProc_Id { get; set; }
        public Nullable<int> Claim_Type_Id { get; set; }
        public Nullable<int> Provider_Type_Id { get; set; }
        public Nullable<int> Provider_Id { get; set; }
        public Nullable<decimal> Total_Charges { get; set; }
        public Nullable<decimal> Total_Paid_Amount { get; set; }
        public Nullable<decimal> Total_Deductable_Value { get; set; }
        public Nullable<decimal> Companyl_Paid_Amount { get; set; }
        public Nullable<System.DateTime> Claim_Date { get; set; }
        public Nullable<System.DateTime> Claim_Event_Date { get; set; }
        public Nullable<System.DateTime> Claim_Close_Date { get; set; }
        public Nullable<System.DateTime> Claim_Service_Date { get; set; }
        public Nullable<int> Claimer_Contact_Id { get; set; }
        public Nullable<int> Claim_Control_Status_Id { get; set; }
    }
}