using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Cico.Models
{
    public class CheckListTemplate:EntityBase
    {
        public CheckListTemplate()
        {
            CheckListItemTemplates = new List<CheckListItemTemplate>();
        }

        [Key]
        public int CheckListTemplateId { get; set; }
        [Required]
        [Display(Name = "CheckList Type")]
        public string Type { get; set; }
        //[Required]
        [Display(Name = "CheckList Description")]
        public string Name {get; set; }
        [Display(Name = "CheckList Start Date: Format: mm/dd/yyyy")]
        [RegularExpression("^([0-9]{1,2})[./-]+([0-9]{1,2})[./-]+([0-9]{2}|[0-9]{4})$", ErrorMessage = "Invalid format for Start Date.")]
        public DateTime? StartDate { get; set; }
        [Display(Name = "CheckList EndDate")]
        [RegularExpression("^([0-9]{1,2})[./-]+([0-9]{1,2})[./-]+([0-9]{2}|[0-9]{4})$", ErrorMessage = "Invalid format for End Date.")]
        public DateTime? EndDate { get; set; }
        [Display(Name = "CheckList Due Date")]
        [RegularExpression("^([0-9]{1,2})[./-]+([0-9]{1,2})[./-]+([0-9]{2}|[0-9]{4})$", ErrorMessage = "Invalid format for Due Date.")]
        public DateTime? DueDate { get; set; }
        public bool Published { get; set; }

        public virtual IList<CheckListItemTemplate> CheckListItemTemplates { get; set; }
        public virtual IList<CheckListSession> CheckListSessions { get; set; }
   }
}
