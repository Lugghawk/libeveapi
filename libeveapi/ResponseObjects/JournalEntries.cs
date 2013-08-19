using System;

namespace libeveapi
{
    /// <summary>
    /// Represents a character or corporation journal entry from the eve api
    /// http://wiki.eve-dev.net/APIv2_Char_JournalEntries_XML
    /// </summary>
    public class JournalEntries : ApiResponse
    {
        /// <summary>
        /// API Version Compatibility
        /// </summary>
        public const string API_VERSION = "1";
        private JournalEntryItem[] journalEntryItems = new JournalEntryItem[0];

        /// <summary>
        /// 
        /// </summary>
        public JournalEntryItem[] JournalEntryItems
        {
            get { return journalEntryItems; }
            set { journalEntryItems = value; }
        }

        /// <summary>
        /// Contains the data for one journal entry item
        /// </summary>
        public class JournalEntryItem
        {
            private DateTime date;
            private DateTime dateLocal;
            private int refId;
            private int refTypeId;
            private string ownerName1;
            private int ownerId1;
            private string ownerName2;
            private int ownerId2;
            private string argName1;
            private int argId1;
            private double amount;
            private double balance;
            private string reason;

            /// <summary>
            /// The date / time of the entry
            /// </summary>
            public DateTime Date
            {
                get { return date; }
                set { date = value; }
            }

            /// <summary>
            /// The date / time of the entry in local time
            /// </summary>
            public DateTime DateLocal
            {
                get { return dateLocal; }
                set { dateLocal = value; }
            }

            /// <summary>
            /// A unique referenceId for each transaction
            /// </summary>
            public int RefId
            {
                get { return refId; }
                set { refId = value; }
            }

            /// <summary>
            /// The typeId of the item that is referenced in the entry
            /// </summary>
            public int RefTypeId
            {
                get { return refTypeId; }
                set { refTypeId = value; }
            }

            /// <summary>
            /// Name of the person/corp giving money
            /// </summary>
            public string OwnerName1
            {
                get { return ownerName1; }
                set { ownerName1 = value; }
            }

            /// <summary>
            /// TypeId/CharId of the person/corp giving money
            /// </summary>
            public int OwnerId1
            {
                get { return ownerId1; }
                set { ownerId1 = value; }
            }

            /// <summary>
            /// Name of the person/corp recieving money
            /// </summary>
            public string OwnerName2
            {
                get { return ownerName2; }
                set { ownerName2 = value; }
            }

            /// <summary>
            /// TypeId/CharId of the person/corp recieving money
            /// </summary>
            public int OwnerId2
            {
                get { return ownerId2; }
                set { ownerId2 = value; }
            }

            /// <summary>
            /// This is either the system where the transaction took place
            /// For example when recieving bounties, or "EVE System" if used
            /// for other items like buying and selling
            /// </summary>
            public string ArgName1
            {
                get { return argName1; }
                set { argName1 = value; }
            }

            /// <summary>
            /// This is an argument Id that goes with the argument
            /// Use the data from: http://wiki.eve-dev.net/APIv2_Eve_RefTypes_XML
            /// to determine what this is used for
            /// </summary>
            public int ArgId1
            {
                get { return argId1; }
                set { argId1 = value; }
            }

            /// <summary>
            /// The amount of the transaction
            /// </summary>
            public double Amount
            {
                get { return amount; }
                set { amount = value; }
            }

            /// <summary>
            /// The balance left in the account after the transaction
            /// </summary>
            public double Balance
            {
                get { return balance; }
                set { balance = value; }
            }

            /// <summary>
            /// This is the note attached to the transaction if any
            /// If exists for bounties this is the list of people killed and how many
            /// Ex: TypeId:Number;TypeId:Number
            /// </summary>
            public string Reason
            {
                get { return reason; }
                set { reason = value; }
            }
        }
    }

    /// <summary>
    /// Represents what type of journal entry, corporation or character
    /// </summary>
    public enum JournalEntryType
    {
        /// <summary>
        /// Corporation journal entry
        /// </summary>
        Corporation,
        /// <summary>
        /// Character journal entry
        /// </summary>
        Character
    }
}
