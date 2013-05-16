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

        

        public bool CanCompleteCheckListItem(CheckListItemSubmitionTrack track)
        {
            var usersession = new UserSession(_db, _http);
            var staff = usersession.GetCurrentStaff();
            var itemTemplate = track.CheckListItemTemplate;
            var session = track.CheckListSession;
            if (IsUsersCheckList(session))
            {
                return true;
            }

            if (_http.User.IsInRole(SystemRole.GlobalAdmin))
                return true;

            if (staff != null)
            {

                if (staff.Office.Name == itemTemplate.Office.Name)
                {
                    return true;
                }
            }

            if (session.Employee.Proxy != null)
            {
                if (session.Employee.Proxy.UserId.Equals(_http.User.Identity.Name,StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsUsersCheckList(CheckListSession session)
        {
            return string.Equals( session.UserId.TrimEnd() ,_http.User.Identity.Name,StringComparison.OrdinalIgnoreCase);
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

            if (employee.Proxy != null)
            {
                if (employee.Proxy.UserId.Equals(_http.User.Identity.Name,StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            
            if (staff == null)
                return false;

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
            return CanEditEmployee(employee.Employee, modelState);
        }
    }
}