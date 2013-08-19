using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace libeveapi.ResponseObjects.Parsers
{
    ///<summary>
    /// A parser which converts a given <see cref="XmlDocument"/> to a <see cref="JournalEntries"/>.
    ///</summary>
    internal class JournalEntriesResponseParser : IApiResponseParser<JournalEntries>
    {
        public JournalEntries Parse(XmlDocument xmlDocument)
        {
            this.CheckVersion(xmlDocument);
            JournalEntries JournalEntryList = new JournalEntries();
            JournalEntryList.ParseCommonElements(xmlDocument);

            List<JournalEntries.JournalEntryItem> journalEntry = new List<JournalEntries.JournalEntryItem>();
            foreach (XmlNode node in xmlDocument.SelectNodes("//rowset[@name='entries']/row"))
            {
                journalEntry.Add(ParseTransactionRow(node));
            }
            JournalEntryList.JournalEntryItems = journalEntry.ToArray();

            return JournalEntryList;
        }

        /// <summary>
        /// Create an JournalEntryItem by parsing a single row
        /// </summary>
        /// <param name="journalTransactionRow"></param>
        /// <returns></returns>
        private static JournalEntries.JournalEntryItem ParseTransactionRow(XmlNode journalTransactionRow)
        {
            JournalEntries.JournalEntryItem journalEntryItem = new JournalEntries.JournalEntryItem();

            journalEntryItem.Date = TimeUtilities.ConvertCCPTimeStringToDateTimeUTC(journalTransactionRow.Attributes["date"].InnerText);
            journalEntryItem.DateLocal = TimeUtilities.ConvertCCPToLocalTime(journalEntryItem.Date);
            journalEntryItem.RefId = Convert.ToInt32(journalTransactionRow.Attributes["refID"].InnerText, CultureInfo.InvariantCulture);
            journalEntryItem.RefTypeId = Convert.ToInt32(journalTransactionRow.Attributes["refTypeID"].InnerText, CultureInfo.InvariantCulture);
            journalEntryItem.OwnerName1 = journalTransactionRow.Attributes["ownerName1"].InnerText;
            journalEntryItem.OwnerId1 = Convert.ToInt32(journalTransactionRow.Attributes["ownerID1"].InnerText, CultureInfo.InvariantCulture);
            journalEntryItem.OwnerName2 = journalTransactionRow.Attributes["ownerName2"].InnerText;
            journalEntryItem.OwnerId2 = Convert.ToInt32(journalTransactionRow.Attributes["ownerID2"].InnerText, CultureInfo.InvariantCulture);
            journalEntryItem.ArgName1 = journalTransactionRow.Attributes["argName1"].InnerText;
            journalEntryItem.ArgId1 = Convert.ToInt32(journalTransactionRow.Attributes["argID1"].InnerText, CultureInfo.InvariantCulture);
            journalEntryItem.Amount = Convert.ToDouble(journalTransactionRow.Attributes["amount"].InnerText, CultureInfo.InvariantCulture);
            journalEntryItem.Balance = Convert.ToDouble(journalTransactionRow.Attributes["balance"].InnerText, CultureInfo.InvariantCulture);
            journalEntryItem.Reason = journalTransactionRow.Attributes["reason"].InnerText;

            return journalEntryItem;
        }

        public void CheckVersion(XmlDocument xmlDocument)
        {
            if (JournalEntries.VersionCheck)
            {
                string version = xmlDocument.SelectSingleNode("//eveapi").Attributes["version"].InnerText;
                if (version.CompareTo(JournalEntries.API_VERSION) != 0)
                {
                    throw new ApiVersionException(version, JournalEntries.API_VERSION);
                }
            }
        }
    }
}
