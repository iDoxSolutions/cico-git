using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using Cico.Commons.Mail;

namespace Cico.Commons.ExchangeDav
{
    public class ExchangeQuery
    {
        private const string GetInboxXml =
            @"<?xml version=""1.0""?>
                <g:searchrequest xmlns:g=""DAV:"">
                    <g:sql>
                        SELECT
                            ""urn:schemas:mailheader:from"", ""urn:schemas:httpmail:textdescription""
                        FROM
                            ""{0}"" 
                        WHERE 
                            ""urn:schemas:httpmail:read"" = FALSE 
                            AND ""urn:schemas:httpmail:subject"" = 'tbintg' 
                            AND ""DAV:contentclass"" = 'urn:content-classes:message' 
                        </g:sql>
                </g:searchrequest>";

        private const string MarkAsRead = @"<?xml version=""1.0""?>
                         <a:propertyupdate xmlns:a=""DAV:""xmlns:d=""urn:schemas-microsoft-com:exch-data:"" xmlns:b=""urn:schemas:httpmail:"" xmlns:c=""xml:""><a:set><a:prop><b:read> 
                         </b:read></a:prop></a:set></a:propertyupdate>";
        private readonly CredentialCache creds;
        private string _server = "";
        private string _userDomain = "";
        private string _userEmail = "";
        private string _userName = "";
        private string _userPassword = "";

        public ExchangeQuery()
        {
            creds = new CredentialCache();
            creds.Add(new Uri(string.Format("http://{0}/Exchange/{1}/Inbox/",_server,_userEmail)), "Basic",
                      new NetworkCredential(_userName, _userPassword,_userDomain));
        }

        public IList<EMail> GetInbox()
        {
            byte[] reqBytes = Encoding.UTF8.GetBytes(GetInboxXml);

            // set up web request
            var request = (HttpWebRequest) WebRequest.Create("http://mail.domain.com/Exchange/me@domain.com/Inbox/");
            request.Credentials = creds;
            request.Method = "SEARCH";
            request.ContentLength = reqBytes.Length;
            request.ContentType = "text/xml";
            request.Timeout = 300000;

            using (Stream requestStream = request.GetRequestStream())
            {
                try
                {
                    requestStream.Write(reqBytes, 0, reqBytes.Length);
                }
                catch
                {
                }
                finally
                {
                    requestStream.Close();
                }
            }

            var response = (HttpWebResponse) request.GetResponse();
            using (Stream responseStream = response.GetResponseStream())
            {
                var document = new XmlDocument();
                document.Load(responseStream);

                // set up namespaces
                var nsmgr = new XmlNamespaceManager(document.NameTable);
                nsmgr.AddNamespace("a", "DAV:");
                nsmgr.AddNamespace("b", "urn:uuid:c2f41010-65b3-11d1-a29f-00aa00c14882/");
                nsmgr.AddNamespace("c", "xml:");
                nsmgr.AddNamespace("d", "urn:schemas:mailheader:");
                nsmgr.AddNamespace("e", "urn:schemas:httpmail:");
                var list = new List<EMail>();
                // Load each response (each mail item) into an object
                XmlNodeList responseNodes = document.GetElementsByTagName("a:response");
                foreach (XmlNode responseNode in responseNodes)
                {
                    // get the <propstat> node that contains valid HTTP responses
                    XmlNode uriNode = responseNode.SelectSingleNode("child::a:href", nsmgr);
                    XmlNode propstatNode =
                        responseNode.SelectSingleNode("descendant::a:propstat[a:status='HTTP/1.1 200 OK']", nsmgr);
                    if (propstatNode != null)
                    {
                        // read properties of this response, and load into a data object
                        XmlNode fromNode = propstatNode.SelectSingleNode("descendant::d:from", nsmgr);
                        XmlNode descNode = propstatNode.SelectSingleNode("descendant::e:textdescription", nsmgr);

                        // make new data object
                        var mail = new EMail();
                        if (uriNode != null)
                            mail.Uri = uriNode.InnerText;
                        if (fromNode != null)
                            mail.From = fromNode.InnerText;
                        if (descNode != null)
                            mail.Body = descNode.InnerText;
                        list.Add(mail);
                    }
                }

                return list;
            }
        }
    }
}