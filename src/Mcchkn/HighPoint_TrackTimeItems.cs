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
    
    public partial class HighPoint_TrackTimeItems
    {
        public int StateItemFk { get; set; }
        public string TrackerName { get; set; }
        public Nullable<System.DateTime> TrackTime { get; set; }
        public Nullable<int> TrackTimeMode { get; set; }
    
        public virtual HighPoint_StateItems HighPoint_StateItems { get; set; }
    }
}
