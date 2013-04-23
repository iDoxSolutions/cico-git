using System.Text;
using System.Threading.Tasks;
using Cico.Models;
using Cico.Models.Subscriptions;
using Moq;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    public class SubscriptionsServiceTest
    {
        [Test]
        public void TestPerformDaily()
        {
            var dbMock = new Mock<ICicoContext>();
            var staff = new InMemoryDbSet<Staff>();
            staff.Add(new Staff());
            dbMock.Setup(c => c.Staffs).Returns(() => staff);
            var service = new SubscriptionsService(dbMock.Object,null);
            service.PerformDaily();
        }
    }
}
