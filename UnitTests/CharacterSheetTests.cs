using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using libeveapi;

namespace UnitTests
{
    [TestFixture]
    public class CharacterSheetTests
    {
        [SetUp]
        public void Setup()
        {
            Utility.UseLocalUrls();
        }

        [Test]
        public void GetCharacterSheet()
        {
            ResponseCache.Clear();

            CharacterSheet cs = EveApi.GetCharacterSheet(432435, 234523, "apiKey");

            Assert.AreEqual(150337897, cs.CharacterId);
            Assert.AreEqual("corpslave", cs.Name);
            Assert.AreEqual("Minmatar", cs.Race);
            Assert.AreEqual("Brutor", cs.BloodLine);
            Assert.AreEqual("Female", cs.Gender);
            Assert.AreEqual("corpexport Corp", cs.CorporationName);
            Assert.AreEqual(150337746, cs.CorporationId);
            Assert.AreEqual(190210393.87, cs.Balance);

            Assert.AreEqual("Memory Augmentation - Basic", cs.MemoryBonus.Name);
            Assert.AreEqual(3, cs.MemoryBonus.Value);
            Assert.AreEqual("Neural Boost - Basic", cs.WillpowerBonus.Name);
            Assert.AreEqual(3, cs.WillpowerBonus.Value);
            Assert.AreEqual("Ocular Filter - Standard", cs.PerceptionBonus.Name);
            Assert.AreEqual(4, cs.PerceptionBonus.Value);
            Assert.AreEqual(string.Empty, cs.IntelligenceBonus.Name);
            Assert.AreEqual(0, cs.IntelligenceBonus.Value);
            Assert.AreEqual("Social Adaptation Chip - Standard", cs.CharismaBonus.Name);
            Assert.AreEqual(4, cs.CharismaBonus.Value);

            Assert.AreEqual(8, cs.Intelligence);
            Assert.AreEqual(8, cs.Memory);
            Assert.AreEqual(6, cs.Charisma);
            Assert.AreEqual(8, cs.Perception);
            Assert.AreEqual(9, cs.Willpower);

            Assert.AreEqual(11584, cs.SkillItemList[0].TypeId);
            Assert.AreEqual(768000, cs.SkillItemList[0].Skillpoints);
            Assert.AreEqual(5, cs.SkillItemList[0].Level);
            Assert.AreEqual(null, cs.SkillItemList[0].Unpublished);
        }

        [Test]
        public void CharacterSheetPersist()
        {
            ResponseCache.Clear();
            CharacterSheet cs = EveApi.GetCharacterSheet(432435, 234523, "apiKey");
            ResponseCache.Save("ResponseCache.xml");
            ResponseCache.Clear();
            ResponseCache.Load("ResponseCache.xml");
            CharacterSheet cached = EveApi.GetCharacterSheet(432435, 234523, "apiKey");

            Assert.AreEqual(cached.CachedUntilLocal, cs.CachedUntilLocal);

            Assert.AreEqual(cached.CharacterId, cs.CharacterId);
            Assert.AreEqual(cached.Name, cs.Name);
            Assert.AreEqual(cached.Race, cs.Race);
            Assert.AreEqual(cached.BloodLine, cs.BloodLine);
            Assert.AreEqual(cached.Gender, cs.Gender);
            Assert.AreEqual(cached.CorporationName, cs.CorporationName);
            Assert.AreEqual(cached.CorporationId, cs.CorporationId);
            Assert.AreEqual(cached.Balance, cs.Balance);

            Assert.AreEqual(cached.MemoryBonus.Name, cs.MemoryBonus.Name);
            Assert.AreEqual(cached.MemoryBonus.Value, cs.MemoryBonus.Value);
            Assert.AreEqual(cached.WillpowerBonus.Name, cs.WillpowerBonus.Name);
            Assert.AreEqual(cached.WillpowerBonus.Value, cs.WillpowerBonus.Value);
            Assert.AreEqual(cached.PerceptionBonus.Name, cs.PerceptionBonus.Name);
            Assert.AreEqual(cached.PerceptionBonus.Value, cs.PerceptionBonus.Value);
            Assert.AreEqual(cached.IntelligenceBonus.Name, cs.IntelligenceBonus.Name);
            Assert.AreEqual(cached.IntelligenceBonus.Value, cs.IntelligenceBonus.Value);
            Assert.AreEqual(cached.CharismaBonus.Name, cs.CharismaBonus.Name);
            Assert.AreEqual(cached.CharismaBonus.Value, cs.CharismaBonus.Value);

            Assert.AreEqual(cached.Intelligence, cs.Intelligence);
            Assert.AreEqual(cached.Memory, cs.Memory);
            Assert.AreEqual(cached.Charisma, cs.Charisma);
            Assert.AreEqual(cached.Perception, cs.Perception);
            Assert.AreEqual(cached.Willpower, cs.Willpower);

            for (int i = 0; i < cs.SkillItemList.Length; i++)
            {
                Assert.AreEqual(cached.SkillItemList[i].TypeId, cs.SkillItemList[i].TypeId);
                Assert.AreEqual(cached.SkillItemList[i].Skillpoints, cs.SkillItemList[i].Skillpoints);
                Assert.AreEqual(cached.SkillItemList[i].Level, cs.SkillItemList[i].Level);
                Assert.AreEqual(cached.SkillItemList[i].Unpublished, cs.SkillItemList[i].Unpublished);
            }
        }
    }
}
