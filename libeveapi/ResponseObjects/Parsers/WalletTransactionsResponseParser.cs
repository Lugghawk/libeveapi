using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace libeveapi.ResponseObjects.Parsers
{
    ///<summary>
    /// A parser which converts a given <see cref="XmlDocument"/> to a <see cref="WalletTransactions"/>.
    ///</summary>
    internal class WalletTransactionsResponseParser : IApiResponseParser<WalletTransactions>
    {
        public WalletTransactions Parse(XmlDocument xmlDocument)
        {
            this.CheckVersion(xmlDocument);
            WalletTransactions WalletTransactionList = new WalletTransactions();
            WalletTransactionList.ParseCommonElements(xmlDocument);

            List<WalletTransactions.WalletTransactionItem> transactions = new List<WalletTransactions.WalletTransactionItem>();
            foreach (XmlNode node in xmlDocument.SelectNodes("//rowset[@name='transactions']/row"))
            {
                transactions.Add(ParseTransactionRow(node));
            }
            WalletTransactionList.WalletTransactionItems = transactions.ToArray();

            return WalletTransactionList;
        }

        /// <summary>
        /// Create an WalletTransactionItem by parsing a single row
        /// </summary>
        /// <param name="walletTransactionRow"></param>
        /// <returns></returns>
        private static WalletTransactions.WalletTransactionItem ParseTransactionRow(XmlNode walletTransactionRow)
        {
            WalletTransactions.WalletTransactionItem walletTransactionItem = new WalletTransactions.WalletTransactionItem();

            walletTransactionItem.TransactionDateTime = TimeUtilities.ConvertCCPTimeStringToDateTimeUTC(walletTransactionRow.Attributes["transactionDateTime"].InnerText);
            walletTransactionItem.TransactionDateTimeLocal = TimeUtilities.ConvertCCPToLocalTime(walletTransactionItem.TransactionDateTime);
            walletTransactionItem.TransactionId = Convert.ToInt32(walletTransactionRow.Attributes["transactionID"].InnerText, CultureInfo.InvariantCulture);
            walletTransactionItem.Quantity = Convert.ToInt32(walletTransactionRow.Attributes["quantity"].InnerText, CultureInfo.InvariantCulture);
            walletTransactionItem.TypeName = walletTransactionRow.Attributes["typeName"].InnerText;
            walletTransactionItem.TypeId = Convert.ToInt32(walletTransactionRow.Attributes["typeID"].InnerText, CultureInfo.InvariantCulture);
            walletTransactionItem.Price = (float)Convert.ToDouble(walletTransactionRow.Attributes["price"].InnerText, CultureInfo.InvariantCulture);
            walletTransactionItem.ClientId = Convert.ToInt32(walletTransactionRow.Attributes["clientID"].InnerText, CultureInfo.InvariantCulture);
            walletTransactionItem.ClientName = walletTransactionRow.Attributes["clientName"].InnerText;
            
            //These are only present in the corp version
            if (walletTransactionRow.Attributes.GetNamedItem("characterID") != null)
            {
                walletTransactionItem.CharacterId = Convert.ToInt32(walletTransactionRow.Attributes["characterID"].InnerText, CultureInfo.InvariantCulture);
            }
            if (walletTransactionRow.Attributes.GetNamedItem("characterName") != null)
            {
                walletTransactionItem.CharacterName = walletTransactionRow.Attributes["characterName"].InnerText;
            }

            walletTransactionItem.StationId = Convert.ToInt32(walletTransactionRow.Attributes["stationID"].InnerText, CultureInfo.InvariantCulture);
            walletTransactionItem.StationName = walletTransactionRow.Attributes["stationName"].InnerText;
            walletTransactionItem.TransactionType = walletTransactionRow.Attributes["transactionType"].InnerText;
            walletTransactionItem.TransactionFor = walletTransactionRow.Attributes["transactionFor"].InnerText;

            return walletTransactionItem;
        }

        public void CheckVersion(XmlDocument xmlDocument)
        {
            if (WalletTransactions.VersionCheck)
            {
                string version = xmlDocument.SelectSingleNode("//eveapi").Attributes["version"].InnerText;
                if (version.CompareTo(WalletTransactions.API_VERSION) != 0)
                {
                    throw new ApiVersionException(version, WalletTransactions.API_VERSION);
                }
            }
        }
    }
}
