using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace libeveapi.ResponseObjects.Parsers
{
    ///<summary>
    /// A parser which converts a given <see cref="XmlDocument"/> to a <see cref="MemberTracking"/>.
    ///</summary>
    internal class MemberTrackingResponseParser : IApiResponseParser<MemberTracking>
    {
        public MemberTracking Parse(XmlDocument xmlDocument)
        {
            this.CheckVersion(xmlDocument);
            MemberTracking memberTracking = new MemberTracking();
            memberTracking.ParseCommonElements(xmlDocument);

            List<MemberTracking.Member> parsedMemeberTrackingItems = new List<MemberTracking.Member>();
            foreach (XmlNode row in xmlDocument.SelectNodes("//rowset[@name='members']/row"))
            {
                MemberTracking.Member mti = new MemberTracking.Member();
                mti.CharacterId = Convert.ToInt32(row.Attributes["characterID"].InnerText, CultureInfo.InvariantCulture);
                mti.Name = row.Attributes["name"].InnerText;
                mti.BaseId = Convert.ToInt32(row.Attributes["baseID"].InnerText, CultureInfo.InvariantCulture);
                mti.Base = row.Attributes["base"].InnerText;
                mti.Title = row.Attributes["title"].InnerText;
                mti.LocationId = Convert.ToInt32(row.Attributes["locationID"].InnerText, CultureInfo.InvariantCulture);
                mti.Location = row.Attributes["location"].InnerText;
                mti.ShipTypeId = Convert.ToInt32(row.Attributes["shipTypeID"].InnerText, CultureInfo.InvariantCulture);
                mti.ShipType = row.Attributes["shipType"].InnerText;
                mti.RolesMask = row.Attributes["roles"].InnerText;
                mti.GrantableRoles = row.Attributes["grantableRoles"].InnerText;

                mti.StartDateTime = TimeUtilities.ConvertCCPTimeStringToDateTimeUTC(row.Attributes["startDateTime"].InnerText);
                mti.LogonDateTime = TimeUtilities.ConvertCCPTimeStringToDateTimeUTC(row.Attributes["logonDateTime"].InnerText);
                mti.LogoffDateTime = TimeUtilities.ConvertCCPTimeStringToDateTimeUTC(row.Attributes["logoffDateTime"].InnerText);
                mti.StartDateTimeLocal = TimeUtilities.ConvertCCPToLocalTime(mti.StartDateTime);
                mti.LogonDateTimeLocal = TimeUtilities.ConvertCCPToLocalTime(mti.LogonDateTime);
                mti.LogoffDateTimeLocal = TimeUtilities.ConvertCCPToLocalTime(mti.LogoffDateTime);

                parsedMemeberTrackingItems.Add(mti);
            }

            memberTracking.Members = parsedMemeberTrackingItems.ToArray();
            return memberTracking;
        }

        public void CheckVersion(XmlDocument xmlDocument)
        {
            if (MemberTracking.VersionCheck)
            {
                string version = xmlDocument.SelectSingleNode("//eveapi").Attributes["version"].InnerText;
                if (version.CompareTo(MemberTracking.API_VERSION) != 0)
                {
                    throw new ApiVersionException(version, MemberTracking.API_VERSION);
                }
            }
        }
    }
}
