using System;

namespace libeveapi
{
    /// <summary>
    /// Log of 25 most recent kills for a character or 100 most recent for corporation
    /// </summary>
    public class KillLog : ApiResponse
    {
        /// <summary>
        /// API Version Compatibility
        /// </summary>
        public const string API_VERSION = "1";
        private Kill[] kills = new Kill[0];

        /// <summary>
        /// Array containing all the kills returned from the api call 
        /// </summary>
        public Kill[] Kills
        {
            get { return kills; }
            set { kills = value; }
        }

        /// <summary>
        /// A Kill contains a victim, list of attackers, and items lost
        /// </summary>
        public class Kill
        {
            private int killId;
            private int solarSystemId;
            private DateTime killTime;
            private DateTime killTimeLocal;
            private int moonId;
            private VictimPilot victim = new VictimPilot();
            private Attacker[] attackers = new Attacker[0];
            private Item[] items = new Item[0];

            /// <summary>
            /// Unique kill ID for this kill. This is to be used as the input for 
            /// beforeKillID if you need to scroll back. This is globally unique 
            /// and can be used for uniquely identifying a kill to other killboards.
            /// </summary>
            public int KillId
            {
                get { return killId; }
                set { killId = value; }
            }

            /// <summary>
            /// The ID of the solar system this kill occurred in.
            /// </summary>
            public int SolarSystemId
            {
                get { return solarSystemId; }
                set { solarSystemId = value; }
            }

            /// <summary>
            /// What time the event occurred.
            /// </summary>
            public DateTime KillTime
            {
                get { return killTime; }
                set { killTime = value; }
            }

            /// <summary>
            /// What time the event occurred in local time.
            /// </summary>
            public DateTime KillTimeLocal
            {
                get { return killTimeLocal; }
                set { killTimeLocal = value; }
            }

            /// <summary>
            /// In some situations, this is present to define what moon a 
            /// kill occurred at. Note that this is generally only present 
            /// in situations where the loss is a POS structure. It is not
            /// guaranteed to be populated.
            /// </summary>
            public int MoonId
            {
                get { return moonId; }
                set { moonId = value; }
            }

            /// <summary>
            /// The victim of the attack
            /// </summary>
            public VictimPilot Victim
            {
                get { return victim; }
                set { victim = value; }
            }

            /// <summary>
            /// The attacking pilots
            /// </summary>
            public Attacker[] Attackers
            {
                get { return attackers; }
                set { attackers = value; }
            }

            /// <summary>
            /// The items lost / dropped in the attack
            /// </summary>
            public Item[] Items
            {
                get { return items; }
                set { items = value; }
            }
        }

        /// <summary>
        /// A pilot base class
        /// </summary>
        public class Pilot
        {
            private int characterId;
            private string characterName;
            private int corporationId;
            private string corporationName;
            private int allianceId;
            private string allianceName;
            private int shipTypeId;

            /// <summary>
            /// Character's ID. Fairly self-explanatory. Can be 0 in which 
            /// case this kill was not done by a character and instead 
            /// was done by a corporation. In the victim case, this implies 
            /// it is a structure loss.
            /// </summary>
            public int CharacterId
            {
                get { return characterId; }
                set { characterId = value; }
            }

            /// <summary>
            /// If present, the name of the above characterID.
            /// </summary>
            public string CharacterName
            {
                get { return characterName; }
                set { characterName = value; }
            }

            /// <summary>
            /// The ID of the corporation. This will always be present, 
            /// as there is always a corporation behind the victim, 
            /// whether it is the corporation itself or simply someone 
            /// in that corporation.
            /// </summary>
            public int CorporationId
            {
                get { return corporationId; }
                set { corporationId = value; }
            }

            /// <summary>
            /// The name of the corporation associated with the kill.
            /// </summary>
            public string CorporationName
            {
                get { return corporationName; }
                set { corporationName = value; }
            }

            /// <summary>
            /// If not 0, this is the ID of the alliance the corporation belongs to.
            /// </summary>
            public int AllianceId
            {
                get { return allianceId; }
                set { allianceId = value; }
            }

