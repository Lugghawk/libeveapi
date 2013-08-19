using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using libeveapi;

namespace UnitTests
{
    [TestFixture]
    public class StarbaseListTests
    {
        [SetUp]
        public void Setup()
        {
            Utility.UseLocalUrls();
        }

        [Test]
        public void GetStarbaseList()
        {
            StarbaseList starbaseList = EveApi.GetStarbaseList(432435, 234523, "apiKey");

            Assert.AreEqual(4, starbaseList.StarbaseListItems.Length);
            StarbaseList.StarbaseListItem sli;

            sli = starbaseList.StarbaseListItems[0];
            Assert.AreEqual(150354725, sli.ItemId);
            Assert.AreEqual(12235, sli.TypeId);
            Assert.AreEqual(30000380, sli.LocationId);
            Assert.AreEqual(40023754, sli.MoonId);
            Assert.AreEqual(StarbaseList.StarbaseState.Online, sli.State);
            Assert.AreEqual(new DateTime(0001, 01, 01, 00, 00, 00), sli.StateTimestamp);
            Assert.AreEqual(new DateTime(2007, 08, 06, 13, 43, 16), sli.OnlineTimestamp);
        }

        [Test]
        public void StarbaseListPersist()
        {
            ResponseCache.Clear();
            StarbaseList starbaseList = EveApi.GetStarbaseList(432435, 234523, "apiKey");
            ResponseCache.Save("ResponseCache.xml");
            ResponseCache.Clear();
            ResponseCache.Load("ResponseCache.xml");

            StarbaseList cachedStarbaseList = EveApi.GetStarbaseList(432435, 234523, "apiKey");

            Assert.AreEqual(starbaseList.CachedUntilLocal, cachedStarbaseList.CachedUntilLocal);

            for (int i = 0; i < starbaseList.StarbaseListItems.Length; i++)
            {
                Assert.AreEqual(starbaseList.StarbaseListItems[i].ItemId, cachedStarbaseList.StarbaseListItems[i].ItemId);
                Assert.AreEqual(starbaseList.StarbaseListItems[i].TypeId, cachedStarbaseList.StarbaseListItems[i].TypeId);
                Assert.AreEqual(starbaseList.StarbaseListItems[i].LocationId, cachedStarbaseList.StarbaseListItems[i].LocationId);
                Assert.AreEqual(starbaseList.StarbaseListItems[i].MoonId, cachedStarbaseList.StarbaseListItems[i].MoonId);
                Assert.AreEqual(starbaseList.StarbaseListItems[i].State, cachedStarbaseList.StarbaseListItems[i].State);
                Assert.AreEqual(starbaseList.StarbaseListItems[i].StateTimestamp, cachedStarbaseList.StarbaseListItems[i].StateTimestamp);
                Assert.AreEqual(starbaseList.StarbaseListItems[i].OnlineTimestamp, cachedStarbaseList.StarbaseListItems[i].OnlineTimestamp);
                Assert.AreEqual(starbaseList.StarbaseListItems[i].StateTimestampLocal, cachedStarbaseList.StarbaseListItems[i].StateTimestampLocal);
                Assert.AreEqual(starbaseList.StarbaseListItems[i].OnlineTimestampLocal, cachedStarbaseList.StarbaseListItems[i].OnlineTimestampLocal);
            }
        }
    }
}
