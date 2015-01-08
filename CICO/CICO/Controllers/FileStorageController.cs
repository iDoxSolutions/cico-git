using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cico.Models.SharePoint;

namespace Cico.Controllers
{
    public class FileStorageController : ControllerBase
    {
        //
        // GET: /Files/

        public ActionResult Index(int id)
        {
            var file = Db.SystemFiles.Single(c => c.Id == id);
            var storage = new FileStorage();
            var content = storage.GetFile(file.Path);
            return File(content, "application/octet",file.Description);
        }

    }
}
