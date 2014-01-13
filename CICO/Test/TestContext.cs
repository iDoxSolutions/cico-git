using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cico.Models;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    public class TestContext
    {
        [Test]
        public void Test()
        {

            var ctx2 = new CicoContext();
            var res  = ctx2.CheckListTemplates.First(c => c.CheckListTemplateId == 6);
            Assert.IsNotNull(res);
        }
    }
}
