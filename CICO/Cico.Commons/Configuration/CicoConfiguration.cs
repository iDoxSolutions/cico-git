using System.Configuration;

namespace Cico.Commons.Configuration
{
    public class CicoConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("sharepoint", IsRequired = true)]
        public SharePointConfig SharePointConfig
        {
            get
            {
                return (SharePointConfig)this["sharepoint"];
            }
            set
            {
                this["sharepoint"] = value;
            }
        }
    }
    public class SharePointConfig : ConfigurationElement
    {

        [ConfigurationProperty("secure", IsRequired = true)]
        public bool Secure
        {
            get
            { return (bool)this["secure"]; }
            set
            { this["secure"] = value; }
        }

        [ConfigurationProperty("libraryName", IsRequired = true)]
        public string LibraryName
        {
            get
            { return (string)this["libraryName"]; }
            set
            { this["libraryName"] = value; }
        }

        [ConfigurationProperty("siteName", IsRequired = true)]
        public string SiteName
        {
            get
            { return (string)this["siteName"]; }
            set
            { this["siteName"] = value; }
        }

        [ConfigurationProperty("password", IsRequired = true)]
        public string Password
        {
            get
            { return (string)this["password"]; }
            set
            { this["password"] = value; }
        }

        [ConfigurationProperty("user", IsRequired = true)]
        public string User
        {
            get
            { return (string)this["user"]; }
            set
            { this["user"] = value; }
        }

        [ConfigurationProperty("url", IsRequired = true)]
        public string Url
        {
            get
            { return (string)this["url"]; }
            set
            { this["url"] = value; }
        }

        [ConfigurationProperty("listId", IsRequired = true)]
        public string ListId
        {
            get
            { return (string)this["listId"]; }
            set
            { this["listId"] = value; }
        }
        [ConfigurationProperty("domain", IsRequired = true)]
        public string Domain
        {
            get { return (string)this["domain"]; }
            set { this["domain"] = value; }
        }
    }
}