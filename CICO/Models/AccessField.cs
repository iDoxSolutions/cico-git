using System.ComponentModel.DataAnnotations;

namespace Cico.Models
{
    public class AccessField
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Field Name")]
        public string FieldName { get; set; }
        [Required]
        [Display(Name = "Field Description")]
        public string FieldDescription { get; set; }
        public string EmpDep { get; set; }
    }
}