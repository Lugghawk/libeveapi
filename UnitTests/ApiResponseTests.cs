using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using NUnit.Framework;
using libeveapi;

namespace UnitTests
{
    [TestFixture]
    public class ApiResponseTests
    {
        [SetUp]
        public void Setup()
        {
            Utility.UseLocalUrls();
        }

        [Test]
        public void CommonElementParsing()
        {
            ResponseCache.Clear();
            CharacterList characterList = EveApi.GetAccountCharacters(432435, "apiKey");

            DateTime currentTime = new DateTime(2007, 12, 12, 11, 48, 50);
            DateTime cachedUntil = new DateTime(2008, 12, 12, 12, 48, 50);

            Assert.AreEqual(currentTime, characterList.CurrentTime);
            Assert.AreEqual(cachedUntil, characterList.CachedUntil);
            Assert.AreEqual(currentTime.ToLocalTime(), characterList.CurrentTimeLocal);
            Assert.AreEqual(cachedUntil.ToLocalTime(), characterList.CachedUntilLocal);
            Assert.AreEqual(false, characterList.FromCache);
        }

        [Test]
        [ExpectedException(typeof(ApiResponseErrorException))]
        public void ApiResponseError()
        {
            XmlDocument xmlDoc = Network.GetXml(Constants.ApiPrefix + Constants.ExampleError);
            ApiResponse apiResponse = new ApiResponse();
            apiResponse.ParseCommonElements(xmlDoc);
        }
    }
}
