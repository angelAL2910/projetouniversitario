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
    
    public partial class SP_GET_PL_PCY_ALLIEDLINES_REVIEW_DETAILS_Result
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
        public int AlliedLine_Id { get; set; }
        public long Unique_AlliedLine_Id { get; set; }
        public int AlliedLine_Type_Id { get; set; }
        public string AlliedLine_Type_Desc { get; set; }
        public int Review_Detail_Id { get; set; }
        public int Review_Group_Id { get; set; }
        public string Review_Group_Desc { get; set; }
        public int Review_Class_Id { get; set; }
        public string Review_Class_Desc { get; set; }
        public int Review_Item_Id { get; set; }
        public string Review_Item_Desc { get; set; }
        public int Review_Option_Id { get; set; }
        public string Review_Option_Desc { get; set; }
        public bool Review_Option_Endorsement_Clarifying { get; set; }
        public bool Review_Group_Endorsement_Clarifying { get; set; }
        public int Value_Checked { get; set; }
        public string Value_Text { get; set; }
        public Nullable<bool> Review_Status { get; set; }
        public Nullable<bool> Required { get; set; }
        public string UsuarioInspeccion { get; set; }
    }
}
