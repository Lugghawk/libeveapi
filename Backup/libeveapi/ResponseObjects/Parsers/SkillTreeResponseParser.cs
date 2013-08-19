using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace libeveapi.ResponseObjects.Parsers
{
    ///<summary>
    /// A parser which converts a given <see cref="XmlDocument"/> to a <see cref="SkillTree"/>.
    ///</summary>
    internal class SkillTreeResponseParser : IApiResponseParser<SkillTree>
    {
        public SkillTree Parse(XmlDocument xmlDocument)
        {
            this.CheckVersion(xmlDocument);
            SkillTree skillTree = new SkillTree();

            skillTree.ResponseXml = xmlDocument;
            // Skill Groups
            List<SkillTree.SkillGroup> parsedSkillGroups = new List<SkillTree.SkillGroup>();
            foreach (XmlNode row in xmlDocument.SelectNodes("//rowset[@name='skillGroups']/row"))
            {
                SkillTree.SkillGroup skillGroup = new SkillTree.SkillGroup();
                skillGroup.GroupName = row.Attributes["groupName"].InnerText;
                skillGroup.GroupId = Convert.ToInt32(row.Attributes["groupID"].InnerText);
                parsedSkillGroups.Add(skillGroup);
            }
            skillTree.SkillGroups = parsedSkillGroups.ToArray();

            // Skills
            List<SkillTree.Skill> parsedSkills = new List<SkillTree.Skill>();
            foreach (XmlNode row in xmlDocument.SelectNodes("//rowset[@name='skills']/row"))
            {
                SkillTree.Skill skill = new SkillTree.Skill();
                skill.TypeName = row.Attributes["typeName"].InnerText;
                skill.GroupId = Convert.ToInt32(row.Attributes["groupID"].InnerText, CultureInfo.InvariantCulture);
                skill.TypeId = Convert.ToInt32(row.Attributes["typeID"].InnerText, CultureInfo.InvariantCulture);
                skill.Description = row.SelectSingleNode("description").InnerText;
                skill.Rank = Convert.ToInt32(row.SelectSingleNode("rank").InnerText, CultureInfo.InvariantCulture);

                skill.PrimaryAttribute = MapStringToAttributeType(row.SelectSingleNode("requiredAttributes/primaryAttribute").InnerText);
                skill.SecondaryAttribute = MapStringToAttributeType(row.SelectSingleNode("requiredAttributes/secondaryAttribute").InnerText);

                // Required Skills
                List<SkillTree.RequiredSkill> parsedRequiredSkills = new List<SkillTree.RequiredSkill>();
                foreach (XmlNode requiredNode in row.SelectNodes("rowset[@name='requiredSkills']/row"))
                {
                    SkillTree.RequiredSkill requiredSkill = new SkillTree.RequiredSkill();
                    requiredSkill.TypeId = Convert.ToInt32(requiredNode.Attributes["typeID"].InnerText, CultureInfo.InvariantCulture);
                    requiredSkill.SkillLevel = Convert.ToInt32(requiredNode.Attributes["skillLevel"].InnerText, CultureInfo.InvariantCulture);
                    parsedRequiredSkills.Add(requiredSkill);
                }
                skill.RequiredSkills = parsedRequiredSkills.ToArray();

                // Skill Bonuses
                List<SkillTree.SkillBonus> parsedSkillBonuses = new List<SkillTree.SkillBonus>();
                foreach (XmlNode bonusNode in row.SelectNodes("rowset[@name='skillBonusCollection']/row"))
                {
                    SkillTree.SkillBonus skillBonus = new SkillTree.SkillBonus();
                    skillBonus.BonusType = bonusNode.Attributes["bonusType"].InnerText;
                    skillBonus.BonusValue = Convert.ToDouble(bonusNode.Attributes["bonusValue"].InnerText, CultureInfo.InvariantCulture);
                    parsedSkillBonuses.Add(skillBonus);
                }
                skill.SkillBonuses = parsedSkillBonuses.ToArray();

                parsedSkills.Add(skill);
            }
            skillTree.Skills = parsedSkills.ToArray();

            return skillTree;
        }

        private static SkillTree.AttributeType MapStringToAttributeType(string s)
        {
            switch (s)
            {
                case "memory":
                    return SkillTree.AttributeType.Memory;
                    
                case "willpower":
                    return SkillTree.AttributeType.Willpower;
                    
                case "intelligence":
                    return SkillTree.AttributeType.Intelligence;
                    
                case "perception":
                    return SkillTree.AttributeType.Perception;

                case "charisma":
                    return SkillTree.AttributeType.Charisma;
            }

            return SkillTree.AttributeType.Unknown;
        }

        public void CheckVersion(XmlDocument xmlDocument)
        {
            if (SkillTree.VersionCheck)
            {
                string version = xmlDocument.SelectSingleNode("//eveapi").Attributes["version"].InnerText;
                if (version.CompareTo(SkillTree.API_VERSION) != 0)
                {
                    throw new ApiVersionException(version, SkillTree.API_VERSION);
                }
            }
        }
    }
}
