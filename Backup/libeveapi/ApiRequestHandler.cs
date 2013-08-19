using System.Diagnostics;
using System.Xml;
using libeveapi.ResponseObjects.Parsers;
using System.IO;

namespace libeveapi
{
    ///<summary>
    /// The <see cref="ApiRequestHandler{T}"/> handles request towards the underlying ResponseCache or CCP API location.
    ///</summary>
    ///<typeparam name="T">The <see cref="ApiResponse"/> type which the <see cref="ApiRequestHandler{T}"/> should handle.</typeparam>
    internal class ApiRequestHandler<T> where T : ApiResponse
    {
        private readonly IApiResponseParser<T> parser;
        private readonly bool ignoreCacheUntil;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parser">
        /// The <see cref="IApiResponseParser{T}"/> which should handle 
        /// conversion from a <see cref="XmlDocument"/> to an <see cref="ApiResponse"/>
        /// Pre-condition: the given parser should not be null.</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        internal ApiRequestHandler( IApiResponseParser<T> parser, bool ignoreCacheUntil)
        {
            Debug.Assert(parser != null);

            this.parser = parser;
            this.ignoreCacheUntil = ignoreCacheUntil;
        }

        /// <summary>
        /// Handles the request for an <see cref="ApiResponse"/>.
        /// </summary>
        /// <param name="requestUrl">The URL to which the request should be sent, if needed.</param>
        /// <returns>An <see cref="ApiResponse"/> object.</returns>
        internal T HandleRequest(ApiRequestUrl requestUrl)
        {
            string url = requestUrl.Url;

            ApiResponse cachedResponse = ResponseCache.Get(url, ignoreCacheUntil);
            if (cachedResponse != null)
            {
                return (T)cachedResponse;
            }

            XmlDocument xmlDocument = Network.GetXml(url);
            T responseObject = parser.Parse(xmlDocument);
            ResponseCache.Set(url, responseObject);

            return responseObject;
        }
    }
}
