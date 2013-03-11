using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Cico.Models.Authentication
{


    public class UserSession
    {
        public UserSession(CicoContext db,HttpContextBase httpContext)
        {
            _db = db;
            _httpContext = httpContext;
        }

        private readonly CicoContext _db = null;
        private readonly HttpContextBase _httpContext;

        public CheckListSession GetCurrent()
        {
            var uname = _httpContext.User.Identity.Name;
            var session = _db.CheckListSessions.Include("CheckListTemplate").Include("CheckListItemSubmitionTracks").SingleOrDefault(c => c.UserId == uname && c.Active);
            if (session == null)
            {
                var template = GetCurrentTemplate();

                var employee = _db.Employees.FirstOrDefault(c => c.UserId == uname);
                if (employee == null)
                {
                    employee = new Employee() {UserId = uname};
                    _db.Employees.Add(employee);
                }
                session = _db.CheckListSessions.Create();
                session.UserId = uname;
                session.CheckListTemplate = template;
                session.Employee = employee;
                var res = _db.CheckListSessions.Add(session);
                foreach (var checkListItemTemplate in template.CheckListItemTemplates)
                {
                    _db.CheckListItemSubmitionTracks.Add(new CheckListItemSubmitionTrack()
                        {
                            CheckListItemTemplate = checkListItemTemplate,CheckListSession = res
                        });
                }
                _db.SaveChanges();
                return res;
            }
            else
            {
                return session;
            }
        }

        public CheckListTemplate GetCurrentTemplate()
        {
            var template = _db.Settings.First(c => c.Name == "checklisttemplate");
            var templateId = Int32.Parse(template.Value);
            return _db.CheckListTemplates.First(c => c.CheckListTemplateId == templateId);
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