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
    
    public partial class SP_GET_CONFIRMATION_CALL_Result
    {
        public Nullable<int> Corp_Id { get; set; }
        public Nullable<int> Region_Id { get; set; }
        public Nullable<int> Country_Id { get; set; }
        public Nullable<int> Domesticreg_Id { get; set; }
        public Nullable<int> State_Prov_Id { get; set; }
        public Nullable<int> City_Id { get; set; }
        public Nullable<int> Office_Id { get; set; }
        public Nullable<int> Case_Seq_No { get; set; }
        public Nullable<int> Hist_Seq_No { get; set; }
        public Nullable<int> Step_Type_Id { get; set; }
        public Nullable<int> Step_Id { get; set; }
        public Nullable<int> Step_Case_No { get; set; }
        public Nullable<int> InsuredContactId { get; set; }
        public Nullable<int> OwnerContactId { get; set; }
        public string CaseStatus { get; set; }
        public string PolicyNo { get; set; }
        public string PlanName { get; set; }
        public string PlanType { get; set; }
        public string InsuredName { get; set; }
        public string OwnerName { get; set; }
        public string Office { get; set; }
        public Nullable<System.DateTime> InitialDate { get; set; }
        public string Observations { get; set; }
        public Nullable<System.DateTime> WorkedOn { get; set; }
        public Nullable<int> NumberOfCalls { get; set; }
        public Nullable<int> Days { get; set; }
        public Nullable<int> AddContactId { get; set; }
        public string AddInsuredName { get; set; }
        public string WorkedBy { get; set; }
        public string AgentName { get; set; }
    }
}
