<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        [DisplayName("Reference Date. Format: mm/dd/yyyy")]
        [RegularExpression("^(3[01]|[12][0-9]|0[1-9])/(1[0-2]|0[1-9])/[0-9]{4}$", ErrorMessage = "Invalid format for Reference Date.")]
        public DateTime ReferenceDate{get; set; }
        [DisplayName("Departure Date. Format: mm/dd/yyyy")]
        [RegularExpression("^(3[01]|[12][0-9]|0[1-9])/(1[0-2]|0[1-9])/[0-9]{4}$", ErrorMessage = "Invalid format for Departure Date.")]
        public DateTime? DepartureDate { get; set; }
        public bool Completed { get; set; }

        [DisplayName("Date Completed. Format: mm/dd/yyyy")]
        [RegularExpression("^(3[01]|[12][0-9]|0[1-9])/(1[0-2]|0[1-9])/[0-9]{4}$", ErrorMessage = "Invalid format for Date Completed.")]
        public DateTime? DateCompleted { get; set; }
    }
}
=======
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        [DisplayName("Reference Date. Format: mm/dd/yyyy")]
        [RegularExpression("^(3[01]|[12][0-9]|0[1-9])/(1[0-2]|0[1-9])/[0-9]{4}$", ErrorMessage = "Invalid format for Reference Date.")]
        public DateTime ReferenceDate{get; set; }
        [DisplayName("Departure Date. Format: mm/dd/yyyy")]
        [RegularExpression("^(3[01]|[12][0-9]|0[1-9])/(1[0-2]|0[1-9])/[0-9]{4}$", ErrorMessage = "Invalid format for Departure Date.")]
        public DateTime? DepartureDate { get; set; }
        public bool Completed { get; set; }

        [DisplayName("Date Completed. Format: mm/dd/yyyy")]
        [RegularExpression("^(3[01]|[12][0-9]|0[1-9])/(1[0-2]|0[1-9])/[0-9]{4}$", ErrorMessage = "Invalid format for Date Completed.")]
        public DateTime? DateCompleted { get; set; }
    }
}
>>>>>>> 66858e2356cd8f1f9b5604d77e3591930568bbf8
