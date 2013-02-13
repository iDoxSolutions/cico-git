//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cico.Models
{
    #pragma warning disable 1573
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Common;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration;
    using System.Data.Entity.Infrastructure;
    
    internal partial class Department_Mapping : EntityTypeConfiguration<Department>
    {
        public Department_Mapping()
        {                        
              this.HasKey(t => t.Id);        
              this.ToTable("Departments");
              this.Property(t => t.Id).HasColumnName("Id");
              this.Property(t => t.Name).HasColumnName("Name").IsRequired();
              this.Property(t => t.Contact).HasColumnName("Contact").IsRequired();
              this.Property(t => t.CheckListId).HasColumnName("CheckListId");
              this.HasRequired(t => t.CheckList).WithMany(t => t.Departments).HasForeignKey(d => d.CheckListId);
         }
    }
}