using System;
using System.Net;
using System.Drawing;
using libeveapi.ResponseObjects.Parsers;

namespace libeveapi
{
    /// <summary>
    /// 
    /// </summary>
    public class EveApi
    {

        /// <summary>
        /// Sets Domain to 'http://localhost/eveapi' if prefix is null
        /// otherwise sets Domain to prefix
        /// </summary>
        /// <param name="prefix">Domain prefix to use</param>
        public static void UseLocalUrls(string prefix)
        {
            if (prefix.CompareTo(string.Empty) == 0)
            {
                Constants.ApiPrefix = "http://localhost/eveapi";
                Constants.ImageFullURL = "http://localhost/eveapi/test.jpg";
            }
            else
            {
                Constants.ApiPrefix = prefix;
                Constants.ImageFullURL = "http://localhost/eveapi/test.jpg";
            }
        }

        /// <summary>
        /// Resets the Url Domain to the EVE Api domain
        /// </summary>
        public static void UseEveApiUrls()
        {
            Constants.ApiPrefix = "http://api.eve-online.com";
            Constants.ImageFullURL = "http://img.eve.is/serv.asp";
        }

        /// <summary>
        /// Sets a proxy server for the connection to run through
        /// </summary>
        /// <param name="url">The url for the proxy server</param>
        /// <param name="port">The port for the proxy server</param>
        /// <returns></returns>
        public static void SetProxy(string url, int port)
        {
            Network.eveNetworkClientSettings.proxy = new WebProxy(url, port);
        }

        /// <summary>
        /// Sets a proxy server for the connection to run through
        /// </summary>
        /// <param name="url">The url for the proxy server</param>
        /// <param name="port">The port for the proxy server</param>
        /// <param name="username">The username for the proxy server</param>
        /// <param name="password">The password for the proxy server</param>
        /// <returns></returns>
        public static void SetProxy(string url, int port, string username, string password)
        {
            Network.eveNetworkClientSettings.proxy = new WebProxy(url, port);
            Network.eveNetworkClientSettings.proxy.Credentials = new NetworkCredential(username, password);
        }

        /// <summary>
        /// Unsets the proxy server
        /// </summary>
        /// <returns></returns>
        public static void UnsetProxy()
        {
            Network.eveNetworkClientSettings.proxy = null;
        }

        /// <summary>
        /// Allows modification of the user agent to add the program's name into the request for tracking
        /// </summary>
        /// <param name="userAgent">The userAgent string to add to all outgoing webrequests</param>
        /// <returns></returns>
        public static void SetUserAgent(string userAgent)
        {
            Network.eveNetworkClientSettings.userAgent = "libEveApi/1 (" + userAgent + ")";
        }

        /// <summary>
        /// Returns a list of all characters on an account
        /// </summary>
        /// <param name="userId">userId of the account for authentication</param>
        /// <param name="apiKey">limited or full access api key of account</param>
        /// <returns></returns>
        public static CharacterList GetAccountCharacters(int userId, string apiKey)
        {
            return GetAccountCharacters(userId, apiKey, false);
        }

        /// <summary>
        /// Returns a list of all characters on an account
        /// </summary>
        /// <param name="userId">userId of the account for authentication</param>
        /// <param name="apiKey">limited or full access api key of account</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static CharacterList GetAccountCharacters(int userId, string apiKey, bool ignoreCacheUntil)
        {
            var url = new ApiRequestUrl(Constants.CharacterList);
            url.AddProperty(ApiRequestUrl.PROPERTY_USER_ID, userId.ToString());
            url.AddProperty(ApiRequestUrl.PROPERTY_API_KEY, apiKey);

            return HandleRequest(url, new CharacterListResponseParser(), ignoreCacheUntil);
        }

                /// <summary>
        /// Returns the ISK balance of a corporation or character
        /// </summary>
        /// <param name="accountBalanceType">retrieve balance for character or corporation</param>
        /// <param name="userId">user ID of account for authentication</param>
        /// <param name="characterId">
        /// For character balance: The character you are requesting data for
        /// For corporation balance: Character Id of a char with director/CEO access in the corp you want the balance for
        /// </param>
        /// <param name="fullApiKey">Full access api key of account</param>
        /// <returns></returns>
        public static AccountBalance GetAccountBalance(AccountBalanceType accountBalanceType, int userId, int characterId, string fullApiKey)
        {
            return GetAccountBalance(accountBalanceType, userId, characterId, fullApiKey, false);
        }

        /// <summary>
        /// Returns the ISK balance of a corporation or character
        /// </summary>
        /// <param name="accountBalanceType">retrieve balance for character or corporation</param>
        /// <param name="userId">user ID of account for authentication</param>
        /// <param name="characterId">
        /// For character balance: The character you are requesting data for
        /// For corporation balance: Character Id of a char with director/CEO access in the corp you want the balance for
        /// </param>
        /// <param name="fullApiKey">Full access api key of account</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static AccountBalance GetAccountBalance(AccountBalanceType accountBalanceType, int userId, int characterId, string fullApiKey, bool ignoreCacheUntil)
        {
            string apiPath = string.Empty;
            switch (accountBalanceType)
            {
                case AccountBalanceType.Character:
                    apiPath = Constants.CharacterAccountBalance;
                    break;
                case AccountBalanceType.Corporation:
                    apiPath = Constants.CorpAccountBalance;
                    break;
            }

            var url = new ApiRequestUrl(apiPath);
            AddCommonCharacterInformation(url, userId, characterId, fullApiKey);

            return HandleRequest(url, new AccountBalanceResponseParser(), ignoreCacheUntil);
        }

        /// <summary>
        /// Returns a list of starbases owned by a corporation
        /// </summary>
        /// <param name="userId">user Id of account for authentication</param>
        /// <param name="characterId">Character Id of a char with director/CEO access in the corp you want the starbases for</param>
        /// <param name="fullApiKey">Full access api key of account</param>
        /// <returns></returns>
        public static StarbaseList GetStarbaseList(int userId, int characterId, string fullApiKey)
        {
            return GetStarbaseList(userId, characterId, fullApiKey, false);
        }

        /// <summary>
        /// Returns a list of starbases owned by a corporation
        /// </summary>
        /// <param name="userId">user Id of account for authentication</param>
        /// <param name="characterId">Character Id of a char with director/CEO access in the corp you want the starbases for</param>
        /// <param name="fullApiKey">Full access api key of account</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static StarbaseList GetStarbaseList(int userId, int characterId, string fullApiKey, bool ignoreCacheUntil)
        {
            var url = new ApiRequestUrl(Constants.StarbaseList);
            AddCommonCharacterInformation(url, userId, characterId, fullApiKey);
            url.AddProperty(ApiRequestUrl.PROPERTY_VERSION, "2");

            return HandleRequest(url, new StarbaseListResponseParser(), ignoreCacheUntil);
        }

