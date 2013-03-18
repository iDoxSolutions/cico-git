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
        private string _user = "wasilewski.pawel@gmail.com";
        private string _password = "pomodoro74";
        public IList<Mail.EMail> GetInbox()
        {
            var list = new List<EMail>();
            var mailSession = new Application();
            var mailNamespace = mailSession.GetNamespace("MAPI");
            mailNamespace.Logon(_user,_password, false, true);
            var folder =  mailNamespace.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderInbox);
            foreach (object item in folder.Items)
            {
                if (item is MailItem)
                {
                    var mail = item as MailItem;
                    //Console.WriteLine(item.SenderEmailAddress + " " + item.Subject + "\n" + item.Body);
                    list.Add(new EMail()
                        {
                            Body = mail.Body,
                            From = mail.SenderEmailAddress

                        });
                }
            }
            return list;
        }
    }
}
