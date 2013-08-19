using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml.Serialization;
using System.Security.Cryptography;

namespace libeveapi
{
    /// <summary>
    /// The response cache is responsible for holding all of the responses when they are
    /// retrieved from the server, and then caching then, and serving them out instead
    /// of requesting a new reponse from the server, while the item is supposed to be
    /// cached.
    /// </summary>
    public class ResponseCache
    {
        private static object lockThis = new object();
        private static Hashtable hashTable = new Hashtable();

        /// <summary>
        /// Store an ApiResponse in the cache
        /// </summary>
        /// <param name="url"></param>
        /// <param name="apiResponse"></param>
        internal static void Set(string url, ApiResponse apiResponse)
        {
            url = SHA1HashString(url);
            apiResponse.HashedUrl = url;

            lock (lockThis)
            {
                if (hashTable.Contains(url))
                {
                    hashTable.Remove(url);
                }
                hashTable.Add(url, apiResponse);
            }
        }

        /// <summary>
        /// Get an ApiResponse from the cache
        /// </summary>
        /// <param name="url"></param>
        /// <returns>ApiResponse if cached ApiResponse is valid, null if it is expired</returns>
        internal static ApiResponse Get(string url)
        {
            return Get(url, false);
        }

        /// <summary>
        /// Get an ApiResponse from the cache
        /// </summary>
        /// <param name="url"></param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns>ApiResponse if cached ApiResponse is valid, null if it is expired</returns>
        internal static ApiResponse Get(string url, bool ignoreCacheUntil)
        {
            url = SHA1HashString(url);

            if (hashTable.Contains(url))
            {
                ApiResponse cachedResponse = hashTable[url] as ApiResponse;

                if (DateTime.Now >= cachedResponse.CachedUntilLocal)
                {
                    return null;
                }

                cachedResponse.FromCache = true;
                return cachedResponse;
            }

            return null;
        }

        /// <summary>
        /// Remove all saved items from the cache
        /// </summary>
        public static void Clear()
        {
            lock (lockThis)
            {
                hashTable.Clear();
            }
        }

        /// <summary>
        /// Save all currently cached items to the specified file
        /// </summary>
        /// <param name="filePath"></param>
        public static void Save(string filePath)
        {
            List<ApiResponse> apiResponses = new List<ApiResponse>();
            foreach (ApiResponse apiResponse in hashTable.Values)
            {
                apiResponses.Add(apiResponse);
            }

            using (Stream s = new FileStream(filePath, FileMode.Create))
            {
                XmlSerializer xs = new XmlSerializer(typeof(List<ApiResponse>), new Type[] { typeof(ApiResponse) });
                lock (lockThis)
                {
                    xs.Serialize(s, apiResponses);
                }
            }
        }

        /// <summary>
        /// Serialize the ResposeCache to a stream
        /// </summary>
        /// <param name="s"></param>
        public static void Save(Stream s)
        {
            List<ApiResponse> apiResponses = new List<ApiResponse>();
            foreach (ApiResponse apiResponse in hashTable.Values)
            {
                apiResponses.Add(apiResponse);
            }

            XmlSerializer xs = new XmlSerializer(typeof(List<ApiResponse>), new Type[] { typeof(ApiResponse) });
            lock (lockThis)
            {
                xs.Serialize(s, apiResponses);
            }
        }

        /// <summary>
        /// Load cached items from the specified file
        /// </summary>
        /// <param name="filePath"></param>
        public static void Load(string filePath)
        {
            using (Stream s = new FileStream(filePath, FileMode.Open))
            {
                XmlSerializer xs = new XmlSerializer(typeof(List<ApiResponse>), new Type[] { typeof(ApiResponse) });
                List<ApiResponse> apiResponses = xs.Deserialize(s) as List<ApiResponse>;
                lock (lockThis)
                {
                    hashTable.Clear();
                    foreach (ApiResponse apiResponse in apiResponses)
                    {
                        hashTable.Add(apiResponse.HashedUrl, apiResponse);
                    }
                }
            }
        }

        /// <summary>
        /// Load the ResponseCache from a stream
        /// </summary>
        /// <param name="s"></param>
        public static void Load(Stream s)
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<ApiResponse>), new Type[] { typeof(ApiResponse) });
            List<ApiResponse> apiResponses = xs.Deserialize(s) as List<ApiResponse>;

            lock (lockThis)
            {
                hashTable.Clear();
                foreach (ApiResponse apiResponse in apiResponses)
                {
                    hashTable.Add(apiResponse.HashedUrl, apiResponse);
                }
            }
        }

        private static string SHA1HashString(string input)
        {
            Byte[] clearBytes;
            Byte[] hashedBytes;
            string output = String.Empty;

            clearBytes = Encoding.UTF8.GetBytes(input);
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            sha1.ComputeHash(clearBytes);
            hashedBytes = sha1.Hash;
            sha1.Clear();
            output = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

            return output;
        }
    }
}
