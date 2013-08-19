using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using libeveapi;

namespace UnitTests
{
    [TestFixture]
    public class ConquerableStationTests
    {
        [SetUp]
        public void Setup()
        {
            Utility.UseLocalUrls();
        }

        [Test]
        public void StationData()
        {
            ConquerableStationList stationList = EveApi.GetConquerableStationList();

            Assert.AreEqual(233, stationList.ConquerableStations.Length);
            Assert.AreEqual(60014926, stationList.ConquerableStations[0].StationId);
            Assert.AreEqual("The Alamo", stationList.ConquerableStations[0].StationName);
            Assert.AreEqual(12295, stationList.ConquerableStations[0].StationTypeId);
        }

        [Test]
        public void StationPersist()
        {
            ResponseCache.Clear();

            ConquerableStationList stationList = EveApi.GetConquerableStationList();
            ResponseCache.Save("ResponseCache.xml");
            ResponseCache.Clear();
            ResponseCache.Load("ResponseCache.xml");
            ConquerableStationList cachedstationList = EveApi.GetConquerableStationList();

            Assert.AreEqual(stationList.CachedUntilLocal, cachedstationList.CachedUntilLocal);


            for (int i = 0; i < stationList.ConquerableStations.Length; i++)
            {
                Assert.AreEqual(stationList.ConquerableStations[i].CorporationId, cachedstationList.ConquerableStations[i].CorporationId);
                Assert.AreEqual(stationList.ConquerableStations[i].CorporationName, cachedstationList.ConquerableStations[i].CorporationName);
                Assert.AreEqual(stationList.ConquerableStations[i].SolarSystemId, cachedstationList.ConquerableStations[i].SolarSystemId);
            }
            
        }
    }
}
