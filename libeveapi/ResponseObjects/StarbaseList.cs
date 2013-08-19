using System;

namespace libeveapi
{
    /// <summary>
    /// Represents a list of all the starbases owned by the specified corporation
    /// </summary>
    public class StarbaseList : ApiResponse
    {
        /// <summary>
        /// API Version Compatibility
        /// </summary>
        public const string API_VERSION = "2";
        private StarbaseListItem[] starbaseListItems = new StarbaseListItem[0];

        /// <summary>
        /// List of all the starbases owned by this corporation
        /// </summary>
        public StarbaseListItem[] StarbaseListItems
        {
            get { return starbaseListItems; }
            set { starbaseListItems = value; }
        }

        /// <summary>
        /// Represents a single starbase in the starbase list
        /// </summary>
        public class StarbaseListItem
        {
            private int itemId;
            private int typeId;
            private int locationId;
            private int moonId;
            private StarbaseState state;
            private DateTime stateTimestamp;
            private DateTime onlineTimestamp;
            private DateTime stateTimestampLocal;
            private DateTime onlineTimestampLocal;

            /// <summary>
            /// Unique identifier for this starbase
            /// </summary>
            public int ItemId
            {
                get { return itemId; }
                set { itemId = value; }
            }

            /// <summary>
            /// Control tower type id
            /// </summary>
            public int TypeId
            {
                get { return typeId; }
                set { typeId = value; }
            }

            /// <summary>
            /// The id of the system where the starbase is located
            /// </summary>
            public int LocationId
            {
                get { return locationId; }
                set { locationId = value; }
            }

            /// <summary>
            /// The id of the moon where the starbase is located
            /// </summary>
            public int MoonId
            {
                get { return moonId; }
                set { moonId = value; }
            }

            /// <summary>
            /// See <see cref="StarbaseState"/> for full descriptions of each starbase state
            /// </summary>
            public StarbaseState State
            {
                get { return state; }
                set { state = value; }
            }

            /// <summary>
            /// See <see cref="StarbaseState"/> for the potential meanings of StateTimestamp
            /// </summary>
            public DateTime StateTimestamp
            {
                get { return stateTimestamp; }
                set { stateTimestamp = value; }
            }

            /// <summary>
            /// See <see cref="StarbaseState"/> for the potential meanings of OnlineTimestamp
            /// </summary>
            public DateTime OnlineTimestamp
            {
                get { return onlineTimestamp; }
                set { onlineTimestamp = value; }
            }

            /// <summary>
            /// StateTimestamp in local time
            /// </summary>
            public DateTime StateTimestampLocal
            {
                get { return stateTimestampLocal; }
                set { stateTimestampLocal = value; }
            }

            /// <summary>
            /// OnlineTimestamp in local time
            /// </summary>
            public DateTime OnlineTimestampLocal
            {
                get { return onlineTimestampLocal; }
                set { onlineTimestampLocal = value; }
            }
        }

        /// <summary>
        /// This represents the current state of the starbase, of which there are
        /// four different valid states.
        /// </summary>
        public enum StarbaseState
        {
            /// <summary>
            /// Starbase is anchored or offline
            /// if offline then it went offline at StateTimestamp
            /// </summary>
            AnchoredOrOffline = 1,

            /// <summary>
            /// Starbase is in the process of coming online
            /// Will be online at time OnlineTimestamp
            /// </summary>
            Onlining = 2,

            /// <summary>
            /// Starbase in in reinforced mode
            /// Until time StateTimestamp
            /// </summary>
            Reinforced = 3,

            /// <summary>
            /// Starbase is online
            /// Continuously since time OnlineTimestamp
            /// </summary>
            Online = 4
        }
    }
}
