using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using libeveapi;

namespace UnitTests
{
    [TestFixture]
    public class MarketOrdersTests
    {
        [SetUp]
        public void Setup()
        {
            Utility.UseLocalUrls();
        }

        [Test]
        public void GetCharMarketOrdersTest()
        {
            MarketOrders marketOrder = EveApi.GetMarketOrderList(MarketOrdersListType.Character, 432435, 346, "apiKey");   

            Assert.AreEqual(9, marketOrder.MarketOrderItems.Length);
            Assert.AreEqual(150208955, marketOrder.MarketOrderItems[0].CharId);
            Assert.AreEqual(60010783, marketOrder.MarketOrderItems[0].StationId);
            Assert.AreEqual(325.00, marketOrder.MarketOrderItems[0].Price);

        }

        [Test]
        public void GetCorpMarketOrdersTest()
        {
            MarketOrders marketOrder = EveApi.GetMarketOrderList(MarketOrdersListType.Corporation, 432435, 346, "apiKey");

            Assert.AreEqual(9, marketOrder.MarketOrderItems.Length);
            Assert.AreEqual(150208955, marketOrder.MarketOrderItems[0].CharId);
            Assert.AreEqual(60010783, marketOrder.MarketOrderItems[0].StationId);
            Assert.AreEqual(325.00, marketOrder.MarketOrderItems[0].Price);
        }

        [Test]
        public void MarketOrderPersist()
        {
            ResponseCache.Clear();

            MarketOrders marketOrder = EveApi.GetMarketOrderList(MarketOrdersListType.Corporation, 432435, 346, "apiKey");
            ResponseCache.Save("ResponseCache.xml");
            ResponseCache.Clear();
            ResponseCache.Load("ResponseCache.xml");
            MarketOrders cachedmarketOrder = EveApi.GetMarketOrderList(MarketOrdersListType.Corporation, 432435, 346, "apiKey");

            Assert.AreEqual(marketOrder.CachedUntilLocal, cachedmarketOrder.CachedUntilLocal);


            for (int i = 0; i < marketOrder.MarketOrderItems.Length; i++)
            {
                Assert.AreEqual(marketOrder.MarketOrderItems[i].AccountKey, cachedmarketOrder.MarketOrderItems[i].AccountKey);
                Assert.AreEqual(marketOrder.MarketOrderItems[i].CharId, cachedmarketOrder.MarketOrderItems[i].CharId);
                Assert.AreEqual(marketOrder.MarketOrderItems[i].MinVolume, cachedmarketOrder.MarketOrderItems[i].MinVolume);
            }
            
        }
    }
}
