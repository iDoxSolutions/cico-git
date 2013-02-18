using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cico.Models.Authentication
{
    public class UserSession
    {
        private CicoContext db = new CicoContext();
        public User GetCurrent()
        {
            var uname = HttpContext.Current.User.Identity.Name;
            var ouser=   db.Users.SingleOrDefault(c => c.UserId == uname);
            if (ouser == null)
            {
                var res = db.Users.Add(new User() {UserId = uname});
                return res;
            }
            else
            {
                return ouser;
            }
        }
    }
}