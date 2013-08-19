namespace libeveapi
{
    /// <summary>
    /// Holds the full asset list of a corporation or character
    /// </summary>
    /// <remarks>
    /// SQL Query
    /// This works in MySQL 5.0 for me anyway.
    /// Note that dbo.eveNames can be used in place of dbo.mapDenormalize 
    /// 
    /// SELECT case
    /// when a.locationID BETWEEN 66000000 AND 66999999 then
    /// (SELECT s.stationName FROM dbo.staStations AS s
    /// WHERE s.stationID=a.locationID-6000001)
    /// when a.locationID BETWEEN 67000000 AND 67999999 then
    /// (SELECT c.stationName FROM api.ConqStations AS c
    /// WHERE c.stationID=a.locationID-6000000)
    /// when a.locationID BETWEEN 60014861 AND 60014928 then
    /// (SELECT c.stationName FROM api.ConqStations AS c
    /// WHERE c.stationID=a.locationID)
    /// when a.locationID BETWEEN 60000000 AND 61000000 then
    /// (SELECT s.stationName FROM dbo.staStations AS s
    /// WHERE s.stationID=a.locationID)
    /// when a.locationID>=61000000 then
    /// (SELECT c.stationName FROM api.ConqStations AS c
    /// WHERE c.stationID=a.locationID)
    /// else (SELECT m.itemName FROM dbo.mapDenormalize AS m
    /// WHERE m.itemID=a.locationID) end
    /// AS location,a.locationId AS locID FROM aleAssetItems AS a
    /// GROUP BY a.locationID;
    /// </remarks>
    public class AssetList : ApiResponse
    {
        /// <summary>
        /// API Version Compatibility
        /// </summary>
        public const string API_VERSION = "2";
        private AssetListItem[] assetListItems = new AssetListItem[0];

        /// <summary>
        /// 
        /// </summary>
        public AssetListItem[] AssetListItems
        {
            get { return assetListItems; }
            set { assetListItems = value; }
        }

        /// <summary>
        /// Represents one full asset and all information associated with it for a
        /// character or corporation
        /// </summary>
        public class AssetListItem
        {
            private int itemId;
            private int locationId;
            private int typeId;
            private long quantity;
            private InventoryFlagType flag;
            private bool singleton;
            private AssetListItem[] contents = new AssetListItem[0];

            /// <summary>
            /// Unique Id for this item. This is only guaranteed to be unique within 
            /// this page load. Ids are recycled over time and it is possible for this 
            /// to happen. Also, items are not guaranteed to maintain the same itemId 
            /// over time. When they are repackaged, stacks are split or merged, when 
            /// they're assembled, and other actions can cause itemIds to change.
            /// </summary>
            public int ItemId
            {
                get { return itemId; }
                set { itemId = value; }
            }

            /// <summary>
            /// References a solar system or station. Note that this column is not present in 
            /// the sub-asset lists, i.e. for things inside of other things.
            /// </summary>
            public int LocationId
            {
                get { return locationId; }
                set { locationId = value; }
            }

            /// <summary>
            /// The type of this item. References the invTypes table.
            /// </summary>
            public int TypeId
            {
                get { return typeId; }
                set { typeId = value; }
            }

            /// <summary>
            /// How many items are in this stack.
            /// </summary>
            public long Quantity
            {
                get { return quantity; }
                set { quantity = value; }
            }

            /// <summary>
            /// Indicates something about this item's storage location. The flag 
            /// is used to differentiate between hangar divisions, drone bay, 
            /// fitting location, and similar. Please see the <see cref="InventoryFlagType" />.
            /// </summary>
            public InventoryFlagType Flag
            {
                get { return flag; }
                set { flag = value; }
            }

            /// <summary>
            /// If true, indicates that this item is a singleton. This means that 
            /// the item is not packaged.
            /// </summary>
            public bool Singleton
            {
                get { return singleton; }
                set { singleton = value; }
            }

            /// <summary>
            /// The items contained in this item if any
            /// </summary>
            public AssetListItem[] Contents
            {
                get { return contents; }
                set { contents = value; }
            }
        }
    }

    /// <summary>
    /// EVE stores items in a location with a value we call the "flag." 
    /// This value is used to indicate more data about the item's location. 
    /// For example, instead of having different locations for each of the 
    /// modules, we instead have the single location (the ship) with many flags. 
    /// The flags indicate if the item is in cargo or fitted, and if fitted, to which slot.
    /// </summary>
    public enum InventoryFlagType
    {
        /// <summary>
        /// None
        /// </summary>
        FlagNone = 0,

        /// <summary>
        /// Wallet
        /// </summary>
        FlagWallet = 1,

        /// <summary>
        /// Factory
        /// </summary>
        FlagFactory = 2,

        /// <summary>
        /// Hangar
        /// </summary>
        FlagHangar = 4,

        /// <summary>
        /// Cargo
        /// </summary>
        FlagCargo = 5,

        /// <summary>
        /// Briefcase
        /// </summary>
        FlagBriefcase = 6,

        /// <summary>
        /// Skill
        /// </summary>
        FlagSkill = 7,

        /// <summary>
        /// Reward
        /// </summary>
        FlagReward = 8,

        /// <summary>
        /// Character in station connected
        /// </summary>
        FlagConnected = 9,

        /// <summary>
        /// Character in station offline
        /// </summary>
        FlagDisconnected = 10,

        /// <summary>
        /// Low power slot 1
        /// </summary>
        FlagLoSlot0 = 11,

        /// <summary>
        /// Low power slot 2
        /// </summary>
        FlagLoSlot1 = 12,

        /// <summary>
        /// Low power slot 3
        /// </summary>
        FlagLoSlot2 = 13,

        /// <summary>
        /// Low power slot 4
        /// </summary>
        FlagLoSlot3 = 14,

        /// <summary>
        /// Low power slot 5
        /// </summary>
        FlagLoSlot4 = 15,

        /// <summary>
        /// Low power slot 6
        /// </summary>
        FlagLoSlot5 = 16,

        /// <summary>
        /// Low power slot 7
        /// </summary>
        FlagLoSlot6 = 17,

        /// <summary>
        /// Low power slot 8
        /// </summary>
        FlagLoSlot7 = 18,

        /// <summary>
        /// Medium power slot 1
        /// </summary>
        FlagMedSlot0 = 19,

        /// <summary>
        /// Medium power slot 2
        /// </summary>
        FlagMedSlot1 = 20,

        /// <summary>
        /// Medium power slot 3
        /// </summary>
        FlagMedSlot2 = 21,

        /// <summary>
        /// Medium power slot 4
        /// </summary>
        FlagMedSlot3 = 22,

        /// <summary>
        /// Medium power slot 5
        /// </summary>
        FlagMedSlot4 = 23,

        /// <summary>
        /// Medium power slot 6
        /// </summary>
        FlagMedSlot5 = 24,

        /// <summary>
        /// Medium power slot 7
        /// </summary>
        FlagMedSlot6 = 25,

        /// <summary>
        /// Medium power slot 8
        /// </summary>
        FlagMedSlot7 = 26,

        /// <summary>
        /// High power slot 1
        /// </summary>
        FlagHiSlot0 = 27,

        /// <summary>
        /// High power slot 2
        /// </summary>
        FlagHiSlot1 = 28,

        /// <summary>
        /// High power slot 3
        /// </summary>
        FlagHiSlot2 = 29,

        /// <summary>
        /// High power slot 4
        /// </summary>
        FlagHiSlot3 = 30,

        /// <summary>
        /// High power slot 5
        /// </summary>
        FlagHiSlot4 = 31,

        /// <summary>
        /// High power slot 6
        /// </summary>
        FlagHiSlot5 = 32,

        /// <summary>
        /// High power slot 7
        /// </summary>
        FlagHiSlot6 = 33,

        /// <summary>
        /// High power slot 8
        /// </summary>
        FlagHiSlot7 = 34,

        /// <summary>
        /// Fixed Slot
        /// </summary>
        FlagFixedSlot = 35,

        /// <summary>
        /// Capsule
        /// </summary>
        FlagCapsule = 56,

        /// <summary>
        /// Pilot
        /// </summary>
        FlagPilot = 57,

        /// <summary>
        /// Passenger
        /// </summary>
        FlagPassenger = 58,

        /// <summary>
        /// Boarding gate
        /// </summary>
        FlagBoardingGate = 59,

        /// <summary>
        /// Crew
        /// </summary>
        FlagCrew = 60,

        /// <summary>
        /// Skill in training
        /// </summary>
        FlagSkillInTraining = 61,

        /// <summary>
        /// Corporation Market Deliveries / Returns
        /// </summary>
        FlagCorpMarket = 62,

        /// <summary>
        /// Locked item, can not be moved unless unlocked
        /// </summary>
        FlagLocked = 63,

        /// <summary>
        /// Unlocked item, can be moved
        /// </summary>
        FlagUnlocked = 64,

        /// <summary>
        /// Office slot 1
        /// </summary>
        FlagOfficeSlot1 = 70,

        /// <summary>
        /// Office slot 2
        /// </summary>
        FlagOfficeSlot2 = 71,

        /// <summary>
        /// Office slot 3
        /// </summary>
        FlagOfficeSlot3 = 72,

        /// <summary>
        /// Office slot 4
        /// </summary>
        FlagOfficeSlot4 = 73,

        /// <summary>
        /// Office slot 5
        /// </summary>
        FlagOfficeSlot5 = 74,

        /// <summary>
        /// Office slot 6
        /// </summary>
        FlagOfficeSlot6 = 75,

        /// <summary>
        /// Office slot 7
        /// </summary>
        FlagOfficeSlot7 = 76,

        /// <summary>
        /// Office slot 8
        /// </summary>
        FlagOfficeSlot8 = 77,

        /// <summary>
        /// Office slot 9
        /// </summary>
        FlagOfficeSlot9 = 78,

        /// <summary>
        /// Office slot 10
        /// </summary>
        FlagOfficeSlot10 = 79,

        /// <summary>
        /// Office slot 11
        /// </summary>
        FlagOfficeSlot11 = 80,

        /// <summary>
        /// Office slot 12
        /// </summary>
        FlagOfficeSlot12 = 81,

        /// <summary>
        /// Office slot 13
        /// </summary>
        FlagOfficeSlot13 = 82,

        /// <summary>
        /// Office slot 14
        /// </summary>
        FlagOfficeSlot14 = 83,

        /// <summary>
        /// Office slot 15
        /// </summary>
        FlagOfficeSlot15 = 84,

        /// <summary>
        /// Office slot 16
        /// </summary>
        FlagOfficeSlot16 = 85,

        /// <summary>
        /// Bonus
        /// </summary>
        FlagBonus = 86,

        /// <summary>
        /// Drone Bay
        /// </summary>
        FlagDroneBay = 87,

        /// <summary>
        /// Booster
        /// </summary>
        FlagBooster = 88,

        /// <summary>
        /// Implant
        /// </summary>
        FlagImplant = 89,

        /// <summary>
        /// Ship Hangar
        /// </summary>
        FlagShipHangar = 90,

        /// <summary>
        /// Ship Offline
        /// </summary>
        FlagShipOffline = 91,

        /// <summary>
        /// Rig power slot 1
        /// </summary>
        FlagRigSlot0 = 92,

        /// <summary>
        /// Rig power slot 2
        /// </summary>
        FlagRigSlot1 = 93,

        /// <summary>
        /// Rig power slot 3
        /// </summary>
        FlagRigSlot2 = 94,

        /// <summary>
        /// Rig power slot 4
        /// </summary>
        FlagRigSlot3 = 95,

        /// <summary>
        /// Rig power slot 5
        /// </summary>
        FlagRigSlot4 = 96,

        /// <summary>
        /// Rig power slot 6
        /// </summary>
        FlagRigSlot5 = 97,

        /// <summary>
        /// Rig power slot 7
        /// </summary>
        FlagRigSlot6 = 98,

        /// <summary>
        /// Rig power slot 8
        /// </summary>
        FlagRigSlot7 = 99,

        /// <summary>
        /// Factory Background Operation
        /// </summary>
        FlagFactoryOperation = 100,

        /// <summary>
        /// Corp Security Access Group 2
        /// </summary>
        FlagCorpSAG2 = 116,

        /// <summary>
        /// Corp Security Access Group 3
        /// </summary>
        FlagCorpSAG3 = 117,

        /// <summary>
        /// Corp Security Access Group 4
        /// </summary>
        FlagCorpSAG4 = 118,

        /// <summary>
        /// Corp Security Access Group 5
        /// </summary>
        FlagCorpSAG5 = 119,

        /// <summary>
        /// Corp Security Access Group 6
        /// </summary>
        FlagCorpSAG6 = 120,

        /// <summary>
        /// Corp Security Access Group 7
        /// </summary>
        FlagCorpSAG7 = 121,

        /// <summary>
        /// Secondary Storage
        /// </summary>
        FlagSecondaryStorage = 122,

        /// <summary>
        /// Unknown Flag
        /// </summary>
        FlagUnknown = 999
    }

    /// <summary>
    /// A corporation or character asset
    /// </summary>
    public enum AssetListType
    {
        /// <summary>
        /// It is a corporation asset
        /// </summary>
        Corporation,
        /// <summary>
        /// It is a character asset
        /// </summary>
        Character
    }
}
