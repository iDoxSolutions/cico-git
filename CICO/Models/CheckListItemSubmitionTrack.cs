using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cico.Models
{
    public class CheckListItemSubmitionTrack:EntityBaseWithKey
    {
        public CheckListSession CheckListSession { get; set; }
        public CheckListItemTemplate CheckListItemTemplate { get; set; }

    }
}