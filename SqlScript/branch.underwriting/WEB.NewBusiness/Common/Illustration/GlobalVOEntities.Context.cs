﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WEB.NewBusiness.Common.Illustration
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GlobalVOEntities : DbContext
    {
        public GlobalVOEntities()
            : base("name=GlobalVOEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<VwPivotIllustration_en> VwPivotIllustration_en { get; set; }
        public virtual DbSet<VwPivotIllustration_es> VwPivotIllustration_es { get; set; }
    }
}