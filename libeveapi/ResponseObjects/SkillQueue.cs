using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace libeveapi
{
    /// <summary>
    /// Character SkillQueue
    /// Contains a list of skills in the characters Training Queue
    /// </summary>
    public class SkillQueue : ApiResponse
    {
        public const string API_VERSION = "2";
        [XmlIgnore]
        private Skill[] skillList = new Skill[0];
        /// <summary>
        /// List of Skills in the Queue
        /// </summary>
        public Skill[] SkillList { get { return skillList; } set { skillList = value; } }
        /// <summary>
        /// Constructor creats a SkillList with a default capacity of 5
        /// </summary>
        public SkillQueue(){}

        /// <summary>
        /// A Single skill from a training queue
        /// </summary>
        [XmlType(Namespace = "urn:Skill")]
        [XmlRoot(Namespace = "urn:SkillQueue")]
        public class Skill
        {
            private int queuePosition;
            private DateTime trainingEndTime;
            private DateTime trainingStartTime;
            private DateTime trainingEndTimeLocal;
            private DateTime trainingStartTimeLocal;
            private int trainingTypeId;
            private int trainingStartSP;
            private int trainingEndSP;
            private int trainingToLevel;

            /// <summary>
            /// Default Constructor
            /// </summary>
            public Skill()
            {

            }
            /// <summary>
            /// Constructor that initializes all of the local attributes
            /// </summary>
            /// <param name="queuePosition">Queue Posistion</param>
            /// <param name="trainingTypeId">Skill ID</param>
            /// <param name="trainingToLevel">Skill Level</param>
            /// <param name="trainingStartSP">Starting Skill Points</param>
            /// <param name="trainingEndSP">Ending Skill Points</param>
            /// <param name="trainingStartTime">Start Time</param>
            /// <param name="trainingEndTime">End Time</param>
            /// <param name="trainingStartTimeLocal">Local Start Time</param>
            /// <param name="trainingEndTimeLocal">Local End Time</param>
            public Skill(int queuePosition, int trainingTypeId, int trainingToLevel,
                        int trainingStartSP, int trainingEndSP,
                        DateTime trainingStartTime, DateTime trainingEndTime,
                        DateTime trainingStartTimeLocal, DateTime trainingEndTimeLocal)
            {
                this.queuePosition = queuePosition;
                this.trainingEndTime = trainingEndTime;
                this.trainingStartTime = trainingStartTime;
                this.trainingEndTimeLocal = trainingEndTimeLocal;
                this.trainingStartTimeLocal = trainingStartTimeLocal;
                this.trainingTypeId = trainingTypeId;
                this.trainingStartSP = trainingStartSP;
                this.trainingEndSP = trainingEndSP;
                this.trainingToLevel = trainingToLevel;
            }
            /// <summary>
            /// Position in the Training Queue for this skill
            /// </summary>
            public int QueuePosition
            {
                get { return queuePosition; }
                set { queuePosition = value; }
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
            public int TrainingEndSP
            {
                get { return trainingEndSP; }
                set { trainingEndSP = value; }
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
}
