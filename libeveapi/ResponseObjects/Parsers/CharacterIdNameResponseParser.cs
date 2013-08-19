using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml;

namespace libeveapi.ResponseObjects.Parsers
{
    ///<summary>
    /// A parser which converts a given <see cref="XmlDocument"/> to a <see cref="CharacterIdName"/>.
    ///</summary>
    internal class CharacterIdNameResponseParser : IApiResponseParser<CharacterIdName>
    {
        public CharacterIdName Parse(XmlDocument xmlDocument)
        {
            this.CheckVersion(xmlDocument);

            //FIX the row:name problem here (HACK!)
            string fixXML = xmlDocument.OuterXml;
            fixXML = Regex.Replace(fixXML, "row:name", "row");
            xmlDocument.LoadXml(fixXML);
            //END FIX

            //REAL FIX - We're not using this because if they change then this will break -
            /* XmlNamespaceManager namespaceManager = new XmlNamespaceManager( xmlDocument.NameTable);
             * namespaceManager.AddNamespace("row", "characterId");
             * XmlNode n = d.SelectSingleNode("//rowset[@name='characters']/row:name", namespaceManager);
             * Console.WriteLine(n.Attributes["name"].InnerText + " " + n.Attributes["characterID"].InnerText)
             */

            CharacterIdName charId = new CharacterIdName();
            charId.ParseCommonElements(xmlDocument);

            List<CharacterIdName.CharacterIdNameItem> characterList = new List<CharacterIdName.CharacterIdNameItem>();
            foreach (XmlNode charIdRow in xmlDocument.SelectNodes("//rowset[@name='characters']/row"))
            {
                CharacterIdName.CharacterIdNameItem charIdItem = new CharacterIdName.CharacterIdNameItem();
                charIdItem.Name = charIdRow.Attributes["name"].InnerText;
                charIdItem.CharacterId = Convert.ToInt32(charIdRow.Attributes["characterID"].InnerText, CultureInfo.InvariantCulture);
                characterList.Add(charIdItem);
            }
            charId.CharacterIdItems = characterList.ToArray();

            return charId;
        }

        public void CheckVersion(XmlDocument xmlDocument)
        {
            if (CharacterIdName.VersionCheck)
            {
                string version = xmlDocument.SelectSingleNode("//eveapi").Attributes["version"].InnerText;
                if (version.CompareTo(CharacterIdName.API_VERSION) != 0)
                {
                    throw new ApiVersionException(version, CharacterIdName.API_VERSION);
                }
            }
        }

    }
}