            /// <summary>
            /// The name of the alliance associated with the kill.
            /// </summary>
            public string AllianceName
            {
                get { return allianceName; }
                set { allianceName = value; }
            }

            /// <summary>
            /// The item lost. This could be a ship as suggested by the name 
            /// but can potentially be anything that generates a kill event.
            /// </summary>
            public int ShipTypeId
            {
                get { return shipTypeId; }
                set { shipTypeId = value; }
            }
        }
        /// <summary>
        /// The victim pilot
        /// </summary>
        public class VictimPilot : Pilot
        {
            private int damageTaken;

            /// <summary>
            /// How much damage the victim took before 
            /// succumbing to fiery defeat and humiliation.
            /// Please note that this damage is calculated after resists. 
            /// It does give you a decent idea of how much 
            /// they were tanking, however.
            /// </summary>
            public int DamageTaken
            {
                get { return damageTaken; }
                set { damageTaken = value; }
            }
        }

        /// <summary>
        /// An Attacking pilot
        /// </summary>
        public class Attacker : Pilot
        {
            private double securityStatus;
            private int damageDone;
            private bool finalBlow;
            private int weaponTypeId;

            /// <summary>
            /// The security status of the aggressor at the time of this kill.
            /// </summary>
            public double SecurityStatus
            {
                get { return securityStatus; }
                set { securityStatus = value; }
            }

            /// <summary>
            /// The amount of damage done to the victim. This is post-resists damage done.
            /// </summary>
            public int DamageDone
            {
                get { return damageDone; }
                set { damageDone = value; }
            }

            /// <summary>
            /// Whether or not this aggressor is attributed with the so-called "final blow."
            /// </summary>
            public bool FinalBlow
            {
                get { return finalBlow; }
                set { finalBlow = value; }
            }

            /// <summary>
            /// What weapon we decided to show this person as using. Often a weapon, 
            /// sometimes a ship or missile, rarely a fish
            /// </summary>
            public int WeaponTypeId
            {
                get { return weaponTypeId; }
                set { weaponTypeId = value; }
            }
        }

        /// <summary>
        /// An Item from the killmail
        /// </summary>
        public class Item
        {
            private int typeId;
            private InventoryFlagType flag;
            private int qtyDropped;
            private int qtyDestroyed;
            private bool container;
            private Item[] containedItems = new Item[0];

            /// <summary>
            /// The typeID of this item, references the invTypes table
            /// </summary>
            public int TypeId
            {
                get { return typeId; }
                set { typeId = value; }
            }

            /// <summary>
            /// See <see cref="InventoryFlagType" /> for full details.
            /// </summary>
            public InventoryFlagType Flag
            {
                get { return flag; }
                set { flag = value; }
            }

            /// <summary>
            /// How many of this item were dropped. 
            /// If the user had multiple stacks, we compress the total number of 
            /// dropped items to just one stack for space purposes
            /// </summary>
            public int QtyDropped
            {
                get { return qtyDropped; }
                set { qtyDropped = value; }
            }

            /// <summary>
            /// How many of this item fell victim to atomic
            /// dispersal and other sad little ends that such 
            /// things can meet.
            /// </summary>
            public int QtyDestroyed
            {
                get { return qtyDestroyed; }
                set { qtyDestroyed = value; }
            }

            /// <summary>
            /// True if the destroyed item contained other items.
            /// </summary>
            public bool Container
            {
                get { return container; }
                set { container = value; }
            }

            /// <summary>
            /// List of contained items.
            /// </summary>
            public Item[] ContainedItems
            {
                get { return containedItems; }
                set { containedItems = value; }
            }
        }
    }

    /// <summary>
    /// Enum choice for Kill Log type
    /// </summary>
    public enum KillLogType
    {
        /// <summary>
        /// Corporation Kill Log
        /// </summary>
        Corporation,
        /// <summary>
        /// Character Kill Log
        /// </summary>
        Character
    }
}
