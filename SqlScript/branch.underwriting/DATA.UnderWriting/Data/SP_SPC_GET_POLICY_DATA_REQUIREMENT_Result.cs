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
    
    public partial class SP_SPC_GET_POLICY_DATA_REQUIREMENT_Result
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
        public int Requirement_Cat_Id { get; set; }
        public string Requirement_Cat_Desc { get; set; }
        public int Requirement_Type_Id { get; set; }
        public string Requirement_Type_Desc { get; set; }
        public int Requirement_Id { get; set; }
        public Nullable<int> Requirement_Doc_Id { get; set; }
        public Nullable<int> Step_Type_Id { get; set; }
        public Nullable<int> Step_Id { get; set; }
        public Nullable<int> Step_Case_No { get; set; }
        public bool Automatic { get; set; }
        public int Requested_By { get; set; }
        public string Requested_By_Name { get; set; }
        public System.DateTime Requested_Date { get; set; }
        public Nullable<System.DateTime> Received_Date { get; set; }
        public bool IsManual { get; set; }
        public bool Send_To_Reinsurance { get; set; }
        public System.DateTime Create_Date { get; set; }
        public int Create_UsrId { get; set; }
        public Nullable<System.DateTime> Modi_Date { get; set; }
        public Nullable<int> Modi_UsrId { get; set; }
        public Nullable<int> Doc_Type_Id { get; set; }
        public Nullable<int> Doc_Category_Id { get; set; }
        public Nullable<int> Document_Id { get; set; }
        public string Doc_Type_Desc { get; set; }
        public string Doc_Category_Desc { get; set; }
        public string Content_Type { get; set; }
        public string Extension { get; set; }
        public string NameKey { get; set; }
        public byte[] Document_Binary { get; set; }
        public string Document_Desc { get; set; }
        public string Document_Name { get; set; }
        public Nullable<System.DateTime> File_Creation_Date { get; set; }
        public Nullable<System.DateTime> File_Expire_Date { get; set; }
        public string Source_Id { get; set; }
        public Nullable<System.DateTime> Create_Date_D { get; set; }
        public Nullable<System.DateTime> Modi_Date_D { get; set; }
        public Nullable<int> Create_UsrId_D { get; set; }
        public Nullable<int> Modi_UsrId_D { get; set; }
    }
}
