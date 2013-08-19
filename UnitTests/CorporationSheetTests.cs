using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using libeveapi;

namespace UnitTests
{
    [TestFixture]
    public class CorporationSheetTests
    {
        [SetUp]
        public void Setup()
        {
            Utility.UseLocalUrls();
        }

        [Test]
        public void GetCorporationSheet()
        {
            ResponseCache.Clear();
            CorporationSheet cs = EveApi.GetCorporationSheet(432435, 234523, "apiKey");
            testCorporationSheet(cs);

            ResponseCache.Clear();
            CorporationSheet cs2 = EveApi.GetCorporationSheet(432435, 234523, "apiKey", 2345);
            testCorporationSheet(cs2);
        }

        private void testCorporationSheet(CorporationSheet cs)
        {
            Assert.AreEqual(150212025, cs.CorporationId);
            Assert.AreEqual("Banana Republic", cs.CorporationName);
            Assert.AreEqual("BR", cs.Ticker);
            Assert.AreEqual(150208955, cs.CeoId);
            Assert.AreEqual("Mark Roled", cs.CeoName);
            Assert.AreEqual(60003469, cs.StationId);
            Assert.AreEqual("Jita IV - Caldari Business Tribunal Information Center", cs.StationName);
            Assert.AreEqual("test description", cs.Description);
            Assert.AreEqual("some url", cs.Url);
            Assert.AreEqual(150430947, cs.AllianceId);
            Assert.AreEqual("The Dead Rabbits", cs.AllianceName);
            Assert.AreEqual(93.7, cs.TaxRate);
            Assert.AreEqual(3, cs.MemberCount);
            Assert.AreEqual(6300, cs.MemberLimit);
            Assert.AreEqual(1, cs.Shares);

            Assert.AreEqual(7, cs.Divisions.Length);
            Assert.AreEqual(1000, cs.Divisions[0].AccountKey);
            Assert.AreEqual("1ST DIVISION", cs.Divisions[0].Description);

            Assert.AreEqual(7, cs.WalletDivisions.Length);
            Assert.AreEqual(1000, cs.WalletDivisions[0].AccountKey);
            Assert.AreEqual("Master Wallet", cs.WalletDivisions[0].Description);

            Assert.AreEqual(0, cs.Logo.GraphicId);
            Assert.AreEqual(448, cs.Logo.Shape1);
            Assert.AreEqual(0, cs.Logo.Shape2);
            Assert.AreEqual(418, cs.Logo.Shape3);
            Assert.AreEqual(681, cs.Logo.Color1);
            Assert.AreEqual(676, cs.Logo.Color2);
            Assert.AreEqual(0, cs.Logo.Color3);
        }

        [Test]
        public void CorporationSheetPersist()
        {
            ResponseCache.Clear();
            CorporationSheet cs = EveApi.GetCorporationSheet(432435, 234523, "apiKey");
            ResponseCache.Save("ResponseCache.xml");
            ResponseCache.Clear();
            ResponseCache.Load("ResponseCache.xml");
            CorporationSheet cached = EveApi.GetCorporationSheet(432435, 234523, "apiKey");

            Assert.AreEqual(cs.CachedUntilLocal, cached.CachedUntilLocal);

            Assert.AreEqual(cs.CorporationId, cached.CorporationId);
            Assert.AreEqual(cs.CorporationName, cached.CorporationName);
            Assert.AreEqual(cs.Ticker, cached.Ticker);
            Assert.AreEqual(cs.CeoId, cached.CeoId);
            Assert.AreEqual(cs.CeoName, cached.CeoName);
            Assert.AreEqual(cs.StationId, cached.StationId);
            Assert.AreEqual(cs.StationName, cached.StationName);
            Assert.AreEqual(cs.Description, cached.Description);
            Assert.AreEqual(cs.Url, cached.Url);
            Assert.AreEqual(cs.AllianceId, cached.AllianceId);
            Assert.AreEqual(cs.AllianceName, cached.AllianceName);
            Assert.AreEqual(cs.TaxRate, cached.TaxRate);
            Assert.AreEqual(cs.MemberCount, cached.MemberCount);
            Assert.AreEqual(cs.MemberLimit, cached.MemberLimit);
            Assert.AreEqual(cs.Shares, cached.Shares);

            for (int i = 0; i < cs.Divisions.Length; i++)
            {
                Assert.AreEqual(cs.Divisions[i].AccountKey, cached.Divisions[i].AccountKey);
                Assert.AreEqual(cs.Divisions[i].Description, cached.Divisions[i].Description);
            }

            for (int i = 0; i < cs.WalletDivisions.Length; i++)
            {
                Assert.AreEqual(cs.WalletDivisions[i].AccountKey, cached.WalletDivisions[i].AccountKey);
                Assert.AreEqual(cs.WalletDivisions[i].Description, cached.WalletDivisions[i].Description);
            }

            Assert.AreEqual(cs.Logo.GraphicId, cached.Logo.GraphicId);
            Assert.AreEqual(cs.Logo.Shape1, cached.Logo.Shape1);
            Assert.AreEqual(cs.Logo.Shape2, cached.Logo.Shape2);
            Assert.AreEqual(cs.Logo.Shape3, cached.Logo.Shape3);
            Assert.AreEqual(cs.Logo.Color1, cached.Logo.Color1);
            Assert.AreEqual(cs.Logo.Color2, cached.Logo.Color2);
            Assert.AreEqual(cs.Logo.Color3, cached.Logo.Color3);
        }
    }
}
