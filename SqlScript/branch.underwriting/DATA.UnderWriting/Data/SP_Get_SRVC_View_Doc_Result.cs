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
    
    public partial class SP_Get_SRVC_View_Doc_Result
    {
        public int Corp_id { get; set; }
        public int Bl_Id { get; set; }
        public int Contact_Id { get; set; }
        public Nullable<int> Call_Meeting_Id { get; set; }
        public Nullable<int> Note_id { get; set; }
        public int FieldId { get; set; }
        public string Applications { get; set; }
        public string FileNames { get; set; }
        public Nullable<int> FileSize { get; set; }
        public string ContentType { get; set; }
        public string FileExtention { get; set; }
        public byte[] FileContent { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int UsrId { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> UsrModified { get; set; }
    }
}