        /// <summary>
        /// Returns the character id and character name, given the one or the other
        /// </summary>
        /// <param name="charactername">character name string, use to look up character id</param>
        /// <returns></returns>
        public static CharacterIdName GetCharacterIdName(string charactername)
        {
            return GetCharacterIdName(charactername, false);
        }

        /// <summary>
        /// Returns the character id and character name, given the one or the other
        /// </summary>
        /// <param name="charactername">character name string, use to look up character id</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static CharacterIdName GetCharacterIdName(string charactername, bool ignoreCacheUntil)
        {
            var url = new ApiRequestUrl(Constants.CharacterIdName);
            url.AddProperty(ApiRequestUrl.PROPERTY_NAMES, charactername);

            return HandleRequest(url, new CharacterIdNameResponseParser(), ignoreCacheUntil);
        }

        /// <summary>
        /// Returns the character id and character name, given the one or the other
        /// </summary>
        /// <param name="characterId">characterId used to look up character name</param>
        /// <returns></returns>
        public static CharacterIdName GetCharacterIdName(int characterId)
        {
            return GetCharacterIdName(characterId, false);
        }


        /// <summary>
        /// Returns the character id and character name, given the one or the other
        /// </summary>
        /// <param name="characterId">characterId used to look up character name</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static CharacterIdName GetCharacterIdName(int characterId, bool ignoreCacheUntil)
        {
            var url = new ApiRequestUrl(Constants.CharacterIdName);
            url.AddProperty(ApiRequestUrl.PROPERTY_IDENTIFICATIONS, characterId.ToString());

            return HandleRequest(url, new CharacterIdNameResponseParser(), ignoreCacheUntil);
        }

        /// <summary>
        /// Returns the settings and fuel status of a starbase
        /// </summary>
        /// <param name="userId">user Id of account for authentication</param>
        /// <param name="characterId">Character Id of a char with director/CEO access in the corp that owns the starbase</param>
        /// <param name="fullApiKey">Full access api key of account</param>
        /// <param name="itemId">Item Id of the starbase as given in the starbase list</param>
        /// <returns></returns>
        public static StarbaseDetail GetStarbaseDetail(int userId, int characterId, string fullApiKey, int itemId)
        {
            return GetStarbaseDetail(userId, characterId, fullApiKey, itemId, false);
        }

        /// <summary>
        /// Returns the settings and fuel status of a starbase
        /// </summary>
        /// <param name="userId">user Id of account for authentication</param>
        /// <param name="characterId">Character Id of a char with director/CEO access in the corp that owns the starbase</param>
        /// <param name="fullApiKey">Full access api key of account</param>
        /// <param name="itemId">Item Id of the starbase as given in the starbase list</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static StarbaseDetail GetStarbaseDetail(int userId, int characterId, string fullApiKey, int itemId, bool ignoreCacheUntil)
        {
            var url = new ApiRequestUrl(Constants.StarbaseDetails);
            AddCommonCharacterInformation(url, userId, characterId, fullApiKey);
            url.AddProperty(ApiRequestUrl.PROPERTY_ITEM_ID, itemId.ToString());
            url.AddProperty(ApiRequestUrl.PROPERTY_VERSION, "2");

            return HandleRequest(url, new StarbaseDetailResponseParser(), ignoreCacheUntil);
        }

        /// <summary>
        /// Returns a list of error codes that can be returned by the EVE API servers
        /// </summary>
        /// <returns></returns>
        public static ErrorList GetErrorList()
        {
            return GetErrorList(false);
        }

        /// <summary>
        /// Returns a list of error codes that can be returned by the EVE API servers
        /// </summary>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static ErrorList GetErrorList(bool ignoreCacheUntil)
        {
            var url = new ApiRequestUrl(Constants.ErrorList);
            url.AddProperty(ApiRequestUrl.PROPERTY_VERSION, "2");

            return HandleRequest(url, new ErrorListResponseParser(), ignoreCacheUntil);
        }

        /// <summary>
        /// Returns a list of assets owned by a character or corporation.
        /// </summary>
        /// <param name="assetListType"><see cref="AssetListType" /></param>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="fullApiKey">Full access API key of account</param>
        /// <returns></returns>
        public static AssetList GetAssetList(AssetListType assetListType, int userId, int characterId, string fullApiKey)
        {
            return GetAssetList(assetListType, userId, characterId, fullApiKey, false);
        }

        /// <summary>
        /// Returns a list of assets owned by a character or corporation.
        /// </summary>
        /// <param name="assetListType"><see cref="AssetListType" /></param>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="fullApiKey">Full access API key of account</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static AssetList GetAssetList(AssetListType assetListType, int userId, int characterId, string fullApiKey, bool ignoreCacheUntil)
        {
            string apiPath = string.Empty;
            switch (assetListType)
            {
                case AssetListType.Character:
                    apiPath = Constants.CharAssetList;
                    break;
                case AssetListType.Corporation:
                    apiPath = Constants.CorpAssetList;
                    break;
            }

            var url = new ApiRequestUrl(apiPath);
            AddCommonCharacterInformation(url, userId, characterId, fullApiKey);
            url.AddProperty(ApiRequestUrl.PROPERTY_VERSION, "2");

            return HandleRequest(url, new AssetListResponseParser(), ignoreCacheUntil);
        }

        /// <summary>
        /// Retrieves the Kill Log for a character or corporation
        /// </summary>
        /// <param name="killLogType">KillLogType -- Character/Corporation which kill log do you want to retrieve</param>
        /// <param name="userId">User ID for authentication</param>
        /// <param name="characterId">The character your requesting data for</param>
        /// <param name="fullApiKey">Full Api Key for the account</param>
        /// <param name="beforeKillID">Returns the most recent kills before the specified Kill ID - used for scrolling back through the log</param>
        /// <returns>Kill Log object containing the array of kills</returns>
        public static KillLog GetKillLog(KillLogType killLogType, int userId, int characterId, string fullApiKey, int beforeKillID)
        {
            return GetKillLog(killLogType, userId, characterId, fullApiKey, beforeKillID, false);
        }

