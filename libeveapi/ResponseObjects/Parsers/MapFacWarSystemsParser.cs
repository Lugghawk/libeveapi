using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace libeveapi.ResponseObjects.Parsers
{
    /// <summary>
    /// A parser which converts a given <see cref="XmlDocument"/> to a <see cref="MapFacWarSystems"/>.
    /// </summary>
    internal class MapFacWarSystemsParser : IApiResponseParser<MapFacWarSystems>
    {
        public MapFacWarSystems Parse(XmlDocument xmlDocument)
        {
            this.CheckVersion(xmlDocument);
            MapFacWarSystems mapFacWarSystems = new MapFacWarSystems();
            mapFacWarSystems.ParseCommonElements(xmlDocument);

            List<MapFacWarSystems.FactionWarSystem> mapFacWarSystemItems = new List<MapFacWarSystems.FactionWarSystem>();
            foreach (XmlNode row in xmlDocument.SelectNodes("//rowset[@name='solarSystems']/row"))
            {
                MapFacWarSystems.FactionWarSystem item = new MapFacWarSystems.FactionWarSystem();
                item.SolarSystemId = Convert.ToInt32(row.Attributes["solarSystemID"].InnerText, CultureInfo.InvariantCulture);
                item.SolarSystemName = Convert.ToString(row.Attributes["solarSystemName"].InnerText, CultureInfo.InvariantCulture);
                item.OccupyingFactionId = Convert.ToInt32(row.Attributes["occupyingFactionID"].InnerText, CultureInfo.InvariantCulture);
                item.OccupyingFactionName = Convert.ToString(row.Attributes["occupyingFactionName"].InnerText, CultureInfo.InvariantCulture);
                item.Contested = Convert.ToBoolean(row.Attributes["contested"].InnerText, CultureInfo.InvariantCulture);
                mapFacWarSystemItems.Add(item);
            }

            mapFacWarSystems.FactionWarSystems = mapFacWarSystemItems.ToArray();
            return mapFacWarSystems;
        }

        public void CheckVersion(XmlDocument xmlDocument)
        {
            if (MapFacWarSystems.VersionCheck)
            {
                string version = xmlDocument.SelectSingleNode("//eveapi").Attributes["version"].InnerText;
                if (version.CompareTo(MapFacWarSystems.API_VERSION) != 0)
                {
                    throw new ApiVersionException(version, MapFacWarSystems.API_VERSION);
                }
            }
        }
    }
}
