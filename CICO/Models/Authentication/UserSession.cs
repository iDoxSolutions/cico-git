using System;
using System.Collections.Generic;
using System.Data;
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

        
        public bool IsInitialized(int? empId)
        {
            if (empId.HasValue)
            {
                return _db.CheckListSessions.Include("Employee").Any(c => c.Employee.Id == empId.Value && c.Active);
            }
            else
            {
                var uname = _httpContext.User.Identity.Name;
                return _db.CheckListSessions.Include("Employee").Any(c => c.UserId == uname && c.Active);
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
            var employee = initmodel.EmpId.HasValue ? _db.Employees.FirstOrDefault(c => c.Id == initmodel.EmpId) : _db.Employees.FirstOrDefault(c => c.UserId == uname && c.Active);
            if (employee == null)
            {
                employee = new Employee() { UserId = uname, FirstName = initmodel.FirstName, LastName = initmodel.LastName, PersonalEmail = initmodel.EmailAddress, ArrivalDate = initmodel.ArrivalDate };
                _db.Employees.Add(employee);
            }
            else
            {
                employee.ArrivalDate = initmodel.ArrivalDate;
                employee.FirstName = initmodel.FirstName;
                employee.LastName = initmodel.LastName;
                employee.PersonalEmail = initmodel.EmailAddress;
            }
            return InitCheckListSession(employee,template ,initmodel);
        }

        public CheckListSession InitCheckOutSession(Employee employee)
        {
            var existing = _db.CheckListSessions.Include("Employee").FirstOrDefault(c => c.Employee.Id == employee.Id && c.Active);
            if (existing != null)
            {
                existing.Active = false;
                _db.Entry(existing).State = EntityState.Modified;
            }
            var uname = employee.UserId;
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

        private CheckListSession InitCheckListSession(Employee employee, CheckListTemplate template, InitModel initmodel)
        {
            CheckListSession session;
            
            
            session = _db.CheckListSessions.Create();
            session.UserId = employee.UserId;
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
            var staff = _db.Staffs.Include("Office").Include("Proxied").SingleOrDefault(c => c.UserId == _httpContext.User.Identity.Name);
            return staff;
        }


        public CheckListItemSubmitionTrack GetTrack(int checklistItemTemplateId,int checklistId)
        {
            var session = _db.CheckListSessions.Include("CheckListTemplate").Include("CheckListItemSubmitionTracks").Single(c => c.Id == checklistId && c.Active);
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
                track = _db.CheckListItemSubmitionTracks.Add(track);
                _db.SaveChanges();
            }
            return track;
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