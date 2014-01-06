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
    public class CompareStaff:IEqualityComparer<Staff>
    {
        public bool Equals(Staff x, Staff y)
        {
            return x.UserId == y.UserId;
        }

        public int GetHashCode(Staff obj)
        {
           return obj.UserId.GetHashCode();
        }
    }

    public class SubscriptionsService : IDailyExecute
    {
        private readonly ICicoContext _db;
        private readonly HttpContextBase _context;
        private static readonly ILog log = LogManager.GetLogger(typeof(SubscriptionsService).Name);

        public SubscriptionsService(ICicoContext db,HttpContextBase context)
        {
            _db = db;
            _context = context;
        }

        public void PerformDaily(DateTime refDate)
        {
            log.Debug("Performing subscriptions - all users that subscribed elements and there's any modification today");
            var usersToSend = (from t in _db.Staffs
                              join emailSubscription
                                  in _db.EmailSubscriptions on t.UserId equals emailSubscription.Staff.UserId
                              join checkListItemTemplate in _db.CheckListItemTemplates on
                                  emailSubscription.CheckListItemTemplate.CheckListItemTemplateId equals
                                  checkListItemTemplate.CheckListItemTemplateId
                              join checkListItemSubmitionTrack in _db.CheckListItemSubmitionTracks on
                                  checkListItemTemplate.CheckListItemTemplateId equals
                                  checkListItemSubmitionTrack.CheckListItemTemplate.CheckListItemTemplateId
                              where SqlFunctions.DateDiff("day", checkListItemSubmitionTrack.DateEdited.Value,refDate) == 0 && checkListItemSubmitionTrack.Checked
                              && checkListItemTemplate.Active
                              select t ).ToList();
            log.DebugFormat("{0} staff users to be emailed",usersToSend.Count());
            foreach (var staff in usersToSend.Distinct(new CompareStaff()))
            {
                SendEmail(staff,refDate);
            }

        }

        private void SendEmail(Staff staff,DateTime refDate)
        {
            log.DebugFormat("{0} sending email to ", staff.Email);
            var sb = new StringBuilder();
            var tracks = from t in _db.Staffs
                             join emailSubscription
                                 in _db.EmailSubscriptions on t.UserId equals emailSubscription.Staff.UserId
                             join checkListItemTemplate in _db.CheckListItemTemplates on
                                 emailSubscription.CheckListItemTemplate.CheckListItemTemplateId equals
                                 checkListItemTemplate.CheckListItemTemplateId
                             join checkListItemSubmitionTrack in _db.CheckListItemSubmitionTracks on
                                 checkListItemTemplate.CheckListItemTemplateId equals
                                 checkListItemSubmitionTrack.CheckListItemTemplate.CheckListItemTemplateId
                         where SqlFunctions.DateDiff("day", checkListItemSubmitionTrack.DateEdited.Value, refDate) == 0 && t.UserId == staff.UserId && checkListItemSubmitionTrack.Checked
                             orderby checkListItemSubmitionTrack.DateEdited descending 
                             select checkListItemSubmitionTrack;
            string title = string.Format("<h2>Checklist items completed on {0}</h2>", refDate.ToShortDateString());
            sb.Append(title);
            sb.Append("<ul>");
            foreach (var checkListItemSubmitionTrack in tracks)
            {
                sb.Append("<li>");
                var url = string.Format("?id={0}#checkpoint/{1}", checkListItemSubmitionTrack.CheckListSession.Id, checkListItemSubmitionTrack.Id);
                var uri = new UriBuilder(_context.Request.Url.Scheme, _context.Request.Url.Host, _context.Request.Url.Port, "home", url);
                string a = string.Format("<a href='{0}'>", uri.Uri.AbsoluteUri);
                sb.Append(a);
                sb.Append(checkListItemSubmitionTrack.CheckListItemTemplate.Description );
                sb.Append("</a>");
                sb.Append("<div>");
                sb.Append("User: "+checkListItemSubmitionTrack.UserEdited);
                sb.Append(" Time: " + checkListItemSubmitionTrack.DateEdited);
                sb.Append("</div>");
                sb.Append("</li>");
            }
            sb.Append("</ul>");
            log.DebugFormat("sending email to {0}", staff.Email);
            var smtp = new SmtpClient() { };
            var addresses = new MailAddressCollection();

            var message = new MailMessage() { From = new MailAddress("noreply@cico.com") };
            
            message.To.Add(staff.Email);
            

            message.Subject = string.Format("CICO Checklist Completions {0}",refDate.ToShortDateString());
            message.Body = sb.ToString();
            message.IsBodyHtml = true;
            smtp.Send(message);
            
        }
    }
}