        /// <summary>
        /// Retrieves the Kill Log for a character or corporation
        /// </summary>
        /// <param name="killLogType">KillLogType -- Character/Corporation which kill log do you want to retrieve</param>
        /// <param name="userId">User ID for authentication</param>
        /// <param name="characterId">The character your requesting data for</param>
        /// <param name="fullApiKey">Full Api Key for the account</param>
        /// <param name="beforeKillID">Returns the most recent kills before the specified Kill ID - used for scrolling back through the log</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns>Kill Log object containing the array of kills</returns>
        public static KillLog GetKillLog(KillLogType killLogType, int userId, int characterId, string fullApiKey, int beforeKillID, bool ignoreCacheUntil)
        {
            string apiPath = string.Empty;
            switch (killLogType)
            {
                case KillLogType.Character:
                    apiPath = Constants.CharKillLog;
                    break;
                case KillLogType.Corporation:
                    apiPath = Constants.CorpKillLog;
                    break;
            }

            var url = new ApiRequestUrl(apiPath);
            AddCommonCharacterInformation(url, userId, characterId, fullApiKey);

            if (beforeKillID != 0)
            {
                url.AddProperty(ApiRequestUrl.PROPERTY_BEFORE_KILL_ID, beforeKillID.ToString());
            }

            url.AddProperty(ApiRequestUrl.PROPERTY_VERSION, "2");

            return HandleRequest(url, new KillLogResponseParser(), ignoreCacheUntil);
        }

        /// <summary>
        /// Retrieves the Kill Log for a character or corporation
        /// </summary>
        /// <param name="killLogType">KillLogType -- Character/Corporation which kill log do you want to retrieve</param>
        /// <param name="userId">User ID for authentication</param>
        /// <param name="characterId">The character your requesting data for</param>
        /// <param name="fullApiKey">Full Api Key for the account</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns>Kill Log object containing the array of kills</returns>
        public static KillLog GetKillLog(KillLogType killLogType, int userId, int characterId, string fullApiKey, bool ignoreCacheUntil)
        {
            return GetKillLog(killLogType, userId, characterId, fullApiKey, 0, ignoreCacheUntil);
        }

        /// <summary>
        /// Retrieves the Kill Log for a character or corporation
        /// </summary>
        /// <param name="killLogType">KillLogType -- Character/Corporation which kill log do you want to retrieve</param>
        /// <param name="userId">User ID for authentication</param>
        /// <param name="characterId">The character your requesting data for</param>
        /// <param name="fullApiKey">Full Api Key for the account</param>
        /// <returns>Kill Log object containing the array of kills</returns>
        public static KillLog GetKillLog(KillLogType killLogType, int userId, int characterId, string fullApiKey)
        {
            return GetKillLog(killLogType, userId, characterId, fullApiKey, 0, false);
        }

        /// <summary>
        /// Returns a list of industrial jobs owned by a character or corporation.
        /// </summary>
        /// <param name="industryJobListType"><see cref="IndustryJobListType" /></param>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="fullApiKey">Full access API key of account</param>
        /// <returns></returns>
        public static IndustryJobList GetIndustryJobList(IndustryJobListType industryJobListType, int userId, int characterId, string fullApiKey)
        {
            return GetIndustryJobList(industryJobListType, userId, characterId, fullApiKey, false);
        }
        
        /// <summary>
        /// Returns a list of industrial jobs owned by a character or corporation.
        /// </summary>
        /// <param name="industryJobListType"><see cref="IndustryJobListType" /></param>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="fullApiKey">Full access API key of account</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static IndustryJobList GetIndustryJobList(IndustryJobListType industryJobListType, int userId, int characterId, string fullApiKey, bool ignoreCacheUntil)
        {
            string apiPath = string.Empty;
            switch (industryJobListType)
            {
                case IndustryJobListType.Character:
                    apiPath = Constants.CharIndustryJobs;
                    break;
                case IndustryJobListType.Corporation:
                    apiPath = Constants.CorpIndustryJobs;
                    break;
            }

            var url = new ApiRequestUrl(apiPath);
            AddCommonCharacterInformation(url, userId, characterId, fullApiKey);
            url.AddProperty(ApiRequestUrl.PROPERTY_VERSION, "2");

            return HandleRequest(url, new IndustryJobListResponseParser(), ignoreCacheUntil);
        }

        /// <summary>
        /// Returns a list of journal entries owned by a character or corporation.
        /// </summary>
        /// <param name="journalEntriesType"><see cref="JournalEntryType" /></param>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="fullApiKey">Full access API key of account</param>
        /// <returns></returns>
        public static JournalEntries GetJournalEntryList(JournalEntryType journalEntriesType, int userId, int characterId, string fullApiKey)
        {
            return GetJournalEntryList(journalEntriesType, userId, characterId, fullApiKey, 0, false, WalletDivision.Master);
        }

        /// <summary>
        /// Returns a list of journal entries owned by a character or corporation.
        /// </summary>
        /// <param name="journalEntriesType"><see cref="JournalEntryType" /></param>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="fullApiKey">Full access API key of account</param>
        /// <param name="walletDivision">The (corporation) wallet division to retrieve journal entries from</param>
        /// <returns></returns>
        public static JournalEntries GetJournalEntryList(JournalEntryType journalEntriesType, int userId, int characterId, string fullApiKey, WalletDivision walletDivision)
        {
            return GetJournalEntryList(journalEntriesType, userId, characterId, fullApiKey, 0, false, walletDivision);
        }

        /// <summary>
        /// Returns a list of journal entries owned by a character or corporation.
        /// </summary>
        /// <param name="journalEntriesType"><see cref="JournalEntryType" /></param>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="fullApiKey">Full access API key of account</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static JournalEntries GetJournalEntryList(JournalEntryType journalEntriesType, int userId, int characterId, string fullApiKey, bool ignoreCacheUntil)
        {
            return GetJournalEntryList(journalEntriesType, userId, characterId, fullApiKey, 0, ignoreCacheUntil, WalletDivision.Master);
        }

        /// <summary>
        /// Returns a list of journal entries owned by a character or corporation.
        /// </summary>
        /// <param name="journalEntriesType"><see cref="JournalEntryType" /></param>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="fullApiKey">Full access API key of account</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <param name="walletDivision">The (corporation) wallet division to retrieve journal entries from</param>
        /// <returns></returns>
        public static JournalEntries GetJournalEntryList(JournalEntryType journalEntriesType, int userId, int characterId, string fullApiKey, bool ignoreCacheUntil, WalletDivision walletDivision)
        {
            return GetJournalEntryList(journalEntriesType, userId, characterId, fullApiKey, 0, ignoreCacheUntil, walletDivision);
        }

        /// <summary>
        /// Returns a list of journal entries owned by a character or corporation.
        /// </summary>
        /// <param name="journalEntriesType"><see cref="JournalEntryType" /></param>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="fullApiKey">Full access API key of account</param>
        /// <param name="beforeRefId">Retrieve entries after this refId</param>
        /// <returns></returns>
        public static JournalEntries GetJournalEntryList(JournalEntryType journalEntriesType, int userId, int characterId, string fullApiKey, int beforeRefId)
        {
            return GetJournalEntryList(journalEntriesType, userId, characterId, fullApiKey, beforeRefId, false, WalletDivision.Master);
        }

