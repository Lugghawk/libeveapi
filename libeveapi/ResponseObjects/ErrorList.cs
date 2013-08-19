using System.Collections.Generic;

namespace libeveapi
{
    /// <summary>
    /// 
    /// </summary>
    public class ErrorList : ApiResponse
    {
        /// <summary>
        /// API Version Compatibility
        /// </summary>
        public const string API_VERSION = "2";
        /// <summary>
        /// Error Table containing a list of Eve Api Errors keyed by thier code.
        /// </summary>
        public SerializableDictionary<string, string> ErrorTable = new SerializableDictionary<string, string>();

        /// <summary>
        /// Returns the description for the specified error code
        /// </summary>
        /// <param name="errorCode">Api Error code</param>
        /// <returns>Error string</returns>
        public string GetMessageForErrorCode(string errorCode)
        {
            try
            {
                return ErrorTable[errorCode];
            }
            catch (KeyNotFoundException)
            {
                return "Unknown Error Code";
            }
        }
    }
}
