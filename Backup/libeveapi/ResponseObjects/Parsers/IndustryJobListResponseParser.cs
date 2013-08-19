using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace libeveapi.ResponseObjects.Parsers
{
    ///<summary>
    /// A parser which converts a given <see cref="XmlDocument"/> to a <see cref="IndustryJobList"/>.
    ///</summary>
    internal class IndustryJobListResponseParser : IApiResponseParser<IndustryJobList>
    {
        public IndustryJobList Parse(XmlDocument xmlDocument)
        {
            this.CheckVersion(xmlDocument);
            IndustryJobList industryJobList = new IndustryJobList();
            industryJobList.ParseCommonElements(xmlDocument);

            List<IndustryJobList.IndustryJobListItem> jobs = new List<IndustryJobList.IndustryJobListItem>();
            foreach (XmlNode node in xmlDocument.SelectNodes("//rowset[@name='jobs']/row"))
            {
                jobs.Add(ParseIndustryJobRow(node, industryJobList));
            }
            industryJobList.IndustryJobListItems = jobs.ToArray();

            return industryJobList;
        }

        /// <summary>
        /// Create an IndustryJobListItem by parsing a single row
        /// </summary>
        /// <param name="industryJobRow">An xml row containing an industry job</param>
        /// <param name="industryJobList"></param>
        /// <returns></returns>
        private static IndustryJobList.IndustryJobListItem ParseIndustryJobRow(XmlNode industryJobRow, IndustryJobList industryJobList)
        {
            IndustryJobList.IndustryJobListItem IndustryJobListItem = new IndustryJobList.IndustryJobListItem();

            IndustryJobListItem.JobId = Convert.ToInt32(industryJobRow.Attributes["jobID"].InnerText, CultureInfo.InvariantCulture);
            IndustryJobListItem.AssemblyLineId = Convert.ToInt32(industryJobRow.Attributes["assemblyLineID"].InnerText, CultureInfo.InvariantCulture);
            IndustryJobListItem.ContainerId = Convert.ToInt32(industryJobRow.Attributes["containerID"].InnerText, CultureInfo.InvariantCulture);
            IndustryJobListItem.InstalledItemId = Convert.ToInt32(industryJobRow.Attributes["installedItemID"].InnerText, CultureInfo.InvariantCulture);
            IndustryJobListItem.InstalledItemLocationId = Convert.ToInt32(industryJobRow.Attributes["installedItemLocationID"].InnerText, CultureInfo.InvariantCulture);
            IndustryJobListItem.InstalledItemQuantity = Convert.ToInt32(industryJobRow.Attributes["installedItemQuantity"].InnerText, CultureInfo.InvariantCulture);
            IndustryJobListItem.InstalledItemProductivityLevel = Convert.ToInt32(industryJobRow.Attributes["installedItemProductivityLevel"].InnerText, CultureInfo.InvariantCulture);
            IndustryJobListItem.InstalledItemMaterialLevel = Convert.ToInt32(industryJobRow.Attributes["installedItemMaterialLevel"].InnerText, CultureInfo.InvariantCulture);
            IndustryJobListItem.InstalledItemLicensedProductionRunsRemaining = Convert.ToInt32(industryJobRow.Attributes["installedItemLicensedProductionRunsRemaining"].InnerText, CultureInfo.InvariantCulture);
            IndustryJobListItem.OutputLocationId = Convert.ToInt32(industryJobRow.Attributes["outputLocationID"].InnerText, CultureInfo.InvariantCulture);
            IndustryJobListItem.InstallerId = Convert.ToInt32(industryJobRow.Attributes["installerID"].InnerText, CultureInfo.InvariantCulture);
            IndustryJobListItem.Runs = Convert.ToInt32(industryJobRow.Attributes["runs"].InnerText, CultureInfo.InvariantCulture);
            IndustryJobListItem.LicensedProductionRuns = Convert.ToInt32(industryJobRow.Attributes["licensedProductionRuns"].InnerText, CultureInfo.InvariantCulture);
            IndustryJobListItem.InstalledInSolarSystemId = Convert.ToInt32(industryJobRow.Attributes["installedInSolarSystemID"].InnerText, CultureInfo.InvariantCulture);
            IndustryJobListItem.ContainerLocationId = Convert.ToInt32(industryJobRow.Attributes["containerLocationID"].InnerText, CultureInfo.InvariantCulture);
            IndustryJobListItem.MaterialMultiplier = Convert.ToDouble(industryJobRow.Attributes["materialMultiplier"].InnerText, CultureInfo.InvariantCulture);
            IndustryJobListItem.CharMaterialMultiplier = Convert.ToDouble(industryJobRow.Attributes["charMaterialMultiplier"].InnerText, CultureInfo.InvariantCulture);
            IndustryJobListItem.TimeMultiplier = Convert.ToDouble(industryJobRow.Attributes["timeMultiplier"].InnerText, CultureInfo.InvariantCulture);
            IndustryJobListItem.CharTimeMultiplier = Convert.ToDouble(industryJobRow.Attributes["charTimeMultiplier"].InnerText, CultureInfo.InvariantCulture);
            IndustryJobListItem.InstalledItemTypeId = Convert.ToInt32(industryJobRow.Attributes["installedItemTypeID"].InnerText, CultureInfo.InvariantCulture);
            IndustryJobListItem.OutputTypeId = Convert.ToInt32(industryJobRow.Attributes["outputTypeID"].InnerText, CultureInfo.InvariantCulture);
            IndustryJobListItem.ContainerTypeId = Convert.ToInt32(industryJobRow.Attributes["containerTypeID"].InnerText, CultureInfo.InvariantCulture);
            IndustryJobListItem.InstalledItemCopy = Convert.ToBoolean(Convert.ToInt32(industryJobRow.Attributes["installedItemCopy"].InnerText, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
            IndustryJobListItem.Completed = Convert.ToBoolean(Convert.ToInt32(industryJobRow.Attributes["completed"].InnerText, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
            IndustryJobListItem.CompletedSuccessfully = Convert.ToBoolean(Convert.ToInt32(industryJobRow.Attributes["completedSuccessfully"].InnerText, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
            IndustryJobListItem.InstalledItemFlag = Convert.ToInt32(industryJobRow.Attributes["installedItemFlag"].InnerText, CultureInfo.InvariantCulture);
            IndustryJobListItem.OutputFlag = Convert.ToInt32(industryJobRow.Attributes["outputFlag"].InnerText, CultureInfo.InvariantCulture);
            IndustryJobListItem.ActivityId = Convert.ToInt32(industryJobRow.Attributes["activityID"].InnerText, CultureInfo.InvariantCulture);

            switch (Convert.ToInt32(industryJobRow.Attributes["activityID"].InnerText, CultureInfo.InvariantCulture))
            {
                case 1:
                    IndustryJobListItem.Activity = Activities.Manufacturing;
                    break;
                case 2:
                    IndustryJobListItem.Activity = Activities.TechnologyResearch;
                    break;
                case 3:
                    IndustryJobListItem.Activity = Activities.TimeEfficiency;
                    break;
                case 4:
                    IndustryJobListItem.Activity = Activities.MaterialEfficiency;
                    break;
                case 5:
                    IndustryJobListItem.Activity = Activities.Copying;
                    break;
                case 6:
                    IndustryJobListItem.Activity = Activities.Duplicating;
                    break;
                case 7:
                    IndustryJobListItem.Activity = Activities.ReverseEngineering;
                    break;
                case 8:
                    IndustryJobListItem.Activity = Activities.Invention;
                    break;
                default:
                    IndustryJobListItem.Activity = Activities.Unknown;
                    break;
            }

            switch (Convert.ToInt32(industryJobRow.Attributes["completedStatus"].InnerText, CultureInfo.InvariantCulture))
            {
                case 0:
                    IndustryJobListItem.CompletedStatus = IndustryJobCompletedStatuses.Failed;
                    break;
                case 1:
                    IndustryJobListItem.CompletedStatus = IndustryJobCompletedStatuses.Delivered;
                    break;
                case 2:
                    IndustryJobListItem.CompletedStatus = IndustryJobCompletedStatuses.Aborted;
                    break;
                case 3:
                    IndustryJobListItem.CompletedStatus = IndustryJobCompletedStatuses.GM_Aborted;
                    break;
                case 4:
                    IndustryJobListItem.CompletedStatus = IndustryJobCompletedStatuses.Unachored;
                    break;
                case 5:
                    IndustryJobListItem.CompletedStatus = IndustryJobCompletedStatuses.Destroyed;
                    break;
                default:
                    break;
            }
            
            IndustryJobListItem.InstallTime = TimeUtilities.ConvertCCPTimeStringToDateTimeUTC(industryJobRow.Attributes["installTime"].InnerText);
            IndustryJobListItem.BeginProductionTime = TimeUtilities.ConvertCCPTimeStringToDateTimeUTC(industryJobRow.Attributes["beginProductionTime"].InnerText);
            IndustryJobListItem.EndProductionTime = TimeUtilities.ConvertCCPTimeStringToDateTimeUTC(industryJobRow.Attributes["endProductionTime"].InnerText);
            IndustryJobListItem.PauseProductionTime = TimeUtilities.ConvertCCPTimeStringToDateTimeUTC(industryJobRow.Attributes["pauseProductionTime"].InnerText);

            IndustryJobListItem.InstallTimeLocal = TimeUtilities.ConvertCCPToLocalTime(IndustryJobListItem.InstallTime);
            IndustryJobListItem.BeginProductionTimeLocal = TimeUtilities.ConvertCCPToLocalTime(IndustryJobListItem.BeginProductionTime);
            IndustryJobListItem.EndProductionTimeLocal = TimeUtilities.ConvertCCPToLocalTime(IndustryJobListItem.EndProductionTime);
            IndustryJobListItem.PauseProductionTimeLocal = TimeUtilities.ConvertCCPToLocalTime(IndustryJobListItem.PauseProductionTime);

            return IndustryJobListItem;
        }

        public void CheckVersion(XmlDocument xmlDocument)
        {
            if (IndustryJobList.VersionCheck)
            {
                string version = xmlDocument.SelectSingleNode("//eveapi").Attributes["version"].InnerText;
                if (version.CompareTo(IndustryJobList.API_VERSION) != 0)
                {
                    throw new ApiVersionException(version, IndustryJobList.API_VERSION);
                }
            }
        }
    }
}
