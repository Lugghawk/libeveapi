namespace libeveapi
{
    /// <summary>
    /// Holds the full list of Conquerable stations and outposts
    /// </summary>
    public class ConquerableStationList : ApiResponse
    {
        /// <summary>
        /// API Version Compatibility
        /// </summary>
        public const string API_VERSION = "2";
        private ConquerableStation[] conquerableStations = new ConquerableStation[0];

        /// <summary>
        /// 
        /// </summary>
        public ConquerableStation[] ConquerableStations
        {
            get { return conquerableStations; }
            set { conquerableStations = value; }
        }

        /// <summary>
        /// Represents one conquerable station / outpost
        /// </summary>
        public class ConquerableStation
        {
            private int stationId;
            private string stationName;
            private int stationTypeId;
            private int solarSystemId;
            private int corporationId;
            private string corporationName;

            /// <summary>
            /// The unique station Id
            /// </summary>
            public int StationId
            {
                get { return stationId; }
                set { stationId = value; }
            }

            /// <summary>
            /// The name of the station (corp gets to name it)
            /// </summary>
            public string StationName
            {
                get { return stationName; }
                set { stationName = value; }
            }

            /// <summary>
            /// The typeId of the station / outpost
            /// </summary>
            public int StationTypeId
            {
                get { return stationTypeId; }
                set { stationTypeId = value; }
            }

            /// <summary>
            /// The solarSystemId that the station / outpost is located in
            /// </summary>
            public int SolarSystemId
            {
                get { return solarSystemId; }
                set { solarSystemId = value; }
            }

            /// <summary>
            /// The id of the corporation that owns teh station / outpost
            /// </summary>
            public int CorporationId
            {
                get { return corporationId; }
                set { corporationId = value; }
            }

            /// <summary>
            /// The name of the corporation that owns the station / outpost
            /// </summary>
            public string CorporationName
            {
                get { return corporationName; }
                set { corporationName = value; }
            }
        }
    }
}
