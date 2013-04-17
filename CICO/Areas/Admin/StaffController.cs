using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cico.Models;

namespace Cico.Areas.Admin
{
    public class StaffModel
    {
        public StaffModel()
        {
            SelectedRoles = new string[]{};
        }
        public Staff Staff { get; set; }
        public IList<SelectListItem> Roles { get; set; }
        public IList<SelectListItem> Offices { get; set; }
        public int? SelectedOffice { get; set; }
        public string[] SelectedRoles { get; set; }
        public void Load(ICicoContext context)
        {
            Roles = context.SystemRoles.ToList().Select(c => new SelectListItem(){Text = c.Name,Value = c.Name,Selected = Staff.SystemRoles.Any(d=>d.Name==c.Name)}).ToList();
            Offices = context.Offices.ToList().Select(c => new SelectListItem(){Text = c.Name,Value = c.OfficeId.ToString()}).ToList();
            if (Staff.Office != null)
            {
                SelectedOffice = Staff.Office.OfficeId;

            }
        }
    }
    public class StaffController : Cico.Controllers.ControllerBase
    {
        public ActionResult Index()
        {
            var staff = Db.Staffs.Include("Office").ToList();
            return View(staff);
        }

        public void Validate(StaffModel model)
        {
            if (model.SelectedRoles == null || model.SelectedRoles.Length==0)
            {
                ModelState.AddModelError("", "At least one role have to be selected");
                return;
            }
            if (model.SelectedRoles.Contains("OfficeAdmin") && !model.SelectedOffice.HasValue)
            {
                ModelState.AddModelError("","You need to pick the office for OfficeAdmin");
            }

            if (model.SelectedRoles.Contains("OfficeAdmin") && model.SelectedRoles.Contains(SystemRole.GlobalAdmin))
            {
                ModelState.AddModelError("", "Please check OfficeAdmin or " + SystemRole.GlobalAdmin +" not both");
            }

            if (!model.SelectedRoles.Contains("OfficeAdmin") && model.SelectedRoles.Contains(SystemRole.CheckListEditor))
            {
                ModelState.AddModelError("", "CheckListEditor is valid only for OfficeAdmin ");
            }

        }

        public ActionResult Create()
        {
            var model = new StaffModel() {Staff = new Staff(){SystemRoles = new List<SystemRole>()}};
            model.Load(Db);
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(StaffModel model)
        {
            Validate(model);
            if (ModelState.IsValid)
            {
                if (model.SelectedOffice.HasValue)
                {
                    model.Staff.Office = Db.Offices.Single(c => c.OfficeId == model.SelectedOffice);
                }
                Db.Staffs.Add(model.Staff);
               if (model.SelectedRoles != null)
               {
                   model.Staff.SystemRoles = model.Staff.SystemRoles ?? new List<SystemRole>();
                   foreach (var role in model.SelectedRoles)
                   {
                       var orole = Db.SystemRoles.Single(c => c.Name == role);
                       orole.Staffs.Add(model.Staff);
                   }
               }
                return RedirectToAction("index");
            }
            else
            {
                model.Staff.SystemRoles = new List<SystemRole>();
                model.Load(Db);
                return View(model);
            }
            
        }


        public ActionResult Edit(string userid)
        {
            var staff = Db.Staffs.Single(c => c.UserId == userid);
            var model = new StaffModel(){Staff = staff};
            model.Load(Db);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(StaffModel model)
        {
            Validate(model);
            var staff = Db.Staffs.Single(c => c.UserId == model.Staff.UserId);
            if (ModelState.IsValid)
            {
                if (model.SelectedOffice.HasValue)
                {
                    staff.Office = Db.Offices.Single(c => c.OfficeId == model.SelectedOffice);
                }
                else
                {
                    staff.Office = null;
                    Db.Entry(staff).Reference(c => c.Office).CurrentValue = null;
                    
                }
                staff.Email = model.Staff.Email;
                staff.ReqireCheckList = model.Staff.ReqireCheckList;
                if (model.SelectedRoles != null)
                {
                    foreach (var selRole in model.SelectedRoles)
                    {
                        if (staff.SystemRoles.All(c => c.Name != selRole))
                        {
                            var orole = Db.SystemRoles.Single(c => c.Name == selRole);
                            staff.SystemRoles.Add(orole);
                            orole.Staffs.Add(staff);
                            //Db.Entry(staff).Member()
                        }
                    }
                    var toRemove = staff.SystemRoles.Where(c => !model.SelectedRoles.Any(d => d == c.Name)).ToList();
                    foreach (var systemRole in toRemove)
                    {
                        staff.SystemRoles.Remove(systemRole);
                    }
                }
                else
                {
                    staff.SystemRoles.Clear();
                }
                return RedirectToAction("index");
            }
            else
            {
                model.Staff = staff;
                model.Load(Db);
                return View(model);
            }
            

        }
    }

}