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
    
    public partial class SP_PENDING_POLICIES_INFORMATION_DETAIL_Result
    {
        public string POLICY_NO { get; set; }
        public string Owner_name { get; set; }
        public string Family_product { get; set; }
        public string Product_name { get; set; }
        public string Plan_type { get; set; }
        public string Insured { get; set; }
        public string Currency { get; set; }
        public string Frequency { get; set; }
        public string Investment_profile { get; set; }
        public string Additional_Insured { get; set; }
        public Nullable<int> Contribution_period { get; set; }
        public Nullable<decimal> Initial_contribution { get; set; }
        public Nullable<decimal> Financial_goal { get; set; }
        public string Education_period { get; set; }
        public int Deferment_period { get; set; }
        public string Retirement_Period { get; set; }
        public string Designated_pensioner { get; set; }
        public decimal Total_Insured_Amount { get; set; }
        public decimal Annual_premium { get; set; }
        public Nullable<decimal> Periodic_premium { get; set; }
        public Nullable<decimal> Target_annual_premium { get; set; }
        public Nullable<decimal> Minimum_annual_premium { get; set; }
        public int Student_Age_At_Start_Of_Retirement { get; set; }
        public int Prospect_Age_At_Start_Of_Retirement { get; set; }
        public string InsuredPeriod { get; set; }
        public decimal ReturnOfPremium { get; set; }
    }
}