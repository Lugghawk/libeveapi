using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using libeveapi;

namespace UnitTests
{
    [TestFixture]
    public class MapFacWarSystemsTests
    {
        [SetUp]
        public void Setup()
        {
            Utility.UseLocalUrls();
        }

        [Test]
        public void GetFactionSystems()
        {
            MapFacWarSystems systems = EveApi.GetFactionWarSystems();
            Assert.Greater(systems.FactionWarSystems.Length, 0);

            MapFacWarSystems.FactionWarSystem system = systems.FactionWarSystems[0];
            Assert.AreEqual(30002056, system.SolarSystemId);
            Assert.AreEqual("Resbroko", system.SolarSystemName);
            Assert.AreEqual(0, system.OccupyingFactionId);
            Assert.AreEqual("", system.OccupyingFactionName);
            Assert.AreEqual(false, system.Contested);
        }

        [Test]
        public void FactionSystemsPersist()
        {
            ResponseCache.Clear();
            MapFacWarSystems systems = EveApi.GetFactionWarSystems();
            ResponseCache.Save("ResponseCache.xml");
            ResponseCache.Clear();
            ResponseCache.Load("ResponseCache.xml");
            MapFacWarSystems cache = EveApi.GetFactionWarSystems();

            Assert.AreEqual(cache.CachedUntilLocal, systems.CachedUntilLocal);
            Assert.AreEqual(cache.FactionWarSystems.Length, systems.FactionWarSystems.Length);

            for (int i = 0; i < systems.FactionWarSystems.Length; i++)
            {
                MapFacWarSystems.FactionWarSystem system = systems.FactionWarSystems[i];
                MapFacWarSystems.FactionWarSystem cached = cache.FactionWarSystems[i];

                Assert.AreEqual(cached.SolarSystemId, system.SolarSystemId);
                Assert.AreEqual(cached.SolarSystemName, system.SolarSystemName);
                Assert.AreEqual(cached.OccupyingFactionId, system.OccupyingFactionId);
                Assert.AreEqual(cached.OccupyingFactionName, system.OccupyingFactionName);
                Assert.AreEqual(cached.Contested, system.Contested);
            }
        }
    }
}
