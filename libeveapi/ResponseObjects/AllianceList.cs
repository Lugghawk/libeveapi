using System;

namespace libeveapi
{
    /// <summary>
    /// List of Alliances
    /// </summary>
    public class AllianceList : ApiResponse
    {
        /// <summary>
        /// API Version Compatibility
        /// </summary>
        public const string API_VERSION = "2";
        private AllianceListItem[] allianceListItems = new AllianceListItem[0];

        /// <summary>
        /// List of alliances
        /// </summary>
        public AllianceListItem[] AllianceListItems
        {
            get { return allianceListItems; }
            set { allianceListItems = value; }
        }

        /// <summary>
        /// Represents an alliance
        /// </summary>
        public class AllianceListItem
        {
            private string name;
            private string shortName;
            private int allianceId;
            private int executorCorpId;
            private int memberCount;
            private DateTime startDate;
            private DateTime startDateLocal;
            private CorporationListItem[] corporationListItems = new CorporationListItem[0];

            /// <summary>
            /// full name of the alliance
            /// </summary>
            public string Name
            {
                get { return name; }
                set { name = value; }
            }

            /// <summary>
            /// ticker name of the alliance
            /// </summary>
            public string ShortName
            {
                get { return shortName; }
                set { shortName = value; }
            }

            /// <summary>
            /// unique identifier for this alliance
            /// </summary>
            public int AllianceId
            {
                get { return allianceId; }
                set { allianceId = value; }
            }

            /// <summary>
            /// unique identifier of executor corporation
            /// </summary>
            public int ExecutorCorpId
            {
                get { return executorCorpId; }
                set { executorCorpId = value; }
            }

            /// <summary>
            /// Current number of pilots in the alliance
            /// </summary>
            public int MemberCount
            {
                get { return memberCount; }
                set { memberCount = value; }
            }

            /// <summary>
            /// Date the alliance was created in CCP time
            /// </summary>
            public DateTime StartDate
            {
                get { return startDate; }
                set { startDate = value; }
            }

            /// <summary>
            /// Date the alliance was created in local time
            /// </summary>
            public DateTime StartDateLocal
            {
                get { return startDateLocal; }
                set { startDateLocal = value; }
            }

            /// <summary>
            /// List of member corporations
            /// </summary>
            public CorporationListItem[] CorporationListItems
            {
                get { return corporationListItems; }
                set { corporationListItems = value; }
            }
        }

        /// <summary>
        /// Represents a member corporation
        /// </summary>
        public class CorporationListItem
        {
            private int corporationId;
            private DateTime startDate;
            private DateTime startDateLocal;

            /// <summary>
            /// unique identifier for the corporation
            /// </summary>
            public int CorporationId
            {
                get { return corporationId; }
                set { corporationId = value; }
            }

            /// <summary>
            /// date the corporation joined the alliance in CCP time
            /// </summary>
            public DateTime StartDate
            {
                get { return startDate; }
                set { startDate = value; }
            }

            /// <summary>
            /// date the corporation joined the alliance in local time
            /// </summary>
            public DateTime StartDateLocal
            {
                get { return startDateLocal; }
                set { startDateLocal = value; }
            }
        }
    }
}
