using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Cico.Models
{
    public class CheckListItemSubmitionTrack:EntityBaseWithKey
    {
        public CheckListItemSubmitionTrack()
        {
            if (Notes == null)
            {
                Notes = new Collection<Note>();
            }
            //DependentFiles = new List<DependentFile>();
        }
        public virtual CheckListSession CheckListSession { get; set; }
        public virtual CheckListItemTemplate CheckListItemTemplate { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        
        public DateTime? DueDate {
            get { 
                return CheckListSession.ArrivalDate.AddDays(CheckListItemTemplate.DueDays); 
            }
        }
        public virtual SystemFile SubmittedFile { get; set; }
        public bool Checked { get; set; }
        public bool Provisioned { get; set; }
        public virtual IList<DependentFile> DependentFiles { get; set; } 
    }
}