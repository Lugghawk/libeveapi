using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using libeveapi;

namespace UnitTests
{
    [TestFixture]
    public class AllianceListTest
    {
        [SetUp]
        public void Setup()
        {
            Utility.UseLocalUrls();
        }

        [Test]
        public void GetAllianceList()
        {
            ResponseCache.Clear();
            AllianceList al = EveApi.GetAllianceList();
            AllianceList.AllianceListItem ali = al.AllianceListItems[0];
            Assert.AreEqual("GoonSwarm", ali.Name);
            Assert.AreEqual("OHGOD", ali.ShortName);
            Assert.AreEqual(824518128, ali.AllianceId);
            Assert.AreEqual(749147334, ali.ExecutorCorpId);
            Assert.AreEqual(5620, ali.MemberCount);
            Assert.AreEqual(new DateTime(2006, 6, 3, 00, 50, 00), ali.StartDate);

            AllianceList.CorporationListItem cli = ali.CorporationListItems[0];
            Assert.AreEqual(109788662, cli.CorporationId);
            Assert.AreEqual(new DateTime(2007, 9, 9, 19, 12, 00), cli.StartDate);
        }

        [Test]
        public void PersistAllianceList()
        {
            ResponseCache.Clear();
            AllianceList al = EveApi.GetAllianceList();
            ResponseCache.Save("ResponseCache.xml");
            ResponseCache.Clear();
            ResponseCache.Load("ResponseCache.xml");
            AllianceList cal = EveApi.GetAllianceList();

            Assert.AreEqual(al.CachedUntilLocal, cal.CachedUntilLocal);

            for (int i = 0; i < al.AllianceListItems.Length; i++)
            {
                AllianceList.AllianceListItem ali = al.AllianceListItems[i];
                AllianceList.AllianceListItem cali = cal.AllianceListItems[i];

                Assert.AreEqual(ali.Name, cali.Name);
                Assert.AreEqual(ali.ShortName, cali.ShortName);
                Assert.AreEqual(ali.AllianceId, cali.AllianceId);
                Assert.AreEqual(ali.ExecutorCorpId, cali.ExecutorCorpId);
                Assert.AreEqual(ali.MemberCount, cali.MemberCount);
                Assert.AreEqual(ali.StartDate, cali.StartDate);
                Assert.AreEqual(ali.StartDateLocal, cali.StartDateLocal);

                for (int j = 0; j < ali.CorporationListItems.Length; j++)
                {
                    AllianceList.CorporationListItem cli = ali.CorporationListItems[j];
                    AllianceList.CorporationListItem ccli = cali.CorporationListItems[j];

                    Assert.AreEqual(cli.CorporationId, ccli.CorporationId);
                    Assert.AreEqual(cli.StartDate, ccli.StartDate);
                    Assert.AreEqual(cli.StartDateLocal, ccli.StartDateLocal);
                }
            }
        }
    }
}
