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
    
    public partial class SPC_GET_POLICIES_BY_CONTACTID_Result
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
        public Nullable<int> Bussiness_Line_Id { get; set; }
        public Nullable<int> Bussiness_Line_Type { get; set; }
        public string Bl_Desc { get; set; }
        public string Product_Desc { get; set; }
        public Nullable<int> Product_Id { get; set; }
        public Nullable<int> Currency_Id { get; set; }
        public System.DateTime DueDate { get; set; }
        public decimal PendingAmount { get; set; }
        public decimal TotalDueAmount { get; set; }
        public Nullable<decimal> Annual_Premium { get; set; }
        public Nullable<System.DateTime> Expiration_Date { get; set; }
        public string Policy_Status_Desc { get; set; }
    }
}
