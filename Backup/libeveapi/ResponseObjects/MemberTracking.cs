using System;

namespace libeveapi
{
    /// <summary>
    /// Contains information on every member in the corporation. Information retrieved
    /// varies on your roles without within the corporation. Not valid for NPC corps.
    /// </summary>
    public class MemberTracking : ApiResponse
    {
        /// <summary>
        /// API Version Compatibility
        /// </summary>
        public const string API_VERSION = "2";
        private Member[] members = new Member[0];

        /// <summary>
        /// 
        /// </summary>
        public Member[] Members
        {
            get { return members; }
            set { members = value; }
        }

        /// <summary>
        /// Information about an individual member of the corporation.
        /// </summary>
        public class Member
        {
            private int characterId;
            private string name;
            private DateTime startDateTime;
            private DateTime startDateTimeLocal;
            private int baseId;
            private string _base;
            private string title;
            private DateTime logonDateTime;
            private DateTime logonDateTimeLocal;
            private DateTime logoffDateTime;
            private DateTime logoffDateTimeLocal;
            private int locationId;
            private string location;
            private int shipTypeId;
            private string shipType;
            private string rolesMask;
            private string grantableRoles;
            private Roles roles;

            /// <summary>
            /// Unique identifier of the pilot.
            /// </summary>
            public int CharacterId
            {
                get { return characterId; }
                set { characterId = value; }
            }

            /// <summary>
            /// Name of the pilot
            /// </summary>
            public string Name
            {
                get { return name; }
                set { name = value; }
            }

            /// <summary>
            /// Pilot's start date with the corporation
            /// </summary>
            public DateTime StartDateTime
            {
                get { return startDateTime; }
                set { startDateTime = value; }
            }

            /// <summary>
            /// Pilot's start date with the corporation in local time
            /// </summary>
            public DateTime StartDateTimeLocal
            {
                get { return startDateTimeLocal; }
                set { startDateTimeLocal = value; }
            }

            /// <summary>
            /// The unique identifier of the station the pilot's 
            /// base station.
            /// </summary>
            public int BaseId
            {
                get { return baseId; }
                set { baseId = value; }
            }

            /// <summary>
            /// The human readable description of the pilot's base 
            /// station.
            /// </summary>
            public string Base
            {
                get { return _base; }
                set { _base = value; }
            }

            /// <summary>
            /// Pilot's assigned title in the corporation
            /// </summary>
            public string Title
            {
                get { return title; }
                set { title = value; }
            }

            /// <summary>
            /// Timestamp of pilot's last logon
            /// </summary>
            public DateTime LogonDateTime
            {
                get { return logonDateTime; }
                set { logonDateTime = value; }
            }

            /// <summary>
            /// Timestamp of pilot's last logon in local time
            /// </summary>
            public DateTime LogonDateTimeLocal
            {
                get { return logonDateTimeLocal; }
                set { logonDateTimeLocal = value; }
            }

            /// <summary>
            /// Timestamp of pilot's last logoff
            /// </summary>
            public DateTime LogoffDateTime
            {
                get { return logoffDateTime; }
                set { logoffDateTime = value; }
            }

            /// <summary>
            /// Timestamp of pilot's last logoff in local time
            /// </summary>
            public DateTime LogoffDateTimeLocal
            {
                get { return logoffDateTimeLocal; }
                set { logoffDateTimeLocal = value; }
            }

            /// <summary>
            /// The unique id of the system where the pilot is currently located
            /// </summary>
            public int LocationId
            {
                get { return locationId; }
                set { locationId = value; }
            }

            /// <summary>
            /// A human readable description of the current location of the pilot
            /// </summary>
            public string Location
            {
                get { return location; }
                set { location = value; }
            }

            /// <summary>
            /// The unique identifier of the ship type the pilot is 
            /// currently flying
            /// </summary>
            public int ShipTypeId
            {
                get { return shipTypeId; }
                set { shipTypeId = value; }
            }

            /// <summary>
            /// The human readable description of the type of ship the 
            /// pilot is currently flying
            /// </summary>
            public string ShipType
            {
                get { return shipType; }
                set { shipType = value; }
            }

            /// <summary>
            /// A mask describing the pilot's current roles in the corporation
            /// </summary>
            public string RolesMask
            {
                get { return rolesMask; }
                set { rolesMask = value; }
            }

            /// <summary>
            /// Depricated
            /// </summary>
            public string GrantableRoles
            {
                get { return grantableRoles; }
                set { grantableRoles = value; }
            }

            /// <summary>
            /// This pilot's roles in the corporation
            /// </summary>
            public Roles Roles
            {
                get
                {
                    if (this.roles == null)
                    {
                        this.roles = new Roles(this.RolesMask);
                    }

                    return this.roles;
                }
            }
        }
    }

   
}
