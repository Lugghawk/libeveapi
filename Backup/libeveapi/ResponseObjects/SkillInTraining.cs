using System;

namespace libeveapi
{
    /// <summary>
    /// Contains the Current Skill in Training
    /// </summary>
    public class SkillInTraining : ApiResponse 
    {
        /// <summary>
        /// API Version Compatibility
        /// </summary>
        public const string API_VERSION = "2";
        private bool skillCurrentlyInTraining;
        private DateTime trainingEndTime;
        private DateTime trainingStartTime;
        private DateTime trainingEndTimeLocal;
        private DateTime trainingStartTimeLocal;
        private int trainingTypeId;
        private int trainingStartSP;
        private int trainingDestinationSP;
        private int trainingToLevel;

        /// <summary>
        /// True if a skill is currently training, false is no skill is training
        /// </summary>
        public bool SkillCurrentlyInTraining
        {
            get { return skillCurrentlyInTraining; }
            set { skillCurrentlyInTraining = value; }
        }
        /// 
        /// <summary>
        /// Server time when skill training completes in ccp time
        /// Minvalue - when no skill training
        /// </summary>
        public DateTime TrainingEndTime
        {
            get { return trainingEndTime; }
            set { trainingEndTime = value; }
        }
        /// 
        /// <summary>
        /// Server time when skill training started in ccp time
        /// Minvalue - when no skill training
        /// </summary>
        public DateTime TrainingStartTime
        {
            get { return trainingStartTime; }
            set { trainingStartTime = value; }
        }
        /// 
        /// <summary>
        /// Server time when skill training completes in local time
        /// Minvalue - when no skill training
        /// </summary>
        public DateTime TrainingEndTimeLocal
        {
            get { return trainingEndTimeLocal; }
            set { trainingEndTimeLocal = value; }
        }
        /// 
        /// <summary>
        /// Server time when skill training started in local time
        /// Minvalue - when no skill training
        /// </summary>
        public DateTime TrainingStartTimeLocal
        {
            get { return trainingStartTimeLocal; }
            set { trainingStartTimeLocal = value; }
        }
        /// 
        /// <summary>
        /// Id of skill in training
        /// 0 - when no skill training
        /// </summary>
        public int TrainingTypeId
        {
            get { return trainingTypeId; }
            set { trainingTypeId = value; }
        }
        /// 
        /// <summary>
        /// Skill Points at training start
        /// 0 - when no skill training
        /// </summary>
        public int TrainingStartSP
        {
            get { return trainingStartSP; }
            set { trainingStartSP = value; }
        }
        /// 
        /// <summary>
        /// Skill Points after training end
        /// 0 - when no skill training
        /// </summary>
        public int TrainingDestinationSP
        {
            get { return trainingDestinationSP; }
            set { trainingDestinationSP = value; }
        }
        /// 
        /// <summary>
        /// Skill level being trained
        /// 0 - when no skill training
        /// </summary>
        public int TrainingToLevel
        {
            get { return trainingToLevel; }
            set { trainingToLevel = value; }
        }
    }
}
