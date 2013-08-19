using System.Collections.Generic;

namespace libeveapi
{
    /// <summary>
    /// The <see cref="ApiRequestUrl"/> represents the URL to which the request should be sent.
    /// </summary>
    internal class ApiRequestUrl
    {
        internal const string PROPERTY_USER_ID = "keyID";
        internal const string PROPERTY_CHARACTER_ID = "characterID";
        internal const string PROPERTY_API_KEY = "vCode";
        internal const string PROPERTY_VERSION = "version";
        internal const string PROPERTY_NAMES = "names";
        internal const string PROPERTY_IDENTIFICATIONS = "ids";
        internal const string PROPERTY_ITEM_ID = "itemID";
        internal const string PROPERTY_BEFORE_KILL_ID = "beforeKillID";
        internal const string PROPERTY_BEFORE_REF_ID = "beforeRefID";
        internal const string PROPERTY_BEFORE_TRANSACTION_ID = "beforeTransID";
        internal const string PROPERTY_CORPORATION_ID = "corporationID";
        internal const string PROPERTY_ACCOUNT_KEY = "accountKey";

        private readonly string requestPage;
        private readonly List<KeyValuePair<string, string>> customProperties = new List<KeyValuePair<string, string>>();
        
        /// <param name="requestPage">The address to which the request should be sent (e.g. "/account/Characters.xml.aspx").</param>
        internal ApiRequestUrl(string requestPage)
        {
            this.requestPage = requestPage;
        }

        /// <summary>
        /// Adds a property to the <see cref="ApiRequestUrl"/> which will be appended as a parameter.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value for the parameter.</param>
        /// <remarks>Since the properties are known at compile-time I don't deem a RemoveProperty method to be necessary.</remarks>
        internal void AddProperty( string name, string value )
        {
            customProperties.Add(new KeyValuePair<string, string>(name, value));
        }

        /// <summary>
        /// Retrieves the string representation of the URL.
        /// </summary>
        internal string Url
        {
            get
            {
                string value = string.Format("{0}{1}", Constants.ApiPrefix, requestPage);

                if( customProperties.Count > 0 )
                {
                    value += "?";
                }

                for (int i = 0; i < customProperties.Count; i++)
                {
                    KeyValuePair<string, string> property = customProperties[i];
                    value += string.Format("{0}={1}", property.Key, property.Value);

                    if( i + 1 < customProperties.Count )
                    {
                        value += "&";
                    }
                }

                return value;
            }
        }
    }
}
