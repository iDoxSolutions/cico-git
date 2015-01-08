using System.ComponentModel.DataAnnotations;

namespace Cico.Models
{
    public class AccessFieldRight
    {
        [Key]
        public int Id { get; set; }
        public AccessField AccessField { get; set; }
        public Office Office { get; set; }
        [Required]
        [Display(Name = "Access")]
        public string Access { get; set; }

        public string EmpDep { get; set; }
    }
}