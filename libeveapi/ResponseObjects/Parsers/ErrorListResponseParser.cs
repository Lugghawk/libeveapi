using System.Xml;

namespace libeveapi.ResponseObjects.Parsers
{
    ///<summary>
    /// A parser which converts a given <see cref="XmlDocument"/> to a <see cref="ErrorList"/>.
    ///</summary>
    internal class ErrorListResponseParser : IApiResponseParser<ErrorList>
    {
        public ErrorList Parse(XmlDocument xmlDocument)
        {
            this.CheckVersion(xmlDocument);

            ErrorList errorList = new ErrorList();
            errorList.ParseCommonElements(xmlDocument);

            foreach (XmlNode errorNode in xmlDocument.SelectNodes("//rowset[@name='errors']/row"))
            {
                errorList.ErrorTable.Add(errorNode.Attributes["errorCode"].InnerText, errorNode.Attributes["errorText"].InnerText);
            }

            return errorList;
        }

        public void CheckVersion(XmlDocument xmlDocument)
        {
            if (ErrorList.VersionCheck)
            {
                string version = xmlDocument.SelectSingleNode("//eveapi").Attributes["version"].InnerText;
                if (version.CompareTo(ErrorList.API_VERSION) != 0)
                {
                    throw new ApiVersionException(version, ErrorList.API_VERSION);
                }
            }
        }
    }
}
