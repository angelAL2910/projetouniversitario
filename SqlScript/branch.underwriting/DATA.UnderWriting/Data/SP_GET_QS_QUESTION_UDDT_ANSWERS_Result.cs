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
    
    public partial class SP_GET_QS_QUESTION_UDDT_ANSWERS_Result
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
        public int Questionnaire_Id { get; set; }
        public int Questionnaire_Seq { get; set; }
        public int Question_Id { get; set; }
        public int Option_Id { get; set; }
        public Nullable<int> Language_Id { get; set; }
        public string Textual_Answer { get; set; }
        public Nullable<System.DateTime> Date_Answer { get; set; }
        public bool Answer_Status { get; set; }
        public System.DateTime Create_Date { get; set; }
        public Nullable<System.DateTime> Modi_Date { get; set; }
        public int Create_UsrId { get; set; }
        public Nullable<int> Modi_UsrId { get; set; }
        public string HostName { get; set; }
    }
}
