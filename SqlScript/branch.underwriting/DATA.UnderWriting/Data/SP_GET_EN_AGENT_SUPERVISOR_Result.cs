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
    
    public partial class SP_GET_EN_AGENT_SUPERVISOR_Result
    {
        public int Corp_Id { get; set; }
        public int Chain_Id { get; set; }
        public int Chain_Det_Id { get; set; }
        public int Agent_Id { get; set; }
        public int Order_Id { get; set; }
        public int Chain_Level_Id { get; set; }
        public bool Agent_Chain_Status { get; set; }
        public Nullable<int> Relationship_To_Supervisor { get; set; }
        public Nullable<int> Supervisor_Agent_Id { get; set; }
        public string Agent_Code { get; set; }
        public string FullName { get; set; }
        public System.DateTime Create_Date { get; set; }
        public Nullable<System.DateTime> Date_Assigned { get; set; }
        public Nullable<System.DateTime> Date_Unassigned { get; set; }
    }
}