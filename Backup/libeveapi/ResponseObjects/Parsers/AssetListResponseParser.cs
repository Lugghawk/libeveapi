using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace libeveapi.ResponseObjects.Parsers
{
    ///<summary>
    /// A parser which converts a given <see cref="XmlDocument"/> to a <see cref="AssetList"/>.
    ///</summary>
    internal class AssetListResponseParser : IApiResponseParser<AssetList>
    {
        public AssetList Parse(XmlDocument xmlDocument)
        {
            this.CheckVersion(xmlDocument);
            AssetList assetList = new AssetList();
            assetList.ParseCommonElements(xmlDocument);

            List<AssetList.AssetListItem> assets = new List<AssetList.AssetListItem>();
            foreach (XmlNode node in xmlDocument.SelectNodes("//rowset[@name='assets']/row"))
            {
                assets.Add(ParseAssetRow(node));
            }
            assetList.AssetListItems = assets.ToArray();

            return assetList;
        }

        /// <summary>
        /// Create an AssetListItem by parsing a single row
        /// Recursively parses all contained assets
        /// </summary>
        /// <param name="assetRow"></param>
        /// <returns></returns>
        private static AssetList.AssetListItem ParseAssetRow(XmlNode assetRow)
        {
            AssetList.AssetListItem assetListItem = new AssetList.AssetListItem();
            assetListItem.ItemId = Convert.ToInt32(assetRow.Attributes["itemID"].InnerText, CultureInfo.InvariantCulture);
            assetListItem.TypeId = Convert.ToInt32(assetRow.Attributes["typeID"].InnerText, CultureInfo.InvariantCulture);
            assetListItem.Quantity = Convert.ToInt64(assetRow.Attributes["quantity"].InnerText, CultureInfo.InvariantCulture);
            assetListItem.Singleton = Convert.ToBoolean(Convert.ToInt32(assetRow.Attributes["singleton"].InnerText, CultureInfo.InvariantCulture));

            int flagValue = Convert.ToInt32(assetRow.Attributes["flag"].InnerText);
            if (Enum.IsDefined(typeof(InventoryFlagType), flagValue))
            {
                assetListItem.Flag = (InventoryFlagType)Enum.ToObject(typeof(InventoryFlagType), flagValue);
            }
            else
            {
                assetListItem.Flag = InventoryFlagType.FlagUnknown;
            }

            if (assetRow.Attributes.GetNamedItem("locationID") != null)
            {
                assetListItem.LocationId = Convert.ToInt32(assetRow.Attributes["locationID"].InnerText, CultureInfo.InvariantCulture);
            }
            

            // Parse any contained assets and add them to this item's contents
            List<AssetList.AssetListItem> containedAssets = new List<AssetList.AssetListItem>();
            foreach (XmlNode containedAssetRow in assetRow.SelectNodes("./rowset[@name='contents']/row"))
            {
                containedAssets.Add(ParseAssetRow(containedAssetRow));
            }
            assetListItem.Contents = containedAssets.ToArray();

            return assetListItem;
        }

        public void CheckVersion(XmlDocument xmlDocument)
        {
            if (AssetList.VersionCheck)
            {
                string version = xmlDocument.SelectSingleNode("//eveapi").Attributes["version"].InnerText;
                if (version.CompareTo(AssetList.API_VERSION) != 0)
                {
                    throw new ApiVersionException(version, AssetList.API_VERSION);
                }
            }
        }
    }
}
