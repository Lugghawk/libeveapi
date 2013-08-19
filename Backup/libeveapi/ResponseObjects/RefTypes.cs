using System.Collections.Generic;

namespace libeveapi
{
    /// <summary>
    /// This represents the different refence types used in the journal entries
    /// </summary>
    public class RefTypes : ApiResponse
    {
        /// <summary>
        /// API Version Compatibility
        /// </summary>
        public const string API_VERSION = "2";
        /// <summary>
        /// A reference type is made up of an int, which is the referenceId, and then a string
        /// which is the name of the reference.  They are stored in a serializable dictionary
        /// for easy searching and saving.
        /// </summary>
        public SerializableDictionary<int, string> ReferenceTypes = new SerializableDictionary<int, string>();

        /// <summary>
        /// Returns the description for the specified reference type id
        /// </summary>
        /// <param name="referenceTypeId">ID</param>
        /// <returns>Wallet Journal Description according to the referenceTypeId</returns>
        public string GetReferenceTypeNameForId(int referenceTypeId)
        {
            try
            {
                return ReferenceTypes[referenceTypeId];
            }
            catch (KeyNotFoundException)
            {
                return "Unknown Reference Type Id";
            }
        }
    }
}
