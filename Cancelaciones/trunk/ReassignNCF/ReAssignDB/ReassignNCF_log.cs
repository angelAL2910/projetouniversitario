//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ReAssignNCF
{
    using System;
    using System.Collections.Generic;
    
    internal partial class ReassignNCF_log
    {
        public int id { get; set; }
        public string Poliza { get; set; }
        public decimal Valor_old { get; set; }
        public decimal ValorItbis_old { get; set; }
        public string Ncf_old { get; set; }
        public decimal Valor_new { get; set; }
        public decimal ValorItbis_new { get; set; }
        public string Ncf_new { get; set; }
        public System.DateTime fecha_actualizacion { get; set; }
        public string Factura_old { get; set; }
        public string Factura_new { get; set; }
    }
}