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
    
    public partial class SP_GET_PL_PCY_REINSURANCE_FACULTATIVE_Result
    {
        public int Corp_Id { get; set; }
        public int Region_Id { get; set; }
        public int Country_Id { get; set; }
        public int Domesticreg_Id { get; set; }
        public int State_Prov_Id { get; set; }
        public int City_Id { get; set; }
        public int Office_Id { get; set; }
        public int Case_seq_No { get; set; }
        public int Hist_Seq_No { get; set; }
        public Nullable<int> Rider_Type_Id { get; set; }
        public Nullable<int> Rider_Id { get; set; }
        public string Coverage_Type_Desc { get; set; }
        public Nullable<decimal> Beneficiary_Amount { get; set; }
        public Nullable<System.DateTime> Requested_Date { get; set; }
        public Nullable<System.DateTime> Processed_Date { get; set; }
        public Nullable<decimal> Company_Risk_Amount { get; set; }
        public Nullable<decimal> Reinsurance_Risk_Amount { get; set; }
        public Nullable<decimal> Authorized_Amount { get; set; }
        public string Risk_Rating_Table { get; set; }
        public Nullable<decimal> Risk_Rating_Amount { get; set; }
        public Nullable<decimal> Per_Thousend_Risk_Amount { get; set; }
        public string Facultative_Reinsurance_Id { get; set; }
        public int Facultative_Status_Id { get; set; }
        public string Facultative_Status_Desc { get; set; }
        public bool Reinsurance_Facultative_Status { get; set; }
    }
}