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
    
    public partial class SP_Get_CA_CALLREX_BY_NOTES_Result
    {
        public int Corp_Id { get; set; }
        public int Contact_Id { get; set; }
        public int Call_Meeting_Type_Id { get; set; }
        public int Call_Meeting_Id { get; set; }
        public int Note_Id { get; set; }
        public int Call_ID { get; set; }
        public string Call_Duration { get; set; }
        public Nullable<System.DateTime> Call_Start { get; set; }
        public Nullable<System.DateTime> Call_Stop { get; set; }
        public string Dialed_Number { get; set; }
        public string Extension { get; set; }
        public string File_Name { get; set; }
        public string Full_Patch_File { get; set; }
        public string Full_File_Patch { get; set; }
        public string Call_Log_ID { get; set; }
        public string CallRex_User_ID { get; set; }
        public string User_FirstName { get; set; }
        public string User_LastName { get; set; }
        public System.DateTime Create_Date { get; set; }
        public int Create_UsrId { get; set; }
    }
}