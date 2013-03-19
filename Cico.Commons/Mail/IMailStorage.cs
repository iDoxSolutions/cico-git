using System.Collections.Generic;

namespace Cico.Commons.Mail
{
    public interface IMailStorage
    {
        IList<EMail> GetUnreadInbox();
        void MarkAsRead(string id);
    }
}
