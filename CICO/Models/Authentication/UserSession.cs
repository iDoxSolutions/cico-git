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
                var res = _db.CheckListSessions.Add(new CheckListSession() { UserId = uname,CheckListTemplate = template});
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
    }
}