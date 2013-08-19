namespace libeveapi
{
    /// <summary>
    /// A list of characters on the account
    /// </summary>
    public class CharacterList : ApiResponse
    {
        /// <summary>
        /// API Version Compatibility
        /// </summary>
        public const string API_VERSION = "2";
        /// <summary>
        /// List of characters associated with an account
        /// </summary>
        public CharacterListItem[] CharacterListItems = new CharacterListItem[0];

        /// <summary>
        /// Represents a character from the account character list
        /// </summary>
        public class CharacterListItem
        {
            private string name;
            private int characterId;
            private string corporationName;
            private int corporationId;

            /// <summary>
            /// Name of the character
            /// </summary>
            public string Name
            {
                get { return name; }
                set { name = value; }
            }

            /// <summary>
            /// Id of the character
            /// </summary>
            public int CharacterId
            {
                get { return characterId; }
                set { characterId = value; }
            }

            /// <summary>
            /// Name of the corporation the character is a member of
            /// </summary>
            public string CorporationName
            {
                get { return corporationName; }
                set { corporationName = value; }
            }

            /// <summary>
            /// Id of the corporation the character is a member of
            /// </summary>
            public int CorporationId
            {
                get { return corporationId; }
                set { corporationId = value; }
            }
        }
    }
}
