using System;
using System.Collections.Generic;
using System.Text;

namespace libeveapi
{
    /// <summary>
    /// Contains a Faction War Stats for
    /// Players, Corporations, Factions, Eve Server
    /// Including Top Stats on the Server and
    /// Top Stats for Players, Corporations and Factions
    /// </summary>
    public class FacWarStats : ApiResponse
    {
        /// <summary>
        /// API Version Compatibility
        /// </summary>
        public const string API_VERSION = "0";
        /// <summary>
        /// Default Constructor
        /// </summary>
        public FacWarStats() { throw new NotImplementedException("This Class has not been fully implemented"); }

        #region Fields
        private CharFacWarStatsItem characterFactionWarStats = new CharFacWarStatsItem();
        private CorpFacWarStatsItem corporationFactionWarStats = new CorpFacWarStatsItem();
        private EveFacWarStatsItem[] eveFactionWarStats = new EveFacWarStatsItem[0];
        private EveFacWarStatsTotals eveFactionWarStatsTotals = new EveFacWarStatsTotals();
        private EveFacWarStatsTop eveFactionWarStatsTop = new EveFacWarStatsTop();
        #endregion Fields

        #region Properties
        /// <summary>
        /// Faction Warstats for a Character
        /// </summary>
        public CharFacWarStatsItem CharacterFactionWarStats
        {
            get { return characterFactionWarStats; }
            set { characterFactionWarStats = value; }
        }
        /// <summary>
        /// Faction Warstats for a Corporation
        /// </summary>
        public CorpFacWarStatsItem CorporationFactionWarStats
        {
            get { return corporationFactionWarStats; }
            set { corporationFactionWarStats = value; }
        }
        /// <summary>
        /// Eve Faction Warstats for the Tranquility Server
        /// </summary>
        public EveFacWarStatsItem[] EveFactionWarStats
        {
            get { return eveFactionWarStats; }
            set { eveFactionWarStats = value; }
        }
        /// <summary>
        /// Eve Faction Warstat Totals for the Tranquility Server
        /// </summary>
        public EveFacWarStatsTotals EveFactionWarStatsTotals
        {
            get { return eveFactionWarStatsTotals; }
            set { eveFactionWarStatsTotals = value; }
        }
        /// <summary>
        /// Top Faciton Warstats for Players, Corporations, and Factions by catagory
        /// Kills Yesterday, Kills Last Week, Kills Total
        /// Victory Points Yesterday, Victory Points Last Week, Victory Points Total
        /// </summary>
        public EveFacWarStatsTop EveFactionWarStatsTop
        {
            get { return eveFactionWarStatsTop; }
            set { eveFactionWarStatsTop = value; }
        }
        #endregion Properties

        #region SubClasses
        /// <summary>
        /// Structure containing the Faciton Warstats Totals
        /// </summary>
        public struct EveFacWarStatsTotals
        {
            /// <summary>
            /// Initialize structure
            /// </summary>
            /// <param name="killsYesterday">Number of Kills Yesterday</param>
            /// <param name="killsLastWeek">Number of Kills Last Week</param>
            /// <param name="killsTotal">Number of Kills Total</param>
            /// <param name="victoryPointsYesterday">Victory Points Yesterday</param>
            /// <param name="victoryPointsLastWeek">Victory Points Last Week</param>
            /// <param name="victoryPointsTotal">Victory Points Total</param>
            public EveFacWarStatsTotals(int killsYesterday, int killsLastWeek, int killsTotal,
                int victoryPointsYesterday, int victoryPointsLastWeek, int victoryPointsTotal)
            {
                kYesterday = killsYesterday;
                kLastWeek = killsLastWeek;
                kTotal = killsTotal;
                vPointsYesterday = victoryPointsYesterday;
                vPointsLastWeek = victoryPointsLastWeek;
                vPointsTotal = victoryPointsTotal;
            }

