using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace libeveapi.ResponseObjects.Parsers
{
    ///<summary>
    /// A parser which converts a given <see cref="XmlDocument"/> to a <see cref="MapJumps"/>.
    ///</summary>
    internal class MapJumpsResponseParser : IApiResponseParser<MapJumps>
    {
        public MapJumps Parse(XmlDocument xmlDocument)
        {
            this.CheckVersion(xmlDocument);
            MapJumps mapJumps = new MapJumps();
            mapJumps.ParseCommonElements(xmlDocument);

            List<MapJumps.MapSystemItem> systemList = new List<MapJumps.MapSystemItem>();
            foreach (XmlNode systemRow in xmlDocument.SelectNodes("//rowset[@name='solarSystems']/row"))
            {
                MapJumps.MapSystemItem systemData = new MapJumps.MapSystemItem();
                systemData.SolarSystemId = Convert.ToInt32(systemRow.Attributes["solarSystemID"].InnerText, CultureInfo.InvariantCulture);
                systemData.ShipJumps = Convert.ToInt32(systemRow.Attributes["shipJumps"].InnerText, CultureInfo.InvariantCulture);
                systemList.Add(systemData);
            }
            mapJumps.MapSystemJumps = systemList.ToArray();

            return mapJumps;
        }

        public void CheckVersion(XmlDocument xmlDocument)
        {
            if (AccountBalance.VersionCheck)
            {
                string version = xmlDocument.SelectSingleNode("//eveapi").Attributes["version"].InnerText;
                if (version.CompareTo(AccountBalance.API_VERSION) != 0)
                {
                    throw new ApiVersionException(version, AccountBalance.API_VERSION);
                }
            }
        }
    }
}
