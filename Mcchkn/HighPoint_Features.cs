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
    
    public partial class HighPoint_Features
    {
        public HighPoint_Features()
        {
            this.HighPoint_Features1 = new HashSet<HighPoint_Features>();
            this.HighPoint_Roles = new HashSet<HighPoint_Roles>();
        }
    
        public int Id { get; set; }
        public Nullable<int> FeatureKind { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public Nullable<int> ParentFk { get; set; }
    
        public virtual ICollection<HighPoint_Features> HighPoint_Features1 { get; set; }
        public virtual HighPoint_Features HighPoint_Features2 { get; set; }
        public virtual ICollection<HighPoint_Roles> HighPoint_Roles { get; set; }
    }
}
