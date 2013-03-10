using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cico.Models
{
    public class Office
    {

        public int OfficeId { get; set; }
        [DisplayName ("Office Name")]
         [StringLength(65)]
        public string Name { get; set; }

        [Required]
        [DisplayName ("Office Contact")]
        [StringLength(65)]
        public string ContactUser{get; set; }
        
    }
}