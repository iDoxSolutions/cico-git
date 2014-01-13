using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cico.Models;
using Cico.Models.Helpers;

namespace Cico.Controllers
{
    public class EmergencyDataDto
    {
        public EmergencyDataDto(){}
        [Required(ErrorMessage = "Contact Email Address is Required")]
        [RegularExpression("^[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?$", ErrorMessage = "Invalid e-mail.")]
        public string ContactEmailAddress { get; set; }
        [Required(ErrorMessage = "Contact Office Phone is Required")]
        
        public string ContactOfficePhone { get; set; }
        
        public string ContactPhone { get; set; }
        public string ContactRepationship { get; set; }
        public int EmployeeId { get; set; }
        public EmergencyDataDto(Employee employee)
        {
            this.ContactEmailAddress = employee.EmergencyContactEmail??"";
            this.ContactOfficePhone = employee.EmergencyContactOfficePhone??"";
            this.ContactPhone = employee.EmergencyContactPhone??"";
            this.ContactRepationship = employee.EmergencyContactRelationship??"";
            this.EmployeeId = employee.Id;
        }
    }

    public class EmergencyDataController : Cico.Controllers.ControllerBase
    {
        //
        // GET: /EmergencyData/

        public ActionResult GetByCheckListId(int id)
        {
            var session = Db.CheckListSessions.Single(c => c.Id == id);
            return Json(new EmergencyDataDto(session.Employee));
        }
        [HandleModelStateException]
        [HttpPost]
        public ActionResult Save(EmergencyDataDto model)
        {
            if (ModelState.IsValid)
            {
                var emp = Db.Employees.Single(c => c.Id == model.EmployeeId);
                emp.EmergencyContactEmail = model.ContactEmailAddress;
                emp.EmergencyContactOfficePhone = model.ContactOfficePhone;
                emp.EmergencyContactPhone  =model.ContactPhone  ;
                 emp.EmergencyContactRelationship = model.ContactRepationship ;
                return Json(model);
            }
            else
            {
                throw new ModelStateException(ModelState);
            }
        }

    }
}
