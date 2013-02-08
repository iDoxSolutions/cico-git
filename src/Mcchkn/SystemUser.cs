//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mcchkn
{
    using System;
    using System.Collections.Generic;
    
    public partial class SystemUser
    {
        public SystemUser()
        {
            this.Households = new HashSet<Household>();
            this.Notes = new HashSet<Note>();
            this.Tasks = new HashSet<Task>();
            this.SystemUsers1 = new HashSet<SystemUser>();
            this.Roles = new HashSet<Role>();
        }
    
        public int SystemUserId { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string Telephone { get; set; }
        public Nullable<int> IsManagedById { get; set; }
    
        public virtual ICollection<Household> Households { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<SystemUser> SystemUsers1 { get; set; }
        public virtual SystemUser SystemUser1 { get; set; }
        public virtual SystemUser SystemUsers11 { get; set; }
        public virtual SystemUser SystemUser2 { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}
