namespace libeveapi
{
    /// <summary>
    /// Shows the settings and fuel status of a starbase
    /// </summary>
    public class StarbaseDetail : ApiResponse
    {
        /// <summary>
        /// API Version Compatibility
        /// </summary>
        public const string API_VERSION = "2";
        private string usageFlags;
        private string deployFlags;
        private bool allowCorporationMembers;
        private bool allowAllianceMembers;
        private bool claimSovereignty;
        private bool onStandingDropEnabled;
        private string onStandingDropStanding;
        private bool onStatusDropEnabled;
        private string onStatusDropStanding;
        private bool onAgressionEnabled;
        private bool onCorporationWarEnabled;
        private FuelListItem[] fuelList = new FuelListItem[0];

        /// <summary>
        /// 
        /// </summary>
        public string UsageFlags
        {
            get { return usageFlags; }
            set { usageFlags = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DeployFlags
        {
            get { return deployFlags; }
            set { deployFlags = value; }
        }
        /// <summary>
        /// Allow corporation members inside the force field
        /// </summary>
        public bool AllowCorporationMembers
        {
            get { return allowCorporationMembers; }
            set { allowCorporationMembers = value; }
        }
        /// <summary>
        /// Allow alliance members inside the force field
        /// </summary>
        public bool AllowAllianceMembers
        {
            get { return allowAllianceMembers; }
            set { allowAllianceMembers = value; }
        }
        /// <summary>
        /// Is the starbase claiming sovernty (only in 0.0 space)
        /// </summary>
        public bool ClaimSovereignty
        {
            get { return claimSovereignty; }
            set { claimSovereignty = value; }
        }

        /// <summary>
        /// Shoot on standing drop
        /// </summary>
        public bool OnStandingDropEnabled
        {
            get { return onStandingDropEnabled; }
            set { onStandingDropEnabled = value; }
        }
        /// <summary>
        /// What target standing makes them a valid target
        /// </summary>
        public string OnStandingDropStanding
        {
            get { return onStandingDropStanding; }
            set { onStandingDropStanding = value; }
        }
        /// <summary>
        /// Shoot on status drop
        /// </summary>
        public bool OnStatusDropEnabled
        {
            get { return onStatusDropEnabled; }
            set { onStatusDropEnabled = value; }
        }
        /// <summary>
        /// What target security make them a valid target
        /// </summary>
        public string OnStatusDropStanding
        {
            get { return onStatusDropStanding; }
            set { onStatusDropStanding = value; }
        }
        /// <summary>
        /// Shoot on target agression
        /// </summary>
        public bool OnAgressionEnabled
        {
            get { return onAgressionEnabled; }
            set { onAgressionEnabled = value; }
        }
        /// <summary>
        /// Shoot if at war with target
        /// </summary>
        public bool OnCorporationWarEnabled
        {
            get { return onCorporationWarEnabled; }
            set { onCorporationWarEnabled = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public FuelListItem[] FuelList
        {
            get { return fuelList; }
            set { fuelList = value; }
        }

        /// <summary>
        /// Represents a type of fuel present in the starbase
        /// </summary>
        public class FuelListItem
        {
            private int typeId;
            private long quantity;

            /// <summary>
            /// Type Id of the fuel
            /// </summary>
            public int TypeId
            {
                get { return typeId; }
                set { typeId = value; }
            }

            /// <summary>
            /// Number of units of the fuel remaining
            /// </summary>
            public long Quantity
            {
                get { return quantity; }
                set { quantity = value; }
            }
        }
    }
}
