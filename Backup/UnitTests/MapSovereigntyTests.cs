using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using libeveapi;

namespace UnitTests
{
    [TestFixture]
    public class MapSovereigntyTests
    {
        [SetUp]
        public void Setup()
        {
            Utility.UseLocalUrls();
        }

        [Test]
        public void GetMapSovereigntyTests()
        {
            MapSovereignty mapSoverignty = EveApi.GetMapSovereignty(true);

            Assert.AreEqual(30023410, mapSoverignty.MapSystemSovereigntyItems[0].SolarSystemId);
            Assert.AreEqual(0, mapSoverignty.MapSystemSovereigntyItems[0].AllianceId);
            Assert.AreEqual(500002, mapSoverignty.MapSystemSovereigntyItems[0].FactionId);
            Assert.AreEqual("Embod", mapSoverignty.MapSystemSovereigntyItems[0].SolarSystemName);
            Assert.AreEqual(0, mapSoverignty.MapSystemSovereigntyItems[0].CorporationID);

            Assert.AreEqual(30001597, mapSoverignty.MapSystemSovereigntyItems[1].SolarSystemId);
            Assert.AreEqual(1028876240, mapSoverignty.MapSystemSovereigntyItems[1].AllianceId);
            Assert.AreEqual(0, mapSoverignty.MapSystemSovereigntyItems[1].FactionId);
            Assert.AreEqual("M-NP5O", mapSoverignty.MapSystemSovereigntyItems[1].SolarSystemName);
            Assert.AreEqual(421957727, mapSoverignty.MapSystemSovereigntyItems[1].CorporationID);

            Assert.AreEqual(30001815, mapSoverignty.MapSystemSovereigntyItems[2].SolarSystemId);
            Assert.AreEqual(389924442, mapSoverignty.MapSystemSovereigntyItems[2].AllianceId);
            Assert.AreEqual(0, mapSoverignty.MapSystemSovereigntyItems[2].FactionId);
            Assert.AreEqual("4AZV-W", mapSoverignty.MapSystemSovereigntyItems[2].SolarSystemName);
            Assert.AreEqual(123456789, mapSoverignty.MapSystemSovereigntyItems[2].CorporationID);

        }

        [Test]
        public void JumpsPersist()
        {
            ResponseCache.Clear();

            MapSovereignty mapSovereignty = EveApi.GetMapSovereignty();
            ResponseCache.Save("ResponseCache.xml");
            ResponseCache.Clear();
            ResponseCache.Load("ResponseCache.xml");
            MapSovereignty cachedMapSovereignty = EveApi.GetMapSovereignty();

            Assert.AreEqual(mapSovereignty.CachedUntilLocal, cachedMapSovereignty.CachedUntilLocal);

            for (int i = 0; i < mapSovereignty.MapSystemSovereigntyItems.Length; i++)
            {
                Assert.AreEqual(mapSovereignty.MapSystemSovereigntyItems[i].AllianceId, cachedMapSovereignty.MapSystemSovereigntyItems[i].AllianceId);
                Assert.AreEqual(mapSovereignty.MapSystemSovereigntyItems[i].SolarSystemId, cachedMapSovereignty.MapSystemSovereigntyItems[i].SolarSystemId);
                Assert.AreEqual(mapSovereignty.MapSystemSovereigntyItems[i].FactionId, cachedMapSovereignty.MapSystemSovereigntyItems[i].FactionId);
                Assert.AreEqual(mapSovereignty.MapSystemSovereigntyItems[i].SolarSystemName, cachedMapSovereignty.MapSystemSovereigntyItems[i].SolarSystemName);
                Assert.AreEqual(mapSovereignty.MapSystemSovereigntyItems[i].CorporationID, cachedMapSovereignty.MapSystemSovereigntyItems[i].CorporationID);
            }
        }
    }
}
