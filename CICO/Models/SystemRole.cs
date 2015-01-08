using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cico.Models
{
    public class SystemRole:EntityBase
    {
        public SystemRole()
        {
            Staffs = new List<Staff>();
            AppFeatures = new List<AppFeature>();
        }

        public const string GlobalAdmin = "GlobalAdmin";
        public const string OfficeAdmin = "OfficeAdmin";
        public const string UserProxy = "UserProxy";
        public const string CheckListEditor = "CheckListEditor";

        [Key]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        public virtual IList<Staff> Staffs { get; set; }
        public virtual IList<AppFeature> AppFeatures { get; set; }  
    }


    public class AppFeature : EntityBase
    {
        public AppFeature()
        {
            SystemRoles = new List<SystemRole>();
        }
        [Key]
        [StringLength(100)]
        public string Name { get; set; }
        
        
        public virtual IList<SystemRole> SystemRoles { get; set; }  
    }

    
    
}