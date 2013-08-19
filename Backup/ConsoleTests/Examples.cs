using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using libeveapi;

namespace ConsoleTests
{
    class Examples
    {
        public static void Main(String[] args)
        {
            Console.WriteLine("*******\nExample\n*******\n");
            UseLocalUrls();
            ResponseCache.Clear();
            //MemberTrackingExample();
            
            //MapSovereigntyExample();
            //CharFacWarStatsExample();
            WalletJournalExample();
            JournalExample();
            ResponseCache.Save("ResponseCache.xml");
            Console.WriteLine("Press Any Key to Continue");
            Console.ReadKey();
        }

        public static void CharFacWarStatsExample()
        {
            FacWarStats stats = new FacWarStats();
            stats.CharacterFactionWarStats = EveApi.GetCharacterFactionWarStats(123, 456, "apikey");
            
            Console.WriteLine("**** Character Stats ****");
            Console.WriteLine("Faciton ID: {0}", stats.CharacterFactionWarStats.FactionID);
            Console.WriteLine("Faciton Name: {0}", stats.CharacterFactionWarStats.FactionName);
            Console.WriteLine("Enlisted: {0}", stats.CharacterFactionWarStats.Enlisted);
            Console.WriteLine("Current Rank: {0}", stats.CharacterFactionWarStats.CurrentRank);
            Console.WriteLine("Highest Rank: {0}", stats.CharacterFactionWarStats.HighestRank);
            Console.WriteLine("Kills Yesterday: {0}", stats.CharacterFactionWarStats.KillsYesterday);
            Console.WriteLine("Kills Last Week: {0}", stats.CharacterFactionWarStats.KillsLastWeek);
            Console.WriteLine("Kills Total: {0}", stats.CharacterFactionWarStats.KillsTotal);
            Console.WriteLine("Victory Points Yesterday: {0}", stats.CharacterFactionWarStats.VictoryPointsYesterday);
            Console.WriteLine("Victory Points Last Week: {0}", stats.CharacterFactionWarStats.VictoryPointsLastWeek);
            Console.WriteLine("Victory Points Total: {0}", stats.CharacterFactionWarStats.VictoryPointsTotal);
        }

        public static void MapFactionWarSystemsExample()
        {
            MapFacWarSystems mapFacWarSystems = EveApi.GetFactionWarSystems();

            Console.WriteLine("Currently occupied by Gallente Federation:");
            foreach (MapFacWarSystems.FactionWarSystem system in mapFacWarSystems.FactionWarSystems)
            {
                if (system.OccupyingFactionId == 500004)
                {
                    Console.WriteLine(system.SolarSystemName);
                }
            }
        }

        public static void ServerStatus()
        {
            ServerStatus serverStatus = EveApi.GetServerStatus();

            if (serverStatus.ServerOpen)
            {
                Console.WriteLine("Tranquility is currently up");
                Console.WriteLine("There are currently {0} players online", serverStatus.OnlinePlayers);
            }
            else
            {
                Console.WriteLine("Tranquility is currently down");
            }
        }

        public static void UseLocalUrls()
        {
            Constants.ApiPrefix = "http://localhost/eveapi";
        }

        public static void KillLog()
        {
            KillLog killLog = EveApi.GetKillLog(KillLogType.Character, 0, 0, "fullApiKey");
            KillLog.Kill kill = killLog.Kills[0];

            Console.WriteLine("{0} was killed at {1} in system {2}", kill.Victim.CharacterName, kill.KillTimeLocal, kill.SolarSystemId);
            Console.WriteLine("The attackers were:");
            foreach (KillLog.Attacker attacker in kill.Attackers)
            {
                Console.WriteLine("{0} from corporation {1}", attacker.CharacterName, attacker.CorporationName);
            }
        }

        public static void MarketOrdersExample()
        {
            MarketOrders orders = EveApi.GetMarketOrderList(MarketOrdersListType.Character, 0, 0, "fullApiKey");
            foreach (MarketOrders.MarketOrderItem item in orders.MarketOrderItems)
            {
                if (item.OrderType == MarketOrders.MarketOrderType.Sell && item.TypeId == 123)
                {
                    Console.WriteLine("Item 123 is for sale at: {0}", item.StationId);
                }
            }
        }

        public static void MapJumpsExample()
        {
            MapJumps mapJumps = EveApi.GetMapJumps();
            foreach (MapJumps.MapSystemItem system in mapJumps.MapSystemJumps)
            {
                Console.WriteLine("System: {0} Number of Jumps: {1}", system.SolarSystemId, system.ShipJumps);
            }
        }

