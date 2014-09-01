using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cico.Models.Authentication;

namespace Cico.Models.Services
{
    public class ProxyModel
    {
        public IList<SelectListItem> ProxiedList { get; set; }
        public int? EmployeeId { get; set; }
    }

    public class ProxyService
    {
        private readonly HttpContextBase _context;
        private readonly ICicoContext _db;
        private UserSession _userSession;
        public ProxyService(HttpContextBase context,ICicoContext db)
        {
            _context = context;
            _db = db;
            _userSession = new UserSession(_db,context);
        }

        public ProxyModel GetModel()
        {
            var staff = _userSession.GetCurrentStaff();
            if (staff == null)
                return null;
            var model = new ProxyModel();
            if (staff.Proxied.Count == 0)
               return null;
            
            model.ProxiedList = staff.Proxied.Select(c => new SelectListItem() {Text = c.LastName+", "+c.FirstName,Value = c.Id.ToString()}).ToList();
            model.ProxiedList.Add(new SelectListItem() {Text = _userSession.GetCurrent().Employee.LastName+", "+_userSession.GetCurrent().Employee.FirstName,Value = _userSession.GetCurrent().Employee.Id.ToString()});

            return model;
        }

        

    }
}