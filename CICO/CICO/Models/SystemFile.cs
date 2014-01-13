using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cico.Models
{
    public class SystemFile:EntityBaseWithKey
    {
        public SystemFile()
        {
            
        }

        public SystemFile(HttpPostedFileBase httpFile,string fileType,string title)
        {
            this.Path = System.IO.Path.GetFileName(httpFile.FileName);
            this.Extension = System.IO.Path.GetExtension (httpFile.FileName);
            FileType = fileType;
            Description = title;
        }
        public virtual IList<CheckListItemTemplate> CheckListItemTemplates { get; set; } 
        public string Description { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }
        public string FileType{get; set; }
    }
}