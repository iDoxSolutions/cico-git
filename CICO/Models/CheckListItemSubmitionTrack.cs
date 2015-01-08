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
        public bool ForDependents {
            get { return this.CheckListItemTemplate.Dependents; }
        }
        public bool Completed { 
            get
            {
                if (this.SubmittedFile == null &&
                    (this.CheckListItemTemplate.Item.Trim() == "DocumentSubmitted"
                    || this.CheckListItemTemplate.Item.Trim() == "DocumentWriting"))
                {
                    log.DebugFormat("type={0}; Item Name={1}",this.CheckListItemTemplate.Item, this.CheckListItemTemplate.Description);
                    return false;
                }

                log.DebugFormat("dependents = {0}",this.CheckListItemTemplate.Dependents);
                if (this.CheckListItemTemplate.Dependents && this.CheckListItemTemplate.Item == ChckItemTypes.DocumentSubmitted.ToString()
                    || this.CheckListItemTemplate.Item == ChckItemTypes.DocumentWriting.ToString())
                {
                    log.DebugFormat("Dependents found: type={0} dependents={1} Item Name={2}", this.CheckListItemTemplate.Item, this.CheckListItemTemplate.Dependents, this.CheckListItemTemplate.Description);
                    foreach (var dependent in this.CheckListSession.Employee.Dependents)
                    {
                        var depFile = this.DependentFiles.FirstOrDefault(c => c.Dependent.Id == dependent.Id);
                        if (depFile == null) 
                        { 
                            log.DebugFormat("empty dependent file - not complete");
                            return false;
                        }
                    }
                    log.DebugFormat("all dependents have file - complete");
                    return true;
                }
                else
                {
                    log.DebugFormat("No dependents or not Doc w/Writing or Doc Submitted type Item Name {0}", this.CheckListItemTemplate.Description);
                    return true;
                }
            } 
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