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
    
    public partial class SP_GET_PL_POLICY_BY_COUNTRY_RUN_CLAIMS_Result
    {
        public int Corp_Id { get; set; }
        public int Region_Id { get; set; }
        public int Country_Id { get; set; }
        public int Domesticreg_Id { get; set; }
        public int State_Prov_Id { get; set; }
        public int City_Id { get; set; }
        public int Office_Id { get; set; }
        public int Case_Seq_No { get; set; }
        public int Hist_Seq_No { get; set; }
        public string Policy_No { get; set; }
        public string Poliza_Source_ID { get; set; }
        public string Insured_Vehicle_Id_Source_ID { get; set; }
        public string Reclamo_Source_ID { get; set; }
        public Nullable<int> Case_Type_Id { get; set; }
        public Nullable<int> Case_Id { get; set; }
        public Nullable<int> Case_Sequence { get; set; }
        public Nullable<int> Case_No { get; set; }
        public string Claim_Description { get; set; }
        public Nullable<System.DateTime> Claim_Date { get; set; }
        public Nullable<System.DateTime> Claim_Event_Date { get; set; }
        public Nullable<System.DateTime> Claim_Close_Date { get; set; }
        public Nullable<int> Claimer_Contact_Id { get; set; }
        public Nullable<int> Claim_Control_Status_Id { get; set; }
        public Nullable<bool> Claim_Status_Id { get; set; }
        public string Claim_Control_Status { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> Create_Date { get; set; }
        public Nullable<System.DateTime> Modi_Date { get; set; }
        public Nullable<int> Create_UsrId { get; set; }
        public Nullable<int> Modi_UsrId { get; set; }
        public string hostname { get; set; }
        public string Claim_Core_Number { get; set; }
        public int Cause_Id { get; set; }
        public Nullable<int> Fault_Id { get; set; }
        public string Claim_Detail_Desc { get; set; }
        public Nullable<System.DateTime> Claim_Detail_Date { get; set; }
        public Nullable<int> Driver_Contact_Id { get; set; }
        public Nullable<decimal> Claim_Value { get; set; }
        public Nullable<decimal> Deductible_Amount_Paid { get; set; }
        public Nullable<decimal> Amount_Paid_By_Company { get; set; }
        public Nullable<bool> Reported_From_Site { get; set; }
        public string Claim_Location_Address { get; set; }
        public string Claim_Location_City { get; set; }
        public string Claim_Location_State_Province { get; set; }
        public Nullable<int> Claim_Det_Status_Id { get; set; }
        public Nullable<bool> Claim_Status { get; set; }
        public Nullable<bool> Claim_Status_Check { get; set; }
        public string Claim_Status_Desc { get; set; }
        public int Insured_Vehicle_Id { get; set; }
        public long Vehicle_Unique_Id { get; set; }
        public int Cause_Type_Id { get; set; }
        public string Cause_Desc { get; set; }
        public Nullable<int> Insured_Id { get; set; }
        public Nullable<bool> Smoker { get; set; }
        public Nullable<int> Marital_Stat_Id { get; set; }
        public Nullable<int> Status_Id { get; set; }
        public string Source_ID { get; set; }
    }
}
