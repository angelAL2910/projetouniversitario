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
    
    public partial class SP_GET_PL_POLICY_BAIL_INSURED_Result
    {
        public Nullable<int> Equipment_Qty { get; set; }
        public Nullable<System.DateTime> Insured_Date { get; set; }
        public decimal Insured_Amount { get; set; }
        public decimal Rate { get; set; }
        public decimal Premium_Amount { get; set; }
        public decimal Base_Premium_Amount { get; set; }
        public decimal Deductible_Percentage { get; set; }
        public decimal Deductible_Amount { get; set; }
        public decimal Minimum_Deductible_Amount { get; set; }
        public bool New { get; set; }
        public bool Requires_Inspection { get; set; }
        public bool Reinsurance { get; set; }
        public bool Inspected { get; set; }
        public bool Endorsement_Clarifying { get; set; }
        public bool Endorsement { get; set; }
        public string Endorsement_Beneficiary { get; set; }
        public string Endorsement_Beneficiary_Rnc { get; set; }
        public Nullable<decimal> Endorsement_Amount { get; set; }
        public string Endorsement_Contact_Name { get; set; }
        public string Endorsement_Contact_Phone { get; set; }
        public string Endorsement_Contact_Email { get; set; }
        public string Inspection_Address { get; set; }
        public int Bail_Status_Id { get; set; }
        public int Corp_Id { get; set; }
        public int Region_Id { get; set; }
        public int Country_Id { get; set; }
        public int Domesticreg_Id { get; set; }
        public int State_Prov_Id { get; set; }
        public int City_Id { get; set; }
        public int Office_Id { get; set; }
        public int Case_Seq_No { get; set; }
        public int Hist_Seq_No { get; set; }
        public int Bail_Id { get; set; }
        public long Unique_Bail_Id { get; set; }
        public int Bl_Type_Id { get; set; }
        public int Bl_Id { get; set; }
        public int Product_Id { get; set; }
        public string Product_Desc { get; set; }
        public Nullable<int> Product_Type_Id { get; set; }
        public int Reinsurance_Id { get; set; }
        public decimal Reinsurance_Amount { get; set; }
        public Nullable<decimal> Reinsurance_Percentage { get; set; }
        public Nullable<decimal> ContractAmount { get; set; }
        public string Beneficiary { get; set; }
        public string Activity { get; set; }
        public string BusinessType { get; set; }
        public string BailType { get; set; }
        public string PercentageInsuredAmount { get; set; }
        public string Has_End_Of_Term { get; set; }
        public string Obligations { get; set; }
        public string To_Deposit_In { get; set; }
        public string Address_Street { get; set; }
        public string Address_Number { get; set; }
        public Nullable<int> Address_Country_Id { get; set; }
        public Nullable<int> Address_Domesticreg_Id { get; set; }
        public Nullable<int> Address_State_Prov_Id { get; set; }
        public Nullable<int> Address_City_Id { get; set; }
        public string Is_Building { get; set; }
        public Nullable<decimal> Reinsurance_Premium_Amount { get; set; }
        public Nullable<int> Ramo { get; set; }
        public Nullable<int> SubRamo { get; set; }
        public string Address_Country_Desc { get; set; }
        public string Address_Domesticreg_Desc { get; set; }
        public string Address_State_Prov_Desc { get; set; }
        public string Address_Municipio_Desc { get; set; }
        public string Address_City_Desc { get; set; }
    }
}