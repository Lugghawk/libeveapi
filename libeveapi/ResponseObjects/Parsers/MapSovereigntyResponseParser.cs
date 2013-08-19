using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace libeveapi.ResponseObjects.Parsers
{
    ///<summary>
    /// A parser which converts a given <see cref="XmlDocument"/> to a <see cref="MapSovereignty"/>.
    ///</summary>
    internal class MapSovereigntyResponseParser : IApiResponseParser<MapSovereignty>
    {
        public MapSovereignty Parse(XmlDocument xmlDocument)
        {
            this.CheckVersion(xmlDocument);
            MapSovereignty mapSovereignty = new MapSovereignty();
            mapSovereignty.ParseCommonElements(xmlDocument);

            List<MapSovereignty.MapSovereigntyItem> sovereigntyList = new List<MapSovereignty.MapSovereigntyItem>();
            foreach (XmlNode systemRow in xmlDocument.SelectNodes("//rowset[@name='solarSystems']/row"))
            {
                MapSovereignty.MapSovereigntyItem sovereigntyData = new MapSovereignty.MapSovereigntyItem();
                sovereigntyData.SolarSystemId = Convert.ToInt32(systemRow.Attributes["solarSystemID"].InnerText, CultureInfo.InvariantCulture);
                sovereigntyData.AllianceId = Convert.ToInt32(systemRow.Attributes["allianceID"].InnerText, CultureInfo.InvariantCulture);
                sovereigntyData.FactionId = Convert.ToInt32(systemRow.Attributes["factionID"].InnerText, CultureInfo.InvariantCulture);
                sovereigntyData.SolarSystemName = systemRow.Attributes["solarSystemName"].InnerText;
                sovereigntyData.CorporationID = Convert.ToInt32(systemRow.Attributes["corporationID"].InnerText, CultureInfo.InvariantCulture);
                sovereigntyList.Add(sovereigntyData);
            }
            mapSovereignty.MapSystemSovereigntyItems = sovereigntyList.ToArray();

            return mapSovereignty;
        }

        public void CheckVersion(XmlDocument xmlDocument)
        {
            if (MapSovereignty.VersionCheck)
            {
                string version = xmlDocument.SelectSingleNode("//eveapi").Attributes["version"].InnerText;
                if (version.CompareTo(MapSovereignty.API_VERSION) != 0)
                {
                    throw new ApiVersionException(version, MapSovereignty.API_VERSION);
                }
            }
        }
    }
}
