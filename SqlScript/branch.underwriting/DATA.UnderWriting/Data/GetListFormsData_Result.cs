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
    
    public partial class GetListFormsData_Result
    {
        public int Doc_Type_Id { get; set; }
        public int Doc_Category_Id { get; set; }
        public string Doc_Category_Desc { get; set; }
        public Nullable<int> Doc_Category_Parent_Id { get; set; }
        public string Doc_Category_Parent_Desc { get; set; }
        public int Document_Id { get; set; }
        public byte[] Document_Binary { get; set; }
        public string Document_Desc { get; set; }
        public string Document_Name { get; set; }
        public Nullable<System.DateTime> File_Creation_Date { get; set; }
        public Nullable<System.DateTime> File_Expire_Date { get; set; }
        public bool Document_Status { get; set; }
        public System.DateTime Create_Date { get; set; }
        public Nullable<System.DateTime> Modi_Date { get; set; }
        public int Create_UsrId { get; set; }
        public Nullable<int> Modi_UsrId { get; set; }
        public string Hostname { get; set; }
        public string Source_Id { get; set; }
    }
}