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
    
    public partial class GetListPaymentDetailData_old_Result
    {
        public string PaymentKey { get; set; }
        public Nullable<int> Directory_id { get; set; }
        public Nullable<int> Country_id { get; set; }
        public string Country_desc { get; set; }
        public Nullable<int> City_id { get; set; }
        public string City_desc { get; set; }
        public Nullable<int> Office_id { get; set; }
        public string Office_desc { get; set; }
        public string Policy_no { get; set; }
        public string ClientFirstName { get; set; }
        public string ClientLastName { get; set; }
        public string AgentFirstName { get; set; }
        public string AgentLastName { get; set; }
        public string Product_desc { get; set; }
        public Nullable<bool> IsInsured { get; set; }
        public string PaymentFrequency { get; set; }
        public Nullable<decimal> Premiun_Amount { get; set; }
        public string PaymentType { get; set; }
        public string Provider { get; set; }
        public string AccountNumberIdentifier { get; set; }
        public string AccountNumber { get; set; }
        public System.DateTime AccountDate { get; set; }
        public decimal Paid_amount { get; set; }
        public decimal InCurrencyPaid_amount { get; set; }
        public Nullable<System.DateTime> Paid_date { get; set; }
        public int PaymentDet_Id { get; set; }
        public string MethodofPayment { get; set; }
        public string Payment_status_desc { get; set; }
        public int Payment_status_id { get; set; }
    }
}