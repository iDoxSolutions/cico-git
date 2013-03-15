using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cico.Controllers.ViewModels;
using Cico.Models;
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
    }

    public class CheckListItemModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string ItemTemplate { get; set; }
        public bool Checked { get; set; }
        public string CssClass{get; set; }
        public string InstructionText { get; set; }
        public IList<NoteViewModel> Notes
        {
            get; set; }

        public string FileUrl
        {get; set; }

        public string FileDesc
        {
            get; set; }

        public string Form
        {get; set; }

        public string DueDate
        {get; set; }

        public FileModel SubmittedFile { get; set; }
    }
}

namespace Cico.Controllers
{
    public class CheckListController : ControllerBase
    {
        public ActionResult Index()
        {
            CheckListSession session = UserSession.GetCurrent();
            var model = new CheckListModel();
            foreach (CheckListItemTemplate checkListItemTemplate in session.CheckListTemplate.CheckListItemTemplates)
            {
                var track =
                    session.CheckListItemSubmitionTracks.FirstOrDefault(
                        c =>
                        c.CheckListItemTemplate.CheckListItemTemplateId == checkListItemTemplate.CheckListItemTemplateId);
                if(track==null)
                    track = new CheckListItemSubmitionTrack();
                var itemCssClass = GetItemCssClass(checkListItemTemplate, session.CheckListItemSubmitionTracks);
                var notes = GetNotes(checkListItemTemplate, session.CheckListItemSubmitionTracks);
                model.CheckListItems.Add(new CheckListItemModel
                    {
                        SubmittedFile = track.SubmittedFile==null?null:new FileModel(){Description = track.SubmittedFile.Description,Url = "/files/"+track.SubmittedFile.Description},
                        Id = checkListItemTemplate.CheckListItemTemplateId,
                        ItemTemplate = checkListItemTemplate.Item,
                        Description = checkListItemTemplate.Description,
                        Checked = track.Checked,
                        CssClass = itemCssClass,
                        Notes = notes,
                        InstructionText = checkListItemTemplate.InstructionText,
                        FileUrl = checkListItemTemplate.SystemFile==null ? "":"/content/"+checkListItemTemplate.SystemFile.Patch,
                        FileDesc = checkListItemTemplate.SystemFile == null ? "" : checkListItemTemplate.SystemFile.Description,
                        Form = checkListItemTemplate.Form,
                        DueDate =session.CheckListItemSubmitionTracks.Any(c => c.CheckListItemTemplate.CheckListItemTemplateId == checkListItemTemplate.CheckListItemTemplateId)?session.CheckListItemSubmitionTracks.First(c => c.CheckListItemTemplate.CheckListItemTemplateId == checkListItemTemplate.CheckListItemTemplateId).DueDate.Value.ToShortDateString():null
                        
                        
                    });
            }
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
            if (track == null)
            {
                return "red";
            }
            else
            {
                if (checkListItemTemplate.Provisional)
                {
                    return "yellow";
                }
                else
                {
                    return "green";
                }
            }
        }

        public ActionResult UploadFile(HttpPostedFileBase docSubmitted,int itemTemplateId)
        {
            if (docSubmitted == null)
                return new HttpStatusCodeResult(400, "File Is required");
            var track = UserSession.GetTrack(itemTemplateId);
            if (track.SubmittedFile == null)
            {
                track.SubmittedFile = new SystemFile();
                Db.SystemFiles.Add(track.SubmittedFile);
            }
            string filename = DateTime.Now.Ticks.ToString()+"-" + Path.GetFileName(docSubmitted.FileName);
            if(!Directory.Exists(Server.MapPath("/Files")))
            {
                Directory.CreateDirectory(Server.MapPath("/Files"));
            }
            docSubmitted.SaveAs(Server.MapPath("/Files/")+filename);
            track.SubmittedFile.Description = filename;
            track.Checked = true;
            var storage = new FileStorage();
            storage.PutFile(docSubmitted,track.SubmittedFile);
            Db.SaveChanges();
            return Json(new FileModel(){Description = track.SubmittedFile.Description,Url = "/files/"+filename});
        }

        public ActionResult Check(int id)
        {
            var track = UserSession.GetTrack(id);
            track.Checked = true;
            return Json(true);
        }

        public ActionResult CloseCurrentSession()
        {
            this.UserSession.GetCurrent().Active = false;
            Db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}