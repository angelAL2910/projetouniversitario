//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Web.SubmittedPolicies.Dal
{
    using System;
    using System.Collections.Generic;
    
    public partial class VW_GET_POLICY_NEVER_ISSUED
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
        public int Step_Type_Id { get; set; }
        public int Step_Id { get; set; }
        public Nullable<int> Step_Case_No { get; set; }
        public string Policy_No { get; set; }
        public string OwnerFullName { get; set; }
        public System.DateTime ApplicationDate { get; set; }
        public string Product_Desc { get; set; }
        public Nullable<decimal> Initial_Premium { get; set; }
        public Nullable<decimal> Insured_Amount { get; set; }
        public string InsuredFullName { get; set; }
        public string Payment_Freq_Type_Desc { get; set; }
        public string MethodOfPayment { get; set; }
        public Nullable<System.DateTime> FirstPaymentDate { get; set; }
        public System.DateTime LastPaymentDate { get; set; }
        public string AgentFullName { get; set; }
        public string Office_Desc { get; set; }
        public int Company_Id { get; set; }
        public Nullable<int> Step_Reason_Id { get; set; }
        public string Step_Reason_Desc { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<bool> Penalty { get; set; }
    }
}
