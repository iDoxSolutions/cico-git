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
    
    public partial class HighPoint_CaseDocuments
    {
        public HighPoint_CaseDocuments()
        {
            this.HighPoint_StateDocuments = new HashSet<HighPoint_StateDocuments>();
            this.HighPoint_ProcessingCases1 = new HashSet<HighPoint_ProcessingCases>();
        }
    
        public int DocumentFk { get; set; }
        public Nullable<int> ProcessingEntityFk { get; set; }
        public Nullable<int> RootCaseFk { get; set; }
    
        public virtual ICollection<HighPoint_StateDocuments> HighPoint_StateDocuments { get; set; }
        public virtual HighPoint_ProcessingEntities HighPoint_ProcessingEntities { get; set; }
        public virtual HighPoint_ProcessingCases HighPoint_ProcessingCases { get; set; }
        public virtual HighPoint_Documents HighPoint_Documents { get; set; }
        public virtual ICollection<HighPoint_ProcessingCases> HighPoint_ProcessingCases1 { get; set; }
    }
}