        public static void MapKillsExample()
        {
            MapKills mapKills = EveApi.GetMapKills();
            foreach (MapKills.MapKillsItem item in mapKills.MapSystemKills)
            {
                if (item.ShipKills > 5)
                {
                    Console.WriteLine("{0} is a bad place to be right now", item.SolarSystemId);
                }
            }
        }

        public static void MapSovereigntyExample()
        {
            MapSovereignty ms = EveApi.GetMapSovereignty();
            foreach (MapSovereignty.MapSovereigntyItem msi in ms.MapSystemSovereigntyItems)
            {
                Console.WriteLine("System Name: {0} FactionId: {1}", msi.SolarSystemName, msi.FactionId);
            }
        }

        public static int GetCharacterIdByName(string characterName)
        {
            CharacterIdName characterId = EveApi.GetCharacterIdName(characterName);
            return characterId.CharacterIdItems[0].CharacterId;
        }

        public static void RefTypesExaple()
        {
            RefTypes refTypes = EveApi.GetRefTypesList();
            Console.WriteLine("Type {0} Name {1}", 1, refTypes.GetReferenceTypeNameForId(1));
        }

        public static void WalletTransactionsExample()
        {
            bool done = false;
            int lastEntrySeen = 0;

            while (!done)
            {
                WalletTransactions transactions = EveApi.GetWalletTransactionsList(WalletTransactionListType.Character, 0, 0, "fullApiKey", lastEntrySeen);
                DisplayWalletTransactions(transactions);
                lastEntrySeen += transactions.WalletTransactionItems.Length;
                if (transactions.WalletTransactionItems.Length < 1000)
                {
                    done = true;
                }
            }
        }

        public static void DisplayWalletTransactions(WalletTransactions transactions)
        {
            foreach (WalletTransactions.WalletTransactionItem transaction in transactions.WalletTransactionItems)
            {
                Console.WriteLine("Date: {0} Quantity: {1} Price: {2}", transaction.TransactionDateTimeLocal, transaction.Quantity, transaction.Price);
            }
        }

        public static void JournalExample()
        {
            bool done = false;
            int lastEntrySeen = 0;

            JournalEntries.VersionCheck = false;
            while (!done)
            {
                JournalEntries entries = EveApi.GetJournalEntryList(JournalEntryType.Character, 0, 0, "fullApiKey", lastEntrySeen);
                DisplayJournalEntries(entries);
                lastEntrySeen += entries.JournalEntryItems.Length;
                if (entries.JournalEntryItems.Length < 1000)
                {
                    done = true;
                }
            }
        }

        public static void WalletJournalExample()
        {
            bool done = false;
            int lastEntrySeen = 0;

            while (!done)
            {
                WalletJournal entries = EveApi.GetWalletJournal(WalletJournalType.Character, 0, 0, "fullApiKey", lastEntrySeen);
                foreach (WalletJournal.WalletJournalItem item in entries.WalletJournalItems)
                {
                    Console.WriteLine("Date: {0} Amount: {1} Balance: {2}", item.DateLocal, item.Amount, item.Balance);
                }
                lastEntrySeen += entries.JournalEntryItems.Length;
                if (entries.JournalEntryItems.Length < 1000)
                {
                    done = true;
                }
            }

            Console.WriteLine();
            done = false;
            lastEntrySeen = 0;
            while (!done)
            {
                WalletJournal entries = EveApi.GetWalletJournal(WalletJournalType.Corporation, 0, 0, "fullApiKey", lastEntrySeen);
                foreach (WalletJournal.WalletJournalItem item in entries.WalletJournalItems)
                {
                    Console.WriteLine("Date: {0} Amount: {1} Balance: {2}", item.DateLocal, item.Amount, item.Balance);
                }
                lastEntrySeen += entries.JournalEntryItems.Length;
                if (entries.JournalEntryItems.Length < 1000)
                {
                    done = true;
                }
            }
        }

        public static void DisplayJournalEntries(JournalEntries entries)
        {
            foreach (JournalEntries.JournalEntryItem item in entries.JournalEntryItems)
            {
                Console.WriteLine("Date: {0} Amount: {1} Balance: {2}", item.DateLocal, item.Amount, item.Balance);
            }
        }

