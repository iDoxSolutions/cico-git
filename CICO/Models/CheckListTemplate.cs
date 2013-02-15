using System;
using System.ComponentModel.DataAnnotations;

namespace Cico.Models
{
    public class CheckListTemplate
    {
        [Key]
        public int CheckListTemplateId { get; set; }
        [Required]
        [Display(Name = "CheckList Type")]
        public string Type { get; set; }
        [Required]
        [Display(Name = "CheckList Description")]
        public string Name {get; set; }
        [Display(Name = "CheckList Start Date")]
        public DateTime? StartDate { get; set; }
        [Display(Name = "CheckList EndDate")]
        public DateTime? EndDate { get; set; }
        [Display(Name = "CheckList Due Date")]
        public DateTime? DueDate { get; set; }
        
   }
}
