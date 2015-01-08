using System;
using System.Linq;
using System.Data.Entity;
using System.Data;
using System.Web.Mvc;
using Cico.Models;

namespace Cico.Areas.Admin
{
    public class EmployeeController : Cico.Controllers.ControllerBase
    {
       
        public ViewResult Index(EmployeeIndexModel model)
        {
            model.Requery(Db);
            bool edit = false;
            if (User.IsInRole(SystemRole.OfficeAdmin))
            {
                var staff = UserSession.GetCurrentStaff();
                if (staff.Office.Name == "HR")
                {
                    edit = true;
                }
            }
            model.EditEnabled = edit || User.IsInRole(SystemRole.GlobalAdmin);

            return View(model);
        }

        public ViewResult DependentIndex(int? id)
        {
            Employee employee = Db.Employees.Find(id);
            return View(employee);
        }

        //
        // GET: /Admin/Employees/Details/5

        public ViewResult Details(int id)
        {
            Employee employee =
                Db.Employees.Include("CheckListSessions")
                    .Include("CheckListSessions.ChecklistTemplate")
                    .Single(c => c.Id == id);
            var model = new EmployeeModel(UserSession) {Employee = employee};
            var staff = UserSession.GetCurrentStaff();
            if (staff.Office != null)
            {
                // model.UserAccessRights = Db.AccessRights.Where(a => a.Office.Name == staff.Office.Name).ToList();
            }
            model.Load(Db);
            model.EditEnabled = false;
            return View(model);
        }

        [HttpPost]
        public ActionResult Details(EmployeeModel model)
        {
            Employee employee =
                Db.Employees.Include("CheckListSessions")
                    .Include("CheckListSessions.ChecklistTemplate")
                    .Single(c => c.Id == model.Employee.Id);
            model.Employee = employee;
            model.UserSession = UserSession;
            model.Load(Db);
            if (!employee.TourEndDate.HasValue)
            {
                ModelState.AddModelError("", "Departure Date is required");

                return View(model);
            }
            else
            {
                UserSession.InitCheckOutSession(employee);
                return RedirectToAction("Details", new {id = model.Employee.Id});
            }

        }


        [HttpPost]
        public ActionResult StartCheckin(Employee model)
        {
            return RedirectToAction("initialize", "home", new {area = "", employeeId = model.Id});
        }

        //
        // GET: /Admin/Employees/Create

        public ActionResult Create()
        {
            var model = new EmployeeModel(UserSession) {Employee = new Employee()};
            model.Load(Db);
            if (UserSession.GetCurrent().Employee.Id == model.Employee.Id)
            {
                model.EditEnabled = true;
            }
            else
            {
                model.EditEnabled = false;
                model.AdminEditEnabled = true;
            }
            
            return View(model);
        }


        
        //
        // POST: /Admin/Employees/Create

        [HttpPost]
        public ActionResult Create(EmployeeModel model)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(model.SelectedProxy))
                {
                    model.Employee.Proxy = Db.Staffs.Find(model.SelectedProxy);
                }
                var emp = Db.Employees.FirstOrDefault(e => e.UserId == model.Employee.UserId);
                if (emp == null)
                {
                    Db.Employees.Add(model.Employee);
                    Db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "User " + model.Employee.UserId + " already exists");
                }
            }
            model.UserSession = UserSession;
            model.Load(Db);
            model.EditEnabled = true;
            return View(model);
        }

        //
        // GET: /Admin/Employees/Edit/5

        public ActionResult Edit(int id)
        {
            Employee employee = Db.Employees.Find(id);
            var model = new EmployeeModel(UserSession)
            {
                Employee = employee,
                AccessRights = Db.AccessFieldRights.Include("Office").Include("AccessField").ToList(),
                //Staff = UserSession.GetCurrentStaff(),
                // **edit invoked from admin menu should follow access rights - filterec by office access
                AdminEditEnabled = true
            };
            
            model.Load(Db);

            return View(model);
        }


        //
        // POST: /Admin/Employees/Edit/5

        [HttpPost]
        public ActionResult Edit(EmployeeModel model)
        {
            var employee = Db.Employees.Single(c => c.Id == model.Employee.Id);
            SecurityGuard.CanEditEmployee(employee, ModelState);
            if (ModelState.IsValid)
            {
                var checklist =
                    Db.CheckListSessions.Include("CheckListTemplate")
                        .FirstOrDefault(c => c.Employee.Id == model.Employee.Id && c.Active);
                if (employee.ArrivalDate.HasValue && model.Employee.ArrivalDate.HasValue &&
                    employee.ArrivalDate.Value.Date != model.Employee.ArrivalDate.Value.Date)
                {
                    if (checklist != null)
                    {
                        if (checklist.CheckListTemplate.Type.ToUpper() == "CHECKIN")
                        {
                            checklist.ReferenceDate = model.Employee.ArrivalDate.Value;
                        }

                    }
                }

                if (employee.TourEndDate.HasValue && model.Employee.TourEndDate.HasValue &&
                    employee.TourEndDate.Value.Date != model.Employee.TourEndDate.Value.Date)
                {
                    if (checklist.CheckListTemplate.Type.ToUpper() == "CHECKOUT")
                    {
                        checklist.ReferenceDate = model.Employee.TourEndDate.Value;
                    }
                }

                CopyValues(model.Employee, employee);
                Db.Entry(employee).State = EntityState.Modified;
                if (!string.IsNullOrEmpty(model.SelectedProxy))
                {
                    var staff = Db.Staffs.Find(model.SelectedProxy);
                    employee.Proxy = staff;
                    //model.Employee.Proxy = new Staff() {UserId = model.SelectedProxy};
                }
                else
                {
                    employee.Proxy = null;
                    Db.Entry(employee).Reference(c => c.Proxy).CurrentValue = null;
                }


                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            model.UserSession = UserSession;
            model.Staff = model.UserSession.GetCurrentStaff();
            model.Proxies =
            Db.Staffs.Include("SystemRoles")
                .Where(c => c.SystemRoles.Any(d => d.Name == SystemRole.UserProxy))
                .ToList()
                .Select(c => new SelectListItem() { Text = c.UserId, Value = c.UserId })
                .ToList();
            if (model.Employee != null && model.Employee.Proxy != null)
            {
                model.SelectedProxy = model.Employee.Proxy.UserId;
            }
            model.AdminEditEnabled = true;
            return View(model);
        }

        //
        // GET: /Admin/Employees/Delete/5

        public ActionResult Delete(int id)
        {
            Employee employee = Db.Employees.Find(id);
            return View(employee);
        }

        //
        // POST: /Admin/Employees/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Employee employee = Db.Employees.Find(id);
                // Db.Employees.Remove(employee);
                employee.Active = false;
                foreach (var checkListSession in employee.CheckListSessions)
                {
                    checkListSession.Active = false;
                }
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                DontSave = true;
                ModelState.AddModelError("", "You can't delete an employee that has an active process");
                return View();
            }
        }


    }
}