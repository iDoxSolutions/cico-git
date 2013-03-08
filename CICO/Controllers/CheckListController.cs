﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cico.Controllers.ViewModels;
using Cico.Models;

namespace Cico.Controllers.ViewModels
{
    public class NoteModel
    {
        public string Content { get; set; }
        public string DateCreated { get; set; }
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

        public IList<NoteModel> Notes
        {
            get; set; }
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
                var itemCssClass = GetItemCssClass(checkListItemTemplate, session.CheckListItemSubmitionTracks);
                var notes = GetNotes(checkListItemTemplate, session.CheckListItemSubmitionTracks);
                model.CheckListItems.Add(new CheckListItemModel
                    {
                        Id = checkListItemTemplate.CheckListItemTemplateId,
                        ItemTemplate = checkListItemTemplate.Item,
                        Description = checkListItemTemplate.Description,
                        Checked = session.CheckListItemSubmitionTracks.Any(c => c.CheckListItemTemplate.CheckListItemTemplateId == checkListItemTemplate.CheckListItemTemplateId),
                        CssClass = itemCssClass,
                        Notes = notes
                    });
            }
            return Json(model);
        }

        private IList<NoteModel> GetNotes(CheckListItemTemplate checkListItemTemplate, ICollection<CheckListItemSubmitionTrack> checkListItemSubmitionTracks)
        {
            var track =
                checkListItemSubmitionTracks.FirstOrDefault(
                    c =>
                    c.CheckListItemTemplate.CheckListItemTemplateId == checkListItemTemplate.CheckListItemTemplateId);
            if(track==null)
                return new List<NoteModel>();
            else
            {
                return track.Notes.Select(c=>new NoteModel(){Content = c.Content,DateCreated = c.DateCreated.ToString()}).ToList();
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
            var session = UserSession.GetCurrent();
            var completion = Db.CheckListItemSubmitionTracks.Include("CheckListItemTemplate")
                .SingleOrDefault(c => c.CheckListItemTemplate.CheckListItemTemplateId == itemTemplateId && c.CheckListSession.Id == session.Id);
            var itemTemplate = Db.CheckListItemTemplates.First(c => c.CheckListItemTemplateId==itemTemplateId);
            if (completion == null)
            {
                completion = new CheckListItemSubmitionTrack()
                    {
                        CheckListItemTemplate = itemTemplate,
                        CheckListSession = session
                    };
                session.CheckListItemSubmitionTracks.Add(completion);
                Db.SaveChanges();
            }
            var model = new CheckListItemModel()
                {
                    Id = completion.CheckListItemTemplate.CheckListItemTemplateId,
                    ItemTemplate = completion.CheckListItemTemplate.Item,
                    Description = completion.CheckListItemTemplate.Description,
                    Checked = true,
                    CssClass = GetItemCssClass(itemTemplate, Db.CheckListItemSubmitionTracks.Include("CheckListItemTemplate").Where(c => c.CheckListSession.Id == session.Id).ToList())
                };
            return Json(model);
        }

        public ActionResult CloseCurrentSession()
        {
            this.UserSession.GetCurrent().Active = false;
            Db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}