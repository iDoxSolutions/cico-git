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
                var track =
                    session.CheckListItemSubmitionTracks.FirstOrDefault(
                        c =>
                        c.CheckListItemTemplate.CheckListItemTemplateId == checkListItemTemplate.CheckListItemTemplateId);
                if(track==null)
                    track = new CheckListItemSubmitionTrack();
                var notes = GetNotes(checkListItemTemplate, session.CheckListItemSubmitionTracks);
                var param = string.Format("?id={0}#checkpoint/{1}", session.Id, track.Id);
                var itemUri = new UriBuilder(Request.Url.Scheme, Request.Url.Host, Request.Url.Port, "home" ,param);
                //itemUri. = Request.Url.Authority;

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
                        FileUrl = checkListItemTemplate.SystemFile==null ? "":"/content/"+checkListItemTemplate.SystemFile.Patch,
                        FileDesc = checkListItemTemplate.SystemFile == null ? "" : checkListItemTemplate.SystemFile.Description,
                        Form = checkListItemTemplate.Form,
                        DueDate =session.CheckListItemSubmitionTracks.Any(c => c.CheckListItemTemplate.CheckListItemTemplateId == checkListItemTemplate.CheckListItemTemplateId)?session.CheckListItemSubmitionTracks.First(c => c.CheckListItemTemplate.CheckListItemTemplateId == checkListItemTemplate.CheckListItemTemplateId).DueDate.Value.ToShortDateString():null,
                        ApprovalText = checkListItemTemplate.ApprovalText,
                        TrackId = track.Id,
                        ItemUrl = itemUri.ToString(),
                        CompleteChecklist = checkListItemTemplate.CompleteCheckList
                    });
                
            }
            model.CheckListItems =
                model.CheckListItems.OrderBy(c => c.DueDate).ThenBy(c => c.CompleteChecklist).ToList();
            model.Completed = session.Completed;
            return Json(model);
        }

        private IList<NoteViewModel> GetNotes(CheckListItemTemplate checkListItemTemplate, ICollection<CheckListItemSubmitionTrack> checkListItemSubmitionTracks)
        {
            var track =
                checkListItemSubmitionTracks.FirstOrDefault(
                    c =>
                    c.CheckListItemTemplate.CheckListItemTemplateId == checkListItemTemplate.CheckListItemTemplateId);
            if(track==null)
                return new List<NoteViewModel>();
            else
            {
                return track.Notes.OrderByDescending(c=>c.DateCreated).Select(c => new NoteViewModel()
                    {
                        Content = HttpUtility.HtmlDecode( c.Content), 
                        DateCreated = c.DateCreated.ToString(),Id = c.Id,
                        UserCreated = c.UserCreated
                    }).ToList();
            }
        }

        private string GetItemCssClass(CheckListItemTemplate checkListItemTemplate, ICollection<CheckListItemSubmitionTrack> checkListItemSubmitionTracks)
        {
            var track =
                checkListItemSubmitionTracks.FirstOrDefault(
                    c =>
                    c.CheckListItemTemplate.CheckListItemTemplateId == checkListItemTemplate.CheckListItemTemplateId);
            if (!track.Checked)
            {
                return "red";
            }
            else
            {
                if (checkListItemTemplate.Provisional & track.Checked)
                {
                    return "yellow";
                }
                else
                {
                    return "green";
                }
            }
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

            track.SubmittedFile.Description = Path.GetFileName(docSubmitted.FileName);
            track.Checked = true;
            var storage = new FileStorage();
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