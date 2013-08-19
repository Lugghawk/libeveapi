using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using libeveapi;

namespace UnitTests
{
    [TestFixture]
    public class StarbaseDetailTests
    {
        [SetUp]
        public void Setup()
        {
            Utility.UseLocalUrls();
        }

        [Test]
        public void GetStarbaseDetail()
        {
            ResponseCache.Clear();
            StarbaseDetail starbaseDetail = EveApi.GetStarbaseDetail(4532453, 543453, "fullApiKey", 453453);

            Assert.AreEqual("3", starbaseDetail.UsageFlags);
            Assert.AreEqual("0", starbaseDetail.DeployFlags);
            Assert.AreEqual(true, starbaseDetail.AllowCorporationMembers);
            Assert.AreEqual(false, starbaseDetail.AllowAllianceMembers);
            Assert.AreEqual(false, starbaseDetail.ClaimSovereignty);

            Assert.AreEqual(false, starbaseDetail.OnStandingDropEnabled);
            Assert.AreEqual("0", starbaseDetail.OnStandingDropStanding);
            Assert.AreEqual(false, starbaseDetail.OnStatusDropEnabled);
            Assert.AreEqual("0", starbaseDetail.OnStatusDropStanding);
            Assert.AreEqual(false, starbaseDetail.OnAgressionEnabled);
            Assert.AreEqual(false, starbaseDetail.OnCorporationWarEnabled);

            Assert.AreEqual(3689, starbaseDetail.FuelList[0].TypeId);
            Assert.AreEqual(710, starbaseDetail.FuelList[0].Quantity);
        }

        [Test]
        public void StarbaseDetailPersist()
        {
            ResponseCache.Clear();
            StarbaseDetail starbaseDetail = EveApi.GetStarbaseDetail(432435, 234523, "fullApiKey", 25245);

            ResponseCache.Save("ResponseCache.xml");
            ResponseCache.Clear();
            ResponseCache.Load("ResponseCache.xml");

            StarbaseDetail cachedStarbaseDetail = EveApi.GetStarbaseDetail(432435, 234523, "fullApiKey", 25245);

            Assert.AreEqual(starbaseDetail.CachedUntilLocal, cachedStarbaseDetail.CachedUntilLocal);

            Assert.AreEqual(starbaseDetail.UsageFlags, cachedStarbaseDetail.UsageFlags);
            Assert.AreEqual(starbaseDetail.DeployFlags, cachedStarbaseDetail.DeployFlags);
            Assert.AreEqual(starbaseDetail.AllowCorporationMembers, cachedStarbaseDetail.AllowCorporationMembers);
            Assert.AreEqual(starbaseDetail.AllowAllianceMembers, cachedStarbaseDetail.AllowAllianceMembers);
            Assert.AreEqual(starbaseDetail.ClaimSovereignty, cachedStarbaseDetail.ClaimSovereignty);

            Assert.AreEqual(starbaseDetail.OnStandingDropEnabled, cachedStarbaseDetail.OnStandingDropEnabled);
            Assert.AreEqual(starbaseDetail.OnStandingDropStanding, cachedStarbaseDetail.OnStandingDropStanding);
            Assert.AreEqual(starbaseDetail.OnStatusDropEnabled, cachedStarbaseDetail.OnStatusDropEnabled);
            Assert.AreEqual(starbaseDetail.OnStatusDropStanding, cachedStarbaseDetail.OnStatusDropStanding);
            Assert.AreEqual(starbaseDetail.OnAgressionEnabled, cachedStarbaseDetail.OnAgressionEnabled);
            Assert.AreEqual(starbaseDetail.OnCorporationWarEnabled, cachedStarbaseDetail.OnCorporationWarEnabled);

            for (int i = 0; i < starbaseDetail.FuelList.Length; i++)
            {
                Assert.AreEqual(starbaseDetail.FuelList[i].TypeId, cachedStarbaseDetail.FuelList[i].TypeId);
                Assert.AreEqual(starbaseDetail.FuelList[i].Quantity, cachedStarbaseDetail.FuelList[i].Quantity);
            }
        }
    }
}
