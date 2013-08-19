using System;
using System.Collections.Generic;
using System.Text;

namespace libeveapi
{
    public class WalletJournal :  JournalEntries
    {

        /// <summary>
        /// API Version Compatibility
        /// </summary>
        public const string API_VERSION = "2";
        private WalletJournalItem[] walletJournalItems = new WalletJournalItem[0];

        public WalletJournalItem[] WalletJournalItems { get { return walletJournalItems; } set { walletJournalItems = value; } }
        /// <summary>
        /// Deprecated Entry List.  Exists for legacy compatibility.
        /// Use WalletJournalItems
        /// </summary>
        public new JournalEntryItem[] JournalEntryItems { get { return base.JournalEntryItems; } set { base.JournalEntryItems = value; } }

        public class WalletJournalItem : JournalEntryItem
        {
            public static string ColumnsChar = "date,refID,refTypeID,ownerName1,ownerID1,ownerName2,ownerID2,argName1,argID1,amount,balance,reason, taxRecieverID, taxAmount";
            public static string KeyChar = "refID";
            public static string ColumnsCorp = "date,refID,refTypeID,ownerName1,ownerID1,ownerName2,ownerID2,argName1,argID1,amount,balance,reason";
            public static string KeyCorp = "refID";
            private int taxReceiverID;
            private decimal taxAmount;

            public int TaxRecieverID { get { return taxReceiverID; } set { taxReceiverID = value; } }
            public decimal TaxAmount { get { return taxAmount; } set { taxAmount = value; } }
        }
    }

    /// <summary>
    /// Represents the type of Wallet Journal entry, corporation or character
    /// </summary>
    public enum WalletJournalType
    {
        /// <summary>
        /// Corporation Wallet Journal entry
        /// </summary>
        Corporation,
        /// <summary>
        /// Character Wallet Journal entry
        /// </summary>
        Character
    }
}
