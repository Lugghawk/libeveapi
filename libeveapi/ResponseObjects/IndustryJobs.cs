using System;

namespace libeveapi
{
    /// <summary>
    /// Represents a industry jobs that have been performed and are currently in progress response from the eve api
    /// http://wiki.eve-dev.net/APIv2_Char_IndustryJobs_XML
    /// </summary>
    public class IndustryJobList : ApiResponse
    {
        /// <summary>
        /// API Version Compatibility
        /// </summary>
        public const string API_VERSION = "2";
        private IndustryJobListItem[] industryJobListItems = new IndustryJobListItem[0];

        /// <summary>
        /// 
        /// </summary>
        public IndustryJobListItem[] IndustryJobListItems
        {
            get { return industryJobListItems; }
            set { industryJobListItems = value; }
        }

        /// <summary>
        /// A representation of an industry job
        /// </summary>
        public class IndustryJobListItem
        {
            private long jobId;
            private long assemblyLineId;
            private long containerId;
            private long installedItemId;
            private long installedItemLocationId;
            private long installedItemQuantity;
            private long installedItemProductivityLevel;
            private long installedItemMaterialLevel;
            private long installedItemLicensedProductionRunsRemaining;
            private long outputLocationId;
            private long installerId;
            private long runs;
            private long licensedProductionRuns;
            private long installedInSolarSystemId;
            private long containerLocationId;
            private double materialMultiplier;
            private double charMaterialMultiplier;
            private double timeMultiplier;
            private double charTimeMultiplier;
            private long installedItemTypeId;
            private long outputTypeId;
            private long containerTypeId;
            private bool installedItemCopy;
            private bool completed;
            private bool completedSuccessfully;
            private long installedItemFlag;
            private long outputFlag;
            private long activityId;
            private Activities activity;
            private IndustryJobCompletedStatuses completedStatus;
            private DateTime installTime;
            private DateTime beginProductionTime;
            private DateTime endProductionTime;
            private DateTime pauseProductionTime;
            private DateTime installTimeLocal;
            private DateTime beginProductionTimeLocal;
            private DateTime endProductionTimeLocal;
            private DateTime pauseProductionTimeLocal;

            /// <summary>
            /// This is the unique job id that is assigned 
            /// to the job by the eve system
            /// </summary>
            public long JobId
            {
                get { return jobId; }
                set { jobId = value; }
            }

            /// <summary>
            /// This is the assembly line that it is installed longo if in a station
            /// </summary>
            public long AssemblyLineId
            {
                get { return assemblyLineId; }
                set { assemblyLineId = value; }
            }

            /// <summary>
            /// 
            /// </summary>
            public long ContainerId
            {
                get { return containerId; }
                set { containerId = value; }
            }

            /// <summary>
            /// This is the itemId of the item that was
            /// installed for whatever factory job was happening, this
            /// isnt really useful as it can change, but it will link back
            /// to something in your asset list (hopefully)
            /// </summary>
            public long InstalledItemId
            {
                get { return installedItemId; }
                set { installedItemId = value; }
            }

            /// <summary>
            /// This is the locationId of where the item was installed
            /// </summary>
            public long InstalledItemLocationId
            {
                get { return installedItemLocationId; }
                set { installedItemLocationId = value; }
            }

            /// <summary>
            /// This is how many of the item were installed, (usually 1)
            /// </summary>
            public long InstalledItemQuantity
            {
                get { return installedItemQuantity; }
                set { installedItemQuantity = value; }
            }

            /// <summary>
            /// This is the blueprlongs Productivity Level (TE)
            /// </summary>
            public long InstalledItemProductivityLevel
            {
                get { return installedItemProductivityLevel; }
                set { installedItemProductivityLevel = value; }
            }

            /// <summary>
            /// This is the blueprlongs Material Level (ME)
            /// </summary>
            public long InstalledItemMaterialLevel
            {
                get { return installedItemMaterialLevel; }
                set { installedItemMaterialLevel = value; }
            }

            /// <summary>
            /// This is how many production runs are left on the
            /// blueprlong that was installed.  A -1 represents a BPO
            /// with unlimited copies left.
            /// </summary>
            public long InstalledItemLicensedProductionRunsRemaining
            {
                get { return installedItemLicensedProductionRunsRemaining; }
                set { installedItemLicensedProductionRunsRemaining = value; }
            }

