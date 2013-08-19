using System.Xml;

namespace libeveapi.ResponseObjects.Parsers
{
    ///<summary>
    ///</summary>
    ///<typeparam name="T"></typeparam>
    internal interface IApiResponseParser<T> where T : ApiResponse
    {
        ///<summary>
        /// Parses a <see cref="XmlDocument" /> to an appropriate <see cref="ApiResponse" /> of type <typeparamref name="T"/>.
        ///</summary>
        ///<param name="xmlDocument">The <see cref="XmlDocument" /> from which an <see cref="ApiResponse" /> should be derived.</param>
        ///<returns>An <see cref="ApiResponse" /> of type <typeparamref name="T"/></returns>
        T Parse(XmlDocument xmlDocument);
        /// <summary>
        /// Checks the Eve Xml Response version with the currenct Object being parsed
        /// </summary>
        /// <param name="xmlDocument">The <see cref="XmlDocument" /> from which an <see cref="ApiResponse" /> should be derived.</param>
        void CheckVersion(XmlDocument xmlDocument);
    }
}
