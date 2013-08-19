using System;
using System.Collections.Generic;
using System.Text;

namespace libeveapi
{
    /// <summary>
    /// Exception thrown when Eve XmlResponse Version does not match
    /// Response Object Version
    /// </summary>
    public class ApiVersionException : Exception
    {
        /// <summary>
        /// Version of Eve Xml Response
        /// </summary>
        public string ApiVersion;
        /// <summary>
        /// Version of Response Object
        /// </summary>
        public string ObjectVersion;
        /// <summary>
        /// Message that describes the Exception
        /// </summary>
        public override string Message
        {
            get
            {
                return string.Format("Xml Version Response Mismatch\nXml Version: {0}\nObject Version: {1}", this.ApiVersion, this.ObjectVersion);
            }
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public ApiVersionException() : base("Xml Version Response Missmatch") { }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Description of error</param>
        public ApiVersionException(string message) : base(message) { }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="apiVersion">Version of Xml Response from Eve</param>
        /// <param name="objectVersion">Version of ResponseObject</param>
        public ApiVersionException(string apiVersion, string objectVersion)
        {
            ApiVersion = apiVersion;
            ObjectVersion = objectVersion;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Description of error</param>
        /// <param name="apiVersion">Version of Xml Response from Eve</param>
        /// <param name="objectVersion">Version of ResponseObject</param>
        public ApiVersionException(string message, string apiVersion, string objectVersion)
            : base(message)
        {
            this.ApiVersion = apiVersion;
            this.ObjectVersion = objectVersion;
        }
    }
}
