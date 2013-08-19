using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using libeveapi;

namespace UnitTests
{
    [TestFixture]
    public class CharacterIdTests
    {
        [SetUp]
        public void Setup()
        {
            Utility.UseLocalUrls();
        }

        [Test]
        public void TestCharNameToId()
        {
            CharacterIdName charId = EveApi.GetCharacterIdName(1234);

            Assert.AreEqual(797400947, charId.CharacterIdItems[0].CharacterId);
            Assert.AreEqual("CCP Garthagk", charId.CharacterIdItems[0].Name);
        }

        [Test]
        public void TestCharIdToName()
        {
            CharacterIdName charId = EveApi.GetCharacterIdName("test123");

            Assert.AreEqual(797400947, charId.CharacterIdItems[0].CharacterId);
            Assert.AreEqual("CCP Garthagk", charId.CharacterIdItems[0].Name);
        }

        [Test]
        public void PersistCharacterId()
        {
            ResponseCache.Clear();
            CharacterIdName cid = EveApi.GetCharacterIdName("test");
            ResponseCache.Save("ResponseCache.xml");
            ResponseCache.Clear();
            ResponseCache.Load("ResponseCache.xml");
            CharacterIdName ccid = EveApi.GetCharacterIdName("test");

            Assert.AreEqual(cid.CachedUntilLocal, ccid.CachedUntilLocal);

            Assert.AreEqual(cid.CharacterIdItems[0].Name, ccid.CharacterIdItems[0].Name);
            Assert.AreEqual(cid.CharacterIdItems[0].CharacterId, ccid.CharacterIdItems[0].CharacterId);
        }
    }
}
