using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace Cico.Models.SharePoint
{
    public class FileStorage
    {
        SharePointDocumentsQuery _query = new SharePointDocumentsQuery();
        public void PutFile(HttpPostedFileBase postedFile, SystemFile systemFile)
        {
            var buffer = new byte[postedFile.InputStream.Length];
            postedFile.InputStream.Read(buffer, 0, (int) postedFile.InputStream.Length);
            var fName = "/test/"+DateTime.Now.Ticks.ToString(CultureInfo.InvariantCulture) + Path.GetFileName(postedFile.FileName);
            var path = _query.Save(buffer, new Dictionary<string, object>(),fName );
            systemFile.Description = fName;
            systemFile.Patch = path;
        }
    }
}