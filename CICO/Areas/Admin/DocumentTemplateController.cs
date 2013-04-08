using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cico.Models;

namespace Cico.Areas.Admin
{ 
    public class DocumentTemplateModel
    {
        public DocumentTemplate DocumentTemplate { get; set; }
        public HttpPostedFileBase File { get; set; }
    }


    public class DocumentTemplateController : Cico.Controllers.ControllerBase
    {
       

        //
        // GET: /Admin/DocumentTemplate/

        public ViewResult Index()
        {
            return View(Db.DocumentTemplates.ToList());
        }

        //
        // GET: /Admin/DocumentTemplate/Details/5

        public ViewResult Details(int id)
        {
            DocumentTemplate DocumentTemplate = Db.DocumentTemplates.Find(id);
            return View(DocumentTemplate);
        }

        //
        // GET: /Admin/DocumentTemplate/Create

        public ActionResult Create()
        {
            return View(new DocumentTemplateModel(){DocumentTemplate = new DocumentTemplate()});
        } 

        //
        // POST: /Admin/DocumentTemplate/Create

        private void ValidateFile()
        {
            if (!Request.Files.AllKeys.Contains("File") || Request.Files["File"].ContentLength==0)
            {
                ModelState.AddModelError("File","File is required");
            }
        }

        [HttpPost]
        public ActionResult Create(DocumentTemplateModel model)
        {
            ValidateFile();
            if (ModelState.IsValid)
            {
                var fileName = Path.GetFileName(model.File.FileName);
                model.File.SaveAs(Request.MapPath("/content/doctemplates/"+fileName));
                model.DocumentTemplate.SystemFile = new SystemFile(model.File, "DocTemplate", model.DocumentTemplate.DocumentTitle) { Path = "/content/doctemplates/" + fileName };
                Db.DocumentTemplates.Add(model.DocumentTemplate);
                Db.SaveChanges();
                return RedirectToAction("Index");  
            }
            
            return View(model);
        }
        
        //
        // GET: /Admin/DocumentTemplate/Edit/5
 
        public ActionResult Edit(int id)
        {
            DocumentTemplate DocumentTemplate = Db.DocumentTemplates.Find(id);
            var model = new DocumentTemplateModel() {DocumentTemplate = DocumentTemplate};
            return View(model);
        }

        //
        // POST: /Admin/DocumentTemplate/Edit/5

        [HttpPost]
        public ActionResult Edit(DocumentTemplateModel documentTemplate)
        {
            ValidateFile();
            if (ModelState.IsValid)
            {
                int id = Int32.Parse(Request.Form["DocumentTemplate.Id"]);
                var template = Db.DocumentTemplates.Single(c => c.Id == id);
                var fileName = Path.GetFileName(Request.Files["File"].FileName);
                Request.Files["File"].SaveAs(Request.MapPath("/content/doctemplates/" + fileName));
                var myfile = Request.Files["File"];
                template.DocumentTitle = Request.Form["DocumentTemplate.DocumentTitle"];
                template.SystemFile.Description = Request.Form["DocumentTemplate.DocumentTitle"];
                template.SystemFile.Path="/doctemplates/"+Path.GetFileName(myfile.FileName);
                template.SystemFile.Extension = Path.GetExtension(myfile.FileName);
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                int id = Int32.Parse(Request.Form["DocumentTemplate.Id"]);
                var template = Db.DocumentTemplates.Single(c => c.Id == id);
                documentTemplate.DocumentTemplate = template;
                return View(documentTemplate);
            }
        }

        //
        // GET: /Admin/DocumentTemplate/Delete/5
 
        public ActionResult Delete(int id)
        {
            DocumentTemplate DocumentTemplate = Db.DocumentTemplates.Find(id);
            return View(DocumentTemplate);
        }

        //
        // POST: /Admin/DocumentTemplate/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            DocumentTemplate DocumentTemplate = Db.DocumentTemplates.Find(id);
            Db.DocumentTemplates.Remove(DocumentTemplate);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }

       
    }
}