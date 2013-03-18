using Cico.Commons.Mapi;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Commons.Mapi
{
    [TestFixture]
    public class MapiTest
    {
        [Test]
        public void TestGetInbox()
        {
            var query = new MapiQuery();
            var res = query.GetInbox();
        }
    }
}
