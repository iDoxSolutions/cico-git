using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Cico.Models.Authentication
{
    public class UserSession
    {
        public UserSession(ICicoContext db,HttpContextBase httpContext)
        {
            _db = db;
            _httpContext = httpContext;
        }

        private readonly ICicoContext _db = null;
        private readonly HttpContextBase _httpContext;

        public string GetUserName()
        {
            return _httpContext.User.Identity.Name;
        }

        public bool IsInitialized
        {
            get
            {
                var uname = _httpContext.User.Identity.Name;
                var session = _db.CheckListSessions.Include("CheckListTemplate").Include("CheckListItemSubmitionTracks").SingleOrDefault(c => c.UserId == uname && c.Active);
                return session != null;
            }
        }

        public bool IsOfficeAdmin
        {
            get { return _httpContext.User.IsInRole("OfficeAdmin"); }
        }

        public CheckListSession GetCurrent()
        {
            var uname = _httpContext.User.Identity.Name;
            var session = _db.CheckListSessions.Include("CheckListTemplate").Include("CheckListItemSubmitionTracks").SingleOrDefault(c => c.UserId == uname && c.Active );
            if(session==null)
                throw new InvalidOperationException("Session not initialized!");
            return session;
        }
        public CheckListSession InitCheckListSession(InitModel initmodel)
        {
            var uname = _httpContext.User.Identity.Name;
            var template = GetCurrentTemplate();
            return InitCheckListSession(uname,template ,initmodel);
        }

        public CheckListSession InitCheckOutSession(Employee employee)
        {
            var existing = _db.CheckListSessions.Include("Employee").FirstOrDefault(c => c.Employee.Id == employee.Id);
            if (existing != null)
            {
                existing.Active = false;
            }
            var uname = _httpContext.User.Identity.Name;
            var template = _db.CheckListTemplates.Single(c => c.Type == "CheckOut");
            var session = _db.CheckListSessions.Create();
            session.UserId = uname;
            session.CheckListTemplate = template;
            session.Employee = employee;
            session.ReferenceDate = employee.TourEndDate.Value;
            var res = _db.CheckListSessions.Add(session);
            _db.SaveChanges();
            return res;
        }

        private CheckListSession InitCheckListSession(string uname, CheckListTemplate template, InitModel initmodel)
        {
            CheckListSession session;
            var employee = _db.Employees.FirstOrDefault(c => c.UserId == uname);
            if (employee == null)
            {
                employee = new Employee() {UserId = uname,GivenName = initmodel.GivenName,Surname = initmodel.Surname,PersonalEmail = initmodel.EmailAddress,EmployeeId = initmodel.EmployeeId,ArrivalDate = initmodel.ArrivalDate};
                _db.Employees.Add(employee);
            }
            session = _db.CheckListSessions.Create();
            session.UserId = uname;
            session.CheckListTemplate = template;
            session.Employee = employee;
            session.ReferenceDate = initmodel.ArrivalDate;
            var res = _db.CheckListSessions.Add(session);
           /* foreach (var checkListItemTemplate in template.CheckListItemTemplates)
            {
                _db.CheckListItemSubmitionTracks.Add(new CheckListItemSubmitionTrack()
                    {
                        CheckListItemTemplate = checkListItemTemplate,
                        CheckListSession = res
                    });
            }*/
            _db.SaveChanges();
            return res;
        }

        public CheckListTemplate GetCurrentTemplate()
        {
            var template = _db.Settings.First(c => c.Name == "checklisttemplate");
            var templateId = Int32.Parse(template.Value);
            return _db.CheckListTemplates.Single(c => c.CheckListTemplateId==templateId);
        }

        public Staff GetCurrentStaff()
        {
            var staff = _db.Staffs.Include("Office").Single(c => c.UserId == _httpContext.User.Identity.Name);
            return staff;
        }

        public CheckListItemSubmitionTrack GetTrack(int checklistItemTemplateId)
        {
            var session = GetCurrent();
            var track =
               session.
                CheckListItemSubmitionTracks.FirstOrDefault(
                    c => c.CheckListItemTemplate.CheckListItemTemplateId == checklistItemTemplateId);
            if (track == null)
            {
                var itemTemplate =
                    _db.CheckListItemTemplates.First(c => c.CheckListItemTemplateId == checklistItemTemplateId);
                track = new CheckListItemSubmitionTrack()
                    {
                        CheckListSession = session,
                        CheckListItemTemplate = itemTemplate
                    };
               track =  _db.CheckListItemSubmitionTracks.Add(track);
                _db.SaveChanges();
            }
            return track;
        }
    }
}