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
    
    public partial class HighPoint_BusinessProcesses
    {
        public HighPoint_BusinessProcesses()
        {
            this.HighPoint_ProcessForms = new HashSet<HighPoint_ProcessForms>();
            this.HighPoint_ProcessingCases = new HashSet<HighPoint_ProcessingCases>();
            this.HighPoint_Forms = new HashSet<HighPoint_Forms>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<bool> Deactivated { get; set; }
        public string FileName { get; set; }
        public Nullable<int> DefaultFormFk { get; set; }
    
        public virtual ICollection<HighPoint_ProcessForms> HighPoint_ProcessForms { get; set; }
        public virtual HighPoint_ProcessForms HighPoint_ProcessForms1 { get; set; }
        public virtual ICollection<HighPoint_ProcessingCases> HighPoint_ProcessingCases { get; set; }
        public virtual ICollection<HighPoint_Forms> HighPoint_Forms { get; set; }
    }
}
