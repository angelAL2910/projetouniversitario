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
    
    public partial class SP_GET_MD_CLASS_ATTRIB_DEFINITION_Result
    {
        public int Attrib_Type_Id { get; set; }
        public int Attrib_Class_Id { get; set; }
        public int Attrib_Def_Id { get; set; }
        public string Class_Def_Desc { get; set; }
        public Nullable<decimal> Class_Value { get; set; }
        public string Text_Value { get; set; }
        public Nullable<decimal> Min_Numeric_Value { get; set; }
        public Nullable<decimal> Max_Numeric_Value { get; set; }
        public string Function { get; set; }
        public string Descriptional { get; set; }
        public int Operator_Id { get; set; }
        public string Operator_Desc { get; set; }
        public string Operator_Symbol { get; set; }
        public Nullable<bool> IsCheck { get; set; }
        public Nullable<bool> IsApplied { get; set; }
    }
}