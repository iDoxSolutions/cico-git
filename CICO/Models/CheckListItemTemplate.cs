using System;
using System.Collections.Generic;
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
        [Display(Name = "Item Office")]
        public virtual Office Office { get; set; }
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
        
        [Display(Name = "Item Office Complete ")]
        public string OfficeComplete { get; set; }

        [Display(Name = "Complete Check List")]
        public bool CompleteCheckList { get; set; }
        [Display(Name = "Notes Access")]
        public bool NotesAccess { get; set; }
        [Display(Name = "Item Provisional ")]
        public bool Provisional { get; set; }
        [Display(Name = "Item Document ")]
        public string  Document { get; set; }
        [Display(Name = "Item Form ")]
        public string Form { get; set; }
        [Display(Name = "Dependents ")]
        public bool Dependents { get; set; }

        
        
        [Display(Name = "Item Instructions ")]
        public string InstructionText { get; set; }

        [Display(Name = "Approval Text")]
        public string ApprovalText { get; set; }

        [Display(Name = "Item Group")]
        public string Group { get; set; }
        [Display(Name = "Item Alert Days")]
        public string  AlertDays{ get; set; }
        [Display(Name = "Item Alert Frequency")]
        public string  AlertFrenquency{ get; set; }

        public virtual CheckListTemplate CheckListTemplate { get; set; }
        public virtual IList<CheckListItemSubmitionTrack> CheckListItemSubmitionTracks { get; set; }
        public string Type{get; set; }
        [Display(Name = "File")]
        public virtual SystemFile SystemFile{get; set; }

        [Display(Name = "Number of days to complete item")]
        public int DueDays{get; set; }

        public bool CompletingChecklist { get; set; }

        public virtual IList<EmailSubscription> EmailSubscriptions { get; set; }
    }
}
