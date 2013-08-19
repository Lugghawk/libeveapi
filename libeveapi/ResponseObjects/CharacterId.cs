namespace libeveapi
{
    /// <summary>
    /// Represents a character name and  CharacterId response from the eve api
    /// http://wiki.eve-dev.net/APIv2_Eve_CharacterID_XML
    /// </summary>
    public class CharacterIdName : ApiResponse
    {
        public const string API_VERSION = "2";
        private CharacterIdNameItem[] characterIdItems = new CharacterIdNameItem[0];

        /// <summary>
        /// The character name and character id that are associated with eachother
        /// </summary>
        public CharacterIdNameItem[] CharacterIdItems
        {
            get { return characterIdItems; }
            set { characterIdItems = value; }
        }

        /// <summary>
        /// An charIdItem associated with a character or corporation
        /// </summary>
        public class CharacterIdNameItem
        {
            private string name;
            private int characterId;

            /// <summary>
            /// The character's name
            /// </summary>
            public string Name
            {
                get { return name; }
                set { name = value; }
            }

            /// <summary>
            /// The characterId for the character name
            /// </summary>
            public int CharacterId
            {
                get { return characterId; }
                set { characterId = value; }
            }

        }
    }
}
