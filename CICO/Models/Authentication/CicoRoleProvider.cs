using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;

namespace Cico.Models.Authentication
{
    public class CicoRoleProvider:RoleProvider
    {
        public override bool IsUserInRole(string username, string roleName)
        {
            using (var db = new CicoContext())
            {
                var staff = db.Staffs.FirstOrDefault(c => c.UserId == username);
                if (staff != null)
                {
                    return staff.SystemRoles.Any(c => c.Name == roleName);

                }
                else
                {
                    return false;
                }
            }
        }

        public override string[] GetRolesForUser(string username)
        {
            var userName = HttpContext.Current.User.Identity.Name;
            //trim off the domain - not needed because OpenNet userids are unique across domains
            var dName = Regex.Replace(userName, ".*\\\\(.*)", "$1", RegexOptions.None);
            using (var db = new CicoContext())
            {
                var staff = db.Staffs.FirstOrDefault(c => c.UserId == dName || c.UserId == userName);
                if (staff != null)
                {
                    var roles = staff.SystemRoles.ToList();
                    return roles.Select(c => c.Name).ToArray();
                }
                else
                {
                    return new string[] {};
                }
            }

        }
        //public override bool IsInRole(string role)
        //{

        //    var userName = HttpContext.Current.User.Identity.Name;
        //    //trim off the domain - not needed because OpenNet userids are unique across domains
        //    var dName = Regex.Replace(userName, ".*\\\\(.*)", "$1", RegexOptions.None);
        //    using (var db = new CicoContext())
        //    {
        //        var staff = db.Staffs.FirstOrDefault(c => c.UserId == dName);
        //        if (staff != null)
        //        {
        //            return staff.SystemRoles.Any(c => c.Name == role);
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }

        //    return false;
        //}

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}