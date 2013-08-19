namespace libeveapi
{
    /// <summary>
    /// Represents the number of jumps per system from the eve api
    /// http://wiki.eve-dev.net/APIv2_Map_Jumps_XML
    /// </summary>
    public class MapJumps : ApiResponse
    {
        /// <summary>
        /// API Version Compatibility
        /// </summary>
        public const string API_VERSION = "2";
        private MapSystemItem[] mapSystemJumps = new MapSystemItem[0];

        /// <summary>
        /// 
        /// </summary>
        public MapSystemItem[] MapSystemJumps
        {
            get { return mapSystemJumps; }
            set { mapSystemJumps = value; }
        }

        /// <summary>
        /// The information for a system with one jump or more
        /// </summary>
        public class MapSystemItem
        {
            private int solarSystemId;
            private int shipJumps;

            /// <summary>
            /// The Id of the solarsystem
            /// </summary>
            public int SolarSystemId
            {
                get { return solarSystemId; }
                set { solarSystemId = value; }
            }

            /// <summary>
            /// The number of jumps
            /// </summary>
            public int ShipJumps
            {
                get { return shipJumps; }
                set { shipJumps = value; }
            }
        }
    }
}
