using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cico.Controllers
{
    public class NotesController : ControllerBase
    {
        //
        // GET: /Notes/

        public ActionResult Index(int checklistItemTemplateId)
        {
            var track = UserSession.GetTrack(checklistItemTemplateId);
            return Json(track.Notes);
        }

    }
}
