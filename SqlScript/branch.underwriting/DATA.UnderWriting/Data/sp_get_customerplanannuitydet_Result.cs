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
    
    public partial class sp_get_customerplanannuitydet_Result
    {
        public long customerplanannuityno { get; set; }
        public long customerplanno { get; set; }
        public int age { get; set; }
        public int year { get; set; }
        public Nullable<decimal> accumulatedcontributions { get; set; }
        public Nullable<decimal> deathbenefit { get; set; }
        public Nullable<decimal> benefitexclusion { get; set; }
        public Nullable<decimal> accountvalue { get; set; }
        public Nullable<decimal> surrendervalue { get; set; }
        public Nullable<decimal> annualpartialwithdrawal { get; set; }
    }
}