        public static void SkillInTrainingExample()
        {
            SkillInTraining skillInTraining = EveApi.GetSkillInTraining(1234, 5678, "apiKey");
            if (skillInTraining.SkillCurrentlyInTraining)
            {
                Console.WriteLine("Training of skill: {0} will finish at {1}", skillInTraining.TrainingTypeId, skillInTraining.TrainingEndTimeLocal);
            }
            else
            {
                Console.WriteLine("You should start a skill training!");
            }
        }

        public static void SkillQueueExample()
        {
            SkillQueue skillQueue = EveApi.GetSkillQueue(1234, 1234, "apiKey");
            foreach (SkillQueue.Skill skill in skillQueue.SkillList)
            {
                Console.WriteLine("Queue Position: {0}", skill.QueuePosition);
                Console.WriteLine("Skill ID: {0}", skill.TrainingTypeId);
                Console.WriteLine("Skill Level: {0}", skill.TrainingToLevel);
                Console.WriteLine("Start SP: {0}", skill.TrainingStartSP);
                Console.WriteLine("End SP: {0}", skill.TrainingEndSP);
                Console.WriteLine("StartTime: {0}", skill.TrainingStartTime);
                Console.WriteLine("EndTime: {0}", skill.TrainingEndTime);
                Console.WriteLine("Start Local: {0}", skill.TrainingStartTimeLocal);
                Console.WriteLine("End Local: {0}", skill.TrainingEndTimeLocal);
                Console.WriteLine("");
            }
        }

        public static void IndustryJobListExample()
        {
            IndustryJobList ijl = EveApi.GetIndustryJobList(IndustryJobListType.Corporation, 1234, 5678, "fullApiKey");
            foreach (IndustryJobList.IndustryJobListItem item in ijl.IndustryJobListItems)
            {
                if (item.Completed)
                {
                    Console.WriteLine("Completed JobId: {0} Installed at location: {1}", item.JobId, item.InstalledItemLocationId);
                }
            }
        }

        public static void ConquerableStationListExample()
        {
            ConquerableStationList csl = EveApi.GetConquerableStationList();
            foreach (ConquerableStationList.ConquerableStation station in csl.ConquerableStations)
            {
                Console.WriteLine("Station Name: {0} Corporation Name: {1}", station.StationName, station.CorporationName);
            }
        }

        /// <summary>
        /// Display a skill and its required skills
        /// </summary>
        public static void SkillTreeExample()
        {
            SkillTree skillTree = EveApi.GetSkillTree();

            // Get skill 3344 and display its name
            SkillTree.Skill targetSkill = GetSkillByTypeId(3344, skillTree);
            Console.WriteLine("Skill Name: {0}", targetSkill.TypeName);

            // Display the name and level of each required skill
            foreach (SkillTree.RequiredSkill requiredSkill in targetSkill.RequiredSkills)
            {
                SkillTree.Skill requiredSkillInfo = GetSkillByTypeId(requiredSkill.TypeId, skillTree);
                Console.WriteLine("Required Skill: {0} Level: {1}", requiredSkillInfo.TypeName, requiredSkill.SkillLevel);
            }
        }

        /// <summary>
        /// Find a Skill in the SkillTree by TypeId
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="skillTree"></param>
        /// <returns></returns>
        private static SkillTree.Skill GetSkillByTypeId(int typeId, SkillTree skillTree)
        {
            foreach (SkillTree.Skill skill in skillTree.Skills)
            {
                if (skill.TypeId == typeId)
                {
                    return skill;
                }
            }

            return null;
        }

        public static void MemberTrackingExample()
        {
            MemberTracking memberTracking = EveApi.GetMemberTracking(1234, 5678, "fullApiKey");
            
            // Output the name and location of all corporation directors
            foreach (MemberTracking.Member member in memberTracking.Members)
            {
                Console.WriteLine("CharacterId: {0}", member.CharacterId);
                Console.WriteLine("Name: {0}", member.Name);
                Console.WriteLine("StartDateTime: {0}", member.StartDateTime);
                Console.WriteLine("StartDateTimeLocal: {0}", member.StartDateTimeLocal);
                Console.WriteLine("BaseId: {0}", member.BaseId);
                Console.WriteLine("Base: {0}", member.Base);
                Console.WriteLine("Title: {0}", member.Title);
                Console.WriteLine("LogonDateTime: {0}", member.LogonDateTime);
                Console.WriteLine("LogonDateTimeLocal: {0}", member.LogonDateTimeLocal);
                Console.WriteLine("LogoffDateTime: {0}", member.LogoffDateTime);
                Console.WriteLine("LogoffDateTimeLocal: {0}", member.LogoffDateTimeLocal);
                Console.WriteLine("LocationId: {0}", member.LocationId);
                Console.WriteLine("Location: {0}", member.Location);
                Console.WriteLine("ShipTypeId: {0}", member.ShipTypeId);
                Console.WriteLine("ShipType: {0}", member.ShipType);
                Console.WriteLine("RolesMask: {0}", member.RolesMask);
                Console.WriteLine("GrantableRoles: {0}", member.GrantableRoles);
                Console.Write("Roles: ");
                foreach (RoleTypes role in member.Roles.RoleList)
                {
                    Console.Write("{0}, ", role.ToString());
                }
                Console.WriteLine("\n\n*************************************\n");
            }
        }

