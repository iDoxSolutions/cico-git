using System.ComponentModel.DataAnnotations;

namespace Cico.Models
{
    public class CheckListItemType
    {
        [Key]
        public int CheckListTypeId { get; set; }
        [Required]
        //[StringLength(100)]
        public string Name { get; set; }
        //[Required]
        public string Description { get; set; }
    }
}