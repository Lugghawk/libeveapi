using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace libeveapi.ResponseObjects.Parsers
{
    ///<summary>
    /// A parser which converts a given <see cref="XmlDocument"/> to a <see cref="KillLog"/>.
    ///</summary>
    internal class KillLogResponseParser : IApiResponseParser<KillLog>
    {
        public KillLog Parse(XmlDocument xmlDocument)
        {
            this.CheckVersion(xmlDocument);
            KillLog killLog = new KillLog();
            killLog.ParseCommonElements(xmlDocument);

            //Kills outer loop 
            List<KillLog.Kill> killList = new List<KillLog.Kill>();
            foreach (XmlNode Kill in xmlDocument.SelectNodes("//rowset[@name='kills']/row"))
            {
                KillLog.Kill tmpKill = new KillLog.Kill();
                
                //Kill Info
                tmpKill.KillId = Convert.ToInt32(Kill.Attributes["killID"].InnerText, CultureInfo.InvariantCulture);
                tmpKill.SolarSystemId = Convert.ToInt32(Kill.Attributes["solarSystemID"].InnerText, CultureInfo.InvariantCulture);
                tmpKill.KillTime = TimeUtilities.ConvertCCPTimeStringToDateTimeUTC(Kill.Attributes["killTime"].InnerText);
                tmpKill.KillTimeLocal = TimeUtilities.ConvertCCPToLocalTime(tmpKill.KillTime);
                tmpKill.MoonId = Convert.ToInt32(Kill.Attributes["moonID"].InnerText, CultureInfo.InvariantCulture);
                
                //Victim
                XmlNode victimNode = Kill.SelectSingleNode("./victim");
                tmpKill.Victim.CharacterId = Convert.ToInt32(victimNode.Attributes["characterID"].InnerText, CultureInfo.InvariantCulture);
                tmpKill.Victim.CharacterName = victimNode.Attributes["characterName"].InnerText;
                tmpKill.Victim.CorporationId = Convert.ToInt32(victimNode.Attributes["corporationID"].InnerText, CultureInfo.InvariantCulture);
                tmpKill.Victim.CorporationName = victimNode.Attributes["corporationName"].InnerText;
                tmpKill.Victim.AllianceId = Convert.ToInt32(victimNode.Attributes["allianceID"].InnerText, CultureInfo.InvariantCulture);
                tmpKill.Victim.AllianceName = (victimNode.Attributes.GetNamedItem("allianceName") != null) ? victimNode.Attributes["allianceName"].InnerText : "";
                tmpKill.Victim.DamageTaken = Convert.ToInt32(victimNode.Attributes["damageTaken"].InnerText, CultureInfo.InvariantCulture);
                tmpKill.Victim.ShipTypeId = Convert.ToInt32(victimNode.Attributes["shipTypeID"].InnerText, CultureInfo.InvariantCulture);

                //Atackers
                List<KillLog.Attacker> attackerList = new List<KillLog.Attacker>();
                foreach (XmlNode attacker in Kill.SelectNodes("./rowset[@name='attackers']/row"))
                {
                    KillLog.Attacker tmpAttacker = new KillLog.Attacker();
                    tmpAttacker.CharacterId = Convert.ToInt32(attacker.Attributes["characterID"].InnerText, CultureInfo.InvariantCulture);
                    tmpAttacker.CharacterName = attacker.Attributes["characterName"].InnerText;
                    tmpAttacker.CorporationId = Convert.ToInt32(attacker.Attributes["corporationID"].InnerText, CultureInfo.InvariantCulture);
                    tmpAttacker.CorporationName = attacker.Attributes["corporationName"].InnerText;
                    tmpAttacker.AllianceId = Convert.ToInt32(attacker.Attributes["allianceID"].InnerText, CultureInfo.InvariantCulture);
                    tmpAttacker.AllianceName = attacker.Attributes["allianceName"].InnerText;
                    tmpAttacker.SecurityStatus = Convert.ToDouble(attacker.Attributes["securityStatus"].InnerText, CultureInfo.InvariantCulture);
                    tmpAttacker.DamageDone = Convert.ToInt32(attacker.Attributes["damageDone"].InnerText, CultureInfo.InvariantCulture);
                    tmpAttacker.FinalBlow = Convert.ToBoolean(Convert.ToInt32(attacker.Attributes["finalBlow"].InnerText, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
                    tmpAttacker.WeaponTypeId = Convert.ToInt32(attacker.Attributes["weaponTypeID"].InnerText, CultureInfo.InvariantCulture);
                    tmpAttacker.ShipTypeId = Convert.ToInt32(attacker.Attributes["shipTypeID"].InnerText, CultureInfo.InvariantCulture);
                    attackerList.Add(tmpAttacker);
                }
                tmpKill.Attackers = attackerList.ToArray();

                //Items
                List<KillLog.Item> itemList = new List<KillLog.Item>();
                foreach (XmlNode item in Kill.SelectNodes("./rowset[@name='items']/row"))
                {
                    itemList.Add(ParseItemRow(item));
                }
                tmpKill.Items = itemList.ToArray();

                killList.Add(tmpKill);
            }
            killLog.Kills = killList.ToArray();
            return killLog;
        }

        /// <summary>
        /// This function allows you to recursively build the items list. 
        /// This is needed because the items can be infinitely nested in the killmail
        /// </summary>
        /// <param name="ItemNode">The XmlNode containing the item row you want to parse</param>
        /// <returns>The item with it's child items in it's ContainedItems array</returns>
        private static KillLog.Item ParseItemRow(XmlNode ItemNode)
        {
            KillLog.Item tmpItem = new KillLog.Item();
            tmpItem.TypeId = Convert.ToInt32(ItemNode.Attributes["typeID"].InnerText);
            tmpItem.QtyDropped = Convert.ToInt32(ItemNode.Attributes["qtyDropped"].InnerText);
            tmpItem.QtyDestroyed = Convert.ToInt32(ItemNode.Attributes["qtyDestroyed"].InnerText);

            int flagValue = Convert.ToInt32(ItemNode.Attributes["flag"].InnerText);
            if (Enum.IsDefined(typeof(InventoryFlagType), flagValue))
            {
                tmpItem.Flag = (InventoryFlagType)Enum.ToObject(typeof(InventoryFlagType), flagValue);
            }
            else
            {
                tmpItem.Flag = InventoryFlagType.FlagUnknown;
            }

            //Parse this Items contained items and add it to it's list
            if (ItemNode.HasChildNodes)
            {
                List<KillLog.Item> itemList = new List<KillLog.Item>();
                foreach (XmlNode item in ItemNode.SelectNodes("./rowset[@name='items']/row"))
                {
                    itemList.Add(ParseItemRow(item));
                }
                tmpItem.ContainedItems = itemList.ToArray();
            }
            return tmpItem;
        }

        public void CheckVersion(XmlDocument xmlDocument)
        {
            if (KillLog.VersionCheck)
            {
                string version = xmlDocument.SelectSingleNode("//eveapi").Attributes["version"].InnerText;
                if (version.CompareTo(KillLog.API_VERSION) != 0)
                {
                    throw new ApiVersionException(version, KillLog.API_VERSION);
                }
            }
        }
    }
}
