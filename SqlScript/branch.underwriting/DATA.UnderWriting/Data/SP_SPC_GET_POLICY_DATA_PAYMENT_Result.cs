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
    
    public partial class SP_SPC_GET_POLICY_DATA_PAYMENT_Result
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
        public int Payment_Id { get; set; }
        public Nullable<decimal> Due_Amount { get; set; }
        public Nullable<decimal> Periodic_Premium_Amount { get; set; }
        public Nullable<decimal> Base_Premium { get; set; }
        public Nullable<decimal> Exceptional_premium { get; set; }
        public Nullable<decimal> Exceptional_premium2 { get; set; }
        public Nullable<decimal> Base_Commision { get; set; }
        public Nullable<decimal> Base_Commision2 { get; set; }
        public Nullable<decimal> Exceptional_Commisions { get; set; }
        public Nullable<decimal> Exceptional2_Commisions { get; set; }
        public Nullable<System.DateTime> Due_Date { get; set; }
        public Nullable<System.DateTime> Paid_Date { get; set; }
        public Nullable<decimal> Paid_Amount { get; set; }
        public int Payment_Status_Id { get; set; }
        public string Payment_Status_Desc { get; set; }
        public System.DateTime Create_Date { get; set; }
        public Nullable<System.DateTime> Modi_Date { get; set; }
        public int Create_UsrId { get; set; }
        public Nullable<int> Modi_UsrId { get; set; }
        public string Source_ID { get; set; }
    }
}
