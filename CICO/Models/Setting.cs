using System.ComponentModel.DataAnnotations;

namespace Cico.Models
{
    public class Setting
    {
        [Key]
        public string Name { get; set; }
        [Required]
        public string Value { get; set; }
        public string Name1 { get; set; }
    }
}