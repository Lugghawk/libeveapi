using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace libeveapi.ResponseObjects.Parsers
{
    ///<summary>
    /// A parser which converts a given <see cref="XmlDocument"/> to a <see cref="CharacterList"/>.
    ///</summary>
    internal class CharacterListResponseParser : IApiResponseParser<CharacterList>
    {
        public CharacterList Parse(XmlDocument xmlDocument)
        {
            this.CheckVersion(xmlDocument);

            CharacterList characterList = new CharacterList();
            characterList.ParseCommonElements(xmlDocument);

            List<CharacterList.CharacterListItem> parsedCharacters = new List<CharacterList.CharacterListItem>();
            foreach (XmlNode row in xmlDocument.SelectNodes("//rowset[@name='characters']/row"))
            {
                CharacterList.CharacterListItem character = new CharacterList.CharacterListItem();
                character.Name = row.Attributes["name"].InnerText;
                character.CharacterId = Convert.ToInt32(row.Attributes["characterID"].InnerText, CultureInfo.InvariantCulture);
                character.CorporationName = row.Attributes["corporationName"].InnerText;
                character.CorporationId = Convert.ToInt32(row.Attributes["corporationID"].InnerText, CultureInfo.InvariantCulture);
                parsedCharacters.Add(character);
            }
            characterList.CharacterListItems = parsedCharacters.ToArray();

            return characterList;
        }

        public void CheckVersion(XmlDocument xmlDocument)
        {
            if (CharacterList.VersionCheck)
            {
                string version = xmlDocument.SelectSingleNode("//eveapi").Attributes["version"].InnerText;
                if (version.CompareTo(CharacterList.API_VERSION) != 0)
                {
                    throw new ApiVersionException(version, CharacterList.API_VERSION);
                }
            }
        }

    }
}
