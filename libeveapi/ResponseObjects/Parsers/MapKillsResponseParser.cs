using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace libeveapi.ResponseObjects.Parsers
{
    ///<summary>
    /// A parser which converts a given <see cref="XmlDocument"/> to a <see cref="MapKills"/>.
    ///</summary>
    internal class MapKillsResponseParser : IApiResponseParser<MapKills>
    {
        public MapKills Parse(XmlDocument xmlDocument)
        {
            this.CheckVersion(xmlDocument);
            MapKills mapKills = new MapKills();
            mapKills.ParseCommonElements(xmlDocument);

            List<MapKills.MapKillsItem> systemList = new List<MapKills.MapKillsItem>();
            foreach (XmlNode systemRow in xmlDocument.SelectNodes("//rowset[@name='solarSystems']/row"))
            {
                MapKills.MapKillsItem systemData = new MapKills.MapKillsItem();
                systemData.SolarSystemId = Convert.ToInt32(systemRow.Attributes["solarSystemID"].InnerText, CultureInfo.InvariantCulture);
                systemData.ShipKills = Convert.ToInt32(systemRow.Attributes["shipKills"].InnerText, CultureInfo.InvariantCulture);
                systemData.FactionKills = Convert.ToInt32(systemRow.Attributes["factionKills"].InnerText, CultureInfo.InvariantCulture);
                systemData.PodKills = Convert.ToInt32(systemRow.Attributes["podKills"].InnerText, CultureInfo.InvariantCulture);
                systemList.Add(systemData);
            }
            mapKills.MapSystemKills = systemList.ToArray();

            return mapKills;
        }

        public void CheckVersion(XmlDocument xmlDocument)
        {
            if (MapKills.VersionCheck)
            {
                string version = xmlDocument.SelectSingleNode("//eveapi").Attributes["version"].InnerText;
                if (version.CompareTo(MapKills.API_VERSION) != 0)
                {
                    throw new ApiVersionException(version, MapKills.API_VERSION);
                }
            }
        }
    }
}
