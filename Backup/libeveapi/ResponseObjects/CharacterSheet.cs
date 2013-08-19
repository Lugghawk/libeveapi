namespace libeveapi
{
    /// <summary>
    /// Detailed information about a character
    /// </summary>
    public class CharacterSheet : ApiResponse
    {
        /// <summary>
        /// API Version Compatibility
        /// </summary>
        public const string API_VERSION = "2";
        private int characterId;
        private string name;
        private string race;
        private string bloodLine;
        private string gender;
        private string corporationName;
        private int corporationId;
        private double balance;
        private int intelligence;
        private int memory;
        private int charisma;
        private int perception;
        private int willpower;
        private AttributeEnhancer memoryBonus = new AttributeEnhancer();
        private AttributeEnhancer willpowerBonus = new AttributeEnhancer();
        private AttributeEnhancer perceptionBonus = new AttributeEnhancer();
        private AttributeEnhancer intelligenceBonus = new AttributeEnhancer();
        private AttributeEnhancer charismaBonus = new AttributeEnhancer();
        private SkillItem[] skillItemList = new SkillItem[0];

        /// <summary>
        /// The eve assigned characterId
        /// </summary>
        public int CharacterId
        {
            get { return characterId; }
            set { characterId = value; }
        }
        /// <summary>
        /// The name of the character
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        /// <summary>
        /// The race of the character
        /// </summary>
        public string Race
        {
            get { return race; }
            set { race = value; }
        }
        /// <summary>
        /// The bloodline of the character
        /// </summary>
        public string BloodLine
        {
            get { return bloodLine; }
            set { bloodLine = value; }
        }
        /// <summary>
        /// The gender of the character (currently male/female)
        /// </summary>
        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }
        /// <summary>
        /// The name of the corporation that the character is in
        /// </summary>
        public string CorporationName
        {
            get { return corporationName; }
            set { corporationName = value; }
        }
        /// <summary>
        /// The eve generated Id of the corporation
        /// </summary>
        public int CorporationId
        {
            get { return corporationId; }
            set { corporationId = value; }
        }
        /// <summary>
        /// The character's current wallet balance
        /// </summary>
        public double Balance
        {
            get { return balance; }
            set { balance = value; }
        }

        /// <summary>
        /// The character's intelligence attribute
        /// </summary>
        public int Intelligence
        {
            get { return intelligence; }
            set { intelligence = value; }
        }
        /// <summary>
        /// The character's memory attribute
        /// </summary>
        public int Memory
        {
            get { return memory; }
            set { memory = value; }
        }
        /// <summary>
        /// The character's charisma attribute
        /// </summary>
        public int Charisma
        {
            get { return charisma; }
            set { charisma = value; }
        }
        /// <summary>
        /// The character's perception attribute
        /// </summary>
        public int Perception
        {
            get { return perception; }
            set { perception = value; }
        }
        /// <summary>
        /// The character's willpower attribute
        /// </summary>
        public int Willpower
        {
            get { return willpower; }
            set { willpower = value; }
        }

        /// <summary>
        /// Memory Implant
        /// </summary>
        public AttributeEnhancer MemoryBonus
        {
            get { return memoryBonus; }
            set { memoryBonus = value; }
        }
        /// <summary>
        /// Willpower Implant
        /// </summary>
        public AttributeEnhancer WillpowerBonus
        {
            get { return willpowerBonus; }
            set { willpowerBonus = value; }
        }
        /// <summary>
        /// Perception Implant
        /// </summary>
        public AttributeEnhancer PerceptionBonus
        {
            get { return perceptionBonus; }
            set { perceptionBonus = value; }
        }
        /// <summary>
        /// Intelligence Implant
        /// </summary>
        public AttributeEnhancer IntelligenceBonus
        {
            get { return intelligenceBonus; }
            set { intelligenceBonus = value; }
        }
        /// <summary>
        /// Charisma Implant
        /// </summary>
        public AttributeEnhancer CharismaBonus
        {
            get { return charismaBonus; }
            set { charismaBonus = value; }
        }

        /// <summary>
        /// The list of retrieved skills
        /// </summary>
        public SkillItem[] SkillItemList
        {
            get { return skillItemList; }
            set { skillItemList = value; }
        }

        /// <summary>
        /// Stores valid information about each skill in the character sheet
        /// </summary>
        public class SkillItem
        {
            private int typeId;
            private long skillpoints;
            private int level;
            private string unpublished;

            /// <summary>
            /// TypeId of the skill
            /// </summary>
            public int TypeId
            {
                get { return typeId; }
                set { typeId = value; }
            }
            /// <summary>
            /// Current number of skillpoints the character has in the skill
            /// </summary>
            public long Skillpoints
            {
                get { return skillpoints; }
                set { skillpoints = value; }
            }
            /// <summary>
            /// The highest completed level of the skill
            /// </summary>
            public int Level
            {
                get { return level; }
                set { level = value; }
            }
            /// <summary>
            /// Flag if the skill is an unpublished skill
            /// </summary>
            public string Unpublished
            {
                get { return unpublished; }
                set { unpublished = value; }
            }
        }

        /// <summary>
        /// Contains information about each attributeEnhancer (implant)
        /// </summary>
        public class AttributeEnhancer
        {
            private string name = string.Empty;
            private int _value = 0;

            /// <summary>
            /// The name of the implant
            /// </summary>
            public string Name
            {
                get { return name; }
                set { name = value; }
            }
            /// <summary>
            /// The implant's effect on that attribute
            /// </summary>
            public int Value
            {
                get { return _value; }
                set { _value = value; }
            }
        }
    }
}
