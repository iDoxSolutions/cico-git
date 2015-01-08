using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cico.Models;
using ControllerBase = Cico.Controllers.ControllerBase;

namespace Cico.Areas.Admin
{
    public class AccessRightsJsModel
    {
        public object[] Offices { get; set; }
        public object[] Fields { get; set; }
        public object[] AccessRights { get; set; }
    }

    

    public class AccessRightsController : ControllerBase
    {
        public ActionResult Index()
        {
            return View();

        }
        [HttpPost]
        public ActionResult GetModel(string empDep)
        {
            var offices = Db.Offices.ToList();
            var fields = Db.AccessFields.Where(a => a.EmpDep==empDep).OrderBy(a => a.FieldName).ToList();
            var accessRights = Db.AccessFieldRights.Where(c=>c.EmpDep==empDep).ToList();
            var model = new AccessRightsJsModel()
            {
                Offices = (from o in offices select (object) new {officeName=o.Name,officeId=o.OfficeId}).ToArray(),
                Fields = (from o in fields select (object)new { fieldName = o.FieldName, fieldId = o.Id }).ToArray(),
                AccessRights = (from o in accessRights select (object)new { officeId = o.Office.OfficeId, fieldId = o.AccessField.Id,access=o.Access,EmpDep=o.EmpDep }).ToArray(),
            };
            return Json(model);
        }
        [HttpPost]
        public ActionResult Set(int officeId, int fieldId, string access,string empDep)
        {
           var accessRecord =  Db.AccessFieldRights.FirstOrDefault(c => c.Office.OfficeId == officeId && c.AccessField.Id == fieldId && c.EmpDep==empDep);
            if (accessRecord == null)
            {
                var office = Db.Offices.First(c => c.OfficeId == officeId);
                var field = Db.AccessFields.First(c => c.Id == fieldId);
                accessRecord = new AccessFieldRight(){Office = office,AccessField = field,Access = access,EmpDep = empDep};
                Db.AccessFieldRights.Add(accessRecord);
            }
            accessRecord.Access = access;
            Db.SaveChanges();
            return Json(new { accesRightId=accessRecord.Id});

        }
        
    }
}