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
    
    public partial class Tipo_Movimiento
    {
        public byte Codigo { get; set; }
        public byte Compania { get; set; }
        public string Siglas { get; set; }
        public string Descripcion { get; set; }
        public Nullable<int> Contador { get; set; }
        public Nullable<byte> AceptaBanco { get; set; }
        public Nullable<byte> Concilia { get; set; }
        public Nullable<byte> TipoContrario { get; set; }
        public string Usuario { get; set; }
        public Nullable<byte> Origen { get; set; }
        public Nullable<int> Contador_B { get; set; }
    }
}