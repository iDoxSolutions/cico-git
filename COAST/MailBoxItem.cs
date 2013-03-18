using OpenPop.Mime;
using OpenPop.Mime.Header;

namespace COAST
{
    public class MailBoxItem
    {
        public string Title { get; set; }
        public MessageHeader MessageHeader;
        public Message Message;
        public string VoucherNumber { get; set; }
        public string VoucherStatus { get; set; }
    }
}