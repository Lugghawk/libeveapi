using System;
using System.Globalization;
using System.Xml;

namespace libeveapi.ResponseObjects.Parsers
{
    ///<summary>
    /// A parser which converts a given <see cref="XmlDocument"/> to a <see cref="SkillInTraining"/>.
    ///</summary>
    internal class SkillInTrainingResponseParser : IApiResponseParser<SkillInTraining>
    {
        public SkillInTraining Parse(XmlDocument xmlDocument)
        {
            this.CheckVersion(xmlDocument);
            SkillInTraining skilltraining = new SkillInTraining();
            skilltraining.ParseCommonElements(xmlDocument);
            skilltraining.SkillCurrentlyInTraining = Convert.ToBoolean(Convert.ToInt32(xmlDocument.SelectSingleNode("/eveapi/result/skillInTraining").InnerText));
            if (skilltraining.SkillCurrentlyInTraining)
            {
                skilltraining.TrainingEndTime = TimeUtilities.ConvertCCPTimeStringToDateTimeUTC(xmlDocument.SelectSingleNode("/eveapi/result/trainingEndTime").InnerText);
                skilltraining.TrainingStartTime = TimeUtilities.ConvertCCPTimeStringToDateTimeUTC(xmlDocument.SelectSingleNode("/eveapi/result/trainingStartTime").InnerText);
                skilltraining.TrainingTypeId = Convert.ToInt32(xmlDocument.SelectSingleNode("/eveapi/result/trainingTypeID").InnerText, CultureInfo.InvariantCulture);
                skilltraining.TrainingStartSP = Convert.ToInt32(xmlDocument.SelectSingleNode("/eveapi/result/trainingStartSP").InnerText, CultureInfo.InvariantCulture);
                skilltraining.TrainingDestinationSP = Convert.ToInt32(xmlDocument.SelectSingleNode("/eveapi/result/trainingDestinationSP").InnerText, CultureInfo.InvariantCulture);
                skilltraining.TrainingToLevel = Convert.ToInt32(xmlDocument.SelectSingleNode("/eveapi/result/trainingToLevel").InnerText, CultureInfo.InvariantCulture);

                skilltraining.TrainingEndTimeLocal = TimeUtilities.ConvertCCPToLocalTime(skilltraining.TrainingEndTime);
                skilltraining.TrainingStartTimeLocal = TimeUtilities.ConvertCCPToLocalTime(skilltraining.TrainingStartTime);
            }
            else
            {
                skilltraining.TrainingEndTime = DateTime.MinValue;
                skilltraining.TrainingStartTime = DateTime.MinValue;
                skilltraining.TrainingEndTimeLocal = DateTime.MinValue;
                skilltraining.TrainingStartTimeLocal = DateTime.MinValue;
                skilltraining.TrainingTypeId = 0;
                skilltraining.TrainingStartSP = 0;
                skilltraining.TrainingDestinationSP = 0;
                skilltraining.TrainingToLevel = 0;
            }
            return skilltraining;
        }

        public void CheckVersion(XmlDocument xmlDocument)
        {
            if (SkillInTraining.VersionCheck)
            {
                string version = xmlDocument.SelectSingleNode("//eveapi").Attributes["version"].InnerText;
                if (version.CompareTo(SkillInTraining.API_VERSION) != 0)
                {
                    throw new ApiVersionException(version, SkillInTraining.API_VERSION);
                }
            }
        }
    }
}