            private int kYesterday;
            private int kLastWeek;
            private int kTotal;
            private int vPointsYesterday;
            private int vPointsLastWeek;
            private int vPointsTotal;
            /// <summary>
            /// Number of Kills Yesterday
            /// </summary>
            public int KillsYesterday
            {
                get { return kYesterday; }
                set { kYesterday = value; }
            }
            /// <summary>
            /// Number of Kills Last Week
            /// </summary>
            public int KillsLastWeek
            {
                get { return kLastWeek; }
                set { kLastWeek = value; }
            }
            /// <summary>
            /// Total Number of Kills
            /// </summary>
            public int KillsTotal
            {
                get { return kTotal; }
                set { kTotal = value; }
            }
            /// <summary>
            /// Number of Victory Points Yesterday
            /// </summary>
            public int VictoryPointsYesterday
            {
                get { return vPointsYesterday; }
                set { vPointsYesterday = value; }
            }
            /// <summary>
            /// Number of Victory Points Last Week
            /// </summary>
            public int VictoryPointsLastWeek
            {
                get { return vPointsLastWeek; }
                set { vPointsLastWeek = value; }
            }
            /// <summary>
            /// Total Number of Victory Points
            /// </summary>
            public int VictoryPointsTotal
            {
                get { return vPointsTotal; }
                set { vPointsTotal = value; }
            }
        }

        /// <summary>
        /// Represents Faction stats for a Character 
        /// </summary>
        public class CharFacWarStatsItem : FacWarStatsBase
        {
            /// <summary>
            /// Default Constructor
            /// </summary>
            public CharFacWarStatsItem() { }
            /// <summary>
            /// Constructor with full Field initialization
            /// </summary>
            /// <param name="factionID">Faction ID (Key)</param>
            /// <param name="factionName">Faction Name</param>
            /// <param name="enlisted">Date Enlisted</param>
            /// <param name="currentRank">Current Rank</param>
            /// <param name="highestRank">Highest Rank</param>
            /// <param name="killsYesterday">Number of Kills Yesterday</param>
            /// <param name="killsLastWeek">Number of Kills Last Week</param>
            /// <param name="killsTotal">Total Number of Kills</param>
            /// <param name="victoryPointsYesterday">Number of Victory Points Yesterday</param>
            /// <param name="victoryPointsLastWeek">Number of Victory Points Last Week</param>
            /// <param name="victoryPointsTotal">Total Number of Victory Points</param>
            public CharFacWarStatsItem(int factionID,string factionName,DateTime enlisted,int currentRank,int highestRank,int killsYesterday,
                int killsLastWeek,int killsTotal,int victoryPointsYesterday,int victoryPointsLastWeek,int victoryPointsTotal)

            {
                this.FactionID = factionID;
                this.FactionName = factionName;
                this.enlisted = enlisted;
                this.currentRank = currentRank;
                this.highestRank = highestRank;
                this.KillsYesterday = killsYesterday;
                this.KillsLastWeek = killsLastWeek;
                this.KillsTotal = killsTotal;
                this.VictoryPointsYesterday = victoryPointsYesterday;
                this.VictoryPointsLastWeek = victoryPointsLastWeek;
                this.VictoryPointsTotal = victoryPointsLastWeek;

            }

            private DateTime enlisted;
            private int currentRank;
            private int highestRank;

            /// <summary>
            /// Date Enlisted
            /// </summary>
            public DateTime Enlisted
            {
                get { return enlisted; }
                set { enlisted = value; }
            }
            /// <summary>
            /// Current Rank
            /// </summary>
            public int CurrentRank
            {
                get { return currentRank; }
                set { currentRank = value; }
            }
            /// <summary>
            /// Highest Rank
            /// </summary>
            public int HighestRank
            {
                get { return highestRank; }
                set { highestRank = value; }
            }
        }

