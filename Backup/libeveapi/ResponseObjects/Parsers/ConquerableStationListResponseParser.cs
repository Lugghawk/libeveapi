using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace libeveapi.ResponseObjects.Parsers
{
    ///<summary>
    /// A parser which converts a given <see cref="XmlDocument"/> to a <see cref="ConquerableStationList"/>.
    ///</summary>
    internal class ConquerableStationListResponseParser : IApiResponseParser<ConquerableStationList>
    {
        public ConquerableStationList Parse(XmlDocument xmlDocument)
        {
            this.CheckVersion(xmlDocument);
            ConquerableStationList stationList = new ConquerableStationList();
            stationList.ParseCommonElements(xmlDocument);

            List<ConquerableStationList.ConquerableStation> stationItemList = new List<ConquerableStationList.ConquerableStation>();
            foreach (XmlNode node in xmlDocument.SelectNodes("//rowset[@name='outposts']/row"))
            {
                stationItemList.Add(ParseStationRow(node));
            }
            stationList.ConquerableStations = stationItemList.ToArray();

            return stationList;
        }

        /// <summary>
        /// Create an ConquerableStationItem by parsing a single row
        /// </summary>
        /// <param name="stationRow"></param>
        /// <returns></returns>
        private static ConquerableStationList.ConquerableStation ParseStationRow(XmlNode stationRow)
        {
            ConquerableStationList.ConquerableStation conquerableStationItem = new ConquerableStationList.ConquerableStation();
            conquerableStationItem.StationId = Convert.ToInt32(stationRow.Attributes["stationID"].InnerText, CultureInfo.InvariantCulture);
            conquerableStationItem.StationName = stationRow.Attributes["stationName"].InnerText;
            conquerableStationItem.StationTypeId = Convert.ToInt32(stationRow.Attributes["stationTypeID"].InnerText, CultureInfo.InvariantCulture);
            conquerableStationItem.SolarSystemId = Convert.ToInt32(stationRow.Attributes["solarSystemID"].InnerText, CultureInfo.InvariantCulture);
            conquerableStationItem.CorporationId = Convert.ToInt32(stationRow.Attributes["corporationID"].InnerText, CultureInfo.InvariantCulture);
            conquerableStationItem.CorporationName = stationRow.Attributes["corporationName"].InnerText;

            return conquerableStationItem;
        }

        public void CheckVersion(XmlDocument xmlDocument)
        {
            if (ConquerableStationList.VersionCheck)
            {
                string version = xmlDocument.SelectSingleNode("//eveapi").Attributes["version"].InnerText;
                if (version.CompareTo(ConquerableStationList.API_VERSION) != 0)
                {
                    throw new ApiVersionException(version, ConquerableStationList.API_VERSION);
                }
            }
        }
    }
}
