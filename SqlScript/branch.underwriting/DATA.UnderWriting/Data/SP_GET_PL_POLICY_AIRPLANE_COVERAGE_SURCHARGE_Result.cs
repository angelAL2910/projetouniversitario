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
    
    public partial class SP_GET_PL_POLICY_AIRPLANE_COVERAGE_SURCHARGE_Result
    {
        public int Corp_Id { get; set; }
        public int Region_Id { get; set; }
        public int Country_Id { get; set; }
        public long Unique_Airplane_Id { get; set; }
        public int Bl_Type_Id { get; set; }
        public int Bl_Id { get; set; }
        public int Product_Id { get; set; }
        public int Vehicle_Type_Id { get; set; }
        public int Group_Id { get; set; }
        public int Coverage_Type_Id { get; set; }
        public int Coverage_Id { get; set; }
        public int Surcharge_Id { get; set; }
        public int Discount_Rule_Id { get; set; }
        public int Discount_Rule_Detail_Id { get; set; }
        public decimal Old_Coverage_Amount { get; set; }
        public Nullable<int> Note_Predefinied_Id { get; set; }
        public string Discount_Rule_Desc { get; set; }
        public string Discount_Rule_Name_Key { get; set; }
        public System.DateTime Detail_Apply_Date { get; set; }
        public string Detail_Rule_Value { get; set; }
        public string Detail_Rule_NameKey { get; set; }
        public string Note_Predefinied_Desc { get; set; }
        public string Note_Name_Key { get; set; }
        public decimal Base_Premium_Amount { get; set; }
    }
}
