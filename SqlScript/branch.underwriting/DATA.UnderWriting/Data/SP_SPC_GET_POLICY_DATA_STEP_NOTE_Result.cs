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
    
    public partial class SP_SPC_GET_POLICY_DATA_STEP_NOTE_Result
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
        public int Step_Type_Id { get; set; }
        public int Step_Id { get; set; }
        public int Step_Case_No { get; set; }
        public int Note_Id { get; set; }
        public Nullable<int> Contact_Id { get; set; }
        public Nullable<int> Contact_Role_Type_Id { get; set; }
        public Nullable<int> Note_Type_Id { get; set; }
        public Nullable<int> Reference_Id { get; set; }
        public string Note_Name { get; set; }
        public Nullable<System.DateTime> Date_Added { get; set; }
        public Nullable<System.DateTime> Date_Modified { get; set; }
        public int Originated_By { get; set; }
        public string Originated_By_Name { get; set; }
        public string Note_Desc { get; set; }
        public Nullable<int> Underwriter_Id { get; set; }
        public string UnderwriterName { get; set; }
        public string Note_Type_Desc { get; set; }
        public string Roles { get; set; }
        public string Full_Name { get; set; }
        public string SourceSystem { get; set; }
    }
}
