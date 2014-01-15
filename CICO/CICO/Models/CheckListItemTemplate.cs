using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cico.Models
{
    public class CheckListItemTemplate:EntityBase
    {
        [Key]
        public int CheckListItemTemplateId { get; set; }
        [Required]
        [Display(Name = "Type")]
        public string Item { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description {get; set; }
        [Display(Name = "Office")]
        public virtual Office Office { get; set; }
        [Display(Name = "Priority")]
        public string Priority { get; set; }
        [Display(Name = "Status")]
        public string Status { get; set; }
        [Display(Name = "Viewable")]
        public string Viewable { get; set; }
        [Display(Name = "Subscriber")]
        public string Subscriber { get; set; }
        [Display(Name = "CheckList Id")]
        public string CheckListId { get; set; }
        [Display(Name = "Due Date: Format mm/dd/yyyy")]
        [RegularExpression(@"^(3[01]|[12][0-9]|0[1-9])/(1[0-2]|0[1-9])/[0-9]{4}$", ErrorMessage = "Invalid format for Due Date.")]
        public DateTime? DueDate { get; set; }
        [Display(Name = "Instructions")]
        public string Instructions { get; set; }
        [Display(Name = "Employee Complete ")]
        public string  EmployeeComplete{ get; set; }
        
        [Display(Name = "Office Complete ")]
        public string OfficeComplete { get; set; }

        [Display(Name = "Complete Checklist")]
        public bool CompleteCheckList { get; set; }
        [Display(Name = "Notes Access")]
        public bool NotesAccess { get; set; }
        [Display(Name = "Provisional ")]
        public bool Provisional { get; set; }
        [Display(Name = "Document ")]
        public string  Document { get; set; }
        [Display(Name = "Download Template ")]
        public string Form { get; set; }
        [Display(Name = "Dependents ")]
        public bool Dependents { get; set; }

        
        
        [Display(Name = "Instructions ")]
        public string InstructionText { get; set; }

        [Display(Name = "Approval Text")]
        public string ApprovalText { get; set; }

        [Display(Name = "Group")]
        public string Group { get; set; }
        [Display(Name = "Alert Days")]
        public string  AlertDays{ get; set; }
        [Display(Name = "Alert Frequency")]
        public string  AlertFrenquency{ get; set; }

        public virtual CheckListTemplate CheckListTemplate { get; set; }
        public virtual IList<CheckListItemSubmitionTrack> CheckListItemSubmitionTracks { get; set; }
        public string Type{get; set; }
        [Display(Name = "Custom Form")]
        public virtual SystemFile SystemFile{get; set; }

        [Display(Name = "Custom Form Url")]
        public virtual string CustomFormUrl { get; set; }

       
        [Display(Name = "Due Date: Format mm/dd/yyyy")]
        [RegularExpression(@"^(3[01]|[12][0-9]|0[1-9])/(1[0-2]|0[1-9])/[0-9]{4}$", ErrorMessage = "Invalid format for Due Date.")]
        public int DueDays{get; set; }

        public bool CompletingChecklist { get; set; }

        public virtual IList<EmailSubscription> EmailSubscriptions { get; set; }
    }
}
