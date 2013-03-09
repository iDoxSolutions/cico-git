using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cico.Models;

namespace Cico.Controllers
{

    public class NoteViewModel
    {
        [Required]
        public string Content { get; set; }
        public int TemplateItemId { get; set; }
        public int Id { get; set; }
        public string DateCreated { get; set; }
        public string UserCreated { get; set; }
    }
    public class NotesController : ControllerBase
    {
        //
        // GET: /Notes/

        public ActionResult Index(int checklistItemTemplateId)
        {
            var track = UserSession.GetTrack(checklistItemTemplateId);
            return Json(track.Notes);
        }

        [HttpPost]
        public ActionResult Create(NoteViewModel model)
        {
            var track = UserSession.GetTrack(model.TemplateItemId);
            var note = new Note() {CheckListItemSubmitionTrack = track,Content = model.Content};
            Db.Notes.Add(note);
            Db.SaveChanges();
            return Json(new NoteViewModel() {Content = note.Content, Id = note.Id,DateCreated = note.DateEdited.ToString()});
        }

        public ActionResult Delete(int id)
        {

            var note = Db.Notes.Single(c => c.Id == id);
            Db.Notes.Remove(note);
            Db.SaveChanges();
            return Json(true);
        }
    }
}
