using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace libeveapi.ResponseObjects.Parsers
{
    ///<summary>
    /// A parser which converts a given <see cref="XmlDocument"/> to a <see cref="MarketOrders"/>.
    ///</summary>
    internal class MarketOrdersResponseParser : IApiResponseParser<MarketOrders>
    {
        public MarketOrders Parse(XmlDocument xmlDocument)
        {
            this.CheckVersion(xmlDocument);
            MarketOrders MarketOrderList = new MarketOrders();
            MarketOrderList.ParseCommonElements(xmlDocument);

            List<MarketOrders.MarketOrderItem> orders = new List<MarketOrders.MarketOrderItem>();
            foreach (XmlNode node in xmlDocument.SelectNodes("//rowset[@name='orders']/row"))
            {
                orders.Add(ParseTransactionRow(node));
            }
            MarketOrderList.MarketOrderItems = orders.ToArray();

            return MarketOrderList;
        }

        /// <summary>
        /// Create an MarketOrderItem by parsing a single row
        /// </summary>
        /// <param name="marketOrderRow"></param>
        /// <returns></returns>
        private static MarketOrders.MarketOrderItem ParseTransactionRow(XmlNode marketOrderRow)
        {
            MarketOrders.MarketOrderItem marketItem = new MarketOrders.MarketOrderItem();

            marketItem.OrderId = Convert.ToInt32(marketOrderRow.Attributes["orderID"].InnerText, CultureInfo.InvariantCulture);
            marketItem.CharId = Convert.ToInt64(marketOrderRow.Attributes["charID"].InnerText, CultureInfo.InvariantCulture);
            marketItem.StationId = Convert.ToInt64(marketOrderRow.Attributes["stationID"].InnerText, CultureInfo.InvariantCulture);
            marketItem.VolEntered = Convert.ToInt64(marketOrderRow.Attributes["volEntered"].InnerText, CultureInfo.InvariantCulture);
            marketItem.VolRemaining = Convert.ToInt64(marketOrderRow.Attributes["volRemaining"].InnerText, CultureInfo.InvariantCulture);
            marketItem.MinVolume = Convert.ToInt64(marketOrderRow.Attributes["minVolume"].InnerText, CultureInfo.InvariantCulture);

            switch (Convert.ToInt32(marketOrderRow.Attributes["orderState"].InnerText, CultureInfo.InvariantCulture))
            {
                case 0:
                    marketItem.OrderState = MarketOrders.MarketOrderState.OpenActive;
                    break;
                case 1:
                    marketItem.OrderState = MarketOrders.MarketOrderState.Closed;
                    break;
                case 2:
                    marketItem.OrderState = MarketOrders.MarketOrderState.ExpiredFulfilled;
                    break;
                case 3:
                    marketItem.OrderState = MarketOrders.MarketOrderState.Canceled;
                    break;
                case 4:
                    marketItem.OrderState = MarketOrders.MarketOrderState.Pending;
                    break;
                case 5:
                    marketItem.OrderState = MarketOrders.MarketOrderState.CharacterDeleted;
                    break;
                default:
                    break;
            }

            marketItem.TypeId = Convert.ToInt64(marketOrderRow.Attributes["typeID"].InnerText, CultureInfo.InvariantCulture);
            marketItem.Range = Convert.ToInt32(marketOrderRow.Attributes["range"].InnerText, CultureInfo.InvariantCulture);
            marketItem.AccountKey = Convert.ToInt32(marketOrderRow.Attributes["accountKey"].InnerText, CultureInfo.InvariantCulture);
            marketItem.Duration = Convert.ToInt32(marketOrderRow.Attributes["duration"].InnerText, CultureInfo.InvariantCulture);
            marketItem.Escrow = Convert.ToDouble(marketOrderRow.Attributes["escrow"].InnerText, CultureInfo.InvariantCulture);
            marketItem.Price = Convert.ToDouble(marketOrderRow.Attributes["price"].InnerText, CultureInfo.InvariantCulture);
            marketItem.Bid = Convert.ToBoolean(Convert.ToInt32(marketOrderRow.Attributes["bid"].InnerText, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
            marketItem.Issued = TimeUtilities.ConvertCCPTimeStringToDateTimeUTC(marketOrderRow.Attributes["issued"].InnerText);
            marketItem.IssuedLocal = TimeUtilities.ConvertCCPToLocalTime(marketItem.Issued);
            if (marketItem.Bid)
            {
                marketItem.OrderType = MarketOrders.MarketOrderType.Buy;
            }
            else
            {
                marketItem.OrderType = MarketOrders.MarketOrderType.Sell;
            }

            return marketItem;
        }

        public void CheckVersion(XmlDocument xmlDocument)
        {
            if (MarketOrders.VersionCheck)
            {
                string version = xmlDocument.SelectSingleNode("//eveapi").Attributes["version"].InnerText;
                if (version.CompareTo(MarketOrders.API_VERSION) != 0)
                {
                    throw new ApiVersionException(version, MarketOrders.API_VERSION);
                }
            }
        }
    }
}
