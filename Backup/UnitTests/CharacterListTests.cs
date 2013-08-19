using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using libeveapi;

namespace UnitTests
{
    [TestFixture]
    public class CharacterListTests
    {
        [SetUp]
        public void Setup()
        {
            Utility.UseLocalUrls();
        }

        [Test]
        public void GetCharacterList()
        {
            CharacterList characterList = EveApi.GetAccountCharacters(756567, "asdf");

            Assert.AreEqual(3, characterList.CharacterListItems.Length);

            Assert.AreEqual("Mary", characterList.CharacterListItems[0].Name);
            Assert.AreEqual(150267069, characterList.CharacterListItems[0].CharacterId);
            Assert.AreEqual("Starbase Anchoring Corp", characterList.CharacterListItems[0].CorporationName);
            Assert.AreEqual(150279367, characterList.CharacterListItems[0].CorporationId);
        }

        [Test]
        public void CharacterListPersist()
        {
            ResponseCache.Clear();
            CharacterList characterList = EveApi.GetAccountCharacters(342535, "asdf");

            ResponseCache.Save("ResponseCache.xml");
            ResponseCache.Clear();
            ResponseCache.Load("ResponseCache.xml");
            CharacterList cachedCharacterList = EveApi.GetAccountCharacters(3245345, "asdf");

            Assert.AreEqual(characterList.CachedUntilLocal, cachedCharacterList.CachedUntilLocal);
            Assert.AreEqual(cachedCharacterList.CharacterListItems.Length, characterList.CharacterListItems.Length);

            for (int i = 0; i < characterList.CharacterListItems.Length; i++)
            {
                Assert.AreEqual(characterList.CharacterListItems[i].Name, cachedCharacterList.CharacterListItems[i].Name);
                Assert.AreEqual(characterList.CharacterListItems[i].CharacterId, cachedCharacterList.CharacterListItems[i].CharacterId);
                Assert.AreEqual(characterList.CharacterListItems[i].CorporationName, cachedCharacterList.CharacterListItems[i].CorporationName);
                Assert.AreEqual(characterList.CharacterListItems[i].CorporationId, cachedCharacterList.CharacterListItems[i].CorporationId);
            }
        }
    }
}
