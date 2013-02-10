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
    using System;
    using System.Collections.Generic;
    
    public partial class Employee
    {
        public Employee()
        {
            this.Dependents = new HashSet<Dependent>();
            this.CheckLists = new HashSet<CheckList>();
        }
    
        public int EmployeeId { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string FirstName { get; set; }
        public string PreferredName { get; set; }
        public string DateOfBirth { get; set; }
        public string Title { get; set; }
        public string Nationality { get; set; }
        public string HomePhone { get; set; }
        public string CellPhone { get; set; }
        public string TourStartDate { get; set; }
        public string TourEndDate { get; set; }
        public string AgencyOrSection { get; set; }
        public string PositionTitle { get; set; }
        public string HomeStreetAddress { get; set; }
        public string HomeStreetAddress2 { get; set; }
        public string HomeState { get; set; }
        public string HomeCity { get; set; }
        public string HomeZip { get; set; }
        public string AgencyEmail { get; set; }
        public string PersonalEmail { get; set; }
    
        public virtual ICollection<Dependent> Dependents { get; set; }
        public virtual Case Case { get; set; }
        public virtual ICollection<CheckList> CheckLists { get; set; }
    }
}
