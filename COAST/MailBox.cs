using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using OpenPop.Mime;
using OpenPop.Mime.Header;
using OpenPop.Pop3;

namespace COAST
{
    internal class MailBox
    {
        public Pop3Client Connect(string email,string pwd)
        {
            using (var client = new Pop3Client())
            {
                
                 client.Connect("pop.gmail.com", 995, true);

                // Authenticate ourselves towards the server

                client.Authenticate(email, pwd);
                if (client.Connected)
                {
                    return client;
                }
                return null;
            }
        }
         
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
            (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        internal IList<MailBoxItem> GetItems()
        {
            using (var client = new Pop3Client())
            {
                // Connect to the server
                var email = ConfigurationManager.AppSettings["email"];
                client.Connect("pop.gmail.com", 995, true);

                // Authenticate ourselves towards the server
                var pwd = ConfigurationManager.AppSettings["password"];
                client.Authenticate(email, pwd);

                // Get the number of messages in the inbox
                var messageCount = client.GetMessageCount();
                log.Info("Found " + messageCount + " new email inquiries");

                // We want to download all messages
                var allItems = new List<MailBoxItem>(messageCount);

                // Messages are numbered in the interval: [1, messageCount]
                // Ergo: message numbers are 1-based.
                // Most servers give the latest message the highest number
                var voucher = new Voucher();

                for (var i = messageCount; i > 0; i--)
                {
                    var mbItem = new MailBoxItem();
                    mbItem.MessageHeader = client.GetMessageHeaders(i);
                    mbItem.Message = client.GetMessage(i);
                    voucher = voucher.GetVoucher(mbItem.MessageHeader.Subject);
                    mbItem.VoucherNumber = voucher.VoucherNumber;
                    mbItem.VoucherStatus = voucher.VoucherStatus;
                    log.Debug("Retrieving email # " + i + "Voucher: " + mbItem.VoucherNumber + " Status: " +
                              mbItem.VoucherStatus + "From: " + mbItem.MessageHeader.From);

                    allItems.Add(mbItem);
                    //delete email -- may not work?
                    client.DeleteMessage(i);

                }

                // Now return the fetched messages
                return allItems;
            }
        }

       
}


}
