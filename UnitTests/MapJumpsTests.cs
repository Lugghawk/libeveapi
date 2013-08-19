using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using libeveapi;

namespace UnitTests
{
    [TestFixture]
    public class MapJumpsTests
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

            MapJumps mapJumps = EveApi.GetMapJumps();
            ResponseCache.Save("ResponseCache.xml");
            ResponseCache.Clear();
            ResponseCache.Load("ResponseCache.xml");
            MapJumps cachedMapJumps = EveApi.GetMapJumps();

            Assert.AreEqual(mapJumps.CachedUntilLocal, cachedMapJumps.CachedUntilLocal);

            for (int i = 0; i < mapJumps.MapSystemJumps.Length; i++)
            {
                Assert.AreEqual(mapJumps.MapSystemJumps[i].ShipJumps, cachedMapJumps.MapSystemJumps[i].ShipJumps);
                Assert.AreEqual(mapJumps.MapSystemJumps[i].SolarSystemId, cachedMapJumps.MapSystemJumps[i].SolarSystemId);
            }
        }
    }
}
