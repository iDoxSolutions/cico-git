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
    
    public partial class LetterDefinition
    {
        public LetterDefinition()
        {
            this.Letters = new HashSet<Letter>();
        }
    
        public int LetterDefinitionId { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
    
        public virtual ICollection<Letter> Letters { get; set; }
    }
}
