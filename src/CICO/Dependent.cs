//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cico
{
    using System;
    using System.Collections.Generic;
    
    public partial class Dependent
    {
        public int Id { get; set; }
        public int EmployeesEmployeeId { get; set; }
        public string DependentType { get; set; }
    
        public virtual Employee Employee { get; set; }
    }
}
