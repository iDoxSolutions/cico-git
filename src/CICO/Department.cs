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
    
    public partial class Department
    {
        public Department()
        {
            this.Staffs = new HashSet<Staff>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public int CheckListId { get; set; }
    
        public virtual ICollection<Staff> Staffs { get; set; }
        public virtual CheckList CheckList { get; set; }
    }
}
