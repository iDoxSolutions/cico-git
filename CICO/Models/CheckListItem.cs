//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cico.Models
{
    #pragma warning disable 1573
    using System;
    using System.Collections.Generic;
    
    public partial class CheckListItem
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Department { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public string Viewable { get; set; }
        public string Subscriber { get; set; }
        public int CheckListId { get; set; }
        public string DueDate { get; set; }
        public System.DateTime DueDate1 { get; set; }
        public string InstructionDocument { get; set; }
        public string EmployeeCompleted { get; set; }
        public string Provisional { get; set; }
        public string OfficeComplete { get; set; }
        public string ItemDocument { get; set; }
        public string BlankForm { get; set; }
        public string InstructionText { get; set; }
        public string Group { get; set; }
        public string AlertDays { get; set; }
        public string AlertFrequency { get; set; }
    
        public virtual CheckList CheckList { get; set; }
    }
}