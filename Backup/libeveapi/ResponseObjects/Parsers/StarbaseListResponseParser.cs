using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace libeveapi.ResponseObjects.Parsers
{
    ///<summary>
    /// A parser which converts a given <see cref="XmlDocument"/> to a <see cref="StarbaseList"/>.
    ///</summary>
    internal class StarbaseListResponseParser : IApiResponseParser<StarbaseList>
    {
        public StarbaseList Parse(XmlDocument xmlDocument)
        {
            this.CheckVersion(xmlDocument);
            StarbaseList starbaseList = new StarbaseList();
            starbaseList.ParseCommonElements(xmlDocument);

            List<StarbaseList.StarbaseListItem> starbaseListItems = new List<StarbaseList.StarbaseListItem>();
            foreach (XmlNode starbaseNode in xmlDocument.SelectNodes("//rowset[@name='starbases']/row"))
            {
                StarbaseList.StarbaseListItem starbase = new StarbaseList.StarbaseListItem();
                starbase.ItemId = Convert.ToInt32(starbaseNode.Attributes["itemID"].InnerText, CultureInfo.InvariantCulture);
                starbase.TypeId = Convert.ToInt32(starbaseNode.Attributes["typeID"].InnerText, CultureInfo.InvariantCulture);
                starbase.LocationId = Convert.ToInt32(starbaseNode.Attributes["locationID"].InnerText, CultureInfo.InvariantCulture);
                starbase.MoonId = Convert.ToInt32(starbaseNode.Attributes["moonID"].InnerText, CultureInfo.InvariantCulture);
                starbase.StateTimestamp = TimeUtilities.ConvertCCPTimeStringToDateTimeUTC(starbaseNode.Attributes["stateTimestamp"].InnerText);
                starbase.OnlineTimestamp = TimeUtilities.ConvertCCPTimeStringToDateTimeUTC(starbaseNode.Attributes["onlineTimestamp"].InnerText);

                starbase.StateTimestampLocal = TimeUtilities.ConvertCCPToLocalTime(starbase.StateTimestamp);
                starbase.OnlineTimestampLocal = TimeUtilities.ConvertCCPToLocalTime(starbase.OnlineTimestamp);
                
                switch (Convert.ToInt32(starbaseNode.Attributes["state"].InnerText))
                {
                    case 1:
                        starbase.State = StarbaseList.StarbaseState.AnchoredOrOffline;
                        break;
                    case 2:
                        starbase.State = StarbaseList.StarbaseState.Onlining;
                        break;
                    case 3:
                        starbase.State = StarbaseList.StarbaseState.Reinforced;
                        break;
                    case 4:
                        starbase.State = StarbaseList.StarbaseState.Online;
                        break;
                    default:
                        break;
                }

                starbaseListItems.Add(starbase);
            }

            starbaseList.StarbaseListItems = starbaseListItems.ToArray();
            return starbaseList;
        }

        public void CheckVersion(XmlDocument xmlDocument)
        {
            if (StarbaseList.VersionCheck)
            {
                string version = xmlDocument.SelectSingleNode("//eveapi").Attributes["version"].InnerText;
                if (version.CompareTo(StarbaseList.API_VERSION) != 0)
                {
                    throw new ApiVersionException(version, StarbaseList.API_VERSION);
                }
            }
        }
    }
}
