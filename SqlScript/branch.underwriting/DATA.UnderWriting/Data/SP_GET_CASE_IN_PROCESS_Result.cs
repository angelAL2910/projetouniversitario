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
    
    public partial class SP_GET_CASE_IN_PROCESS_Result
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
        public Nullable<int> OwnerContactId { get; set; }
        public Nullable<int> InsuredContactId { get; set; }
        public Nullable<int> AddInsuredContactId { get; set; }
        public Nullable<int> DesignatedPensionerContactId { get; set; }
        public Nullable<int> StudentNameContactId { get; set; }
        public int Agent_Id { get; set; }
        public Nullable<int> Bussiness_Line_Type { get; set; }
        public Nullable<int> Bussiness_Line_Id { get; set; }
        public Nullable<int> Product_Id { get; set; }
        public System.DateTime LastUpdate { get; set; }
        public int UserId { get; set; }
        public string Product_Desc { get; set; }
        public string OwnerFullName { get; set; }
        public string InsuranceFullName { get; set; }
        public string AgentFullName { get; set; }
        public string UserFullName { get; set; }
        public Nullable<bool> HasComment { get; set; }
        public Nullable<int> Payment_Id { get; set; }
        public Nullable<bool> CanGoRequirement { get; set; }
        public string Office_Desc { get; set; }
        public string Exception { get; set; }
        public string Country_Desc { get; set; }
        public string Product_Name_Key { get; set; }
        public string Product_Type_Desc { get; set; }
        public Nullable<int> AgentLegalContactId { get; set; }
    }
}
