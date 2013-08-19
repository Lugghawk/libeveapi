namespace libeveapi
{
    /// <summary>
    /// Represents the number of kills per system from the eve api
    /// http://wiki.eve-dev.net/APIv2_Map_Kills_XML
    /// </summary>
    public class MapKills : ApiResponse
    {
        public const string API_VERSION = "2";
        private MapKillsItem[] mapSystemKills = new MapKillsItem[0];

        /// <summary>
        /// 
        /// </summary>
        public MapKillsItem[] MapSystemKills
        {
            get { return mapSystemKills; }
            set { mapSystemKills = value; }
        }

        /// <summary>
        /// The information for a system with one jump or more
        /// </summary>
        public class MapKillsItem
        {
            private int solarSystemId;
            private int shipKills;
            private int factionKills;
            private int podKills;

            /// <summary>
            /// The Id of the solarsystem
            /// </summary>
            public int SolarSystemId
            {
                get { return solarSystemId; }
                set { solarSystemId = value; }
            }

            /// <summary>
            /// The number of kills
            /// </summary>
            public int ShipKills
            {
                get { return shipKills; }
                set { shipKills = value; }
            }

            /// <summary>
            /// The number of kills
            /// </summary>
            public int FactionKills
            {
                get { return factionKills; }
                set { factionKills = value; }
            }

            /// <summary>
            /// The number of kills
            /// </summary>
            public int PodKills
            {
                get { return podKills; }
                set { podKills = value; }
            }
        }
    }

    
}
