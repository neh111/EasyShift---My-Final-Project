﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EasyShiftEntities : DbContext
    {
        public EasyShiftEntities()
            : base("name=EasyShiftEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Employee_request_tbl> Employee_request_tbl { get; set; }
        public virtual DbSet<Employee_tbl> Employee_tbl { get; set; }
        public virtual DbSet<Jobs_tbl> Jobs_tbl { get; set; }
        public virtual DbSet<Jobs_to_shift_tbl> Jobs_to_shift_tbl { get; set; }
        public virtual DbSet<PlacementResults_tbl> PlacementResults_tbl { get; set; }
        public virtual DbSet<Priority_tbl> Priority_tbl { get; set; }
        public virtual DbSet<Shift_tbl> Shift_tbl { get; set; }
        public virtual DbSet<ShiftType_tbl> ShiftType_tbl { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Settings_tbl> Settings_tbl { get; set; }
    }
}
