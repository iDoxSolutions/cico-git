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
    
    public partial class HighPoint_ProcessFormBookmarks
    {
        public int WorkflowBookmarkFk { get; set; }
        public Nullable<int> ProcessFormFk { get; set; }
    
        public virtual HighPoint_WorkflowBookmarks HighPoint_WorkflowBookmarks { get; set; }
        public virtual HighPoint_ProcessForms HighPoint_ProcessForms { get; set; }
    }
}
