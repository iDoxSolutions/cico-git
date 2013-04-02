using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cico.Controllers.ViewModels;
using Cico.Models;
using Cico.Models.CheckLists;
using Cico.Models.Helpers;
using Cico.Models.SharePoint;

namespace Cico.Controllers.ViewModels
{
   
    public class FileModel
    {
        public string Url { get; set; }
        public string Description { get; set; }
    }

    public class CheckListModel
    {
        public CheckListModel()
        {
            CheckListItems = new List<CheckListItemModel>();
        }
        public IList<CheckListItemModel> CheckListItems { get; set; }

        public bool Completed{get; set; }
    }

    public class CheckListItemModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string ItemTemplate { get; set; }
        public bool Checked { get; set; }
        public string CssClass{get; set; }
        public string InstructionText { get; set; }
        public IList<NoteViewModel> Notes{get; set; }

        public string FileUrl{get; set; }

        public string FileDesc{get; set; }

        public string Form{get; set; }

        public string DueDate{get; set; }

        public FileModel SubmittedFile { get; set; }

        public string ApprovalText{get; set; }

        public int TrackId{get; set; }

        public string ItemUrl{get; set; }

        public bool CompleteChecklist{get; set; }

        public IList<DependentsFile> DependentsFiles{ get; set; }
    }

    public  class DependentsFile
    {
        public string Url { get; set; }
        public string DependentName { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public int DependentId { get; set; }
    }
}

namespace Cico.Controllers
{
    public class CheckListController : ControllerBase
    {
        public ActionResult Index(int? id)
        {
            CheckListSession session = null;
            if (!id.HasValue)
            {
                session = UserSession.GetCurrent();
            }
            else
            {
                session = Db.CheckListSessions.Include("CheckListTemplate").Single(c => c.Id == id.Value && c.Active);
            }
            var model = new CheckListModel();
            foreach (CheckListItemTemplate checkListItemTemplate in session.CheckListTemplate.CheckListItemTemplates)
            {
                var track = session.GetTrack(checkListItemTemplate.CheckListItemTemplateId);
                if (track.Id == 0)
                {
                    Db.CheckListItemSubmitionTracks.Add(track);
                    Db.SaveChanges();
                }
               
                var notes = GetNotes(session, checkListItemTemplate);
                var param = string.Format("?id={0}#checkpoint/{1}", session.Id, track.Id);
                var itemUri = new UriBuilder(Request.Url.Scheme, Request.Url.Host, Request.Url.Port, "home" ,param);
                model.CheckListItems.Add(new CheckListItemModel
                    {
                        SubmittedFile = track.SubmittedFile==null?null:new FileModel(){Description = track.SubmittedFile.Description,Url = "/filestorage?id="+track.SubmittedFile.Id},
                        Id = checkListItemTemplate.CheckListItemTemplateId,
                        ItemTemplate = checkListItemTemplate.Item,
                        Description = checkListItemTemplate.Description,
                        Checked = track.Checked,
                        CssClass = track.CssClass(),
                        Notes = notes,
                        InstructionText = checkListItemTemplate.InstructionText,
                        FileUrl = checkListItemTemplate.SystemFile==null ? "":"/content/"+checkListItemTemplate.SystemFile.Path,
                        FileDesc = checkListItemTemplate.SystemFile == null ? "" : checkListItemTemplate.SystemFile.Description,
                        Form = checkListItemTemplate.Form,
                        DueDate =session.CheckListItemSubmitionTracks.Any(c => c.CheckListItemTemplate.CheckListItemTemplateId == checkListItemTemplate.CheckListItemTemplateId)?session.CheckListItemSubmitionTracks.First(c => c.CheckListItemTemplate.CheckListItemTemplateId == checkListItemTemplate.CheckListItemTemplateId).DueDate.Value.ToShortDateString():null,
                        ApprovalText = checkListItemTemplate.ApprovalText,
                        TrackId = track.Id,
                        ItemUrl = itemUri.ToString(),
                        CompleteChecklist = checkListItemTemplate.CompleteCheckList,
                        DependentsFiles = GetDependetsFiles(track)
                    });
                
            }
            model.CheckListItems =
                model.CheckListItems.OrderBy(c => c.DueDate).ThenBy(c => c.CompleteChecklist).ToList();
            model.Completed = session.Completed;
            return Json(model);
        }