        /// <summary>
        /// Returns a list of journal entries owned by a character or corporation.
        /// </summary>
        /// <param name="journalEntriesType"><see cref="JournalEntryType" /></param>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="fullApiKey">Full access API key of account</param>
        /// <param name="beforeRefId">Retrieve entries after this refId</param>
        /// <param name="walletDivision">The (corporation) wallet division to retrieve journal entries from</param>
        /// <returns></returns>
        public static JournalEntries GetJournalEntryList(JournalEntryType journalEntriesType, int userId, int characterId, string fullApiKey, int beforeRefId, WalletDivision walletDivision)
        {
            return GetJournalEntryList(journalEntriesType, userId, characterId, fullApiKey, beforeRefId, false, walletDivision);
        }

        /// <summary>
        /// Returns a list of journal entries owned by a character or corporation.
        /// </summary>
        /// <param name="journalEntriesType"><see cref="JournalEntryType" /></param>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="fullApiKey">Full access API key of account</param>
        /// <param name="beforeRefId">Retrieve entries after this refId</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static JournalEntries GetJournalEntryList(JournalEntryType journalEntriesType, int userId, int characterId, string fullApiKey, int beforeRefId, bool ignoreCacheUntil)
        {
            return GetJournalEntryList(journalEntriesType, userId, characterId, fullApiKey, beforeRefId, ignoreCacheUntil, WalletDivision.Master);
        }

        /// <summary>
        /// Returns a list of journal entries owned by a character or corporation.
        /// </summary>
        /// <param name="journalEntriesType"><see cref="JournalEntryType" /></param>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="fullApiKey">Full access API key of account</param>
        /// <param name="beforeRefId">Retrieve entries after this refId</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <param name="walletDivision">The (corporation) wallet division to retrieve journal entries from</param>
        /// <returns></returns>
        public static JournalEntries GetJournalEntryList(JournalEntryType journalEntriesType, int userId, int characterId, string fullApiKey, int beforeRefId, bool ignoreCacheUntil, WalletDivision walletDivision)
        {
            string apiPath = string.Empty;
            switch (journalEntriesType)
            {
                case JournalEntryType.Character:
                    apiPath = Constants.CharJournalEntries;
                    break;
                case JournalEntryType.Corporation:
                    apiPath = Constants.CorpJournalEntries;
                    break;
            }

            var url = new ApiRequestUrl(apiPath);
            AddCommonCharacterInformation(url, userId, characterId, fullApiKey);
            
            if (beforeRefId != 0)
            {
                url.AddProperty(ApiRequestUrl.PROPERTY_BEFORE_REF_ID, beforeRefId.ToString());
            }

            if(journalEntriesType == JournalEntryType.Corporation)
            {
                url.AddProperty(ApiRequestUrl.PROPERTY_ACCOUNT_KEY, ((int)walletDivision).ToString());
            }

            return HandleRequest(url, new JournalEntriesResponseParser(), ignoreCacheUntil);
        }

        /// <summary>
        /// Returns a list of wallet journal entries owned by a character or corporation.
        /// </summary>
        /// <param name="walletJournalType"><see cref="WalletJournalType" /></param>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="fullApiKey">Full access API key of account</param>
        /// <returns>Wallet Journal Entries</returns>
        public static WalletJournal GetWalletJournal(WalletJournalType walletJournalType, int userId, int characterId, string fullApiKey)
        {
            return GetWalletJournal(walletJournalType, userId, characterId, fullApiKey, 0, false, WalletDivision.Master);
        }

        /// <summary>
        /// Returns a list of wallet journal entries owned by a character or corporation.
        /// </summary>
        /// <param name="walletJournalType"><see cref="WalletJournalType" /></param>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="fullApiKey">Full access API key of account</param>
        /// <param name="walletDivision">The (corporation) wallet division to retrieve journal entries from</param>
        /// <returns>Wallet Journal Entries</returns>
        public static WalletJournal GetWalletJournal(WalletJournalType walletJournalType, int userId, int characterId, string fullApiKey, WalletDivision walletDivision)
        {
            return GetWalletJournal(walletJournalType, userId, characterId, fullApiKey, 0, false, walletDivision);
        }

        /// <summary>
        /// Returns a list of wallet journal entries owned by a character or corporation.
        /// </summary>
        /// <param name="walletJournalType"><see cref="WalletJournalType" /></param>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="fullApiKey">Full access API key of account</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns>Wallet Journal Entries</returns>
        public static WalletJournal GetWalletJournal(WalletJournalType walletJournalType, int userId, int characterId, string fullApiKey, bool ignoreCacheUntil)
        {
            return GetWalletJournal(walletJournalType, userId, characterId, fullApiKey, 0, ignoreCacheUntil, WalletDivision.Master);
        }

        /// <summary>
        /// Returns a list of wallet journal entries owned by a character or corporation.
        /// </summary>
        /// <param name="walletJournalType"><see cref="WalletJournalType" /></param>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="fullApiKey">Full access API key of account</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <param name="walletDivision">The (corporation) wallet division to retrieve journal entries from</param>
        /// <returns>Wallet Journal Entries</returns>
        public static WalletJournal GetWalletJournal(WalletJournalType walletJournalType, int userId, int characterId, string fullApiKey, bool ignoreCacheUntil, WalletDivision walletDivision)
        {
            return GetWalletJournal(walletJournalType, userId, characterId, fullApiKey, 0, ignoreCacheUntil, walletDivision);
        }

        /// <summary>
        /// Returns a list of wallet journal entries owned by a character or corporation.
        /// </summary>
        /// <param name="walletJournalType"><see cref="WalletJournalType" /></param>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="fullApiKey">Full access API key of account</param>
        /// <param name="beforeRefId">Retrieve entries after this refId</param>
        /// <returns>Wallet Journal Entries</returns>
        public static WalletJournal GetWalletJournal(WalletJournalType walletJournalType, int userId, int characterId, string fullApiKey, int beforeRefId)
        {
            return GetWalletJournal(walletJournalType, userId, characterId, fullApiKey, beforeRefId, false, WalletDivision.Master);
        }

