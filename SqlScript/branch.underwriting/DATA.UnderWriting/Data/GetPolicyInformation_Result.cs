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
    
    public partial class GetPolicyInformation_Result
    {
        public string PolicyKey { get; set; }
        public string Policy_No { get; set; }
        public decimal AccountValue { get; set; }
        public string Product_desc { get; set; }
        public string Serie_Desc { get; set; }
        public Nullable<System.DateTime> Policy_effective_date { get; set; }
        public string PROFILE_TYPE_DESC { get; set; }
        public string Policy_status_desc { get; set; }
        public Nullable<decimal> Benefit_Amount { get; set; }
        public bool Smoker { get; set; }
        public string Currency_Desc { get; set; }
        public Nullable<decimal> Annual_Premium { get; set; }
        public string Payment_Freq_Type_Desc { get; set; }
        public Nullable<decimal> Minimun_Premiun_Amount { get; set; }
        public Nullable<int> Year_Contribution_Period { get; set; }
        public string InsuranceTerm { get; set; }
        public string Deferment_Period { get; set; }
        public Nullable<int> Bl_Id { get; set; }
        public string Bl_Desc { get; set; }
        public string BenefitPeriod { get; set; }
        public Nullable<decimal> Target_Premium { get; set; }
        public Nullable<System.DateTime> Initial_Benefit_Date { get; set; }
        public Nullable<System.DateTime> Benefit_Ending_Date { get; set; }
        public Nullable<decimal> Modal_Premium { get; set; }
        public Nullable<System.DateTime> Term_Date { get; set; }
        public string PaymentType { get; set; }
        public string Provider { get; set; }
        public string AccountNumberIdentifier { get; set; }
        public string AccountNumber { get; set; }
        public Nullable<System.DateTime> AccountDate { get; set; }
    }
}
