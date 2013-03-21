using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cico.Models.Authentication
{
    public class InitModel
    {
        [DisplayName("Arrival Date")]
        [Required]
        public DateTime ArrivalDate { get; set; }
        [StringLength(65)]
        [Required]
        [DisplayName("Given Name(s)")]
        public string GivenName { get; set; }
        [StringLength(65)]
        [Required]
        [DisplayName("Surname")]
        public string Surname { get; set; }
        [DisplayName("Date of Birth")]
        [Required]
        public DateTime Dob { get; set; }
    }
}