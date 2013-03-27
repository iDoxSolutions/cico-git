using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Cico.Models
{
    public static class CicoHelper
    {
        public static string CssClass(this CheckListItemSubmitionTrack track)
        {
            if (!track.Checked)
                return "red";
            if (track.Checked && track.CheckListItemTemplate.Provisional && !track.Provisioned)
                return "yellow";
            return "green";
        }
    }

    public class CheckListItemSubmitionTrack:EntityBaseWithKey
    {
        public CheckListItemSubmitionTrack()
        {
            if (Notes == null)
            {
                Notes = new Collection<Note>();
            }
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
    }
}