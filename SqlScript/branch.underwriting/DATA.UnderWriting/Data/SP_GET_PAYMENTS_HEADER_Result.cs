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
    
    public partial class SP_GET_PAYMENTS_HEADER_Result
    {
        public string Policy_No { get; set; }
        public int Corp_Id { get; set; }
        public int Region_Id { get; set; }
        public int Country_Id { get; set; }
        public int Domesticreg_Id { get; set; }
        public int State_Prov_Id { get; set; }
        public int City_Id { get; set; }
        public int Office_Id { get; set; }
        public int Case_seq_No { get; set; }
        public int Hist_Seq_No { get; set; }
        public int Payment_Freq_Type_Id { get; set; }
        public int Payment_Freq_Id { get; set; }
        public Nullable<decimal> Annual_Premium { get; set; }
        public string Payment_Freq_Type_Desc { get; set; }
        public Nullable<decimal> Premium_Recieved { get; set; }
        public Nullable<decimal> Account_Amount { get; set; }
        public Nullable<decimal> Periodic_Premium { get; set; }
        public Nullable<decimal> Minimun_Premiun_Amount { get; set; }
        public decimal Initial_Contribution { get; set; }
        public Nullable<System.DateTime> Policy_Effective_Date { get; set; }
        public Nullable<decimal> Minimun_Premiun_Amount_Annual { get; set; }
        public string Payment_Plan { get; set; }
        public System.DateTime Payment_Plan_End_Date { get; set; }
        public System.DateTime Payment_Plan_Start_Date { get; set; }
        public Nullable<System.DateTime> Payment_Date { get; set; }
        public bool Payment_Status { get; set; }
        public Nullable<int> PolicyYear { get; set; }
        public Nullable<decimal> PaymentAmount { get; set; }
    }
}
