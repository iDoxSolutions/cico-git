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
    
    public partial class HighPoint_WorkflowBookmarks
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> ProcessingCaseFk { get; set; }
        public Nullable<int> UserAccessFk { get; set; }
    
        public virtual HighPoint_DocTypeWorkflowBookmarks HighPoint_DocTypeWorkflowBookmarks { get; set; }
        public virtual HighPoint_ProcessFormBookmarks HighPoint_ProcessFormBookmarks { get; set; }
        public virtual HighPoint_ProcessingCases HighPoint_ProcessingCases { get; set; }
        public virtual HighPoint_SelectableOptionBookmarks HighPoint_SelectableOptionBookmarks { get; set; }
        public virtual HighPoint_Users HighPoint_Users { get; set; }
    }
}