        /// <summary>
        /// Represents Faction stats for a Corporation 
        /// </summary>
        public class CorpFacWarStatsItem : FacWarStatsBase
        {
            /// <summary>
            /// Default Constructor
            /// </summary>
            public CorpFacWarStatsItem() { }
            /// <summary>
            /// Constructor with full Field initialization
            /// </summary>
            /// <param name="factionID">Faction ID (Key)</param>
            /// <param name="factionName">Faction Name</param>
            /// <param name="pilots">Total Pilots</param>
            /// <param name="killsYesterday">Number of Kills Yesterday</param>
            /// <param name="killsLastWeek">Number of Kills Last Week</param>
            /// <param name="killsTotal">Total Number of Kills</param>
            /// <param name="victoryPointsYesterday">Number of Victory Points Yesterday</param>
            /// <param name="victoryPointsLastWeek">Number of Victory Points Last Week</param>
            /// <param name="victoryPointsTotal">Total Number of Victory Points</param>
            public CorpFacWarStatsItem(int factionID, string factionName, int pilots, int killsYesterday,int killsLastWeek, int killsTotal,
                int victoryPointsYesterday, int victoryPointsLastWeek, int victoryPointsTotal)
            {
                this.FactionID = factionID;
                this.FactionName = factionName;
                this.pilots = pilots;
                this.KillsYesterday = killsYesterday;
                this.KillsLastWeek = killsLastWeek;
                this.KillsTotal = killsTotal;
                this.VictoryPointsYesterday = victoryPointsYesterday;
                this.VictoryPointsLastWeek = victoryPointsLastWeek;
                this.VictoryPointsTotal = victoryPointsLastWeek;
            }

            private int pilots;
            /// <summary>
            /// Number of Pilots
            /// </summary>
            public int Pilots
            {
                get { return pilots; }
                set { pilots = value; }
            }
        }

        /// <summary>
        /// Represents Eve Faction stats of a faction
        /// FactionID is the 'Key' or Unique Identifier
        /// </summary>
        public class EveFacWarStatsItem : FacWarStatsBase
        {
            /// <summary>
            /// Default Constructor
            /// </summary>
            public EveFacWarStatsItem() { }
            /// <summary>
            /// Constructor with full Field initialization
            /// </summary>
            /// <param name="factionID">Faction ID (Key)</param>
            /// <param name="factionName">Faction Name</param>
            /// <param name="pilots">Number of Pilots</param>
            /// <param name="systemsControlled">Number of Systems Controlled</param>
            /// <param name="killsYesterday">Number of Kills Yesterday</param>
            /// <param name="killsLastWeek">Number of Kills Last Week</param>
            /// <param name="killsTotal">Total Number of Kills</param>
            /// <param name="victoryPointsYesterday">Number of Victory Points Yesterday</param>
            /// <param name="victoryPointsLastWeek">Number of Victory Points Last Week</param>
            /// <param name="victoryPointsTotal">Total Number of Victory Points</param>
            public EveFacWarStatsItem(int factionID,string factionName,int pilots,int systemsControlled,int killsYesterday,
                int killsLastWeek,int killsTotal,int victoryPointsYesterday,int victoryPointsLastWeek,int victoryPointsTotal)
            {
                base.FactionID = factionID;
                this.FactionName = factionName;
                this.pilots = pilots;
                this.systemsControlled = systemsControlled;
                this.KillsYesterday = killsYesterday;
                this.KillsLastWeek = killsLastWeek;
                this.KillsTotal = killsTotal;
                this.VictoryPointsYesterday = victoryPointsYesterday;
                this.VictoryPointsLastWeek = victoryPointsLastWeek;
                this.VictoryPointsTotal = victoryPointsLastWeek;
            }

            private int pilots;
            private int systemsControlled;
            /// <summary>
            /// Number of Pilots
            /// </summary>
            public int Pilots
            {
                get { return pilots; }
                set { pilots = value; }
            }
            /// <summary>
            /// Number of Systems Controlled
            /// </summary>
            public int SystemsControlled
            {
                get { return systemsControlled; }
                set { systemsControlled = value; }
            }
        }

