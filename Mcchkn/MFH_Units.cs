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
    
    public partial class MFH_Units
    {
        public MFH_Units()
        {
            this.MFH_Households = new HashSet<MFH_Households>();
        }
    
        public int Id { get; set; }
        public Nullable<int> AddressFk { get; set; }
        public Nullable<int> OwnerFk { get; set; }
    
        public virtual HighPoint_Addresses HighPoint_Addresses { get; set; }
        public virtual HighPoint_People HighPoint_People { get; set; }
        public virtual ICollection<MFH_Households> MFH_Households { get; set; }
    }
}
