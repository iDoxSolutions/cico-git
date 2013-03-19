﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Cico.Commons.Mapi;
using OpenPop.Mime;
using OpenPop.Mime.Header;
using OpenPop.Pop3;

namespace SelfService
{
    public interface IMailBox
    {
        IList<MailBoxItem> GetItems();
    }

    public class MapiMailBox : IMailBox
    {
        MapiQuery _query = new MapiQuery();
        public IList<MailBoxItem> GetItems()
        {
            var list = new List<MailBoxItem>();
            var items = _query.GetUnreadInbox();
            foreach (var eMail in items)
            {
                

            }
            return list;
        }
    }


    class MailBox : IMailBox
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
                (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public IList<MailBoxItem> GetItems()
        {
            using (var client = new Pop3Client())
            {
                // Connect to the server
                var email = ConfigurationManager.AppSettings["email"];
                client.Connect("pop.gmail.com", 995,true);

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
                    mbItem.VoucherNumber = voucher.GetVoucherNumber(mbItem.MessageHeader.Subject);
                    mbItem.VoucherStatus = voucher.GetVoucherStatus(mbItem.VoucherNumber);
                   
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