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
    
    public partial class SP_GET_MEMBER_COVERAGE_PREMIUM_Result
    {
        public int Coverage_Type_Id { get; set; }
        public int Coverage_Id { get; set; }
        public string Coverage_Desc { get; set; }
        public int Contact_Id { get; set; }
        public int Contact_Role_Type_Id { get; set; }
        public string Contact_First_Name { get; set; }
        public string Contact_Last_Name { get; set; }
        public int Product_id { get; set; }
        public string Product_Desc { get; set; }
        public int Benefit_Plan_Id { get; set; }
        public string Benefit_Plan_Desc { get; set; }
        public decimal Deductible { get; set; }
        public Nullable<decimal> Insured_Amount { get; set; }
        public Nullable<decimal> Premium_Amount { get; set; }
    }
}