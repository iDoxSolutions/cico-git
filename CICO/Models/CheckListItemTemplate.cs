using System;
using System.ComponentModel.DataAnnotations;

namespace Cico.Models
{
    public class CheckListItemTemplate
    {
        [Key]
        public int CheckListItemTemplateId { get; set; }
        [Required]
        [Display(Name = "Item Type")]
        public string Item { get; set; }

        [Required]
        [Display(Name = "Item Description")]
        public string Description {get; set; }
        [Display(Name = "Item Department")]
        public string Department { get; set; }
        [Display(Name = "Item Priority")]
        public string Priority { get; set; }
        [Display(Name = "Item Status")]
        public string Status { get; set; }
        [Display(Name = "Item Viewable")]
        public string Viewable { get; set; }
        [Display(Name = "Item Subscriber")]
        public string Subscriber { get; set; }
        [Display(Name = "Item CheckListId")]
        public string CheckListId { get; set; }
        [Display(Name = "Item DueDate ")]
        public DateTime? DueDate { get; set; }
        [Display(Name = "Item Instructions")]
        public string Instructions { get; set; }
        [Display(Name = "Employee Complete ")]
        public string  EmployeeComplete{ get; set; }
        [Display(Name = "Item Provisional ")]
        public string Provisional { get; set; }
        [Display(Name = "Item Office Complete ")]
        public string OfficeComplete { get; set; }
        [Display(Name = "Item Document ")]
        public string  Document { get; set; }
        [Display(Name = "Item Form ")]
        public string Form { get; set; }
        [Display(Name = "Item Instructions ")]
        public string InstructionText { get; set; }
        [Display(Name = "Item Group")]
        public string Group { get; set; }
        [Display(Name = "Item Alert Days")]
        public string  AlertDays{ get; set; }
        [Display(Name = "Item Alert Frequency")]
        public string  AlertFrenquency{ get; set; }

        public CheckListTemplate CheckListTemplate { get; set; }

        public string Type{get; set; }
    }
}
