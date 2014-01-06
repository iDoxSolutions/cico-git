using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Cico.Models.CheckLists
{
    public class Subscriptions
    {
        private readonly HttpContextBase _context;

        public Subscriptions(HttpContextBase context)
        {
            _context = context;
        }

        public void Process(CheckListItemSubmitionTrack track,string textMessage)
        {
            if (track.CheckListItemTemplate.EmailSubscriptions.Count == 0)
            {
                return;
            }

            var smtp = new SmtpClient() { };
            var addresses = new MailAddressCollection();
            
            var message = new MailMessage() { From = new MailAddress("krzysiek@lightkeeper.co") };
            foreach (var emailSubscription in track.CheckListItemTemplate.EmailSubscriptions)
            {
                message.To.Add(emailSubscription.Staff.Email);
            }

            message.Subject = track.CheckListItemTemplate.Description +" - CICO NOTIFICATION";
            var param = string.Format("?id={0}#checkpoint/{1}", track.CheckListSession.Id, track.Id);
            var itemUri = new UriBuilder(_context.Request.Url.Scheme, _context.Request.Url.Host, _context.Request.Url.Port, "home", param);
            message.Body = textMessage + " " + itemUri;
            Object userState = message;
            smtp.SendCompleted += new SendCompletedEventHandler(smtp_SendCompleted);
            smtp.Send(message);
        }

        private void smtp_SendCompleted(object sender, AsyncCompletedEventArgs e)
        {
           if (e.Error != null)
           {
               throw e.Error.InnerException;
           }

        }

        private void SendEmail(string email,CheckListItemSubmitionTrack track)
        {
            

        }
    }
}