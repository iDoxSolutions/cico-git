using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace SelfService.Tests
{
    [TestFixture]
    public class MailBoxTest
    {
        [Test]
        public void TestGetItems()
        {
            var mailBox = new MailBox();
            var items = mailBox.GetItems();
            foreach (var mailBoxItem in items)
            {
                Console.WriteLine(mailBoxItem.Title);
            }
            Assert.AreNotEqual(0,items.Count);
        }
    }
}
