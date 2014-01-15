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
        [Display(Name = "CheckList Description")]
        public string Name {get; set; }
        [Display(Name = "CheckList Start Date: Format mm/dd/yyyy")]
      //  [RegularExpression("^(3[01]|[12][0-9]|0[1-9])/(1[0-2]|0[1-9])/[0-9]{4}$", ErrorMessage = "Invalid format for Start Date.")]
        public DateTime? StartDate { get; set; }
        [Display(Name = "CheckList EndDate: Format mm/dd/yyyy")]
    //    [RegularExpression("^(3[01]|[12][0-9]|0[1-9])/(1[0-2]|0[1-9])/[0-9]{4}$", ErrorMessage = "Invalid format for End Date.")]
        public DateTime? EndDate { get; set; }
        [Display(Name = "CheckList Due Date: mm/dd/yyyy")]
    //    [RegularExpression("^(3[01]|[12][0-9]|0[1-9])/(1[0-2]|0[1-9])/[0-9]{4}$", ErrorMessage = "Invalid format for Due Date.")]
        public DateTime? DueDate { get; set; }
        [Display(Name = "CheckList Status")]
        public String Status { get; set; }

   }
}
