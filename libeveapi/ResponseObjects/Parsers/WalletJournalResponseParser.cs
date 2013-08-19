using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Xml;

namespace libeveapi.ResponseObjects.Parsers
{
    ///<summary>
    /// A parser which converts a given <see cref="XmlDocument"/> to a <see cref="WalletJournal"/>.
    ///</summary>
    internal class WalletJournalResponseParser : IApiResponseParser<WalletJournal>
    {

        public WalletJournal Parse(XmlDocument xmlDocument)
        {
            this.CheckVersion(xmlDocument);
            WalletJournal walletJournal = new WalletJournal();
            walletJournal.ParseCommonElements(xmlDocument);

            List<WalletJournal.WalletJournalItem> entry = new List<WalletJournal.WalletJournalItem>();
            foreach (XmlNode node in xmlDocument.SelectNodes("//rowset[@name='entries']/row"))
            {
                entry.Add(ParseTransactionRow(node));
            }
            walletJournal.WalletJournalItems = entry.ToArray();

            return walletJournal;
        }

        /// <summary>
        /// Create a Wallet Journal Entry by parsing a single row
        /// </summary>
        /// <param name="walletJournalRow">Xml Wallet Journal Entry</param>
        /// <returns>Populated object</returns>
        private static WalletJournal.WalletJournalItem ParseTransactionRow(XmlNode walletJournalRow)
        {
            WalletJournal.WalletJournalItem entryItem = new WalletJournal.WalletJournalItem();

            entryItem.Date = TimeUtilities.ConvertCCPTimeStringToDateTimeUTC(walletJournalRow.Attributes["date"].InnerText);
            entryItem.DateLocal = TimeUtilities.ConvertCCPToLocalTime(entryItem.Date);
            entryItem.RefId = Convert.ToInt32(walletJournalRow.Attributes["refID"].InnerText, CultureInfo.InvariantCulture);
            entryItem.RefTypeId = Convert.ToInt32(walletJournalRow.Attributes["refTypeID"].InnerText, CultureInfo.InvariantCulture);
            entryItem.OwnerName1 = walletJournalRow.Attributes["ownerName1"].InnerText;
            entryItem.OwnerId1 = Convert.ToInt32(walletJournalRow.Attributes["ownerID1"].InnerText, CultureInfo.InvariantCulture);
            entryItem.OwnerName2 = walletJournalRow.Attributes["ownerName2"].InnerText;
            entryItem.OwnerId2 = Convert.ToInt32(walletJournalRow.Attributes["ownerID2"].InnerText, CultureInfo.InvariantCulture);
            entryItem.ArgName1 = walletJournalRow.Attributes["argName1"].InnerText;
            entryItem.ArgId1 = Convert.ToInt32(walletJournalRow.Attributes["argID1"].InnerText, CultureInfo.InvariantCulture);
            entryItem.Amount = Convert.ToDouble(walletJournalRow.Attributes["amount"].InnerText, CultureInfo.InvariantCulture);
            entryItem.Balance = Convert.ToDouble(walletJournalRow.Attributes["balance"].InnerText, CultureInfo.InvariantCulture);
            entryItem.Reason = walletJournalRow.Attributes["reason"].InnerText;

            //These are only present in the corp version
            if (walletJournalRow.Attributes.GetNamedItem("taxReceiverID") != null)
            {
                if (walletJournalRow.Attributes["taxAmount"].InnerText.CompareTo(string.Empty) != 0)
                    entryItem.TaxRecieverID = Convert.ToInt32(walletJournalRow.Attributes["taxReceiverID"].InnerText, CultureInfo.InvariantCulture);
                else
                    entryItem.TaxRecieverID = 0;
            }
            if (walletJournalRow.Attributes.GetNamedItem("taxAmount") != null)
            {
                if (walletJournalRow.Attributes["taxAmount"].InnerText.CompareTo(string.Empty) != 0)
                    entryItem.TaxAmount = Convert.ToDecimal(walletJournalRow.Attributes["taxAmount"].InnerText, CultureInfo.InstalledUICulture);
                else
                    entryItem.TaxAmount = 0;
            }


            return entryItem;
        }

        public void CheckVersion(XmlDocument xmlDocument)
        {
            if (WalletJournal.VersionCheck)
            {
                string version = xmlDocument.SelectSingleNode("//eveapi").Attributes["version"].InnerText;
                if (version.CompareTo(WalletJournal.API_VERSION) != 0)
                {
                    throw new ApiVersionException(version, WalletJournal.API_VERSION);
                }
            }
        }
    }
}
