using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace Cico.Models
{
    public class CheckListSession:EntityBaseWithKey
    {
        public string UserId { get; set; }
        public virtual ICollection<CheckListItemSubmitionTrack> CheckListItemSubmitionTracks { get; set; }
        public CheckListItemSubmitionTrack GetTrack(int templateId)
        {
            var track = CheckListItemSubmitionTracks.FirstOrDefault(c => c.CheckListItemTemplate.CheckListItemTemplateId == templateId);
            if (track == null)
            {
                var temp =
                    CheckListTemplate.CheckListItemTemplates.FirstOrDefault(c => c.CheckListItemTemplateId == templateId);
                track = new CheckListItemSubmitionTrack() {CheckListItemTemplate = temp,CheckListSession = this,DependentFiles = new List<DependentFile>()};
                CheckListItemSubmitionTracks.Add(track);
            }
            return track;
        }
        public CheckListTemplate CheckListTemplate { get; set; }
        public virtual Employee Employee { get; set; }
        public DateTime ArrivalDate{get; set; }
        public DateTime? DepartureDate { get; set; }
        public bool Completed { get; set; }
    }
}