        /// <summary>
        /// Top Faciton Warstats for Players, Corporations, and Factions by catagory
        /// Kills Yesterday, Kills Last Week, Kills Total
        /// Victory Points Yesterday, Victory Points Last Week, Victory Points Total
        /// </summary>
        public class EveFacWarStatsTop
        {
            /// <summary>
            /// Default Constructor
            /// </summary>
            public EveFacWarStatsTop() { }
            
            #region Fields
            #region Character Top Fields
            private TopKills[] characterTopKillsYesterday;
            private TopKills[] characterTopKillsLastWeek;
            private TopKills[] characterTopKillsTotal;
            private TopVictoryPoints[] characterVictoryPointsYesterday;
            private TopVictoryPoints[] characterVictoryPointsLastWeek;
            private TopVictoryPoints[] characterVictoryPointsTotal;
            #endregion Character Top Fields
            #region Corporation Top Fields
            private TopKills[] corporationTopKillsYesterday;
            private TopKills[] corporationTopKillsLastWeek;
            private TopKills[] corporationTopKillsTotal;
            private TopVictoryPoints[] corporationVictoryPointsYesterday;
            private TopVictoryPoints[] corporationVictoryPointsLastWeek;
            private TopVictoryPoints[] corporationVictoryPointsTotal;
            #endregion Corporation Top Fields
            #region Faction Top Fields
            private TopKills[] factionTopKillsYesterday;
            private TopKills[] factionTopKillsLastWeek;
            private TopKills[] factionTopKillsTotal;
            private TopVictoryPoints[] factionVictoryPointsYesterday;
            private TopVictoryPoints[] factionVictoryPointsLastWeek;
            private TopVictoryPoints[] factionVictoryPointsTotal;
            #endregion Faction Top Fields
            #endregion Fields

            #region Properties
            #region Character Top Properties
            /// <summary>
            /// Player Top Kills Yesterday
            /// </summary>
            public TopKills[] CharacterTopKillsYesterday
            {
                get { return characterTopKillsYesterday; }
                set { characterTopKillsYesterday = value; }
            }
            /// <summary>
            /// Player Top Kills Last Week
            /// </summary>
            public TopKills[] CharacterTopKillsLastWeek
            {
                get { return characterTopKillsLastWeek; }
                set { characterTopKillsLastWeek = value; }
            }
            /// <summary>
            /// Player Top Kills Total
            /// </summary>
            public TopKills[] CharacterTopKillsTotal
            {
                get { return characterTopKillsTotal; }
                set { characterTopKillsTotal = value; }
            }
            /// <summary>
            /// Player Top Victory Points Yesterday
            /// </summary>
            public TopVictoryPoints[] CharacterVictoryPointsYesterday
            {
                get { return characterVictoryPointsYesterday; }
                set { characterVictoryPointsYesterday = value; }
            }
            /// <summary>
            /// Player Top Victory Points Last Week
            /// </summary>
            public TopVictoryPoints[] CharacterVictoryPointsLastWeek
            {
                get { return characterVictoryPointsLastWeek; }
                set { characterVictoryPointsLastWeek = value; }
            }
            /// <summary>
            /// Player Top Victory Points Total
            /// </summary>
            public TopVictoryPoints[] CharacterVictoryPointsTotal
            {
                get { return characterVictoryPointsTotal; }
                set { characterVictoryPointsTotal = value; }
            }
            #endregion Character Top Properties

