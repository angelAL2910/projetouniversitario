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
    
    public partial class SP_GET_PL_POLICY_TRANSPORT_INSURED_COVERAGE_Result
    {
        public int Currency_Id { get; set; }
        public decimal Unitary_Price { get; set; }
        public decimal Package_Price { get; set; }
        public Nullable<decimal> Deductible_Amount { get; set; }
        public Nullable<decimal> Deductible_Percentage { get; set; }
        public Nullable<decimal> Manual_Deductible_Amount { get; set; }
        public Nullable<decimal> Manual_Deductible_Percentage { get; set; }
        public Nullable<decimal> Coverage_Limit { get; set; }
        public bool Coverage_Status { get; set; }
        public int Corp_Id { get; set; }
        public long Unique_Transport_Id { get; set; }
        public int Region_Id { get; set; }
        public int Country_Id { get; set; }
        public int Bl_Type_Id { get; set; }
        public int Bl_Id { get; set; }
        public int Product_Id { get; set; }
        public int Vehicle_Type_Id { get; set; }
        public int Group_Id { get; set; }
        public int Coverage_Type_Id { get; set; }
        public int Coverage_Id { get; set; }
        public string Coverage_Type_Desc { get; set; }
        public string Group_Desc { get; set; }
        public string Coverage_Desc { get; set; }
        public Nullable<int> Ramo { get; set; }
        public Nullable<int> SubRamo { get; set; }
        public string Ramo_Desc { get; set; }
        public string SubRamo_Desc { get; set; }
        public Nullable<decimal> Coverage_Percentage { get; set; }
        public Nullable<decimal> Premium_Percentage { get; set; }
        public Nullable<decimal> Coinsurance_Percentage { get; set; }
    }
}
