using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Web;
using System.Xml;
using Cico.Models.SharePoint.SpWs;
using log4net;

namespace Cico.Models.SharePoint
{
    public class SharePointDocumentsQuery //: IDocumentsQuery
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(SharePointDocumentsQuery).Name);
        protected string userName = "pawel@lightkeeper2.onmicrosoft.com";
        protected string password = "26rtYsxGL";
        protected string domain = "LIGHTKEEPERSERV";
        protected string url = "http://lightkeeper2.sharepoint.com";
        protected string siteName = "TeamSite";
        protected string libId = "{AFF799FF-F870-4A3A-9A14-2C6121F828D2}";
        protected string libraryName = "DocLib";
    
        private CookieContainer _cc;
        private CookieContainer cc
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.Session["cc"] != null)
                {
                    return HttpContext.Current.Session["cc"] as CookieContainer;
                }
                else
                {
                    return _cc;
                }
            }
            set
            {
                if (HttpContext.Current != null)
                {
                    HttpContext.Current.Session["cc"] = value;
                }
                else
                {
                    _cc = value;
                }
            }
        }
        public void LoginSol()
        {

        }

        public SharePointDocumentsQuery()
        {
            var config = (CicoConfiguration)ConfigurationManager.GetSection("cicoconfiguration");
            userName = config.SharePointConfig.User;
            password = config.SharePointConfig.Password;
            domain = config.SharePointConfig.Domain;
            url = config.SharePointConfig.Url;
            siteName = config.SharePointConfig.SiteName;
            libId = config.SharePointConfig.ListId;
            libraryName = config.SharePointConfig.LibraryName;
        }

        /*private CookieContainer GetCookie()
        {
            if (cc == null)
            {
                var claimsHelper = new MsOnlineClaimsHelper(url,
                                                            userName, password);
                log.DebugFormat("Url:{0} user:{1} password:{2}", url, userName, password);
                using (ClientContext context = new ClientContext(url))
                {
                    context.ExecutingWebRequest += claimsHelper.clientContext_ExecutingWebRequest;

                    context.Load(context.Web);

                    context.ExecuteQuery();
                }
                return claimsHelper.CookieContainer;
            }
            else
            {
                return cc;
            }
        }*/

        public IList<DocumentDto> GetByContract(string contractNo)
        {

            var proxy = new Lists();

            //proxy.CookieContainer = GetCookie();
            //proxy.Url = "http://lightkeeper2.sharepoint.com/TeamSite/_vti_bin/Lists.asmx";
            //proxy.Credentials = new NetworkCredential(userName, password, domain);
            proxy.Url = url + "/" + siteName + "/_vti_bin/Lists.asmx";
            //proxy.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Identification;
            var query = new XmlDocument();
            var sQuery = "<Query xmlns=\"http://schemas.microsoft.com/sharepoint/soap/\">" +
                         "<OrderBy>" +
                         "<FieldRef Name=\"FileLeafRef\" />" +
                         "</OrderBy>" +
                         "<Where>" +
                         "<Eq>" +
                         "<FieldRef Name='CONTRACT_NO' />" +
                         "<Value Type='Text'>" + contractNo + "</Value>" +
                         "</Eq>" +
                         "</Where>" +
                         "</Query>";
            query.LoadXml(sQuery);

            var sFields = "<ViewFields xmlns=\"http://schemas.microsoft.com/sharepoint/soap/\">" +
                          "<FieldRef Name=\"ACTIVITYDATE\" />" +
                          "<FieldRef Name=\"Created\" />" +
                          "<FieldRef Name=\"Modified\" />" +
                          "<FieldRef Name=\"ACTIVITY\" />" +
                          "<FieldRef Name=\"CONTRACT_NO\" />" +
                          "<FieldRef Name=\"RECEIVED\" />" +
                          "<FieldRef Name=\"DOCCATEGORY\" />" +
                          "<FieldRef Name=\"DOCSTATUS\" />" +
                          "<FieldRef Name=\"FileRef\" />" +
                          "<FieldRef Name=\"FileLeafRef\" />" +
                          "<FieldRef Name=\"ID\" />" +
                          "<FieldRef Name=\"DOC_TYPE\" />" +
                          "<FieldRef Name=\"VS_BASEID\" />" +
                          "<FieldRef Name=\"REQID\" />" +
                          "<FieldRef Name=\"_UniqueId\" />" +
                          "<FieldRef Name=\"LIB_ID\" />" +
                          "<FieldRef Name=\"ID\" />" +
                          "</ViewFields>";
            var fields = new XmlDocument();
            fields.LoadXml(sFields);
            var ndQueryOptions = fields.CreateNode(XmlNodeType.Element, "QueryOptions", "");
            ndQueryOptions.InnerXml = "<ViewAttributes Scope='Recursive' />";
            var result = proxy.GetListItems(libraryName, "", query.DocumentElement,
                                            fields.DocumentElement, "10000", ndQueryOptions, "");

            var tr = new StringReader(result.OuterXml);
            var ds = new DataSet();
            ds.ReadXml(tr);
            var list = new List<DocumentDto>();
            if (ds.Tables["row"] != null)
            {
                foreach (DataRow dr in ds.Tables["row"].Rows)
                {
                    var doc = new DocumentDto()
                        {
                            Activity = SafeGetStringField(dr, "ACTIVITY"),
                            ActivityDate = SafeGetDateField(dr, "ACTIVITYDATE"),
                            ContractNo = SafeGetStringField(dr, "CONTRACT_NO"),
                            DocCategory = SafeGetStringField(dr, "DOCCATEGORY"),
                            DocumentType = SafeGetStringField(dr, "DOC_TYPE"),
                            ReqId = SafeGetIntField(dr, "REQID"),
                            Status = SafeGetIntField(dr, "DOCSTATUS"),
                            Url = url + "/" + SafeGetStringField(dr, "FileRef")
                        };
                    list.Add(doc);
                }
            }
            return list;

        }

        public string Save(byte[] content, IDictionary<string, object> properties, string name)
        {
            string msg = String.Empty;

            var copy = new Copy { Url = url + "/" + siteName + "/_vti_bin/copy.asmx", Credentials = new NetworkCredential(userName, password) };


            var dest = !string.IsNullOrEmpty(siteName)
                           ? string.Format("{0}/{3}/{1}/{2}", url, libraryName, name, siteName)
                           : string.Format("{0}/{1}/{2}", url, libraryName, name);
            string[] destinationUrlColl = { dest };

            var fieldInfo = new List<FieldInformation>();

            foreach (var parameter in properties)
            {
                //var fType = parameter.Value.GetType().ToString() == "DateTime" ? FieldType.DateTime : FieldType.Text;
                FieldType fType = FieldType.Invalid;
                switch (parameter.Value.GetType().ToString())
                {
                    case "System.DateTime":
                        fType = FieldType.DateTime;
                        break;
                    case "System.Int32":
                        fType = FieldType.Number;
                        break;
                    case "System.String":
                        fType = FieldType.Text;
                        break;
                }
                var fi = new FieldInformation()
                    {
                        DisplayName = parameter.Key,
                        InternalName = parameter.Key,
                        Value = parameter.Value.ToString(),
                        Type = fType
                    };
                fieldInfo.Add(fi);
            }
            //fieldInfo.Add(new FieldInformation(){DisplayName = "Name"});
            var resultTest = new CopyResult();


            //When creating new content use the same URL in the SourceURI as in the Destination URL argument

            CopyResult[] result = new CopyResult[] { resultTest };
            var res = copy.CopyIntoItems(dest, destinationUrlColl, fieldInfo.ToArray(), content, out result);
            if (result[0].ErrorMessage != null)
            {
                msg = "Error: " + result[0].ErrorMessage;
                throw new Exception(result[0].ErrorMessage);
            }
            else
            {
                msg = "Success";
            }


            return result[0].DestinationUrl;
        }

        public byte[] LoadDocument(string url)
        {
            var copy = new Copy { Url = url + "/" + siteName + "/_vti_bin/copy.asmx" };
            var fi = new FieldInformation[] { new FieldInformation() };
            byte[] buff;
            copy.GetItem(url, out fi, out buff);
            return buff;
        }

        private int SafeGetIntField(DataRow dr, string name)
        {
            string sValue = SafeGetStringField(dr, name);
            if (string.IsNullOrEmpty(sValue))
            {
                return 0;
            }
            else
            {
                return (int)Convert.ToDouble(sValue);
            }
        }

        private DateTime? SafeGetDateField(DataRow dr, string name)
        {
            string sValue = SafeGetStringField(dr, name);
            if (string.IsNullOrEmpty(sValue))
            {
                return new DateTime?();
            }
            else
            {
                return Convert.ToDateTime(sValue);
            }
        }

        private string SafeGetStringField(DataRow dr, string name)
        {
            string spName = "ows_" + name;
            if (dr.Table.Columns.Contains(spName))
            {
                string sValue = dr[spName].ToString();
                if (sValue.IndexOf(";#") != -1)
                {
                    sValue = sValue.Substring(sValue.IndexOf(";#") + 2);
                }
                return sValue;
            }
            else
            {
                return "";
            }
        }


    }
}