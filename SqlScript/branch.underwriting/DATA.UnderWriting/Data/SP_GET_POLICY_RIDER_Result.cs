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
    
    public partial class SP_GET_POLICY_RIDER_Result
    {
        public int Corp_Id { get; set; }
        public int Region_Id { get; set; }
        public int Country_Id { get; set; }
        public int Domesticreg_Id { get; set; }
        public int State_Prov_Id { get; set; }
        public int City_Id { get; set; }
        public int Office_Id { get; set; }
        public int Case_seq_No { get; set; }
        public int Hist_Seq_No { get; set; }
        public int Rider_Type_Id { get; set; }
        public int Rider_id { get; set; }
        public string Ryder_Type_Desc { get; set; }
        public Nullable<decimal> Beneficiary_Amount { get; set; }
        public Nullable<System.DateTime> Effective_Date { get; set; }
        public Nullable<System.DateTime> Expire_Date { get; set; }
        public Nullable<int> Number_Of_Year { get; set; }
        public Nullable<decimal> Extra_Premium { get; set; }
        public string Extra_Premium_Comments { get; set; }
        public int Rider_Status_Id { get; set; }
        public string Rider_Status_Desc { get; set; }
    }
}
