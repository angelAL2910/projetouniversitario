//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KSI.Cobranza.DataLayer.EFModel
{
    using System;
    
    public partial class SP_GET_DATA_FIELD_Result
    {
        public int IdField { get; set; }
        public string FieldName { get; set; }
        public int IdFieldType { get; set; }
        public string FieldTypeName { get; set; }
        public string ColumnName { get; set; }
        public int OrderForm { get; set; }
        public string cssClass { get; set; }
        public int IdFieldGroup { get; set; }
        public bool IsMoney { get; set; }
        public bool IsPercent { get; set; }
        public string FormatString { get; set; }
        public bool IsReadOnly { get; set; }
        public bool IsVisibleCrud { get; set; }
        public bool IsVisibleGrid { get; set; }
        public long QueueId { get; set; }
        public int Sequence { get; set; }
        public string DatoStr { get; set; }
        public Nullable<decimal> DatoNum { get; set; }
        public Nullable<int> DatoInt { get; set; }
        public Nullable<System.DateTime> DatoDat { get; set; }
        public bool DatoBit { get; set; }
        public string DatoText { get; set; }
        public Nullable<int> IdCatalogHeader { get; set; }
        public string Code { get; set; }
    }
}