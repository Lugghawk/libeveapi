using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace libeveapi.ResponseObjects.Parsers
{
    ///<summary>
    /// A parser which converts a given <see cref="XmlDocument"/> to a <see cref="AllianceList"/>.
    ///</summary>
    internal class AllianceListResponseParser : IApiResponseParser<AllianceList>
    {
        public AllianceList Parse(XmlDocument xmlDocument)
        {
            this.CheckVersion(xmlDocument);
            AllianceList allianceList = new AllianceList();
            allianceList.ParseCommonElements(xmlDocument);

            List<AllianceList.AllianceListItem> parsedAllianceListItems = new List<AllianceList.AllianceListItem>();
            foreach (XmlNode allianceRow in xmlDocument.SelectNodes("//rowset[@name='alliances']/row"))
            {
                AllianceList.AllianceListItem ali = new AllianceList.AllianceListItem();
                ali.Name = allianceRow.Attributes["name"].InnerText;
                ali.ShortName = allianceRow.Attributes["shortName"].InnerText;
                ali.AllianceId = Convert.ToInt32(allianceRow.Attributes["allianceID"].InnerText, CultureInfo.InvariantCulture);
                ali.ExecutorCorpId = Convert.ToInt32(allianceRow.Attributes["executorCorpID"].InnerText, CultureInfo.InvariantCulture);
                ali.MemberCount = Convert.ToInt32(allianceRow.Attributes["memberCount"].InnerText, CultureInfo.InvariantCulture);
                ali.StartDate = TimeUtilities.ConvertCCPTimeStringToDateTimeUTC(allianceRow.Attributes["startDate"].InnerText);
                ali.StartDateLocal = TimeUtilities.ConvertCCPToLocalTime(ali.StartDate);

                List<AllianceList.CorporationListItem> parsedCorporationListItems = new List<AllianceList.CorporationListItem>();
                foreach (XmlNode corpRow in allianceRow.SelectNodes("rowset[@name='memberCorporations']/row"))
                {
                    AllianceList.CorporationListItem cli = new AllianceList.CorporationListItem();
                    cli.CorporationId = Convert.ToInt32(corpRow.Attributes["corporationID"].InnerText, CultureInfo.InvariantCulture);
                    cli.StartDate = TimeUtilities.ConvertCCPTimeStringToDateTimeUTC(corpRow.Attributes["startDate"].InnerText);
                    cli.StartDateLocal = TimeUtilities.ConvertCCPToLocalTime(cli.StartDate);

                    parsedCorporationListItems.Add(cli);
                }
                ali.CorporationListItems = parsedCorporationListItems.ToArray();

                parsedAllianceListItems.Add(ali);
            }

            allianceList.AllianceListItems = parsedAllianceListItems.ToArray();
            return allianceList;
        }
        public void CheckVersion(XmlDocument xmlDocument)
        {
            if (AllianceList.VersionCheck)
            {
                string version = xmlDocument.SelectSingleNode("//eveapi").Attributes["version"].InnerText;
                if (version.CompareTo(AllianceList.API_VERSION) != 0)
                {
                    throw new ApiVersionException(version, AllianceList.API_VERSION);
                }
            }
        }

    }
}