        /// <summary>
        /// Returns a list of wallet journal entries owned by a character or corporation.
        /// </summary>
        /// <param name="walletJournalType"><see cref="WalletJournalType" /></param>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="fullApiKey">Full access API key of account</param>
        /// <param name="beforeRefId">Retrieve entries after this refId</param>
        /// <param name="walletDivision">The (corporation) wallet division to retrieve journal entries from</param>
        /// <returns>Wallet Journal Entries</returns>
        public static WalletJournal GetWalletJournal(WalletJournalType walletJournalType, int userId, int characterId, string fullApiKey, int beforeRefId, WalletDivision walletDivision)
        {
            return GetWalletJournal(walletJournalType, userId, characterId, fullApiKey, beforeRefId, false, walletDivision);
        }

        /// <summary>
        /// Returns a list of wallet journal entries owned by a character or corporation.
        /// </summary>
        /// <param name="walletJournalType"><see cref="WalletJournalType" /></param>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="fullApiKey">Full access API key of account</param>
        /// <param name="beforeRefId">Retrieve entries after this refId</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns>Wallet Journal Entries</returns>
        public static WalletJournal GetWalletJournal(WalletJournalType walletJournalType, int userId, int characterId, string fullApiKey, int beforeRefId, bool ignoreCacheUntil)
        {
            return GetWalletJournal(walletJournalType, userId, characterId, fullApiKey, beforeRefId, ignoreCacheUntil, WalletDivision.Master);
        }

        /// <summary>
        /// Returns a list of wallet journal entries owned by a character or corporation.
        /// </summary>
        /// <param name="walletJournalType"><see cref="WalletJournalType" /></param>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="fullApiKey">Full access API key of account</param>
        /// <param name="beforeRefId">Retrieve entries after this refId</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <param name="walletDivision">The (corporation) wallet division to retrieve journal entries from</param>
        /// <returns>Wallet Journal Entries</returns>
        public static WalletJournal GetWalletJournal(WalletJournalType walletJournalType, int userId, int characterId, string fullApiKey, int beforeRefId, bool ignoreCacheUntil, WalletDivision walletDivision)
        {
            string apiPath = string.Empty;
            switch (walletJournalType)
            {
                case WalletJournalType.Character:
                    apiPath = Constants.CharJournalEntries;
                    break;
                case WalletJournalType.Corporation:
                    apiPath = Constants.CorpJournalEntries;
                    break;
            }

            var url = new ApiRequestUrl(apiPath);
            AddCommonCharacterInformation(url, userId, characterId, fullApiKey);

            if (beforeRefId != 0)
            {
                url.AddProperty(ApiRequestUrl.PROPERTY_BEFORE_REF_ID, beforeRefId.ToString());
            }

            if (walletJournalType == WalletJournalType.Corporation)
            {
                url.AddProperty(ApiRequestUrl.PROPERTY_ACCOUNT_KEY, ((int)walletDivision).ToString());
            }

            return HandleRequest(url, new WalletJournalResponseParser(), ignoreCacheUntil);
        }



        /// <summary>
        /// Returns a list of market orders owned by a character or corporation.
        /// </summary>
        /// <param name="marketOrdersListType"><see cref="MarketOrdersListType" /></param>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="fullApiKey">Full access API key of account</param>
        /// <returns></returns>
        public static MarketOrders GetMarketOrderList(MarketOrdersListType marketOrdersListType, int userId, int characterId, string fullApiKey)
        {
            return GetMarketOrderList(marketOrdersListType, userId, characterId, fullApiKey, false);
        }

        /// <summary>
        /// Returns a list of market orders owned by a character or corporation.
        /// </summary>
        /// <param name="marketOrdersListType"><see cref="MarketOrdersListType" /></param>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="fullApiKey">Full access API key of account</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static MarketOrders GetMarketOrderList(MarketOrdersListType marketOrdersListType, int userId, int characterId, string fullApiKey, bool ignoreCacheUntil)
        {
            string apiPath = string.Empty;
            switch (marketOrdersListType)
            {
                case MarketOrdersListType.Character:
                    apiPath = Constants.CharMarketOrders;
                    break;
                case MarketOrdersListType.Corporation:
                    apiPath = Constants.CorpMarketOrders;
                    break;
            }

            var url = new ApiRequestUrl(apiPath);
            AddCommonCharacterInformation(url, userId, characterId, fullApiKey);
            url.AddProperty(ApiRequestUrl.PROPERTY_VERSION, "2");

            return HandleRequest(url, new MarketOrdersResponseParser(), ignoreCacheUntil);
        }

        /// <summary>
        /// Returns a list of market transactions (wallet transactions) owned by a character or corporation.
        /// </summary>
        /// <param name="walletTransactionType"><see cref="WalletTransactionListType" /></param>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="fullApiKey">Full access API key of account</param>
        /// <returns></returns>
        public static WalletTransactions GetWalletTransactionsList(WalletTransactionListType walletTransactionType, int userId, int characterId, string fullApiKey)
        {
            return GetWalletTransactionsList(walletTransactionType, userId, characterId, fullApiKey, 0, false, WalletDivision.Master);
        }

        /// <summary>
        /// Returns a list of market transactions (wallet transactions) owned by a character or corporation.
        /// </summary>
        /// <param name="walletTransactionType"><see cref="WalletTransactionListType" /></param>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="fullApiKey">Full access API key of account</param>
        /// <param name="walletDivision">The (corporation) wallet division to retrieve transaction entries from</param>
        /// <returns></returns>
        public static WalletTransactions GetWalletTransactionsList(WalletTransactionListType walletTransactionType, int userId, int characterId, string fullApiKey, WalletDivision walletDivision)
        {
            return GetWalletTransactionsList(walletTransactionType, userId, characterId, fullApiKey, 0, false, walletDivision);
        }

        /// <summary>
        /// Returns a list of market transactions (wallet transactions) owned by a character or corporation.
        /// </summary>
        /// <param name="walletTransactionType"><see cref="WalletTransactionListType" /></param>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="fullApiKey">Full access API key of account</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static WalletTransactions GetWalletTransactionsList(WalletTransactionListType walletTransactionType, int userId, int characterId, string fullApiKey, bool ignoreCacheUntil)
        {
            return GetWalletTransactionsList(walletTransactionType, userId, characterId, fullApiKey, 0, ignoreCacheUntil, WalletDivision.Master);
        }

        /// <summary>
        /// Returns a list of market transactions (wallet transactions) owned by a character or corporation.
        /// </summary>
        /// <param name="walletTransactionType"><see cref="WalletTransactionListType" /></param>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="fullApiKey">Full access API key of account</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <param name="walletDivision">The (corporation) wallet division to retrieve transaction entries from</param>
        /// <returns></returns>
        public static WalletTransactions GetWalletTransactionsList(WalletTransactionListType walletTransactionType, int userId, int characterId, string fullApiKey, bool ignoreCacheUntil, WalletDivision walletDivision)
        {
            return GetWalletTransactionsList(walletTransactionType, userId, characterId, fullApiKey, 0, ignoreCacheUntil, walletDivision);
        }

