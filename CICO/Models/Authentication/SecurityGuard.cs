using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;

namespace Cico.Models.Authentication
{
    public class SecurityGuard
    {
        private readonly ICicoContext _db;
        private readonly HttpContextBase _http;
        private static readonly ILog log = LogManager.GetLogger(typeof(SecurityGuard).Name);
        private UserSession _usersession;

        public SecurityGuard(ICicoContext db,HttpContextBase http)
        {
            _db = db;
            _http = http;
            _usersession = new UserSession(_db, _http);
        }


        public bool ViewNotes(CheckListItemSubmitionTrack track)
        {
            if (IsUsersCheckList(track.CheckListSession))
            {
                return false;
            }
            if (track.CheckListItemTemplate.NotesAccess)
            {
                var staff = _usersession.GetCurrentStaff();
                if (staff != null && _http.User.IsInRole(SystemRole.OfficeAdmin))
                {
                    if (staff.Office.OfficeId != track.CheckListItemTemplate.Office.OfficeId)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool NotesEnabled(CheckListItemSubmitionTrack track)
        {
            if (IsUsersCheckList(track.CheckListSession))
            {
                return true;
            }

            if (!track.CheckListItemTemplate.NotesAccess)
            {
                var staff = _usersession.GetCurrentStaff();
                if (staff != null && _http.User.IsInRole(SystemRole.OfficeAdmin))
                {
                    if (staff.Office.OfficeId != track.CheckListItemTemplate.Office.OfficeId)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool CanCompleteCheckListItem(CheckListItemSubmitionTrack track)
        {
            log.DebugFormat("{0} user determine can complete ",_http.User.Identity.Name);
            
            var staff = _usersession.GetCurrentStaff();
            var itemTemplate = track.CheckListItemTemplate;
            var session = track.CheckListSession;
            log.DebugFormat("{0} session owner ", session.UserId);
            if (IsUsersCheckList(session))
            {
                return true;
            }
            log.DebugFormat("not session owner session owner ", session.UserId);
            if (_http.User.IsInRole(SystemRole.GlobalAdmin))
                return true;
            log.DebugFormat("not global admin  ", session.UserId);

            if (session.Employee.Proxy != null)
            {
                if (session.Employee.Proxy.UserId.Equals(_http.User.Identity.Name, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            log.DebugFormat("not ritht proxy person  ", session.UserId);

            if (staff != null)
            {

                if (staff.Office.Name == itemTemplate.Office.Name)
                {
                    return true;
                }
            }
            log.DebugFormat("not ritht office admin  ", session.UserId);
            
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
            var sessions = _db.CheckListSessions.FirstOrDefault(c => c.UserId == _http.User.Identity.Name && c.Active);
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