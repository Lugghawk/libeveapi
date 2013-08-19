namespace libeveapi
{
    /// <summary>
    /// Returns a list of solarsystems and what faction or alliance controls them.
    /// http://wiki.eve-dev.net/APIv2_Map_Kills_XML
    /// </summary>
    public class MapSovereignty : ApiResponse
    {
        /// <summary>
        /// API Version Compatibility
        /// </summary>
        public const string API_VERSION = "2";
        private MapSovereigntyItem[] mapSystemSovereigntyItems = new MapSovereigntyItem[0];

        /// <summary>
        /// 
        /// </summary>
        public MapSovereigntyItem[] MapSystemSovereigntyItems
            {
                get { return mapSystemSovereigntyItems; }
                set { mapSystemSovereigntyItems = value; }
            }

        /// <summary>
        /// The information for a system with one jump or more
        /// </summary>
        public class MapSovereigntyItem
        {
            private int solarSystemId;
            private int allianceId;
            private int factionId;
            private string solarSystemName;
            private int corporationID;

            /// <summary>
            /// The unique identification number of a solar system
            /// Solar System details located in the table mapSolarSystems of the CCP Database Dump
            /// </summary>
            public int SolarSystemId
            {
                get { return solarSystemId; }
                set { solarSystemId = value; }
            }

            /// <summary>
            /// The Id of the alliance that has sovereignty of this solar system, or 0 if nobody has sovereignty.
            /// The Alliance List provides a list of the alliances. 
            /// </summary>
            public int AllianceId
            {
                get { return allianceId; }
                set { allianceId = value; }
            }

            /// <summary>
            /// The NPC faction that controls this system
            /// The CCP Database Dump has a table with the faction names. 
            /// </summary>
            public int FactionId
            {
                get { return factionId; }
                set { factionId = value; }
            }

            /// <summary>
            /// Name of the solar system
            /// </summary>
            public string SolarSystemName
            {
                get { return solarSystemName; }
                set { solarSystemName = value; }
            }

            /// <summary>
            /// The ID of the corporation that owns the Territorial Claim Unit (TCU) if there is one in the system. 
            /// </summary>
            public int CorporationID
            {
                get { return corporationID; }
                set { corporationID = value; }
            }

        }
    }
}