            #region Corporation Top Properties
            /// <summary>
            /// Corporation Top Kills Yesterday
            /// </summary>
            public TopKills[] CorporationTopKillsYesterday
            {
                get { return corporationTopKillsYesterday; }
                set { corporationTopKillsYesterday = value; }
            }
            /// <summary>
            /// Corporation Top Kills Last Week
            /// </summary>
            public TopKills[] CorporationTopKillsLastWeek
            {
                get { return corporationTopKillsLastWeek; }
                set { corporationTopKillsLastWeek = value; }
            }
            /// <summary>
            /// Corporation Top Kills Total
            /// </summary>
            public TopKills[] CorporationTopKillsTotal
            {
                get { return corporationTopKillsTotal; }
                set { corporationTopKillsTotal = value; }
            }
            /// <summary>
            /// Corporation Top Victory Points Yesterday
            /// </summary>
            public TopVictoryPoints[] CorporationVictoryPointsYesterday
            {
                get { return corporationVictoryPointsYesterday; }
                set { corporationVictoryPointsYesterday = value; }
            }
            /// <summary>
            /// Corporation Top Victory Points Last Week
            /// </summary>
            public TopVictoryPoints[] CorporationVictoryPointsLastWeek
            {
                get { return corporationVictoryPointsLastWeek; }
                set { corporationVictoryPointsLastWeek = value; }
            }
            /// <summary>
            /// Corporation Top Victory Points Total
            /// </summary>
            public TopVictoryPoints[] CorporationVictoryPointsTotal
            {
                get { return corporationVictoryPointsTotal; }
                set { corporationVictoryPointsTotal = value; }
            }
            #endregion Corporation Top Properties

            #region Faction Top Properties
            /// <summary>
            /// Faction Top Kills Yesterday
            /// </summary>
            public TopKills[] FactionTopKillsYesterday
            {
                get { return factionTopKillsYesterday; }
                set { factionTopKillsYesterday = value; }
            }
            /// <summary>
            /// Faction Top Kills Last Week
            /// </summary>
            public TopKills[] FactionTopKillsLastWeek
            {
                get { return factionTopKillsLastWeek; }
                set { factionTopKillsLastWeek = value; }
            }
            /// <summary>
            /// Faction Top Kills Totals
            /// </summary>
            public TopKills[] FactionTopKillsTotal
            {
                get { return factionTopKillsTotal; }
                set { factionTopKillsTotal = value; }
            }
            /// <summary>
            /// Faction Top Victory Points Yesterday
            /// </summary>
            public TopVictoryPoints[] FactionVictoryPointsYesterday
            {
                get { return factionVictoryPointsYesterday; }
                set { factionVictoryPointsYesterday = value; }
            }
            /// <summary>
            /// Faction Top Victory Points Last Week
            /// </summary>
            public TopVictoryPoints[] FactionVictoryPointsLastWeek
            {
                get { return factionVictoryPointsLastWeek; }
                set { factionVictoryPointsLastWeek = value; }
            }
            /// <summary>
            /// Faction Top Victory Points Totals
            /// </summary>
            public TopVictoryPoints[] FactionVictoryPointsTotal
            {
                get { return factionVictoryPointsTotal; }
                set { factionVictoryPointsTotal = value; }
            }
            #endregion Faction Top Properties
            #endregion Properties

            /// <summary>
            /// Represents a Top Kills Stat
            /// </summary>
            public struct TopKills
            {
                /// <summary>
                /// Full Initialization
                /// </summary>
                /// <param name="id">ID</param>
                /// <param name="name">Name</param>
                /// <param name="kills">Number of Kills</param>
                public TopKills(int id, string name, int kills)
                {
                    this.id = id;
                    this.name = name;
                    this.kills = kills;
                }
                private int id;
                private string name;
                private int kills;
                /// <summary>
                /// ID
                /// </summary>
                public int ID
                {
                    get { return id; }
                    set { id = value; }
                }
                /// <summary>
                /// Name
                /// </summary>
                public string Name
                {
                    get { return name; }
                    set { name = value; }
                }
                /// <summary>
                /// Number of kills
                /// </summary>
                public int Kills
                {
                    get { return kills; }
                    set { kills = value; }
                }
            }

