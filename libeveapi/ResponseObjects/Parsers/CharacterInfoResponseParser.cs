using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace libeveapi.ResponseObjects.Parsers
{
    ///<summary>
    /// A parser which converts a given <see cref="XmlDocument"/> to a <see cref="CharacterSheet"/>.
    ///</summary>
    internal class CharacterInfoResponseParser : IApiResponseParser<CharacterInfo>
    {
        public CharacterInfo Parse(XmlDocument xmlDocument)
        {
            this.CheckVersion(xmlDocument);
            CharacterInfo charInfo = new CharacterInfo();
            charInfo.ParseCommonElements(xmlDocument);

            //Info
            //May not be in an alliance.
            if (xmlDocument.SelectSingleNode("/eveapi/result/alliance") != null)
            {
                charInfo.alliance = xmlDocument.SelectSingleNode("/eveapi/result/alliance").InnerText;
            }
            charInfo.balance = Convert.ToDouble(xmlDocument.SelectSingleNode("/eveapi/result/accountBalance").InnerText);
            charInfo.bloodLine = xmlDocument.SelectSingleNode("/eveapi/result/bloodline").InnerText;
            charInfo.characterId = Convert.ToInt32(xmlDocument.SelectSingleNode("/eveapi/result/characterID").InnerText);
            charInfo.corporationName = xmlDocument.SelectSingleNode("/eveapi/result/corporation").InnerText;
            charInfo.location = xmlDocument.SelectSingleNode("/eveapi/result/lastKnownLocation").InnerText;
            charInfo.name = xmlDocument.SelectSingleNode("/eveapi/result/characterName").InnerText;
            charInfo.race = xmlDocument.SelectSingleNode("/eveapi/result/race").InnerText;
            charInfo.secStatus = Convert.ToDouble(xmlDocument.SelectSingleNode("/eveapi/result/securityStatus").InnerText);
            charInfo.shipName = xmlDocument.SelectSingleNode("/eveapi/result/shipName").InnerText;
            charInfo.shipType = xmlDocument.SelectSingleNode("/eveapi/result/shipTypeName").InnerText;

            return charInfo;
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