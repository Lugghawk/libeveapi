using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace libeveapi.ResponseObjects.Parsers
{
    ///<summary>
    /// A parser which converts a given <see cref="XmlDocument"/> to a <see cref="StarbaseDetail"/>.
    ///</summary>
    internal class StarbaseDetailResponseParser : IApiResponseParser<StarbaseDetail>
    {
        public StarbaseDetail Parse(XmlDocument xmlDocument)
        {
            this.CheckVersion(xmlDocument);
            StarbaseDetail starbaseDetail = new StarbaseDetail();
            starbaseDetail.ParseCommonElements(xmlDocument);

            starbaseDetail.UsageFlags = xmlDocument.SelectSingleNode("//generalSettings/usageFlags").InnerText;
            starbaseDetail.DeployFlags = xmlDocument.SelectSingleNode("//generalSettings/deployFlags").InnerText;
            starbaseDetail.AllowCorporationMembers = Convert.ToBoolean(Convert.ToInt32(xmlDocument.SelectSingleNode("//generalSettings/allowCorporationMembers").InnerText, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
            starbaseDetail.AllowAllianceMembers = Convert.ToBoolean(Convert.ToInt32(xmlDocument.SelectSingleNode("//generalSettings/allowAllianceMembers").InnerText, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
            starbaseDetail.ClaimSovereignty = Convert.ToBoolean(Convert.ToInt32(xmlDocument.SelectSingleNode("//generalSettings/claimSovereignty").InnerText, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);

            starbaseDetail.OnStandingDropEnabled = Convert.ToBoolean(Convert.ToInt32(xmlDocument.SelectSingleNode("//combatSettings/onStandingDrop").Attributes["enabled"].InnerText, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
            starbaseDetail.OnStandingDropStanding = xmlDocument.SelectSingleNode("//combatSettings/onStandingDrop").Attributes["standing"].InnerText;
            starbaseDetail.OnStatusDropEnabled = Convert.ToBoolean(Convert.ToInt32(xmlDocument.SelectSingleNode("//combatSettings/onStatusDrop").Attributes["enabled"].InnerText, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
            starbaseDetail.OnStatusDropStanding = xmlDocument.SelectSingleNode("//combatSettings/onStatusDrop").Attributes["standing"].InnerText;
            starbaseDetail.OnAgressionEnabled = Convert.ToBoolean(Convert.ToInt32(xmlDocument.SelectSingleNode("//combatSettings/onAggression").Attributes["enabled"].InnerText, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
            starbaseDetail.OnCorporationWarEnabled = Convert.ToBoolean(Convert.ToInt32(xmlDocument.SelectSingleNode("//combatSettings/onCorporationWar").Attributes["enabled"].InnerText, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
            
            List<StarbaseDetail.FuelListItem> fuelList = new List<StarbaseDetail.FuelListItem>();
            foreach (XmlNode fuelNode in xmlDocument.SelectNodes("//rowset[@name='fuel']/row"))
            {
                StarbaseDetail.FuelListItem fli = new StarbaseDetail.FuelListItem();
                fli.TypeId = Convert.ToInt32(fuelNode.Attributes["typeID"].InnerText, CultureInfo.InvariantCulture);
                fli.Quantity = Convert.ToInt64(fuelNode.Attributes["quantity"].InnerText, CultureInfo.InvariantCulture);
                fuelList.Add(fli);
            }
            starbaseDetail.FuelList = fuelList.ToArray();

            return starbaseDetail;
        }

        public void CheckVersion(XmlDocument xmlDocument)
        {
            if (StarbaseDetail.VersionCheck)
            {
                string version = xmlDocument.SelectSingleNode("//eveapi").Attributes["version"].InnerText;
                if (version.CompareTo(StarbaseDetail.API_VERSION) != 0)
                {
                    throw new ApiVersionException(version, StarbaseDetail.API_VERSION);
                }
            }
        }
    }
}
