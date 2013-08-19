using System;
using System.Globalization;
using System.Xml;

namespace libeveapi.ResponseObjects.Parsers
{
    ///<summary>
    /// A parser which converts a given <see cref="XmlDocument"/> to a <see cref="RefTypes"/>.
    ///</summary>
    internal class RefTypesResponseParser : IApiResponseParser<RefTypes>
    {
        public RefTypes Parse(XmlDocument xmlDocument)
        {
            this.CheckVersion(xmlDocument);
            RefTypes refTypes = new RefTypes();
            refTypes.ParseCommonElements(xmlDocument);

            foreach (XmlNode refTypeNode in xmlDocument.SelectNodes("//rowset[@name='refTypes']/row"))
            {
                refTypes.ReferenceTypes.Add(Convert.ToInt32(refTypeNode.Attributes["refTypeID"].InnerText, CultureInfo.InvariantCulture), refTypeNode.Attributes["refTypeName"].InnerText);
            }

            return refTypes;
        }

        public void CheckVersion(XmlDocument xmlDocument)
        {
            if (RefTypes.VersionCheck)
            {
                string version = xmlDocument.SelectSingleNode("//eveapi").Attributes["version"].InnerText;
                if (version.CompareTo(RefTypes.API_VERSION) != 0)
                {
                    throw new ApiVersionException(version, RefTypes.API_VERSION);
                }
            }
        }
    }
}
