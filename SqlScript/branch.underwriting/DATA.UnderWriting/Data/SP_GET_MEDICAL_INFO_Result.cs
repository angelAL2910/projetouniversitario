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
    
    public partial class SP_GET_MEDICAL_INFO_Result
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
        public int Contact_Id { get; set; }
        public int Contact_Role_Type_Id { get; set; }
        public Nullable<decimal> Health_Weigth { get; set; }
        public Nullable<int> Health_Weigth_Type_Id { get; set; }
        public Nullable<decimal> Health_Height { get; set; }
        public Nullable<int> Health_Heigth_Type_Id { get; set; }
        public Nullable<int> Health_Age { get; set; }
        public string Health_Gender { get; set; }
        public Nullable<bool> Health_Smoke { get; set; }
        public Nullable<int> Health_Excercise { get; set; }
        public Nullable<int> Health_Drugs { get; set; }
        public Nullable<int> Health_Systolic { get; set; }
        public Nullable<int> Health_Diastolic { get; set; }
        public Nullable<System.DateTime> Health_LastMedVisit { get; set; }
        public string Health_LastMed_Reason { get; set; }
        public string Health_LastMed_Result { get; set; }
        public string Health_Dr_Name { get; set; }
        public string Health_Dr_Address { get; set; }
        public string Health_Dr_Phone_Prefix { get; set; }
        public string Health_Dr_Phone_Area { get; set; }
        public string Health_Dr_Phone_Num { get; set; }
        public string Health_Medication { get; set; }
    }
}
