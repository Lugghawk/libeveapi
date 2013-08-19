namespace libeveapi
{
    /// <summary>
    /// Detailed information about a corporation
    /// </summary>
    public class CorporationSheet : ApiResponse
    {
        /// <summary>
        /// API Version Compatibility
        /// </summary>
        public const string API_VERSION = "2";
        private int corporationId;
        private string corporationName;
        private string ticker;
        private int ceoId;
        private string ceoName;
        private int stationId;
        private string stationName;
        private string description;
        private string url;
        private int allianceId;
        private string allianceName;
        private double taxRate;
        private int memberCount;
        private int memberLimit;
        private long shares;
        private Division[] divisions = new Division[0];
        private WalletDivision[] walletDivisions = new WalletDivision[0];
        private CorpLogo logo = new CorpLogo();

        /// <summary>
        /// Unique identifier for the corporation
        /// </summary>
        public int CorporationId
        {
            get { return corporationId; }
            set { corporationId = value; }
        }

        /// <summary>
        /// Name of the corporation
        /// </summary>
        public string CorporationName
        {
            get { return corporationName; }
            set { corporationName = value; }
        }

        /// <summary>
        /// Stock ticker symbol for the corporation
        /// </summary>
        public string Ticker
        {
            get { return ticker; }
            set { ticker = value; }
        }

        /// <summary>
        /// The characterId of the character that is CEO of the corporation
        /// </summary>
        public int CeoId
        {
            get { return ceoId; }
            set { ceoId = value; }
        }

        /// <summary>
        /// The name of the character that is CEO of the corporation
        /// </summary>
        public string CeoName
        {
            get { return ceoName; }
            set { ceoName = value; }
        }

        /// <summary>
        /// The unique id of the station where the corporation is based
        /// </summary>
        public int StationId
        {
            get { return stationId; }
            set { stationId = value; }
        }

        /// <summary>
        /// The name of the station where the corporation is based
        /// </summary>
        public string StationName
        {
            get { return stationName; }
            set { stationName = value; }
        }

        /// <summary>
        /// A description of the corporation as set by the corporation
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// A url to the corporation's web site
        /// </summary>
        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        /// <summary>
        ///  	 The unique identifier of this corporation's alliance
        /// </summary>
        public int AllianceId
        {
            get { return allianceId; }
            set { allianceId = value; }
        }

        /// <summary>
        /// The name of this corporation's alliance
        /// </summary>
        public string AllianceName
        {
            get { return allianceName; }
            set { allianceName = value; }
        }

        /// <summary>
        /// The tax rate of the corporation
        /// </summary>
        public double TaxRate
        {
            get { return taxRate; }
            set { taxRate = value; }
        }

        /// <summary>
        /// The current number of pilots in the corporation
        /// </summary>
        public int MemberCount
        {
            get { return memberCount; }
            set { memberCount = value; }
        }

        /// <summary>
        /// The current maximum number of pilots the corporation can contain
        /// </summary>
        public int MemberLimit
        {
            get { return memberLimit; }
            set { memberLimit = value; }
        }

        /// <summary>
        /// The current number of outstanding shares of the corporation
        /// </summary>
        public long Shares
        {
            get { return shares; }
            set { shares = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Division[] Divisions
        {
            get { return divisions; }
            set { divisions = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public WalletDivision[] WalletDivisions
        {
            get { return walletDivisions; }
            set { walletDivisions = value; }
        }

        /// <summary>
        /// An object describing the corporation logo
        /// </summary>
        public CorpLogo Logo
        {
            get { return logo; }
            set { logo = value; }
        }

        /// <summary>
        /// Information about a corporate division
        /// </summary>
        public class Division
        {
            private int accountKey;
            private string description;

            /// <summary>
            /// The account key used to access the corporate division
            /// </summary>
            public int AccountKey
            {
                get { return accountKey; }
                set { accountKey = value; }
            }
            /// <summary>
            /// The name of hte division
            /// </summary>
            public string Description
            {
                get { return description; }
                set { description = value; }
            }
        }

        /// <summary>
        /// The different corporation wallet divisions
        /// </summary>
        public class WalletDivision
        {
            private int accountKey;
            private string description;

            /// <summary>
            /// The account key used to access this division
            /// </summary>
            public int AccountKey
            {
                get { return accountKey; }
                set { accountKey = value; }
            }
            /// <summary>
            /// The name given to the wallet
            /// </summary>
            public string Description
            {
                get { return description; }
                set { description = value; }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public class CorpLogo
        {
            private int graphicId;
            private int shape1;
            private int shape2;
            private int shape3;
            private int color1;
            private int color2;
            private int color3;

            /// <summary>
            /// 
            /// </summary>
            public int GraphicId
            {
                get { return graphicId; }
                set { graphicId = value; }
            }
            /// <summary>
            /// 
            /// </summary>
            public int Shape1
            {
                get { return shape1; }
                set { shape1 = value; }
            }
            /// <summary>
            /// 
            /// </summary>
            public int Shape2
            {
                get { return shape2; }
                set { shape2 = value; }
            }
            /// <summary>
            /// 
            /// </summary>
            public int Shape3
            {
                get { return shape3; }
                set { shape3 = value; }
            }
            /// <summary>
            /// 
            /// </summary>
            public int Color1
            {
                get { return color1; }
                set { color1 = value; }
            }
            /// <summary>
            /// 
            /// </summary>
            public int Color2
            {
                get { return color2; }
                set { color2 = value; }
            }
            /// <summary>
            /// 
            /// </summary>
            public int Color3
            {
                get { return color3; }
                set { color3 = value; }
            }
        }
    }
}
