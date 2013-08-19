using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using libeveapi;

namespace UnitTests
{
    [TestFixture]
    public class SkillTreeTests
    {
        [SetUp]
        public void Setup()
        {
            Utility.UseLocalUrls();
        }

        [Test]
        public void GetSkillTree()
        {
            ResponseCache.Clear();
            SkillTree skillTree = EveApi.GetSkillTree();
            SkillTree.Skill skill = findSkill(3368, skillTree);

            Assert.AreEqual("Ethnic Relations", skill.TypeName);
            Assert.AreEqual(266, skill.GroupId);
            Assert.AreEqual(3368, skill.TypeId);
            Assert.AreEqual("Skill at operating multiracial", skill.Description.Substring(0, 30));
            Assert.AreEqual(2, skill.Rank);

            Assert.AreEqual(2, skill.RequiredSkills.Length);
            Assert.AreEqual(3363, skill.RequiredSkills[0].TypeId);
            Assert.AreEqual(2, skill.RequiredSkills[0].SkillLevel);
            Assert.AreEqual(3355, skill.RequiredSkills[1].TypeId);
            Assert.AreEqual(3, skill.RequiredSkills[1].SkillLevel);

            Assert.AreEqual(SkillTree.AttributeType.Memory, skill.PrimaryAttribute);
            Assert.AreEqual(SkillTree.AttributeType.Charisma, skill.SecondaryAttribute);

            Assert.AreEqual(1, skill.SkillBonuses.Length);
            Assert.AreEqual("nonRaceCorporationMembersBonus", skill.SkillBonuses[0].BonusType);
            Assert.AreEqual(20, skill.SkillBonuses[0].BonusValue);

            Assert.AreEqual("Corporation Management", skillTree.SkillGroups[0].GroupName);
            Assert.AreEqual(266, skillTree.SkillGroups[0].GroupId);
        }

        [Test]
        public void PersistSkillTree()
        {
            ResponseCache.Clear();
            SkillTree skillTree = EveApi.GetSkillTree();
            ResponseCache.Save("ResponseCache.xml");
            ResponseCache.Clear();
            ResponseCache.Load("ResponseCache.xml");
            SkillTree cached = EveApi.GetSkillTree();

            Assert.AreEqual(skillTree.CachedUntilLocal, cached.CachedUntilLocal);

            // Skill Groups
            for (int i = 0; i < skillTree.SkillGroups.Length; i++)
            {
                Assert.AreEqual(skillTree.SkillGroups[i].GroupName, cached.SkillGroups[i].GroupName);
                Assert.AreEqual(skillTree.SkillGroups[i].GroupId, cached.SkillGroups[i].GroupId);
            }
            
            // Skills
            for (int i = 0; i < skillTree.Skills.Length; i++)
            {
                SkillTree.Skill skill = skillTree.Skills[i];
                SkillTree.Skill cskill = cached.Skills[i];

                Assert.AreEqual(skill.TypeName, cskill.TypeName);
                Assert.AreEqual(skill.GroupId, cskill.GroupId);
                Assert.AreEqual(skill.TypeId, cskill.TypeId);
                Assert.AreEqual(skill.Description, cskill.Description);
                Assert.AreEqual(skill.Rank, cskill.Rank);
                Assert.AreEqual(skill.PrimaryAttribute, cskill.PrimaryAttribute);
                Assert.AreEqual(skill.SecondaryAttribute, cskill.SecondaryAttribute);

                for (int j = 0; j < skill.RequiredSkills.Length; j++)
                {
                    Assert.AreEqual(skill.RequiredSkills[j].TypeId, cskill.RequiredSkills[j].TypeId);
                    Assert.AreEqual(skill.RequiredSkills[j].SkillLevel, cskill.RequiredSkills[j].SkillLevel);
                }

                for (int j = 0; j < skill.SkillBonuses.Length; j++)
                {
                    Assert.AreEqual(skill.SkillBonuses[j].BonusType, cskill.SkillBonuses[j].BonusType);
                    Assert.AreEqual(skill.SkillBonuses[j].BonusType, cskill.SkillBonuses[j].BonusType);
                }
            }
        }

        private SkillTree.Skill findSkill(int typeId, SkillTree skillTree)
        {
            foreach (SkillTree.Skill skill in skillTree.Skills)
            {
                if (skill.TypeId == typeId)
                {
                    return skill;
                }
            }

            return null;
        }
    }
}
