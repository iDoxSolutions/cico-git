using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace Cico.Models.SharePoint
{
    public class FileStorage
    {
        SharePointDocumentsQuery _query = new SharePointDocumentsQuery();
        public void PutFile(HttpPostedFileBase postedFile, SystemFile systemFile)
        {
            var username = HttpContext.Current.User.Identity.Name;
            if (username.Contains("\\"))
            {
                username = username.Substring(username.IndexOf("\\") + 1);
            }
            _query.CreateFolder(username);
            var buffer = new byte[postedFile.InputStream.Length];
            postedFile.InputStream.Read(buffer, 0, (int) postedFile.InputStream.Length);
            var fName = DateTime.Now.Ticks.ToString(CultureInfo.InvariantCulture) + Path.GetFileName(postedFile.FileName);
            var path = _query.Save(buffer, new Dictionary<string, object>(),username+"/"+fName );
            systemFile.Description = fName;
            systemFile.Patch = path;
        }

        public byte[] GetFile(string url)
        {

           return  _query.GetFile(url);
        }
    }
}