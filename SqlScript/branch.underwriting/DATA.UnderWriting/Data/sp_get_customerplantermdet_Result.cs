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
    
    public partial class sp_get_customerplantermdet_Result
    {
        public long customerplantermno { get; set; }
        public long customerplanno { get; set; }
        public int tableno { get; set; }
        public int age { get; set; }
        public int year { get; set; }
        public Nullable<decimal> primabasiccoverage { get; set; }
        public Nullable<decimal> premiumextras { get; set; }
        public Nullable<decimal> totalpremium { get; set; }
        public Nullable<decimal> accumulatedpremiums { get; set; }
        public Nullable<decimal> deathbenefit { get; set; }
    }
}