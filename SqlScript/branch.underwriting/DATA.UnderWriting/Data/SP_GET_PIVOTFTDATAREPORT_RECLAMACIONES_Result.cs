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
    
    public partial class SP_GET_PIVOTFTDATAREPORT_RECLAMACIONES_Result
    {
        public int Id { get; set; }
        public Nullable<int> Corp_Id { get; set; }
        public Nullable<int> Company_Id { get; set; }
        public string Company_Desc { get; set; }
        public Nullable<int> Bl_Id { get; set; }
        public string Bl_Desc { get; set; }
        public Nullable<int> Agent_Id { get; set; }
        public Nullable<int> Supervisor_Agent_Id { get; set; }
        public string Agent_Name { get; set; }
        public string Manager { get; set; }
        public string Canal { get; set; }
        public string Office_Desc { get; set; }
        public string Agent_Status { get; set; }
        public Nullable<int> Reclamacion { get; set; }
        public string EstatusReclamacion { get; set; }
        public Nullable<decimal> MontoAjustado { get; set; }
        public Nullable<decimal> MontoReclamado { get; set; }
        public Nullable<decimal> ReclamoPagado { get; set; }
        public Nullable<decimal> MontoPendientePago { get; set; }
        public Nullable<decimal> MontoPagado { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<int> Month { get; set; }
        public Nullable<int> Day { get; set; }
        public Nullable<System.DateTime> DateApertura { get; set; }
        public Nullable<System.DateTime> DateSiniestro { get; set; }
        public Nullable<System.DateTime> DateCierre { get; set; }
        public Nullable<bool> Declinada { get; set; }
        public string Poliza { get; set; }
        public string Producto { get; set; }
        public string TipoReclamacion { get; set; }
        public string Sexo { get; set; }
        public Nullable<int> Age { get; set; }
        public string PivotDay { get; set; }
        public string PivotMonth { get; set; }
        public string PivotQuarter { get; set; }
        public string PivotSemestral { get; set; }
    }
}
