using System.ComponentModel.DataAnnotations;

namespace Cico.Models
{
    public enum ChckItemTypes
    {
        DocumentSubmitted,
        DocumentWriting,
        PhysicalActivity,
        DocumentApproval,
        OnlineForm
    }

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