        public static void CorporationSheetExample()
        {
            CorporationSheet corporationSheet = EveApi.GetCorporationSheet(452453, 452452, "apiKey");
            Console.WriteLine("Corporation Name: {0} Ticker: {1}", corporationSheet.CorporationName, corporationSheet.Ticker);
            Console.WriteLine("Logo GraphicId: {0}", corporationSheet.Logo.GraphicId);

            foreach (CorporationSheet.Division division in corporationSheet.Divisions)
            {
                Console.WriteLine("Division AccountKey: {0} Description: {1}", division.AccountKey, division.Description);
            }

            foreach (CorporationSheet.WalletDivision walletDivision in corporationSheet.WalletDivisions)
            {
                Console.WriteLine("Wallet Division AccountKey: {0} Description: {1}", walletDivision.AccountKey, walletDivision.Description);
            }
        }

        public static void CharacterSheetExample()
        {
            CharacterSheet characterSheet = EveApi.GetCharacterSheet(4378412, 453272742, "apiKey");
            
            long totalSkillpoints = 0;
            foreach (CharacterSheet.SkillItem skillItem in characterSheet.SkillItemList)
            {
                totalSkillpoints += skillItem.Skillpoints;
            }

            Console.WriteLine("Character Name: {0} Total Skillpoints: {1}", characterSheet.Name, totalSkillpoints);
        }

        public static void KillLogExample()
        {
            KillLog killLog = EveApi.GetKillLog(KillLogType.Character, 4532132, 4537, "apiKey");
            Console.WriteLine("Total Kills: {0}", killLog.Kills.Length);
        }

        public static void PrintAllianceList()
        {
            AllianceList allianceList = EveApi.GetAllianceList();
            foreach (AllianceList.AllianceListItem ali in allianceList.AllianceListItems)
            {
                Console.WriteLine("Alliance name: {0} members: {1}", ali.Name, ali.MemberCount);
                foreach (AllianceList.CorporationListItem cli in ali.CorporationListItems)
                {
                    Console.WriteLine("Member Corporation Id: {0} Started: {1}", cli.CorporationId, cli.StartDate);
                }
            }
        }

        public static void PrintStarbaseList()
        {
            StarbaseList starbaseList = EveApi.GetStarbaseList(5385, 5431487, "fullApiKey");
            foreach (StarbaseList.StarbaseListItem sli in starbaseList.StarbaseListItems)
            {
                Console.WriteLine("ItemID: {0} LocationID: {1} State: {2}", sli.ItemId, sli.LocationId, sli.State);
            }
        }

        public static void PrintStarbaseDetail()
        {
            StarbaseDetail starbaseDetail = EveApi.GetStarbaseDetail(45353, 45317, "fullApiKey", 43534);
            Console.WriteLine("usageFlags: {0} deployFlags: {1}", starbaseDetail.UsageFlags, starbaseDetail.DeployFlags);

            foreach (StarbaseDetail.FuelListItem fli in starbaseDetail.FuelList)
            {
                Console.WriteLine("TypeID: {0} Quantity: {1}", fli.TypeId, fli.Quantity);
            }
        }

        public static void ErrorListExample()
        {
            ErrorList errorList = EveApi.GetErrorList();
            Console.WriteLine("{0}", errorList.GetMessageForErrorCode("100"));
        }

        public static void PrintAllAssets()
        {
            AssetList assetList = EveApi.GetAssetList(AssetListType.Corporation, 1237, 453, "fullApiKey");
            foreach (AssetList.AssetListItem ali in assetList.AssetListItems)
            {
                PrintAsset(ali);
            }
        }

        public static void PrintAsset(AssetList.AssetListItem ali)
        {
            Console.WriteLine("itemID: {0} quantity: {1}", ali.ItemId, ali.Quantity);
            foreach (AssetList.AssetListItem childAsset in ali.Contents)
            {
                PrintAsset(childAsset);
            }
        }
    }
}
