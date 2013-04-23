using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            model.Id = session.Id;
            foreach (CheckListItemTemplate checkListItemTemplate in session.CheckListTemplate.CheckListItemTemplates.Where(c=>c.Active))
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
                        DependentsFiles = GetDependetsFiles(track),
                        CustomFormUrl = checkListItemTemplate.CustomFormUrl
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
                file.DependentName = dependent.FirstName + ", " + dependent.LastName;
                file.DependentId = dependent.Id;
                var dependentFile = track.DependentFiles.FirstOrDefault(c => c.Dependent.Id == dependent.Id);
                if (dependentFile != null)
                {
                    file.FileName = string.IsNullOrEmpty(dependentFile.SystemFile.Description) ? "(No name)" : dependentFile.SystemFile.Description;
                    file.Url = "/filestorage?id=" + dependentFile.SystemFile.Id;

                }
                
                list.Add(file);
            }
            return list;
        }

        private IList<NoteViewModel> GetNotes(CheckListSession session,CheckListItemTemplate template)
        {
            var user = UserSession.GetUserName();
            var track = session.GetTrack(template.CheckListItemTemplateId);
            var notes = track.Notes;
            if (!template.NotesAccess)
            {
                var staff = UserSession.GetCurrentStaff();
                if (staff != null  && User.IsInRole(SystemRole.OfficeAdmin))
                {
                    if (staff.Office.OfficeId != template.Office.OfficeId)
                    {
                        notes = new Collection<Note>();
                    }
                }
                // notes = notes.
            }
            return notes.OrderByDescending(c => c.DateCreated).Select(c => new NoteViewModel()
                    {
                        Content = HttpUtility.HtmlDecode( c.Content), 
                        DateCreated = c.DateCreated.ToString(),Id = c.Id,
                        UserCreated = c.UserCreated,
                        Deletable = c.UserCreated == user
                    }).ToList();
            
        }

        
        [HandleModelStateException]
        public ActionResult UploadFile(HttpPostedFileBase docSubmitted,int itemTemplateId,int checklistId)
        {
           
            var track = UserSession.GetTrack(itemTemplateId,checklistId);
            track.DateEdited = DateTime.Now;
            if (docSubmitted == null && track.SubmittedFile==null)
                throw new ModelStateException("File is Required");
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
                    if (ofile.ContentLength == 0)
                    {
                        continue;
                    }
                    var id = key.Split("-".ToCharArray())[1];
                    var dependent = Db.Dependents.Find(Int32.Parse(id));

                    if(string.IsNullOrEmpty( Path.GetExtension(ofile.FileName)))
                    {
                        throw new ModelStateException("File extension is Required");
                    }

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
            if (docSubmitted != null)
            {
                track.SubmittedFile.Description = Path.GetFileName(docSubmitted.FileName);
                track.Checked = true;

                storage.PutFile(docSubmitted, track.SubmittedFile);
            }
            var subs = new Subscriptions(HttpContext);
            //subs.Process(track, string.Format("Document uploaded by user {0} ", UserSession.GetUserName()));
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

        public ActionResult Check(int id, int checklistId)
        {
            var track = UserSession.GetTrack(id,checklistId);
            track.DateEdited = DateTime.Now;
            track.Checked = true;
            var session = track.CheckListSession;
            if (track.CheckListItemTemplate.CompleteCheckList)
            {
                session.Completed = true;
            }
            var subs = new Subscriptions(HttpContext);
            //subs.Process(track,string.Format("Checkpoint completed by user {0} ",UserSession.GetUserName()));
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