using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Cico.Models.Helpers;

namespace Cico.Models    //  [DisplayName("")]   [StringLength()]
{
    public partial class EmployeeAccess
    {
        public override void OnSave()
        {
            
        }
    }

   
    public partial class EmployeeAccess:EntityBaseWithKey
    {
       
        [DisplayName("Field Name")]
        [StringLength (30)]
        public string Name { get; set; }
         
        public string Office0 { get; set; }
         
        public string Office1 { get; set; }
         
        public string Office2 { get; set; }
        
        public string Office3 { get; set; }
        
        public string Office4 { get; set; }
        
        public string Office5 { get; set; }
        
        public string Office6 { get; set; }
        
        public string Office7 { get; set; }
         
        public string Office8 { get; set; }
        
        public string Office9 { get; set; }
         
        public string Office10 { get; set; }
         
        public string Office11 { get; set; }
        
        public string Office12 { get; set; }
       
    }

    public partial class DependentAccess
    {
        public override void OnSave()
        {

        }
    }


    public partial class DependentAccess : EntityBaseWithKey
    {

        [DisplayName("Field Name")]
        [StringLength(50)]
        public string Name { get; set; }
        public string AccessLevel { get; set; }
        public virtual Office Office { get; set; }

    }
}
