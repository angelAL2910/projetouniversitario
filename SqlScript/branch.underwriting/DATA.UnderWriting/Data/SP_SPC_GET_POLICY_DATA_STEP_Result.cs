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
    
    public partial class SP_SPC_GET_POLICY_DATA_STEP_Result
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
        public string Step_Type_Desc { get; set; }
        public int Step_Id { get; set; }
        public string Step_Desc { get; set; }
        public string Step_Code { get; set; }
        public int Step_Case_No { get; set; }
        public string Step_Seq_Reference { get; set; }
        public Nullable<System.DateTime> Process_Date { get; set; }
        public bool Automatic { get; set; }
        public int ProcessStatus { get; set; }
        public System.DateTime Create_Date { get; set; }
        public Nullable<System.DateTime> Modi_Date { get; set; }
        public int Create_UsrId { get; set; }
        public Nullable<int> Modi_UsrId { get; set; }
        public string Source_ID { get; set; }
    }
}
