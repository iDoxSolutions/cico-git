using System;
using System.Collections.Generic;
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
            var filename = Guid.NewGuid().ToString().Replace("-", "");
            _query.Save(buffer, new Dictionary<string, object>(), filename);
        }
    }
}