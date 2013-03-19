using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cico.Commons.Mail;
using Microsoft.Office.Interop.Outlook;

namespace Cico.Commons.Mapi
{
    public class MapiQuery : IMailStorage
    {
        private string _user = "kenhambright@lightkeeper.onmicrosoft.com";
        private string _password = "26rtYsxGL";
        public IList<Mail.EMail> GetUnreadInbox()
        {
            var list = new List<EMail>();
            var folder = MapiFolder();
            foreach (object item in folder.Items.Restrict("[Unread]=true"))
            {
                if (item is MailItem)
                {
                    var mail = item as MailItem;
                    
                    //var ur = mail.UnRead=false;
                    //Console.WriteLine(item.SenderEmailAddress + " " + item.Subject + "\n" + item.Body);
                    list.Add(new EMail()
                        {
                            Body = mail.Body,
                            From = mail.SenderEmailAddress,
                            EmailId = mail.EntryID
                        });
                }
            }
            return list;
        }

        private MAPIFolder MapiFolder()
        {
            var mailNamespace = MailNamespace();

            mailNamespace.Logon(_user, _password, false, true);
            var folder = mailNamespace.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderInbox);
            return folder;
        }

        private static NameSpace MailNamespace()
        {
            var mailSession = new Application();

            var mailNamespace = mailSession.GetNamespace("MAPI");
            return mailNamespace;
        }

        public void MarkAsRead(string id)
        {
            var ns = MailNamespace();
            var mailItem = ns.GetItemFromID(id) as MailItem;
            mailItem.UnRead = false;
        }
    }
}
