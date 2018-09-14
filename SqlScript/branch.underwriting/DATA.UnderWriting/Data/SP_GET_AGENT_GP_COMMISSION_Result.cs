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
    
    public partial class SP_GET_AGENT_GP_COMMISSION_Result
    {
        public int Corp_Id { get; set; }
        public int Agent_Id { get; set; }
        public Nullable<int> Unique_Agent_Id { get; set; }
        public int Office_Id { get; set; }
        public string Office_Desc { get; set; }
        public string Country_Desc { get; set; }
        public int Bl_Id { get; set; }
        public string Bl_Desc { get; set; }
        public string Agent_Code { get; set; }
        public string Name_Id { get; set; }
        public string Agent_Identifier { get; set; }
        public string Vendor_Id { get; set; }
        public string Distribution_Desc { get; set; }
        public int Chain_Level_Id { get; set; }
        public string Agent_FullName { get; set; }
        public string Id { get; set; }
        public int IdTypeId { get; set; }
        public string IdTypeDesc { get; set; }
        public string Gender { get; set; }
        public int Agent_Status_Id { get; set; }
        public string Agent_Status_Desc { get; set; }
        public Nullable<int> Supervisor_Agent_Id { get; set; }
        public string Supervisor_Agent_Code { get; set; }
        public int Payment_Type_Id { get; set; }
        public string Payment_Type_Desc { get; set; }
        public int Bank_Account_Type_Id { get; set; }
        public string Bnk_Account_Type_Desc { get; set; }
        public string Bnk_Account_Type_Code { get; set; }
        public string Bank_Account_Number { get; set; }
        public string Aba_Number { get; set; }
        public string Bank_Desc { get; set; }
        public int CheckDigit { get; set; }
        public System.DateTime Inactive_Date { get; set; }
        public int Company_Id { get; set; }
        public string Company_Desc { get; set; }
        public int Directory_Id { get; set; }
        public string PhoneNumber { get; set; }
        public string CellNumber { get; set; }
        public string OfficeNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int Commission_Behavior_Id { get; set; }
        public string Commission_Behavior_Desc { get; set; }
        public string Commission_Behavior_Name_Key { get; set; }
        public string Source_Id { get; set; }
        public decimal Indice_Current { get; set; }
        public decimal Indice_Value { get; set; }
        public int License_Id { get; set; }
        public string License { get; set; }
        public System.DateTime License_Start_Date { get; set; }
        public System.DateTime License_End_Date { get; set; }
        public System.DateTime License_Revalidation_Date { get; set; }
        public int License_Country_Id { get; set; }
    }
}