        /// <summary>
        /// Returns a list of market transactions (wallet transactions) owned by a character or corporation.
        /// </summary>
        /// <param name="walletTransactionType"><see cref="WalletTransactionListType" /></param>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="fullApiKey">Full access API key of account</param>
        /// <param name="beforeTransId">retrieve up to 1000 entries after this transactionId</param>
        /// <returns></returns>
        public static WalletTransactions GetWalletTransactionsList(WalletTransactionListType walletTransactionType, int userId, int characterId, string fullApiKey, int beforeTransId)
        {
            return GetWalletTransactionsList(walletTransactionType, userId, characterId, fullApiKey, beforeTransId, false, WalletDivision.Master);
        }

        /// <summary>
        /// Returns a list of market transactions (wallet transactions) owned by a character or corporation.
        /// </summary>
        /// <param name="walletTransactionType"><see cref="WalletTransactionListType" /></param>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="fullApiKey">Full access API key of account</param>
        /// <param name="beforeTransId">retrieve up to 1000 entries after this transactionId</param>
        /// <param name="walletDivision">The (corporation) wallet division to retrieve transaction entries from</param>
        /// <returns></returns>
        public static WalletTransactions GetWalletTransactionsList(WalletTransactionListType walletTransactionType, int userId, int characterId, string fullApiKey, int beforeTransId, WalletDivision walletDivision)
        {
            return GetWalletTransactionsList(walletTransactionType, userId, characterId, fullApiKey, beforeTransId, false, walletDivision);
        }

                /// <summary>
        /// Returns a list of market transactions (wallet transactions) owned by a character or corporation.
        /// </summary>
        /// <param name="walletTransactionType"><see cref="WalletTransactionListType" /></param>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="fullApiKey">Full access API key of account</param>
        /// <param name="beforeTransId">retrieve up to 1000 entries after this transactionId</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static WalletTransactions GetWalletTransactionsList(WalletTransactionListType walletTransactionType, int userId, int characterId, string fullApiKey, int beforeTransId, bool ignoreCacheUntil)
        {
            return GetWalletTransactionsList(walletTransactionType, userId, characterId, fullApiKey, beforeTransId, ignoreCacheUntil, WalletDivision.Master);
        }

        /// <summary>
        /// Returns a list of market transactions (wallet transactions) owned by a character or corporation.
        /// </summary>
        /// <param name="walletTransactionType"><see cref="WalletTransactionListType" /></param>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="fullApiKey">Full access API key of account</param>
        /// <param name="beforeTransId">retrieve up to 1000 entries after this transactionId</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <param name="walletDivision">The (corporation) wallet division to retrieve transaction entries from</param>
        /// <returns></returns>
        public static WalletTransactions GetWalletTransactionsList(WalletTransactionListType walletTransactionType, int userId, int characterId, string fullApiKey, int beforeTransId, bool ignoreCacheUntil, WalletDivision walletDivision)
        {
            string apiPath = string.Empty;
            switch (walletTransactionType)
            {
                case WalletTransactionListType.Character:
                    apiPath = Constants.CharWalletTransactions;
                    break;
                case WalletTransactionListType.Corporation:
                    apiPath = Constants.CorpWalletTransactions;
                    break;
            }

            var url = new ApiRequestUrl(apiPath);
            AddCommonCharacterInformation(url, userId, characterId, fullApiKey);

            if(beforeTransId != 0)
            {
                url.AddProperty(ApiRequestUrl.PROPERTY_BEFORE_TRANSACTION_ID, beforeTransId.ToString());
            }

            url.AddProperty(ApiRequestUrl.PROPERTY_VERSION, "2");

            if(walletTransactionType == WalletTransactionListType.Corporation)
            {
                url.AddProperty(ApiRequestUrl.PROPERTY_ACCOUNT_KEY, ((int)walletDivision).ToString());
            }

            return HandleRequest(url, new WalletTransactionsResponseParser(), ignoreCacheUntil);
        }

        /// <summary>
        /// Returns a list of RefTypes that are used by certain API Calls
        /// </summary>
        /// <returns></returns>
        public static RefTypes GetRefTypesList()
        {
            return GetRefTypesList(false);
        }

        /// <summary>
        /// Returns a list of RefTypes that are used by certain API Calls
        /// </summary>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static RefTypes GetRefTypesList(bool ignoreCacheUntil)
        {
            var url = new ApiRequestUrl(Constants.RefTypesList);
            url.AddProperty(ApiRequestUrl.PROPERTY_VERSION, "2");

            return HandleRequest(url, new RefTypesResponseParser(), ignoreCacheUntil);
        }

        /// <summary>
        /// Returns a list solar systems that have more than 0 jumps with the jump count
        /// </summary>
        /// <returns></returns>
        public static MapJumps GetMapJumps()
        {
            return GetMapJumps(false);
        }

        /// <summary>
        /// Returns a list solar systems that have more than 0 jumps with the jump count
        /// </summary>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static MapJumps GetMapJumps(bool ignoreCacheUntil)
        {
            var url = new ApiRequestUrl(Constants.MapJumps);
            url.AddProperty(ApiRequestUrl.PROPERTY_VERSION, "2");

            return HandleRequest(url, new MapJumpsResponseParser(), ignoreCacheUntil);
        }

        /// <summary>
        /// Returns a list solar systems that have sovereignty
        /// </summary>
        /// <returns></returns>
        public static MapSovereignty GetMapSovereignty()
        {
            return GetMapSovereignty(false);
        }

        /// <summary>
        /// Returns a list solar systems that have sovereignty
        /// </summary>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static MapSovereignty GetMapSovereignty(bool ignoreCacheUntil)
        {
            var url = new ApiRequestUrl(Constants.MapSoveignty);
            url.AddProperty(ApiRequestUrl.PROPERTY_VERSION, "2");

            return HandleRequest(url, new MapSovereigntyResponseParser(), ignoreCacheUntil);
        }

        /// <summary>
        /// Returns a list kills in solar systems with more than 0 kills
        /// </summary>
        /// <returns></returns>
        public static MapKills GetMapKills()
        {
            return GetMapKills(false);
        }


        /// <summary>
        /// Returns a list kills in solar systems with more than 0 kills
        /// </summary>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static MapKills GetMapKills(bool ignoreCacheUntil)
        {
            var url = new ApiRequestUrl(Constants.MapKills);
            url.AddProperty(ApiRequestUrl.PROPERTY_VERSION, "2");

            return HandleRequest(url, new MapKillsResponseParser(), ignoreCacheUntil);
        }

