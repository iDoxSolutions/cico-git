﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    public class SharePointTest
    {
        [Test]
        public void TestUploadDoc()
        {
            var spQuery = new Cico.Models.SharePoint.SharePointDocumentsQuery();
            spQuery.Save(new byte[] { }, new Dictionary<string, object>(), "sample3.txt");
        }

        [Test]
        public void CreateFolder()
        {
            var spQuery = new Cico.Models.SharePoint.SharePointDocumentsQuery();
            spQuery.CreateFolder("http://win-jt00o5f5kmt/testlib", "newfolder");
        }
    }
}
