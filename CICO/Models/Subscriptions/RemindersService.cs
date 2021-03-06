﻿using System;
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


        public void PerformDaily(DateTime refDate)
        {
            log.Debug("Performing reminders");
            foreach (var reminder in _db.Reminders)
            {
                log.DebugFormat("Performing reminder {0}",reminder.MessageSubject);
                var remindingDate = refDate.AddDays(reminder.DateToSend);
                var sessions = from session in _db.CheckListSessions
                               where
                                   SqlFunctions.DateDiff("day", session.ReferenceDate, refDate) == reminder.DateToSend &&
                                   session.CheckListTemplate.Type == reminder.Checklisttype &&
                                   session.Completed == false 
                select session;
                log.DebugFormat("{0} Sessions found", sessions.Count());
                foreach (var checkListSession in sessions)
                {
                    var tracks = from track in _db.CheckListItemSubmitionTracks.ToList()
                                 where track.CheckListSession.Id == checkListSession.Id && track.DueDate<=refDate && (!track.Checked || !track.Completed) && track.CheckListItemTemplate.Active
                                 select track;
                    if (tracks.Any())
                    {
                        SendEmail(checkListSession, reminder,tracks,refDate);    
                    }
                    
                    
                }
            }
        }

        private void SendEmail(CheckListSession checkListSession, Reminder reminder, IEnumerable<CheckListItemSubmitionTrack> tracks,DateTime refDate)
        {
            if (string.IsNullOrEmpty(checkListSession.Employee.PersonalEmail))
            {
                log.ErrorFormat("{0} {1} doesn't have email updated -  reminder could not be sent", checkListSession.Employee.FirstName, checkListSession.Employee.LastName);
                return;
            }
            var sb = new StringBuilder();
            sb.Append("<h2>");
            
            sb.Append("<p>");
            sb.Append(reminder.MessagePreface);
            sb.Append("</p>");

            sb.Append("<ul>");

            var sentBoxItem = new SentBoxItem()
            {
                Employee = checkListSession.Employee,DateSent = DateTime.Now,ReminderThreshold = reminder.DateToSend
                ,Reminder = reminder
            };
            sentBoxItem.ChecklistItems = new List<CheckListItemSubmitionTrack>();
            foreach (var checkListItemSubmitionTrack in tracks)
            {
                sb.Append("<li>");
                var link = string.Format("<a href='{0}'>{1}</a>", checkListItemSubmitionTrack.AbsoluteUri(), checkListItemSubmitionTrack.CheckListItemTemplate.Description);
                sb.Append(link);
                sb.Append("</li>");
                sentBoxItem.ChecklistItems.Add(checkListItemSubmitionTrack);
            }
            
            sb.Append("</ul>");


            sb.Append("<p>");
            sb.Append(reminder.MessageClosing);
            sb.Append("</p>");
            var addressedTo = new StringBuilder();
            var smtp = new SmtpClient() { };
            var message = new MailMessage() { From = new MailAddress("noreply@cico.com") };
            if (!string.IsNullOrEmpty(checkListSession.Employee.PersonalEmail))
            {
                message.To.Add(checkListSession.Employee.PersonalEmail);
                addressedTo.Append(checkListSession.Employee.PersonalEmail+",");
            }
                
            if (!string.IsNullOrEmpty(checkListSession.Employee.WorkEmail))
            {
                message.To.Add(checkListSession.Employee.WorkEmail);
                addressedTo.Append(checkListSession.Employee.WorkEmail+",");
            }
                
            foreach (var dependent in checkListSession.Employee.Dependents)
            {
                if (!string.IsNullOrEmpty(dependent.PersonalEmail))
                {
                   message.To.Add(dependent.PersonalEmail); 
                    addressedTo.Append(dependent.PersonalEmail+",");
                }
                   
            }
            message.Subject = reminder.MessageSubject;
            message.Body = sb.ToString();
            message.IsBodyHtml = true;
            smtp.Send(message);
            sentBoxItem.AddressedTo = addressedTo.ToString();
            _db.SentBoxItems.Add(sentBoxItem);
        }
    }
}