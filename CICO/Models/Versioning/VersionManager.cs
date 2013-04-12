using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cico.Models.Helpers;

namespace Cico.Models.Versioning
{
    public static class VersionManager
    {
        public static string GetVersion()
        {
            var context = new CicoContext();
            return context.Settings.Single(c => c.Name == "AppVersion").Value;
        }

        public static string VerRes(this HtmlHelper helper, string path)
        {
            var ver = CacheHelper.Cache(GetVersion,"version_cache");
            return path + "?ver=" + ver;
        }
    }
}