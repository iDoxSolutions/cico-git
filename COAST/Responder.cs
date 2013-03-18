using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using OpenPop.Pop3;

namespace COAST
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
     
        internal void Send(Voucher voucher,DateTime paidDate)
        {
            var toAddress = ConfigurationManager.AppSettings["email"];
            var fromAddress = "kenhambright@gmail.com";
            var fromPassword = ConfigurationManager.AppSettings["password"];
            var subject = ConfigurationManager.AppSettings["subject"];
            string body;

            body = voucher.VoucherNumber != "Not Found" ? ConfigurationManager.AppSettings["voucherpaid"] : ConfigurationManager.AppSettings["voucherpaid"];
            log.Debug("message body raw: " + body);
            body = body.Replace("!vouchernumber", voucher.VoucherNumber)
                       .Replace("!receivedate", voucher.VoucherReceiveDate.ToShortDateString())
                       .Replace("!paiddate", paidDate.ToShortDateString())
                       .Replace("!receivedate",DateTime.Today.ToShortDateString())
                       .Replace("!vouchernumber", subject);
            log.Debug("message body replaced: " + body);
            log.Debug("sending response - Voucher: " + voucher.VoucherNumber + " Status: " 
                              + "Originator: " + toAddress);
            log.Info("message: " + body);


           
            log.Debug("message body raw: " + body);
            
            log.Info("message: " + body);

            Smtp.Send(fromAddress,toAddress,subject,body);
          
           }
    }
}