        /// <summary>
        /// Returns information on every member in the corporation. Information retrieved
        /// varies on your roles without within the corporation. Not valid for NPC corps.
        /// </summary>
        /// <param name="userId">user Id of account for authentication</param>
        /// <param name="characterId">Character Id of a char with director/CEO access in the corp that owns the starbase</param>
        /// <param name="fullApiKey">Full access api key of account</param>
        /// <returns></returns>
        public static MemberTracking GetMemberTracking(int userId, int characterId, string fullApiKey)
        {
            return GetMemberTracking(userId, characterId, fullApiKey, false);
        }

        /// <summary>
        /// Returns information on every member in the corporation. Information retrieved
        /// varies on your roles without within the corporation. Not valid for NPC corps.
        /// </summary>
        /// <param name="userId">user Id of account for authentication</param>
        /// <param name="characterId">Character Id of a char with director/CEO access in the corp that owns the starbase</param>
        /// <param name="fullApiKey">Full access api key of account</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static MemberTracking GetMemberTracking(int userId, int characterId, string fullApiKey, bool ignoreCacheUntil)
        {
            var url = new ApiRequestUrl(Constants.MemberTracking);
            AddCommonCharacterInformation(url, userId, characterId, fullApiKey);
            url.AddProperty(ApiRequestUrl.PROPERTY_VERSION, "1");

            return HandleRequest(url, new MemberTrackingResponseParser(), ignoreCacheUntil);
        }

        /// <summary>
        /// Returns a detailed description of a character
        /// </summary>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="apiKey">Limited access API key of account</param>
        /// <returns></returns>
        public static CharacterSheet GetCharacterSheet(int userId, int characterId, string apiKey)
        {
            return GetCharacterSheet(userId, characterId, apiKey, false);
        }

        /// <summary>
        /// Returns a detailed description of a character
        /// </summary>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="apiKey">Limited access API key of account</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static CharacterSheet GetCharacterSheet(int userId, int characterId, string apiKey, bool ignoreCacheUntil)
        {
            var url = new ApiRequestUrl(Constants.CharacterSheet);
            AddCommonCharacterInformation(url, userId, characterId, apiKey);

            return HandleRequest(url, new CharacterSheetResponseParser(), ignoreCacheUntil);
        }

        /// <summary>
        /// Returns information about a characters status
        /// </summary>
        /// <param name="userId">userId in api key</param>
        /// <param name="characterId">character Id of character we are interested in</param>
        /// <param name="apiKey">Key code from Api</param>
        /// <returns></returns>
        public static CharacterInfo GetCharacterInfo(int userId, int characterId, string apiKey)
        {
            return GetCharacterInfo(userId, characterId, apiKey, false);
        }
        /// <summary>
        /// Returns information about a characters status
        /// </summary>
        /// <param name="userId">userId in api key</param>
        /// <param name="characterId">character Id of character we are interested in</param>
        /// <param name="apiKey">Key code from Api</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired<</param>
        /// <returns></returns>
        public static CharacterInfo GetCharacterInfo(int userId, int characterId, string apiKey, bool ignoreCacheUntil)
        {
            var url = new ApiRequestUrl(Constants.CharacterInfo);
            AddCommonCharacterInformation(url, userId, characterId, apiKey);
            return HandleRequest(url, new CharacterInfoResponseParser(), ignoreCacheUntil);
        }

        /// <summary>
        /// Returns a detailed description of a corporation
        /// </summary>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="apiKey">Limited access API key of account</param>
        /// <returns></returns>
        public static CorporationSheet GetCorporationSheet(int userId, int characterId, string apiKey)
        {
            return GetCorporationSheet(userId, characterId, apiKey, 0, false);
        }

        /// <summary>
        /// Returns a detailed description of a corporation
        /// </summary>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="apiKey">Limited access API key of account</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static CorporationSheet GetCorporationSheet(int userId, int characterId, string apiKey, bool ignoreCacheUntil)
        {
            return GetCorporationSheet(userId, characterId, apiKey, 0, ignoreCacheUntil);
        }

        /// <summary>
        /// Returns a detailed description of a corporation
        /// </summary>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="apiKey">Limited access API key of account</param>
        /// <param name="corporationId">retrieve information on the corporation with this id</param>
        /// <returns></returns>
        public static CorporationSheet GetCorporationSheet(int userId, int characterId, string apiKey, int corporationId)
        {
            return GetCorporationSheet(userId, characterId, apiKey, corporationId, false);
        }

        /// <summary>
        /// Returns a detailed description of a corporation
        /// </summary>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="apiKey">Limited access API key of account</param>
        /// <param name="corporationId">retrieve information on the corporation with this id</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static CorporationSheet GetCorporationSheet(int userId, int characterId, string apiKey, int corporationId, bool ignoreCacheUntil)
        {
            var url = new ApiRequestUrl(Constants.CorporationSheet);
            AddCommonCharacterInformation(url, userId, characterId, apiKey);

            if(corporationId != 0)
            {
                url.AddProperty(ApiRequestUrl.PROPERTY_CORPORATION_ID, corporationId.ToString());
            }

            return HandleRequest(url, new CorporationSheetResponseParser(), ignoreCacheUntil);
        }

        /// <summary>
        /// Gets a list of conquerable stations from the api
        /// </summary>
        /// <returns></returns>
        public static ConquerableStationList GetConquerableStationList()
        {
            return GetConquerableStationList(false);
        }

        /// <summary>
        /// Gets a list of conquerable stations from the api
        /// </summary>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static ConquerableStationList GetConquerableStationList(bool ignoreCacheUntil)
        {
            var url = new ApiRequestUrl(Constants.ConquerableStationOutpost);
            url.AddProperty(ApiRequestUrl.PROPERTY_VERSION, "2");

            return HandleRequest(url, new ConquerableStationListResponseParser(), ignoreCacheUntil);
        }

        /// <summary>
        /// Gets a data structure containing information on every skill in the game.
        /// </summary>
        /// <returns></returns>
        public static SkillTree GetSkillTree()
        {
            return GetSkillTree(false);
        }

        /// <summary>
        /// Gets a data structure containing information on every skill in the game.
        /// </summary>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static SkillTree GetSkillTree(bool ignoreCacheUntil)
        {
            var url = new ApiRequestUrl(Constants.SkillTree);

            return HandleRequest(url, new SkillTreeResponseParser(), ignoreCacheUntil);
        }

        /// <summary>
        /// Gets a list of all alliances and their member corporations
        /// </summary>
        /// <returns></returns>
        public static AllianceList GetAllianceList()
        {
            return GetAllianceList(false);
        }

