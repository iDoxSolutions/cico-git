using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using log4net;

namespace Cico.Models.Subscriptions
{
    public class RemindersService:IDailyExecute
    {
        private readonly ICicoContext _db;
        private readonly HttpContextBase _context;
        private static readonly ILog log = LogManager.GetLogger(typeof(RemindersService).Name);
        public RemindersService(ICicoContext db, HttpContextBase context)
        {
            _db = db;
            _context = context;
        }


        public void PerformDaily()
        {
            foreach (var reminder in _db.Reminders)
            {
                var remindingDate = DateTime.Today.AddDays(reminder.DateToSend);
                var sessions = from session in _db.CheckListSessions
                where
                    SqlFunctions.DateDiff("day", session.ReferenceDate, DateTime.Today) == reminder.DateToSend &&
                    session.CheckListTemplate.Type == reminder.Checklisttype 

                    select session;
                foreach (var checkListSession in sessions)
                {
                    var tracks = from track in _db.CheckListItemSubmitionTracks.ToList()
                                 where track.CheckListSession.Id == checkListSession.Id && track.DueDate>=DateTime.Today
                                 select track;
                    if (tracks.Any())
                    {
                        SendEmail(checkListSession, reminder,tracks);    
                    }
                    
                    
                }
            }
        }

        private void SendEmail(CheckListSession checkListSession, Reminder reminder, IEnumerable<CheckListItemSubmitionTrack> tracks)
        {
            if (string.IsNullOrEmpty(checkListSession.Employee.PersonalEmail))
            {
                log.ErrorFormat("{0} {1} doesn't have email updated -  reminder could not be sent", checkListSession.Employee.FirstName, checkListSession.Employee.LastName);
                return;
            }
            var sb = new StringBuilder();
            sb.Append("<h2>");
            string title = string.Format("CICO application Reminder on {0}", DateTime.Today.ToShortDateString() );
            sb.Append(title);
            
            sb.Append("</h2>");
            sb.Append("<h3>");
            sb.Append(reminder.MessageSubject);
            sb.Append("</h3>");
            sb.Append("<p>");
            sb.Append(reminder.MessagePreface);
            sb.Append("</p>");

            sb.Append("<ul>");
            foreach (var checkListItemSubmitionTrack in tracks)
            {
                sb.Append("<li>");
                var link = string.Format("<a href='{0}'>{1}</a>", checkListItemSubmitionTrack.AbsoluteUri(), checkListItemSubmitionTrack.CheckListItemTemplate.Description);
                sb.Append(link);
                sb.Append("</li>");
            }
            sb.Append("</ul>");


            sb.Append("<p>");
            sb.Append(reminder.MessageClosing);
            sb.Append("</p>");

            var smtp = new SmtpClient() { };
            var message = new MailMessage() { From = new MailAddress("noreply@cico.com") };
            message.To.Add(checkListSession.Employee.PersonalEmail);
            message.Subject = string.Format("CICO Reminder", DateTime.Today.ToShortDateString());
            message.Body = sb.ToString();
            message.IsBodyHtml = true;
            smtp.Send(message);
        }
    }
}