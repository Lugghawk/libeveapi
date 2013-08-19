namespace libeveapi
{
    /// <summary>
    /// A list of solar systems and the NPC faction currently occupying them.
    /// http://wiki.eve-id.net/APIv2_Factional_Warfare_Occupancy_XML
    /// </summary>
    public class MapFacWarSystems : ApiResponse
    {
        /// <summary>
        /// API Version Compatibility
        /// </summary>
        public const string API_VERSION = "2";
        private FactionWarSystem[] factionWarSystems = new FactionWarSystem[0];

        /// <summary>
        /// List of Faction War Systems
        /// </summary>
        public FactionWarSystem[] FactionWarSystems
        {
            get { return factionWarSystems; }
            set { factionWarSystems = value; }
        }

        /// <summary>
        /// Faction occupation information about a single solar system
        /// </summary>
        public class FactionWarSystem
        {
            private int solarSystemId;
            private string solarSystemName;
            private int occupyingFactionId;
            private string occupyingFactionName;
            private bool contested;

            /// <summary>
            /// Unique ID of a solar system
            /// </summary>
            public int SolarSystemId
            {
                get { return solarSystemId; }
                set { solarSystemId = value; }
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
            /// ID of the NPC faction occupying this system or 0 if the default faction occupies the system
            /// </summary>
            public int OccupyingFactionId
            {
                get { return occupyingFactionId; }
                set { occupyingFactionId = value; }
            }

            /// <summary>
            /// Name of the occupying faction
            /// </summary>
            public string OccupyingFactionName
            {
                get { return occupyingFactionName; }
                set { occupyingFactionName = value; }
            }

            /// <summary>
            /// Whether the system is being fought over
            /// </summary>
            public bool Contested
            {
                get { return contested; }
                set { contested = value; }
            }
        }
    }
}
