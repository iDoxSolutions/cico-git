﻿using System;
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
        public CheckListTemplate CheckListTemplate { get; set; }


        
    }
}