        public IList<DependentsFile> GetDependetsFiles(CheckListItemSubmitionTrack track)
        {
            var employee = track.CheckListSession.Employee;
            if (!track.CheckListItemTemplate.Dependents || employee.Dependents.Count()==0)
                return null;
            var dependents = track.CheckListSession.Employee.Dependents;
            var list = new List<DependentsFile>();
            foreach (var dependent in dependents)
            {
                var file = new DependentsFile();
                file.DependentName = dependent.GivenName + ", " + dependent.Surname;
                file.DependentId = dependent.Id;
                var dependentFile = track.DependentFiles.FirstOrDefault(c => c.Dependent.Id == dependent.Id);
                if (dependentFile != null)
                {
                    file.FileName = dependentFile.SystemFile.Description;
                    file.Url = "/filestorage?id=" + dependentFile.SystemFile.Id;

                }
                
                list.Add(file);
            }
            return list;
        }

        private IList<NoteViewModel> GetNotes(CheckListSession session,CheckListItemTemplate template)
        {
            var track = session.GetTrack(template.CheckListItemTemplateId);
            return track.Notes.OrderByDescending(c=>c.DateCreated).Select(c => new NoteViewModel()
                    {
                        Content = HttpUtility.HtmlDecode( c.Content), 
                        DateCreated = c.DateCreated.ToString(),Id = c.Id,
                        UserCreated = c.UserCreated
                    }).ToList();
            
        }

        
        [HandleModelStateException]
        public ActionResult UploadFile(HttpPostedFileBase docSubmitted,int itemTemplateId)
        {
            if (docSubmitted == null)
                throw new ModelStateException("File is Required");
            var track = UserSession.GetTrack(itemTemplateId);
            if (track.SubmittedFile == null)
            {
                track.SubmittedFile = new SystemFile();
                Db.SystemFiles.Add(track.SubmittedFile);
            }
            var storage = new FileStorage();
            foreach (string key in Request.Files.AllKeys)
            {
                if (key.StartsWith("dependent"))
                {
                    var ofile = Request.Files[key];
                    var id = key.Split("-".ToCharArray())[1];
                    var dependent = Db.Dependents.Find(Int32.Parse(id));

                    var depFile = track.DependentFiles.FirstOrDefault(c => c.Dependent.Id == int.Parse(id));
                    if (depFile == null)
                    {
                        var file = new SystemFile()
                        {
                            Description = Path.GetFileName(ofile.FileName),
                        };
                        Db.SystemFiles.Add(file);
                        //storage.PutFile(ofile,track.SubmittedFile);
                        depFile = new DependentFile()
                        {
                            CheckListItemSubmitionTrack = track,
                            SystemFile = file,
                            Dependent = dependent
                        };
                        Db.DependentFiles.Add(depFile);    
                    }
                    depFile.SystemFile.Description = Path.GetFileName(ofile.FileName);
                    storage.PutFile(ofile, depFile.SystemFile);
                }
                Db.SaveChanges();
            }

            track.SubmittedFile.Description = Path.GetFileName(docSubmitted.FileName);
            track.Checked = true;
            
            storage.PutFile(docSubmitted,track.SubmittedFile);
            var subs = new Subscriptions(HttpContext);
            subs.Process(track, string.Format("Document uploaded by user {0} ", UserSession.GetUserName()));
            Db.SaveChanges();
            var model = new CheckListItemModel
                {
                    SubmittedFile = new FileModel()
                        {
                            Description = track.SubmittedFile.Description,
                            Url = "/filestorage?id=" + track.SubmittedFile.Id
                        },
                    CssClass = track.CssClass(),
                    DependentsFiles = GetDependetsFiles(track)
                    
                };
            
                return Json(model, "text/html");
            
        }

        public ActionResult Check(int id)
        {
            var track = UserSession.GetTrack(id);
            track.Checked = true;
            var session = UserSession.GetCurrent();
            if (track.CheckListItemTemplate.CompleteCheckList)
            {
                session.Completed = true;
            }
            var subs = new Subscriptions(HttpContext);
            subs.Process(track,string.Format("Checkpoint completed by user {0} ",UserSession.GetUserName()));
            return Json(new CheckListItemModel()
                {
                    CssClass = track.CssClass(),
                    Checked = true,
                    CompleteChecklist = session.Completed
                });
        }

        public ActionResult CloseCurrentSession()
        {
            this.UserSession.GetCurrent().Active = false;
            Db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}