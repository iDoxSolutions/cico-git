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
    
    internal partial class Employee_Mapping : EntityTypeConfiguration<Employee>
    {
        public Employee_Mapping()
        {                        
              this.HasKey(t => t.EmployeeId);        
              this.ToTable("Employees");
              this.Property(t => t.EmployeeId).HasColumnName("EmployeeId");
              this.Property(t => t.LastName).HasColumnName("LastName").IsRequired();
              this.Property(t => t.MiddleName).HasColumnName("MiddleName").IsRequired();
              this.Property(t => t.FirstName).HasColumnName("FirstName").IsRequired();
              this.Property(t => t.PreferredName).HasColumnName("PreferredName").IsRequired();
              this.Property(t => t.DateOfBirth).HasColumnName("DateOfBirth").IsRequired();
              this.Property(t => t.Title).HasColumnName("Title").IsRequired();
              this.Property(t => t.Nationality).HasColumnName("Nationality").IsRequired();
              this.Property(t => t.HomePhone).HasColumnName("HomePhone").IsRequired();
              this.Property(t => t.CellPhone).HasColumnName("CellPhone").IsRequired();
              this.Property(t => t.TourStartDate).HasColumnName("TourStartDate").IsRequired();
              this.Property(t => t.TourEndDate).HasColumnName("TourEndDate").IsRequired();
              this.Property(t => t.AgencyOrSection).HasColumnName("AgencyOrSection").IsRequired();
              this.Property(t => t.PositionTitle).HasColumnName("PositionTitle").IsRequired();
              this.Property(t => t.HomeStreetAddress).HasColumnName("HomeStreetAddress").IsRequired();
              this.Property(t => t.HomeStreetAddress2).HasColumnName("HomeStreetAddress2").IsRequired();
              this.Property(t => t.HomeState).HasColumnName("HomeState").IsRequired();
              this.Property(t => t.HomeCity).HasColumnName("HomeCity").IsRequired();
              this.Property(t => t.HomeZip).HasColumnName("HomeZip").IsRequired();
              this.Property(t => t.AgencyEmail).HasColumnName("AgencyEmail").IsRequired();
              this.Property(t => t.PersonalEmail).HasColumnName("PersonalEmail").IsRequired();
              this.Property(t => t.UserId).HasColumnName("UserId").IsRequired();
              this.Property(t => t.LastLogin).HasColumnName("LastLogin").IsRequired();
         }
    }
}