            /// <summary>
            /// This is where the output of the job will be placed
            /// </summary>
            public long OutputLocationId
            {
                get { return outputLocationId; }
                set { outputLocationId = value; }
            }

            /// <summary>
            /// The characterId of the person who installed the job
            /// </summary>
            public long InstallerId
            {
                get { return installerId; }
                set { installerId = value; }
            }


            /// <summary>
            /// This is how many runs of the object are being made.
            /// </summary>
            public long Runs
            {
                get { return runs; }
                set { runs = value; }
            }


            /// <summary>
            /// 
            /// </summary>
            public long LicensedProductionRuns
            {
                get { return licensedProductionRuns; }
                set { licensedProductionRuns = value; }
            }


            /// <summary>
            /// This is the solarsystemId of where the job was installed
            /// </summary>
            public long InstalledInSolarSystemId
            {
                get { return installedInSolarSystemId; }
                set { installedInSolarSystemId = value; }
            }


            /// <summary>
            /// Where the container is located at, usually a moon or station.
            /// </summary>
            public long ContainerLocationId
            {
                get { return containerLocationId; }
                set { containerLocationId = value; }
            }


            /// <summary>
            /// This is the ME multiplier from the installation place.
            /// </summary>
            public double MaterialMultiplier
            {
                get { return materialMultiplier; }
                set { materialMultiplier = value; }
            }


            /// <summary>
            /// This is the ME multiplier that id done by the character's skills
            /// </summary>
            public double CharMaterialMultiplier
            {
                get { return charMaterialMultiplier; }
                set { charMaterialMultiplier = value; }
            }


            /// <summary>
            /// This is the TE multiplier of the station
            /// </summary>
            public double TimeMultiplier
            {
                get { return timeMultiplier; }
                set { timeMultiplier = value; }
            }


            /// <summary>
            /// The TE multiplier that id done by the character's skills
            /// </summary>
            public double CharTimeMultiplier
            {
                get { return charTimeMultiplier; }
                set { charTimeMultiplier = value; }
            }


            /// <summary>
            /// This is the typeId of the item that was installed (a blueprint)
            /// </summary>
            public long InstalledItemTypeId
            {
                get { return installedItemTypeId; }
                set { installedItemTypeId = value; }
            }


            /// <summary>
            /// This is the typeId of what will be outputted when the item
            /// is finished doing whatever it is doing.  For research this will
            /// be the blueprint itself, for manufacturing this will be the item
            /// </summary>
            public long OutputTypeId
            {
                get { return outputTypeId; }
                set { outputTypeId = value; }
            }


            /// <summary>
            /// This is the lab the items are currently in
            /// Can be looked up like any typeId, usually a mobile lab 
            /// or something similar
            /// </summary>
            public long ContainerTypeId
            {
                get { return containerTypeId; }
                set { containerTypeId = value; }
            }

            /// <summary>
            /// This is a bool value if the blueprlong installed is a copy or not
            /// </summary>
            public bool InstalledItemCopy
            {
                get { return installedItemCopy; }
                set { installedItemCopy = value; }
            }

            /// <summary>
            /// This is a boolean value if the item has completed or not
            /// </summary>
            public bool Completed
            {
                get { return completed; }
                set { completed = value; }
            }

            /// <summary>
            /// Boolean value if the job completed successfully or not
            /// </summary>
            public bool CompletedSuccessfully
            {
                get { return completedSuccessfully; }
                set { completedSuccessfully = value; }
            }

            /// <summary>
            /// Flags, same as used in the assets
            /// </summary>
            public long InstalledItemFlag
            {
                get { return installedItemFlag; }
                set { installedItemFlag = value; }
            }

            /// <summary>
            /// Flags, same as used in the assets
            /// </summary>
            public long OutputFlag
            {
                get { return outputFlag; }
                set { outputFlag = value; }
            }

            /// <summary>
            /// This is what kind of activity was going on with the item
            /// (use enum Activities)
            /// </summary>
            public long ActivityId
            {
                get { return activityId; }
                set { activityId = value; }
            }

