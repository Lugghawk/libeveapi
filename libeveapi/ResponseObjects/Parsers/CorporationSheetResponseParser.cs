using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace libeveapi.ResponseObjects.Parsers
{
    ///<summary>
    /// A parser which converts a given <see cref="XmlDocument"/> to a <see cref="CorporationSheet"/>.
    ///</summary>
    internal class CorporationSheetResponseParser : IApiResponseParser<CorporationSheet>
    {
        public CorporationSheet Parse(XmlDocument xmlDocument)
        {
            this.CheckVersion(xmlDocument);
            CorporationSheet corporationSheet = new CorporationSheet();
            corporationSheet.ParseCommonElements(xmlDocument);

            corporationSheet.CorporationId = Convert.ToInt32(xmlDocument.SelectSingleNode("//result/corporationID").InnerText, CultureInfo.InvariantCulture);
            corporationSheet.CorporationName = xmlDocument.SelectSingleNode("//result/corporationName").InnerText;
            corporationSheet.Ticker = xmlDocument.SelectSingleNode("//result/ticker").InnerText;
            corporationSheet.CeoId = Convert.ToInt32(xmlDocument.SelectSingleNode("//result/ceoID").InnerText, CultureInfo.InvariantCulture);
            corporationSheet.CeoName = xmlDocument.SelectSingleNode("//result/ceoName").InnerText;
            corporationSheet.StationId = Convert.ToInt32(xmlDocument.SelectSingleNode("//result/stationID").InnerText, CultureInfo.InvariantCulture);
            corporationSheet.StationName = xmlDocument.SelectSingleNode("//result/stationName").InnerText;
            corporationSheet.Description = xmlDocument.SelectSingleNode("//result/description").InnerText;
            corporationSheet.Url = xmlDocument.SelectSingleNode("//result/url").InnerText;
            corporationSheet.AllianceId = Convert.ToInt32(xmlDocument.SelectSingleNode("//result/allianceID").InnerText, CultureInfo.InvariantCulture);
            if( corporationSheet.AllianceId != 0 )
            {
                corporationSheet.AllianceName = xmlDocument.SelectSingleNode("//result/allianceName").InnerText;
            }
            corporationSheet.TaxRate = Convert.ToDouble(xmlDocument.SelectSingleNode("//result/taxRate").InnerText, CultureInfo.InvariantCulture);
            corporationSheet.MemberCount = Convert.ToInt32(xmlDocument.SelectSingleNode("//result/memberCount").InnerText, CultureInfo.InvariantCulture);
            corporationSheet.MemberLimit = Convert.ToInt32(xmlDocument.SelectSingleNode("//result/memberLimit").InnerText, CultureInfo.InvariantCulture);
            corporationSheet.Shares = Convert.ToInt64(xmlDocument.SelectSingleNode("//result/shares").InnerText, CultureInfo.InvariantCulture);

            List<CorporationSheet.Division> parsedDivisions = new List<CorporationSheet.Division>();
            foreach (XmlNode row in xmlDocument.SelectNodes("//rowset[@name='divisions']/row"))
            {
                CorporationSheet.Division division = new CorporationSheet.Division();
                division.AccountKey = Convert.ToInt32(row.Attributes["accountKey"].InnerText, CultureInfo.InvariantCulture);
                division.Description = row.Attributes["description"].InnerText;
                parsedDivisions.Add(division);
            }
            corporationSheet.Divisions = parsedDivisions.ToArray();

            List<CorporationSheet.WalletDivision> parsedWalletDivisions = new List<CorporationSheet.WalletDivision>();
            foreach (XmlNode row in xmlDocument.SelectNodes("//rowset[@name='walletDivisions']/row"))
            {
                CorporationSheet.WalletDivision walletDivision = new CorporationSheet.WalletDivision();
                walletDivision.AccountKey = Convert.ToInt32(row.Attributes["accountKey"].InnerText, CultureInfo.InvariantCulture);
                walletDivision.Description = row.Attributes["description"].InnerText;
                parsedWalletDivisions.Add(walletDivision);
            }
            corporationSheet.WalletDivisions = parsedWalletDivisions.ToArray();

            corporationSheet.Logo.GraphicId = Convert.ToInt32(xmlDocument.SelectSingleNode("//result/logo/graphicID").InnerText, CultureInfo.InvariantCulture);
            corporationSheet.Logo.Shape1 = Convert.ToInt32(xmlDocument.SelectSingleNode("//result/logo/shape1").InnerText, CultureInfo.InvariantCulture);
            corporationSheet.Logo.Shape2 = Convert.ToInt32(xmlDocument.SelectSingleNode("//result/logo/shape2").InnerText, CultureInfo.InvariantCulture);
            corporationSheet.Logo.Shape3 = Convert.ToInt32(xmlDocument.SelectSingleNode("//result/logo/shape3").InnerText, CultureInfo.InvariantCulture);
            corporationSheet.Logo.Color1 = Convert.ToInt32(xmlDocument.SelectSingleNode("//result/logo/color1").InnerText, CultureInfo.InvariantCulture);
            corporationSheet.Logo.Color2 = Convert.ToInt32(xmlDocument.SelectSingleNode("//result/logo/color2").InnerText, CultureInfo.InvariantCulture);
            corporationSheet.Logo.Color3 = Convert.ToInt32(xmlDocument.SelectSingleNode("//result/logo/color3").InnerText, CultureInfo.InvariantCulture);

            return corporationSheet;
        }

        public void CheckVersion(XmlDocument xmlDocument)
        {
            if (CorporationSheet.VersionCheck)
            {
                string version = xmlDocument.SelectSingleNode("//eveapi").Attributes["version"].InnerText;
                if (version.CompareTo(CorporationSheet.API_VERSION) != 0)
                {
                    throw new ApiVersionException(version, CorporationSheet.API_VERSION);
                }
            }
        }
    }
}
