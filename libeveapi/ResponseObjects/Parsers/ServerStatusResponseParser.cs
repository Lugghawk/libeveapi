using System;
using System.Globalization;
using System.Xml;

namespace libeveapi.ResponseObjects.Parsers
{
    ///<summary>
    /// A parser which converts a given <see cref="XmlDocument"/> to a <see cref="ServerStatus"/>.
    ///</summary>
    internal class ServerStatusResponseParser : IApiResponseParser<ServerStatus>
    {
        public ServerStatus Parse(XmlDocument xmlDocument)
        {
            this.CheckVersion(xmlDocument);
            ServerStatus serverStatus = new ServerStatus();
            serverStatus.ParseCommonElements(xmlDocument);

            serverStatus.ServerOpen = Convert.ToBoolean(xmlDocument.SelectSingleNode("/eveapi/result/serverOpen").InnerText);
            serverStatus.OnlinePlayers = Convert.ToInt32(xmlDocument.SelectSingleNode("/eveapi/result/onlinePlayers").InnerText);

            return serverStatus;
        }

        public void CheckVersion(XmlDocument xmlDocument)
        {
            if (ServerStatus.VersionCheck)
            {
                string version = xmlDocument.SelectSingleNode("//eveapi").Attributes["version"].InnerText;
                if (version.CompareTo(ServerStatus.API_VERSION) != 0)
                {
                    throw new ApiVersionException(version, ServerStatus.API_VERSION);
                }
            }
        }
    }
}
