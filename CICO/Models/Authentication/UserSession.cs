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
        private CheckListSession InitCheckListSession(string uname, CheckListTemplate template, InitModel initmodel)
        {
            CheckListSession session;
            var employee = _db.Employees.FirstOrDefault(c => c.UserId == uname);
            if (employee == null)
            {
                employee = new Employee() {UserId = uname,GivenName = initmodel.GivenName,Surname = initmodel.Surname,DateOfBirth = initmodel.Dob};
                _db.Employees.Add(employee);
            }
            session = _db.CheckListSessions.Create();
            session.UserId = uname;
            session.CheckListTemplate = template;
            session.Employee = employee;
            session.ArrivalDate = initmodel.ArrivalDate;
            var res = _db.CheckListSessions.Add(session);
            foreach (var checkListItemTemplate in template.CheckListItemTemplates)
            {
                _db.CheckListItemSubmitionTracks.Add(new CheckListItemSubmitionTrack()
                    {
                        CheckListItemTemplate = checkListItemTemplate,
                        CheckListSession = res,
                        DueDate = initmodel.ArrivalDate.AddDays(checkListItemTemplate.DueDays)
                    });
            }
            _db.SaveChanges();
            return res;
        }

        public CheckListTemplate GetCurrentTemplate()
        {
            var template = _db.Settings.First(c => c.Name == "checklisttemplate");
            var templateId = Int32.Parse(template.Value);
            return _db.CheckListTemplates.Single(c => c.Published && c.Active);
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