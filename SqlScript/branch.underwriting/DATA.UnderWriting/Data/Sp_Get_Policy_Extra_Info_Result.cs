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
    
    public partial class Sp_Get_Policy_Extra_Info_Result
    {
        public int Agent_Id { get; set; }
        public int Distribution_Id { get; set; }
        public string Distribution_Desc { get; set; }
        public string Policy_No { get; set; }
        public int Product_ID { get; set; }
        public int Product_Type_Id { get; set; }
        public string Product_Type_Desc { get; set; }
        public Nullable<bool> Additional_Doc { get; set; }
        public Nullable<int> Bussiness_Line_Id { get; set; }
    }
}