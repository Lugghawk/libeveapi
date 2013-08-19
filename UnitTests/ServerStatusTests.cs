using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using libeveapi;

namespace UnitTests
{
    [TestFixture]
    public class ServerStatusTests
    {
        [SetUp]
        public void Setup()
        {
            Utility.UseLocalUrls();
        }

        [Test]
        public void GetServerStatusTest()
        {
            ResponseCache.Clear();

            ServerStatus serverStatus = EveApi.GetServerStatus();

            Assert.AreEqual(true, serverStatus.ServerOpen);
            Assert.AreEqual(28968, serverStatus.OnlinePlayers);
        }

        [Test]
        public void ServerStatusPersist()
        {
            ResponseCache.Clear();
            ServerStatus ss = EveApi.GetServerStatus();
            ResponseCache.Save("ResponseCache.xml");
            ResponseCache.Clear();
            ResponseCache.Load("ResponseCache.xml");
            ServerStatus cached = EveApi.GetServerStatus();

            Assert.AreEqual(cached.CachedUntilLocal, ss.CachedUntilLocal);
            Assert.AreEqual(cached.ServerOpen, ss.ServerOpen);
            Assert.AreEqual(cached.OnlinePlayers, ss.OnlinePlayers);
        }
    }
}
