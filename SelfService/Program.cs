using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using OpenPop.Mime.Header;
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace SelfService
{

    class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
                (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static void Main(string[] args)
        {
            log.Info("Starting SelfServe Voucher Status Program");
            var mailbox = new MailBox();
            var mailBoxItems = mailbox.GetItems();
            var responder = new Responder();
            
            foreach (var item in mailBoxItems)
            {
                log.Info("Sending: " + "Voucher Number: " + item.VoucherNumber + " Voucher Status: " + item.VoucherStatus);
                responder.Send(item);
                
            }
            log.Info("Ending SelfServe Voucher Status Program");
       }
    }
}
