using System;
using System.Linq;
using System.Web;
using Cico.Models;
using Cico.Models.Subscriptions;
using Moq;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    public class ReminderServiceTest
    {
        [Test]
        public void TestPerformDaily()
        {
            var httpMock = new Mock<HttpContextBase>();
            var db = new Mock<ICicoContext>();
            var staff = new InMemoryDbSet<Reminder>();
            staff.Add(new Reminder(){Checklisttype = "checkin",DateToSend = 0});
            db.Setup(c => c.Reminders).Returns(() => staff);

            var sessions = new InMemoryDbSet<CheckListSession>();
            sessions.Add(new CheckListSession() { CheckListTemplate = new CheckListTemplate() { Type = "checkin" },ReferenceDate = DateTime.Today,Employee = new Employee(){PersonalEmail = "wasilewski.pawel@gmail.com"}});
            db.Setup(c => c.CheckListSessions).Returns(() => sessions);

            var tracks = new InMemoryDbSet<CheckListItemSubmitionTrack>();
            tracks.Add(new CheckListItemSubmitionTrack() { CheckListSession = sessions.FirstOrDefault(),CheckListItemTemplate = new CheckListItemTemplate()});
            db.Setup(c => c.CheckListItemSubmitionTracks).Returns(() => tracks);

            var service = new RemindersService(db.Object, httpMock.Object);
            service.PerformDaily(DateTime.Now);
        }
    }
}