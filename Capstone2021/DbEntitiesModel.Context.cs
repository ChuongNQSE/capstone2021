﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Capstone2021
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DbEntities : DbContext
    {
        public DbEntities()
            : base("name=DbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<category> categories { get; set; }
        public virtual DbSet<job> jobs { get; set; }
        public virtual DbSet<job_has_category> job_has_category { get; set; }
        public virtual DbSet<manager> managers { get; set; }
        public virtual DbSet<student> students { get; set; }
        public virtual DbSet<student_apply_job> student_apply_job { get; set; }
        public virtual DbSet<student_save_job> student_save_job { get; set; }
        public virtual DbSet<cv> cvs { get; set; }
        public virtual DbSet<recruiter> recruiters { get; set; }
        public virtual DbSet<company> companies { get; set; }
    }
}