            /// <summary>
            /// Represents a Top Victory Points Stat
            /// </summary>
            public struct TopVictoryPoints
            {
                /// <summary>
                /// Full Initialization
                /// </summary>
                /// <param name="id">ID</param>
                /// <param name="name">Name</param>
                /// <param name="victoryPoints">Number Victory Points</param>
                public TopVictoryPoints(int id, string name, int victoryPoints)
                {
                    this.id = id;
                    this.name = name;
                    this.victoryPoints = victoryPoints;
                }
                private int id;
                private string name;
                private int victoryPoints;
                /// <summary>
                /// ID
                /// </summary>
                public int ID
                {
                    get { return id; }
                    set { id = value; }
                }
                /// <summary>
                /// Name
                /// </summary>
                public string Name
                {
                    get { return name; }
                    set { name = value; }
                }
                /// <summary>
                /// Number Victory Points
                /// </summary>
                public int VictoryPoints
                {
                    get { return victoryPoints; }
                    set { victoryPoints = value; }
                }
            }

        }

        /// <summary>
        /// Base class with common properties of all Faction Warstats Data
        /// </summary>
        public abstract class FacWarStatsBase : ApiResponse
        {
            /// <summary>
            /// Default Constructor
            /// </summary>
            public FacWarStatsBase() { }
            /// <summary>
            /// Constructor with full Field initialization
            /// </summary>
            /// <param name="factionID">Faction ID (Key)</param>
            /// <param name="factionName">Faction Name</param>
            /// <param name="killsYesterday">Number of Kills Yesterday</param>
            /// <param name="killsLastWeek">Number of Kills Last Week</param>
            /// <param name="killsTotal">Total Number of Kills</param>
            /// <param name="victoryPointsYesterday">Number of Victory Points Yesterday</param>
            /// <param name="victoryPointsLastWeek">Number of Victory Points Last Week</param>
            /// <param name="victoryPointsTotal">Total Number of Victory Points</param>
            public FacWarStatsBase(int factionID, string factionName, int killsYesterday, int killsLastWeek, int killsTotal,
                int victoryPointsYesterday, int victoryPointsLastWeek, int victoryPointsTotal)
            {
                this.factionID = factionID;
                this.factionName = factionName;
                this.killsYesterday = killsYesterday;
                this.killsLastWeek = killsLastWeek;
                this.killsTotal = killsTotal;
                this.victoryPointsYesterday = victoryPointsYesterday;
                this.victoryPointsLastWeek = victoryPointsLastWeek;
                this.VictoryPointsTotal = victoryPointsLastWeek;
            }


            private int factionID;
            private string factionName;
            private int killsYesterday;
            private int killsLastWeek;
            private int killsTotal;
            private int victoryPointsYesterday;
            private int victoryPointsLastWeek;
            private int victoryPointsTotal;

            /// <summary>
            /// Faction ID (Key)
            /// </summary>
            public int FactionID
            {
                get { return factionID; }
                set { factionID = value; }
            }
            /// <summary>
            /// Faction Name
            /// </summary>
            public string FactionName
            {
                get { return factionName; }
                set { factionName = value; }
            }
            /// <summary>
            /// Number of Kills Yesterday
            /// </summary>
            public int KillsYesterday
            {
                get { return killsYesterday; }
                set { killsYesterday = value; }
            }
            /// <summary>
            /// Number of Kills Last Week
            /// </summary>
            public int KillsLastWeek
            {
                get { return killsLastWeek; }
                set { killsLastWeek = value; }
            }
            /// <summary>
            /// Total Number of Kills
            /// </summary>
            public int KillsTotal
            {
                get { return killsTotal; }
                set { killsTotal = value; }
            }
            /// <summary>
            /// Number of Victory Points Yesterday
            /// </summary>
            public int VictoryPointsYesterday
            {
                get { return victoryPointsYesterday; }
                set { victoryPointsYesterday = value; }
            }
            /// <summary>
            /// Number of Victory Points Last Week
            /// </summary>
            public int VictoryPointsLastWeek
            {
                get { return victoryPointsLastWeek; }
                set { victoryPointsLastWeek = value; }
            }
            /// <summary>
            /// Total Number of Victory Points
            /// </summary>
            public int VictoryPointsTotal
            {
                get { return victoryPointsTotal; }
                set { victoryPointsTotal = value; }
            }
        }
        #endregion SubClasses

    }
}
