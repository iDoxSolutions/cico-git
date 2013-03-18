using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using OpenPop.Pop3;

namespace SelfService
{
    public class MessageResponse
    {
        public MailBoxItem MailBoxItem { get;set; }
    }

    public class Responder
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
                 (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        internal SmtpClient Smtp = new SmtpClient {
            Host = ConfigurationManager.AppSettings["host"],
            Port = 587,
            EnableSsl = true,
            //DeliveryMethod = SmtpDeliveryMethod.Network,
            //UseDefaultCredentials = false,
            Credentials = new NetworkCredential(ConfigurationManager.AppSettings["email"], ConfigurationManager.AppSettings["password"])

        };
     
        internal void Send(MailBoxItem mailBoxItem)
        {
            var fromAddress = ConfigurationManager.AppSettings["email"];
            var toAddress = mailBoxItem.MessageHeader.From;
            var fromPassword = ConfigurationManager.AppSettings["password"];
            var subject = ConfigurationManager.AppSettings["subject"];
            string body;
            body = mailBoxItem.VoucherNumber != "Not Found" ? ConfigurationManager.AppSettings["statussuccess"] : ConfigurationManager.AppSettings["statusnotfound"];
            log.Debug("message body raw: " + body);
            body = body.Replace("!vouchernumber", mailBoxItem.VoucherNumber)
                       .Replace("!voucherstatus",mailBoxItem.VoucherStatus)
                       .Replace("!senddate",mailBoxItem.MessageHeader.DateSent.Date.ToShortDateString())
                       .Replace("!subject",mailBoxItem.MessageHeader.Subject);
            log.Debug("message body replaced: " + body);
            log.Debug("sending response - Voucher: " + mailBoxItem.VoucherNumber + " Status: " +
                             mailBoxItem.VoucherStatus + "Originator: " + mailBoxItem.MessageHeader.From);
            log.Info("message: " + body);

            Smtp.Send(fromAddress,toAddress.ToString(),subject,body);
          
           }
    }
}