        /// <summary>
        /// Gets a list of all alliances and their member corporations
        /// </summary>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static AllianceList GetAllianceList(bool ignoreCacheUntil)
        {
            var url = new ApiRequestUrl(Constants.AllianceList);
            url.AddProperty(ApiRequestUrl.PROPERTY_VERSION, "1");

            return HandleRequest(url, new AllianceListResponseParser(), ignoreCacheUntil);
        }

        /// <summary>
        /// Get the currently training Skill for a character
        /// </summary>
        /// <param name="userId">User Id of account for authentication</param>
        /// <param name="characterId">Character Id of the character to get skill info for</param>
        /// <param name="apiKey">limited access API key of Account</param>
        /// <returns></returns>
        public static SkillInTraining GetSkillInTraining(int userId, int characterId, string apiKey)
        {
            return GetSkillInTraining(userId, characterId, apiKey, false);
        }

        /// <summary>
        /// Get the currently training Skill for a character
        /// </summary>
        /// <param name="userId">User Id of account for authentication</param>
        /// <param name="characterId">Character Id of the character to get skill info for</param>
        /// <param name="apiKey">limited access API key of Account</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static SkillInTraining GetSkillInTraining(int userId, int characterId, string apiKey, bool ignoreCacheUntil)
        {
            var url = new ApiRequestUrl(Constants.SkillInTraining);
            AddCommonCharacterInformation(url, userId, characterId, apiKey);

            return HandleRequest(url, new SkillInTrainingResponseParser(), ignoreCacheUntil);
        }

        /// <summary>
        /// Retrives the Training Skill Queue for the specified character
        /// </summary>
        /// <param name="userId">userID of account for authentication.</param>
        /// <param name="characterId">Character you wish to request data from.</param>
        /// <param name="apiKey">Limited access API key of account.</param>
        /// <returns>Array of skills</returns>
        public static SkillQueue GetSkillQueue(int userId, int characterId, string apiKey)
        {
            return GetSkillQueue(userId, characterId, apiKey, false);
        }

        /// <summary>
        /// Retrives the Training Skill Queue for the specified character
        /// </summary>
        /// <param name="userId">userID of account for authentication.</param>
        /// <param name="characterId">Character you wish to request data from.</param>
        /// <param name="apiKey">Limited access API key of account.</param>
        /// <param name="ignoreCacheUntil">Force Update</param>
        /// <returns>Array of skills</returns>
        public static SkillQueue GetSkillQueue(int userId, int characterId, string apiKey, bool ignoreCacheUntil)
        {
            var url = new ApiRequestUrl(Constants.SkillQueue);
            AddCommonCharacterInformation(url, userId, characterId, apiKey);
            return HandleRequest(url, new SkillQueueResponseParser(), ignoreCacheUntil);
        }

        /// <summary>
        /// Retrieve the portrait for a character
        /// </summary>
        /// <param name="characterId">Retrieve the portrait of the character with this id</param>
        /// <param name="portraitSize">Small (64) or Large (256)</param>
        /// <returns></returns>
        public static Image GetCharacterPortrait(int characterId, PortraitSize portraitSize)
        {
            int imageSize;
            if (portraitSize == PortraitSize.Large)
                imageSize = 256;
            else if(portraitSize == PortraitSize.Small)
                imageSize = 64;
            else
                imageSize = 64;

            string url = String.Format("{0}?c={1}&s={2}", Constants.ImageFullURL, characterId, imageSize);
            return Network.GetImage(url);
        }

        /// <summary>
        /// Retrieve current Tranquility status
        /// </summary>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static ServerStatus GetServerStatus(bool ignoreCacheUntil)
        {
            var url = new ApiRequestUrl(Constants.ServerStatus);
            return HandleRequest(url, new ServerStatusResponseParser(), ignoreCacheUntil);
        }

        /// <summary>
        /// Retrieve current Tranquility status
        /// </summary>
        /// <returns></returns>
        public static ServerStatus GetServerStatus()
        {
            return GetServerStatus(false);
        }

        /// <summary>
        /// Get a list of contestable solar systems and the NPC faction currently occupying them
        /// </summary>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static MapFacWarSystems GetFactionWarSystems(bool ignoreCacheUntil)
        {
            var url = new ApiRequestUrl(Constants.MapFactionWarSystems);
            return HandleRequest(url, new MapFacWarSystemsParser(), ignoreCacheUntil);
        }

        /// <summary>
        /// Get a list of contestable solar systems and the NPC faction currently occupying them
        /// </summary>
        /// <returns></returns>
        public static MapFacWarSystems GetFactionWarSystems()
        {
            return GetFactionWarSystems(false);
        }

        /// <summary>
        /// Handles a request to the <see cref="ApiRequestHandler{T}" />.
        /// </summary>
        /// <param name="url">The <see cref="ApiRequestUrl" /> to which the request should be sent.</param>
        /// <param name="parser">The <see cref="IApiResponseParser{T}" /> which should be used to parse the response.</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired.</param>
        /// <returns>The appropriate (parsed) <see cref="ApiResponse" /></returns>
        private static T HandleRequest<T>( ApiRequestUrl url, IApiResponseParser<T> parser, bool ignoreCacheUntil ) where T : ApiResponse
        {
            var requestHandler = new ApiRequestHandler<T>(parser, ignoreCacheUntil);
            return requestHandler.HandleRequest(url);
        }

        /// <summary>
        /// Adds the common character information elements to a the request web address.
        /// </summary>
        private static void AddCommonCharacterInformation(ApiRequestUrl url, int userId, int characterId, string apiKey)
        {
            url.AddProperty(ApiRequestUrl.PROPERTY_USER_ID, userId.ToString());
            url.AddProperty(ApiRequestUrl.PROPERTY_CHARACTER_ID, characterId.ToString());
            url.AddProperty(ApiRequestUrl.PROPERTY_API_KEY, apiKey);
        }
    }

    /// <summary>
    /// Raised when an error reponse is received from an eve api request
    /// </summary>
    public class ApiResponseErrorException : Exception
    {
        /// <summary>
        /// The error code
        /// </summary>
        public string Code;

        /// <summary>
        /// Sets the current error code to the code recieved
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public ApiResponseErrorException(string code, string message)
            : base(message)
        {
            Code = code;
        }
    }

    /// <summary>
    /// Character portrait size
    /// </summary>
    public enum PortraitSize
    {
        /// <summary>
        /// A small portrait, 64x64 pixels
        /// </summary>
        Small,
        /// <summary>
        /// A large portrait, 256x256 pixels
        /// </summary>
        Large
    }
}
