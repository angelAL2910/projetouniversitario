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
    
    public partial class SP_GET_CLIENT_POLICY_Result
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
        public int Contact_ID { get; set; }
        public string Policy_No { get; set; }
        public string Insured_Name { get; set; }
        public string Plan_Name { get; set; }
        public string Policy_Type { get; set; }
        public string policy_Status { get; set; }
        public Nullable<System.DateTime> Effective_Date { get; set; }
        public string Policy_Currency { get; set; }
        public Nullable<decimal> Main_Benefit { get; set; }
        public Nullable<decimal> Rider_Benefit { get; set; }
        public Nullable<decimal> Total_Benefit { get; set; }
        public Nullable<decimal> Account_Value { get; set; }
        public Nullable<int> Rider_Quantity { get; set; }
        public Nullable<System.DateTime> Expiraciation_date { get; set; }
    }
}
