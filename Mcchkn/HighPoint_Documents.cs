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
    
    public partial class HighPoint_Documents
    {
        public HighPoint_Documents()
        {
            this.HighPoint_FormTracks = new HashSet<HighPoint_FormTracks>();
            this.HighPoint_PostMessages = new HashSet<HighPoint_PostMessages>();
            this.HighPoint_Forms = new HashSet<HighPoint_Forms>();
        }
    
        public int Id { get; set; }
        public byte[] ThumbnailData { get; set; }
        public string DocumentSystemId { get; set; }
        public string Extension { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<int> Size { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public Nullable<int> DocumentTypeFk { get; set; }
        public Nullable<int> UserCreatedFk { get; set; }
        public Nullable<int> UserModifiedFk { get; set; }
    
        public virtual HighPoint_CaseDocuments HighPoint_CaseDocuments { get; set; }
        public virtual ICollection<HighPoint_FormTracks> HighPoint_FormTracks { get; set; }
        public virtual ICollection<HighPoint_PostMessages> HighPoint_PostMessages { get; set; }
        public virtual HighPoint_DocumentTypes HighPoint_DocumentTypes { get; set; }
        public virtual HighPoint_Users HighPoint_Users { get; set; }
        public virtual HighPoint_Users HighPoint_Users1 { get; set; }
        public virtual ICollection<HighPoint_Forms> HighPoint_Forms { get; set; }
    }
}
