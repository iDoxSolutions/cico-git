using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cico.Models
{
    public class SystemFile:EntityBaseWithKey
    {
        public string Description { get; set; }
        public string Patch { get; set; }
        public string Extension { get; set; }
    }
}