            /// <summary>
            /// ENUM of what activity is currently being done
            /// </summary>
            public Activities Activity
            {
                get { return activity; }
                set { activity = value; }
            }

            /// <summary>
            /// Status of the item when it was completed
            /// </summary>
            public IndustryJobCompletedStatuses CompletedStatus
            {
                get { return completedStatus; }
                set { completedStatus = value; }
            }

            /// <summary>
            /// When this item was installed in ccp time
            /// </summary>
            public DateTime InstallTime
            {
                get { return installTime; }
                set { installTime = value; }
            }

            /// <summary>
            /// When production time started in ccp time (different than install time if a queue)
            /// </summary>
            public DateTime BeginProductionTime
            {
                get { return beginProductionTime; }
                set { beginProductionTime = value; }
            }

            /// <summary>
            /// When the job will be finished in ccp time.
            /// </summary>
            public DateTime EndProductionTime
            {
                get { return endProductionTime; }
                set { endProductionTime = value; }
            }

            /// <summary>
            /// 
            /// </summary>
            public DateTime PauseProductionTime
            {
                get { return pauseProductionTime; }
                set { pauseProductionTime = value; }
            }

            /// <summary>
            /// When this item was installed in local time.
            /// </summary>
            public DateTime InstallTimeLocal
            {
                get { return installTimeLocal; }
                set { installTimeLocal = value; }
            }

            /// <summary>
            /// When production time started in local time (different than install time if a queue)
            /// </summary>
            public DateTime BeginProductionTimeLocal
            {
                get { return beginProductionTimeLocal; }
                set { beginProductionTimeLocal = value; }
            }

            /// <summary>
            /// When the job will be finished in local time.
            /// </summary>
            public DateTime EndProductionTimeLocal
            {
                get { return endProductionTimeLocal; }
                set { endProductionTimeLocal = value; }
            }

            /// <summary>
            /// 
            /// </summary>
            public DateTime PauseProductionTimeLocal
            {
                get { return pauseProductionTimeLocal; }
                set { pauseProductionTimeLocal = value; }
            }
        }
    }

    /// <summary>
    /// Is this a corporation or character type of job
    /// </summary>
    public enum IndustryJobListType
    {
        /// <summary>
        /// This is a job for the Corporation
        /// </summary>
        Corporation,
        /// <summary>
        /// This is a personal job for the Character
        /// </summary>
        Character
    }

    /// <summary>
    /// This represents the status of a job once completed
    /// </summary>
    public enum IndustryJobCompletedStatuses
    {
        /// <summary>
        /// It was completed; however, the job failed (invention)
        /// </summary>
        Failed = 0,
        /// <summary>
        /// The job was completed and delivered successfully
        /// </summary>
        Delivered = 1,
        /// <summary>
        /// The job was completed, because it was aborted
        /// </summary>
        Aborted = 2,
        /// <summary>
        /// A GM Completed/Aborted the job
        /// </summary>
        GM_Aborted = 3,
        /// <summary>
        /// The job was completed because the item it was in was unanchored
        /// </summary>
        Unachored = 4,
        /// <summary>
        /// The job is considered completed because the item was destroyed (mobile lab, etc).
        /// </summary>
        Destroyed = 5
    }

    /// <summary>
    /// The different activities that can occur for an Science and Industry job
    /// </summary>
    public enum Activities
    {
        /// <summary>
        /// Manufacturing
        /// </summary>
        Manufacturing = 1,
        /// <summary>
        /// Researching Technology (not in game at current)
        /// </summary>
        TechnologyResearch = 2,
        /// <summary>
        /// Time Efficiency Research
        /// </summary>
        TimeEfficiency = 3,
        /// <summary>
        /// Material Efficiency Research
        /// </summary>
        MaterialEfficiency = 4,
        /// <summary>
        /// Blueprint Copying
        /// </summary>
        Copying = 5,
        /// <summary>
        /// Blueprint Duplication (not in gate at current)
        /// </summary>
        Duplicating = 6,
        /// <summary>
        /// Reverse Engineering (not in game at current)
        /// </summary>
        ReverseEngineering = 7,
        /// <summary>
        /// Blueprint Invention
        /// </summary>
        Invention = 8,
        /// <summary>
        /// Unknown type of job
        /// </summary>
        Unknown = 0
    }
}
