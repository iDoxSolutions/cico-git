using System;
using System.ComponentModel.DataAnnotations;

namespace Cico.Models
{
    public class CheckList
    {
        [Key]
        public int CheckListId { get; set; }
        [Required]
        [Display(Name = "CheckList Type")]
        public string Type { get; set; }
        [Required]
        [Display(Name = "CheckList Description: Format: mm/dd/yyyy")]
        public string Name {get; set; }
        [Display(Name = "CheckList Start Date")]
        [RegularExpression("^([0-9]{1,2})[./-]+([0-9]{1,2})[./-]+([0-9]{2}|[0-9]{4})$", ErrorMessage = "Invalid format for Start Date.")]
        public DateTime? StartDate { get; set; }
        [Display(Name = "CheckList EndDate: Format: mm/dd/yyyy")]
        [RegularExpression("^([0-9]{1,2})[./-]+([0-9]{1,2})[./-]+([0-9]{2}|[0-9]{4})$", ErrorMessage = "Invalid format for Start Date.")]
        public DateTime? EndDate { get; set; }
        [Display(Name = "CheckList Due Date: Format: mm/dd/yyyy")]
        [RegularExpression("^([0-9]{1,2})[./-]+([0-9]{1,2})[./-]+([0-9]{2}|[0-9]{4})$", ErrorMessage = "Invalid format for Start Date.")]
        public DateTime? DueDate { get; set; }
        [Display(Name = "CheckList Status")]
        public String Status { get; set; }
        
   }
}
