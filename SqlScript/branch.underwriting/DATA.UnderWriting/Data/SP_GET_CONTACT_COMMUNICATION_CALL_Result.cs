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
    
    public partial class SP_GET_CONTACT_COMMUNICATION_CALL_Result
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
        public Nullable<int> Communication_Type_Id { get; set; }
        public string Communication_Type_Desc { get; set; }
        public Nullable<int> Call_Id { get; set; }
        public int Call_Note_Id { get; set; }
        public Nullable<int> Note_Type_Id { get; set; }
        public string Note_Type_Desc { get; set; }
        public Nullable<int> Priority_Id { get; set; }
        public string Priority_Desc { get; set; }
        public Nullable<int> Category_Id { get; set; }
        public string Category_Desc { get; set; }
        public Nullable<int> Result_Id { get; set; }
        public string Result_Desc { get; set; }
        public string Subject { get; set; }
        public string Short_Text { get; set; }
        public string Large_Text { get; set; }
        public Nullable<int> Person_To_Contact { get; set; }
        public string Contacted_Person { get; set; }
        public Nullable<bool> Call_Direction_Outbound { get; set; }
        public Nullable<int> Contacted_By { get; set; }
        public string Duration { get; set; }
        public Nullable<bool> Timeless { get; set; }
        public Nullable<bool> Recurring { get; set; }
        public string Result { get; set; }
        public Nullable<System.DateTime> Completed_Date { get; set; }
        public Nullable<int> Completed_By_User_Id { get; set; }
        public Nullable<bool> Attachment { get; set; }
        public Nullable<bool> Has_Call { get; set; }
        public Nullable<System.DateTime> Date_Added { get; set; }
        public Nullable<System.DateTime> Date_Modified { get; set; }
        public Nullable<int> Originated_By { get; set; }
        public Nullable<int> Step_Type_Id { get; set; }
        public Nullable<int> Step_Id { get; set; }
        public Nullable<int> Step_Case_No { get; set; }
        public Nullable<System.DateTime> Date_Sent { get; set; }
        public string Callreg_Notehistoryid { get; set; }
        public string History_Id { get; set; }
        public Nullable<System.DateTime> Start_Date { get; set; }
        public Nullable<System.DateTime> End_Date { get; set; }
        public string CallerId_Number { get; set; }
        public string CallerId_Name { get; set; }
        public string Outbound_Number { get; set; }
        public string Recording_File { get; set; }
        public Nullable<System.DateTime> Processed_Date { get; set; }
        public string Processed_By { get; set; }
    }
}