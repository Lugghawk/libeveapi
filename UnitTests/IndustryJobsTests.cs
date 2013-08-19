using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using libeveapi;

namespace UnitTests
{
    [TestFixture]
    public class IndustryJobsTests
    {
        [SetUp]
        public void Setup()
        {
            Utility.UseLocalUrls();
        }

        [Test]
        public void GetCharIndustryJobsTest()
        {
            IndustryJobList jobList = EveApi.GetIndustryJobList(IndustryJobListType.Character, 432435, 234523, "fullApiKey");

            Assert.AreEqual(22, jobList.IndustryJobListItems.Length);

            Assert.AreEqual(444, jobList.IndustryJobListItems[0].JobId);
            Assert.AreEqual(60010783, jobList.IndustryJobListItems[0].ContainerId);
            Assert.AreEqual(60010783, jobList.IndustryJobListItems[0].InstalledItemLocationId);
        }

        [Test]
        public void GetCorpIndustryJobsTest()
        {
            IndustryJobList jobList = EveApi.GetIndustryJobList(IndustryJobListType.Corporation, 432435, 234523, "fullApiKey");

            Assert.AreEqual(22, jobList.IndustryJobListItems.Length);

            Assert.AreEqual(444, jobList.IndustryJobListItems[0].JobId);
            Assert.AreEqual(60010783, jobList.IndustryJobListItems[0].ContainerId);
            Assert.AreEqual(60010783, jobList.IndustryJobListItems[0].InstalledItemLocationId);
        }

        [Test]
        public void IndustryJobsPersist()
        {
            ResponseCache.Clear();

            IndustryJobList industryJobList = EveApi.GetIndustryJobList(IndustryJobListType.Corporation, 432435, 346, "apiKey");
            ResponseCache.Save("ResponseCache.xml");
            ResponseCache.Clear();
            ResponseCache.Load("ResponseCache.xml");
            IndustryJobList cachedIndustryJobList = EveApi.GetIndustryJobList(IndustryJobListType.Corporation, 432435, 346, "apiKey");

            Assert.AreEqual(industryJobList.CachedUntilLocal, cachedIndustryJobList.CachedUntilLocal);

            
            for (int i = 0; i < industryJobList.IndustryJobListItems.Length; i++)
            {
                Assert.AreEqual(industryJobList.IndustryJobListItems[i].JobId, cachedIndustryJobList.IndustryJobListItems[i].JobId);
                Assert.AreEqual(industryJobList.IndustryJobListItems[i].AssemblyLineId, cachedIndustryJobList.IndustryJobListItems[i].AssemblyLineId);
                Assert.AreEqual(industryJobList.IndustryJobListItems[i].Completed, cachedIndustryJobList.IndustryJobListItems[i].Completed);
            }
            
        }
    }
}
