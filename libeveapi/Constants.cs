namespace libeveapi
{
    /// <summary>
    /// These are the different constants needed throughout the program
    /// </summary>
    internal class Constants
    {
        /// <summary>
        /// This is the main domain name that is the prefix for all requests.
        /// To change what server is being used for testing, this URL would be
        /// changed to a test server.
        /// </summary>
        internal static string ApiPrefix = "https://api.eveonline.com";
        
        #region /account/
        /// <summary>
        /// The location of the character list xml file
        /// </summary>
        internal const string CharacterList = "/account/Characters.xml.aspx";
        #endregion /account/

        #region /char/
        /// <summary>
        /// The location of the character account balance xml file
        /// </summary>
        internal const string CharacterAccountBalance = "/char/AccountBalance.xml.aspx";
        /// <summary>
        /// The location of the character's asset list xml file
        /// </summary>
        internal const string CharAssetList = "/char/AssetList.xml.aspx";
        /// <summary>
        /// The location of the character sheet xml file
        /// </summary>
        internal const string CharacterSheet = "/char/CharacterSheet.xml.aspx";
        /// <summary>
        /// The location of the character info xml file.
        /// </summary>
        internal const string CharacterInfo = "/eve/CharacterInfo.xml.aspx";
        /// <summary>
        /// The location of the Character Factional Warfare Stats xml file
        /// </summary>
        internal const string CharFactionalWarfareStats = "/char/FacWarStats.xml.aspx";
        /// <summary>
        /// The location of the character's industrial jobs xml file
        /// </summary>
        internal const string CharIndustryJobs = "/char/IndustryJobs.xml.aspx";
        /// <summary>
        /// The location of the characters killlog xml file
        /// </summary>
        internal const string CharKillLog = "/char/Killlog.xml.aspx";
        /// <summary>
        /// The location of the Character Mailing Lists xml file
        /// </summary>
        internal const string CharMailingLists = "/char/MailingLists.xml.aspx";
        /// <summary>
        /// The location of the Character's Mail Messages xml file
        /// </summary>
        internal const string CharMailMessages = "/char/MailMessages.xml.aspx";
        /// <summary>
        /// The location of the character's market order xml file
        /// </summary>
        internal const string CharMarketOrders = "/char/MarketOrders.xml.aspx";
        /// <summary>
        /// The location of the Character's Medals xml file
        /// </summary>
        internal const string CharMedals = "/char/Medals.xml.aspx";
        /// <summary>
        /// The location of the Character's Notificatinos xml file
        /// </summary>
        internal const string CharacterNotifications = "/char/Notifications.xml.aspx";
        /// <summary>
        /// The location of the Characters Current Research jobs xml file
        /// </summary>
        internal const string CharResearch = "/char/Research.xml.aspx";
        /// <summary>
        /// The location of the characters current skill in training xml file
        /// </summary>
        internal const string SkillInTraining = "/char/SkillInTraining.xml.aspx";
        /// <summary>
        /// The location of the characters current skill queue xml file
        /// </summary>
        internal const string SkillQueue = "/char/SkillQueue.xml.aspx";
        /// <summary>
        /// The locatin of the Character's Standings xml file
        /// </summary>
        internal const string CharStandings = "/char/Standings.xml.aspx";
        /// <summary>
        /// The location of the character's journal entries xml file
        /// </summary>
        internal const string CharJournalEntries = "/char/WalletJournal.xml.aspx";
        /// <summary>
        /// The location of the character's Wallet Journal Entries xml file
        /// </summary>
        internal const string CharWalletJournal = "/char/WalletJournal.xml.aspx";
        /// <summary>
        /// The location of the character's wallet transaction xml file
        /// </summary>
        internal const string CharWalletTransactions = "/char/WalletTransactions.xml.aspx";
        #endregion /char/

        #region /corp/
        /// <summary>
        /// The location of the corporation account balance xml file
        /// </summary>
        internal const string CorpAccountBalance = "/corp/AccountBalance.xml.aspx";
        /// <summary>
        /// The location of the corporation asset list xml file
        /// </summary>
        internal const string CorpAssetList = "/corp/AssetList.xml.aspx";
        /// <summary>
        /// The locatin of the Corporation Container Log xml file
        /// </summary>
        internal const string CorpContainerLog = "/corp/ContainerLog.xml.aspx";
        /// <summary>
        /// The location of the coporation sheet xml file
        /// </summary>
        internal const string CorporationSheet = "/corp/CorporationSheet.xml.aspx";
        /// <summary>
        /// The location of the corporation's Factional Warfare Stats xml file
        /// </summary>
        internal const string CorpFactionalWarfareStats = "/corp/FacWarStats.xml.aspx";
        /// <summary>
        /// The location of the corporation's industrial jobs xml file
        /// </summary>
        internal const string CorpIndustryJobs = "/corp/IndustryJobs.xml.aspx";
        /// <summary>
        /// The location of the corporation killlog xml file
        /// </summary>
        internal const string CorpKillLog = "/corp/Killlog.xml.aspx";
        /// <summary>
        /// The location of the corporation's market order xml file
        /// </summary>
        internal const string CorpMarketOrders = "/corp/MarketOrders.xml.aspx";
        /// <summary>
        /// The location of the Corporations Medals xml file
        /// </summary>
        internal const string CorpMedals = "/corp/Medals.xml.aspx";
        /// <summary>
        /// The location of the Corporation Members Medals xml file
        /// </summary>
        internal const string CorpMemberMedals = "/corp/MemberMedals.xml.aspx";
        /// <summary>
        /// The location of the Corporations Member Security xml file
        /// </summary>
        internal const string CorpMemberSecurity = "/corp/MemberSecurity.xml.aspx";
        /// <summary>
        /// The location of the Corporations Member Security Log xml file
        /// </summary>
        internal const string CorpMemberSecurityLog = "/corp/MemberSecurityLog.xml.aspx";
        /// <summary>
        /// The location of the member tracking xml file
        /// </summary>
        internal const string MemberTracking = "/corp/MemberTracking.xml.aspx";
        /// <summary>
        /// The location of the corporation's starbase details xml file
        /// </summary>
        internal const string StarbaseDetails = "/corp/StarbaseDetail.xml.aspx";
        /// <summary>
        /// The location of the corporation's starbase list xml file
        /// </summary>
        internal const string StarbaseList = "/corp/StarbaseList.xml.aspx";
        /// <summary>
        /// The location of the Corporations Shareholders xml file
        /// </summary>
        internal const string CorpShareholders = "/corp/Shareholders.xml.aspx";
        /// <summary>
        /// The location of the Corporations Standings xml file
        /// </summary>
        internal const string CorpStandings = "/corp/Standings.xml.aspx";
        /// <summary>
        /// The location of the Corporations Titles xml file
        /// </summary>
        internal const string CorpTitles = "/corp/Titles.xml.aspx";
        /// <summary>
        /// The location of the corporation's journal entries xml file
        /// </summary>
        internal const string CorpJournalEntries = "/corp/WalletJournal.xml.aspx";
        /// <summary>
        /// The location of the corporation's Wallet Journal xml file
        /// </summary>
        internal const string CorpWalletJournal = "/corp/WalletJournal.xml.aspx";
        /// <summary>
        /// The location of the corporation's wallet transactions xml file
        /// </summary>
        internal const string CorpWalletTransactions = "/corp/WalletTransactions.xml.aspx";
        #endregion /corp/

        #region /eve/
        /// <summary>
        /// The location of the alliance list xml file
        /// </summary>
        internal const string AllianceList = "/eve/AllianceList.xml.aspx";
        /// <summary>
        /// The location of the Eve Cirtificate Tree xml file
        /// </summary>
        internal const string CertificateTree = "/eve/CertificateTree.xml.aspx";
        /// <summary>
        /// The location of the conquerable stations and outpost statistics xml file
        /// </summary>
        internal const string ConquerableStationOutpost = "/eve/ConquerableStationList.xml.aspx";
        /// <summary>
        /// The location of the error list xml file
        /// </summary>
        internal static string ErrorList = "/eve/ErrorList.xml.aspx";
        /// <summary>
        /// The location of the Eve Factional Warfare Stats xml file
        /// </summary>
        internal const string EveFactionalWarfareStats = "/eve/FacWarStats.xml.aspx";
        /// <summary>
        /// The location of the Eve Factional Warfare Top 100 Stats xml file
        /// </summary>
        internal const string EveFactionalWarfareTopStats = "/eve/FacWarTopStats.xml.aspx";
        /// <summary>
        /// The location of the characterId to characterName conversion xml file
        /// </summary>
        internal const string CharacterNameId = "/eve/CharacterName.xml.aspx";
        /// <summary>
        /// The location of the characterName and characterId conversion xml file
        /// </summary>
        internal const string CharacterIdName = "/eve/CharacterID.xml.aspx";
        /// <summary>
        /// The location of the reference type list xml file
        /// </summary>
        internal const string RefTypesList = "/eve/RefTypes.xml.aspx";
        /// <summary>
        /// The location of the current eve skill tree xml file
        /// </summary>
        internal const string SkillTree = "/eve/SkillTree.xml.aspx";
        #endregion /eve/

        
        
        #region /map/
        /// <summary>
        /// A list of contestable solar systems and the NPD faction currently occupying them
        /// </summary>
        internal const string MapFactionWarSystems = "/map/FacWarSystems.xml.aspx";
        /// <summary>
        /// The location of the map jump statistics xml file
        /// </summary>
        internal const string MapJumps = "/map/Jumps.xml.aspx";
        /// <summary>
        /// The location of the map kills statistics xml file
        /// </summary>
        internal const string MapKills = "/map/Kills.xml.aspx";
        /// <summary>
        /// The location of the map sovernty statistics xml file
        /// </summary>
        internal const string MapSoveignty = "/map/Sovereignty.xml.aspx";
        #endregion /map/

        #region /server/
        /// <summary>
        /// Location of the Tranquility Server Status xml file 
        /// </summary>
        internal const string ServerStatus = "/Server/ServerStatus.xml.aspx";
        #endregion /server/

        #region Misc
        /// <summary>
        /// The FULL PATH to the image generator
        /// </summary>
        internal static string ImageFullURL = "http://img.eve.is/serv.asp";
        #endregion Misc

        /// <summary>
        /// Not part of the eve-api - used for unit tests
        /// </summary>
        internal const string ExampleError = "/Error.xml.aspx";
    }
}
