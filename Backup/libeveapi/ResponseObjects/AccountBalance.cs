namespace libeveapi
{
    /// <summary>
    /// Represents a character or corporation AccountBalance response from the eve api
    /// http://wiki.eve-dev.net/APIv2_Char_AccountBalance_XML
    /// http://wiki.eve-dev.net/APIv2_Corp_AccountBalance_XML
    /// </summary>
    public class AccountBalance : ApiResponse
    {
        /// <summary>
        /// API Version Compatibility
        /// </summary>
        public const string API_VERSION = "2";
        private AccountBalanceItem[] accountBalanceItems = new AccountBalanceItem[0];

        /// <summary>
        /// The accounts associated with this character or corporation
        /// </summary>
        public AccountBalanceItem[] AccountBalanceItems
        {
            get { return accountBalanceItems; }
            set { accountBalanceItems = value; }
        }

        /// <summary>
        /// An account associated with a character or corporation
        /// </summary>
        public class AccountBalanceItem
        {
            private int accountId;
            private int accountKey;
            private double balance;

            /// <summary>
            /// ID number of the account
            /// </summary>
            public int AccountId
            {
                get { return accountId; }
                set { accountId = value; }
            }
            
            /// <summary>
            /// Account Identifier
            /// For character: always 1000
            /// For corporation: the corp wallet division
            /// </summary>
            public int AccountKey
            {
                get { return accountKey; }
                set { accountKey = value; }
            }
            

            /// <summary>
            /// Amount of isk in the account
            /// </summary>
            public double Balance
            {
                get { return balance; }
                set { balance = value; }
            }
        }
    }

    /// <summary>
    /// The api allows retrieving the account balances for a character or a corporation
    /// </summary>
    public enum AccountBalanceType
    {
        /// <summary>
        /// A character account
        /// </summary>
        Character,
        /// <summary>
        /// A corporation account
        /// </summary>
        Corporation
    }
}
