using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using libeveapi;

namespace UnitTests
{
    [TestFixture]
    public class MapKillsTests
    {
        [SetUp]
        public void Setup()
        {
            Utility.UseLocalUrls();
        }

        [Test]
        public void JumpsPersist()
        {
            ResponseCache.Clear();

            MapKills mapKills = EveApi.GetMapKills();
            ResponseCache.Save("ResponseCache.xml");
            ResponseCache.Clear();
            ResponseCache.Load("ResponseCache.xml");
            MapKills cachedmapKills = EveApi.GetMapKills();

            Assert.AreEqual(mapKills.CachedUntilLocal, cachedmapKills.CachedUntilLocal);

            for (int i = 0; i < mapKills.MapSystemKills.Length; i++)
            {
                Assert.AreEqual(mapKills.MapSystemKills[i].PodKills, cachedmapKills.MapSystemKills[i].PodKills);
                Assert.AreEqual(mapKills.MapSystemKills[i].ShipKills, cachedmapKills.MapSystemKills[i].ShipKills);
            }
        }
    }
}
