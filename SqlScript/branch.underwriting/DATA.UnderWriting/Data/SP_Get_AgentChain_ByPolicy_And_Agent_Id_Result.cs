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
    
    public partial class SP_Get_AgentChain_ByPolicy_And_Agent_Id_Result
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
        public int Chain_Det_Id { get; set; }
        public Nullable<int> Agent_Id { get; set; }
        public int Order_Id { get; set; }
        public int Agent_Chain_Status { get; set; }
        public System.DateTime Create_Date { get; set; }
        public Nullable<int> Modi_Date { get; set; }
        public Nullable<short> Create_UsrId { get; set; }
        public Nullable<int> Modi_UsrID { get; set; }
        public string Hostname { get; set; }
        public string Source_ID { get; set; }
    }
}