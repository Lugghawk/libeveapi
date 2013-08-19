using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace libeveapi.ResponseObjects.Parsers
{
    ///<summary>
    /// A parser which converts a given <see cref="XmlDocument"/> to a <see cref="CharacterSheet"/>.
    ///</summary>
    internal class CharacterSheetResponseParser : IApiResponseParser<CharacterSheet>
    {
        public CharacterSheet Parse(XmlDocument xmlDocument)
        {
            this.CheckVersion(xmlDocument);
            CharacterSheet characterSheet = new CharacterSheet();
            characterSheet.ParseCommonElements(xmlDocument);

            // general info
            characterSheet.CharacterId = Convert.ToInt32(xmlDocument.SelectSingleNode("/eveapi/result/characterID").InnerText, CultureInfo.InvariantCulture);
            characterSheet.Name = xmlDocument.SelectSingleNode("/eveapi/result/name").InnerText;
            characterSheet.Race = xmlDocument.SelectSingleNode("/eveapi/result/race").InnerText;
            characterSheet.BloodLine = xmlDocument.SelectSingleNode("/eveapi/result/bloodLine").InnerText;
            characterSheet.Gender = xmlDocument.SelectSingleNode("/eveapi/result/gender").InnerText;
            characterSheet.CorporationName = xmlDocument.SelectSingleNode("/eveapi/result/corporationName").InnerText;
            characterSheet.CorporationId = Convert.ToInt32(xmlDocument.SelectSingleNode("/eveapi/result/corporationID").InnerText, CultureInfo.InvariantCulture);
            characterSheet.Balance = Convert.ToDouble(xmlDocument.SelectSingleNode("/eveapi/result/balance").InnerText, CultureInfo.InvariantCulture);

            // attribute enhancers
            ParseAugmentator("/eveapi/result/attributeEnhancers/memoryBonus", xmlDocument, characterSheet.MemoryBonus);
            ParseAugmentator("/eveapi/result/attributeEnhancers/willpowerBonus", xmlDocument, characterSheet.WillpowerBonus);
            ParseAugmentator("/eveapi/result/attributeEnhancers/perceptionBonus", xmlDocument, characterSheet.PerceptionBonus);
            ParseAugmentator("/eveapi/result/attributeEnhancers/intelligenceBonus", xmlDocument, characterSheet.IntelligenceBonus);
            ParseAugmentator("/eveapi/result/attributeEnhancers/charismaBonus", xmlDocument, characterSheet.CharismaBonus);

            // attributes
            characterSheet.Intelligence = Convert.ToInt32(xmlDocument.SelectSingleNode("/eveapi/result/attributes/intelligence").InnerText, CultureInfo.InvariantCulture);
            characterSheet.Memory = Convert.ToInt32(xmlDocument.SelectSingleNode("/eveapi/result/attributes/memory").InnerText, CultureInfo.InvariantCulture);
            characterSheet.Charisma = Convert.ToInt32(xmlDocument.SelectSingleNode("/eveapi/result/attributes/charisma").InnerText, CultureInfo.InvariantCulture);
            characterSheet.Perception = Convert.ToInt32(xmlDocument.SelectSingleNode("/eveapi/result/attributes/perception").InnerText, CultureInfo.InvariantCulture);
            characterSheet.Willpower = Convert.ToInt32(xmlDocument.SelectSingleNode("/eveapi/result/attributes/willpower").InnerText, CultureInfo.InvariantCulture);

            // skills
            List<CharacterSheet.SkillItem> parsedSkillItems = new List<CharacterSheet.SkillItem>();
            foreach (XmlNode row in xmlDocument.SelectNodes("//rowset[@name='skills']/row"))
            {
                CharacterSheet.SkillItem si = new CharacterSheet.SkillItem();
                si.TypeId = Convert.ToInt32(row.Attributes["typeID"].InnerText, CultureInfo.InvariantCulture);
                si.Skillpoints = Convert.ToInt64(row.Attributes["skillpoints"].InnerText, CultureInfo.InvariantCulture);
                si.Level = Convert.ToInt32(row.Attributes["level"].InnerText, CultureInfo.InvariantCulture);

                if (row.Attributes.GetNamedItem("unpublished") != null)
                {
                    si.Unpublished = row.Attributes["unpublished"].InnerText;
                }

                parsedSkillItems.Add(si);
            }
            characterSheet.SkillItemList = parsedSkillItems.ToArray();

            return characterSheet;
        }

        /// <summary>
        /// Parses out each implant that the character has in
        /// </summary>
        /// <param name="xpath">The xpath location of the implant in the xml</param>
        /// <param name="xmlDoc">The xml document</param>
        /// <param name="ae">The correct attribute enhancer to modify</param>
        /// <returns></returns>
        private static void ParseAugmentator(string xpath, XmlNode xmlDoc, CharacterSheet.AttributeEnhancer ae)
        {
            //XmlNode enhancer = xmlDoc.SelectSingleNode("/eveapi/result/attributeEnhancers/memoryBonus");
            XmlNode enhancer = xmlDoc.SelectSingleNode(xpath);
            if (enhancer != null)
            {
                ae.Name = enhancer.SelectSingleNode("./augmentatorName").InnerText;
                ae.Value = Convert.ToInt32(enhancer.SelectSingleNode("./augmentatorValue").InnerText);
            }
        }

        public void CheckVersion(XmlDocument xmlDocument)
        {
            if (CharacterSheet.VersionCheck)
            {
                string version = xmlDocument.SelectSingleNode("//eveapi").Attributes["version"].InnerText;
                if (version.CompareTo(CharacterSheet.API_VERSION) != 0)
                {
                    throw new ApiVersionException(version, CharacterSheet.API_VERSION);
                }
            }
        }

    }
}
