//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Web.SubmittedPolicies.Dal
{
    using System;
    
    public partial class SP_GET_PAYMENTS_PAYMENTS_DOCUMENT_Result
    {
        public Nullable<int> Corp_Id { get; set; }
        public Nullable<int> Region_Id { get; set; }
        public Nullable<int> Country_Id { get; set; }
        public Nullable<int> Domesticreg_Id { get; set; }
        public Nullable<int> State_Prov_Id { get; set; }
        public Nullable<int> City_Id { get; set; }
        public Nullable<int> Office_Id { get; set; }
        public Nullable<int> Case_Seq_No { get; set; }
        public Nullable<int> Hist_Seq_No { get; set; }
        public int Doc_Category_Id { get; set; }
        public int Doc_Type_Id { get; set; }
        public int Document_Id { get; set; }
        public string Document_Name { get; set; }
        public byte[] Document_Binary { get; set; }
        public Nullable<System.DateTime> File_Creation_Date { get; set; }
        public string Doc_Type_Desc { get; set; }
        public string Content_Type { get; set; }
        public string Extension { get; set; }
    }
}
