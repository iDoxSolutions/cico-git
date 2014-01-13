using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using log4net;


namespace Cico.Models
{
    public class CheckListItemSubmitionTrack:EntityBaseWithKey
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CheckListItemSubmitionTrack).Name);
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
        public virtual IList<SentBoxItem> SentBoxItems { get; set; } 
        public DateTime? DueDate {
            get { 
                return CheckListSession.ReferenceDate.AddDays(CheckListItemTemplate.DueDays); 
            }
        }
        public virtual SystemFile SubmittedFile { get; set; }
        public bool Checked { get; set; }
        public bool Completed { 
            get
            {
                if (this.SubmittedFile == null &&
                    this.CheckListItemTemplate.Type == ChckItemTypes.DocumentSubmitted.ToString()
                    || this.CheckListItemTemplate.Type == ChckItemTypes.DocumentWriting.ToString())
                {
                    return false;
                }

                //log.DebugFormat("item type:{0} dependents:{1} item name {2}, ", CheckListItemTemplate.Type, CheckListItemTemplate.Dependents,this.CheckListItemTemplate.Description);
                if (this.CheckListItemTemplate.Dependents && this.CheckListItemTemplate.Type == ChckItemTypes.DocumentSubmitted.ToString()
                    || this.CheckListItemTemplate.Type == ChckItemTypes.DocumentWriting.ToString())
                {
                    foreach (var dependent in this.CheckListSession.Employee.Dependents)
                    {
                        var depFile = this.DependentFiles.FirstOrDefault(c => c.Dependent.Id == dependent.Id);
                        if (depFile == null)
                            return false;
                    }
                  //  log.DebugFormat("all dependent fine");
                    return true;
                }
                else
                {
                    //log.DebugFormat("condition nut passed");
                    return true;
                }
            } 
        }
        public bool ForDependents
        {
            get { return this.CheckListItemTemplate.Dependents; }
        }
        public bool Provisioned { get; set; }
        public virtual IList<DependentFile> DependentFiles { get; set; } 
    }

    public static class CheckListItemSubmitionTrackHelper
    {
        public static string AbsoluteUri(this CheckListItemSubmitionTrack arg)
        {
            var url = string.Format("?id={0}#checkpoint/{1}", arg.CheckListSession.Id, arg.Id);
            var uri = new UriBuilder(HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.Port, "home", url);
            return uri.Uri.AbsoluteUri;
        }
    }
}