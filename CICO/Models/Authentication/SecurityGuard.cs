using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cico.Models.Authentication
{
    public class SecurityGuard
    {
        private readonly ICicoContext _db;
        private readonly HttpContextBase _http;

        public SecurityGuard(ICicoContext db,HttpContextBase http)
        {
            _db = db;
            _http = http;
        }

        public bool CanEditEmployee(Employee employee,ModelStateDictionary modelState)
        {
            var usersession = new UserSession(_db, _http);
            var staff = usersession.GetCurrentStaff();
            var sessions = _db.CheckListSessions.FirstOrDefault(c => c.UserId == _http.User.Identity.Name);
            if (sessions != null)
            {
                if (sessions.Employee.Id == employee.Id)
                {
                    return true;
                }
            }
            if (staff.SystemRoles.Any(c => c.Name == SystemRole.GlobalAdmin))
            {
                return true;
            }
            if (staff.Office.Name == "HR")
                return true;
            modelState.AddModelError("","Permission denied!");

            return false;

        }

        public bool CanEditDependent(Dependent employee, ModelStateDictionary modelState)
        {
            var usersession = new UserSession(_db, _http);
            var staff = usersession.GetCurrentStaff();
            var sessions = _db.CheckListSessions.FirstOrDefault(c => c.UserId == _http.User.Identity.Name);
            if (sessions != null)
            {
                if (sessions.Employee.Id == employee.Employee.Id)
                {
                    return true;
                }
            }
            if (staff.SystemRoles.Any(c => c.Name == SystemRole.GlobalAdmin))
            {
                return true;
            }
            if (staff.Office.Name == "HR")
                return true;
            modelState.AddModelError("", "Permission denied!");

            return false;

        }
    }
}