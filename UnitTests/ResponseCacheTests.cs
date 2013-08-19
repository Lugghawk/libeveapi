using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using NUnit.Framework;
using libeveapi;

namespace UnitTests
{
    [TestFixture]
    public class ResponseCacheTests
    {
        [SetUp]
        public void Setup()
        {
            Utility.UseLocalUrls();
        }

        [Test]
        public void ExpiredApiResponse()
        {
            ResponseCache.Clear();
            string errorListLocation = Constants.ErrorList;
            Constants.ErrorList = "/ErrorListExpired.xml.aspx";
            ErrorList errorList = EveApi.GetErrorList();
            ErrorList errorList2 = EveApi.GetErrorList();

            Assert.AreEqual(false, errorList.FromCache);
            Assert.AreEqual(false, errorList2.FromCache);

            Constants.ErrorList = errorListLocation;
        }

        [Test]
        public void NotExpiredApiResponse()
        {
            ResponseCache.Clear();
            string errorListLocation = Constants.ErrorList;
            Constants.ErrorList = "/ErrorListNotExpired.xml.aspx";

            ErrorList errorList = EveApi.GetErrorList();
            bool errorListFromCache = errorList.FromCache;

            ErrorList errorList2 = EveApi.GetErrorList();

            Assert.AreEqual(false, errorListFromCache);
            Assert.AreEqual(true, errorList2.FromCache);

            Constants.ErrorList = errorListLocation;
        }

        [Test]
        public void UsingStreams()
        {
            ResponseCache.Clear();
            string errorListLocation = Constants.ErrorList;
            Constants.ErrorList = "/ErrorListNotExpired.xml.aspx";

            ErrorList errorList = EveApi.GetErrorList();

            using (Stream s = new FileStream("ResponseCache.xml", FileMode.Create))
            {
                ResponseCache.Save(s);
            }
            
            ResponseCache.Clear();
            
            using (Stream s = new FileStream("ResponseCache.xml", FileMode.Open))
            {
                ResponseCache.Load(s);
            }

            ErrorList cached = EveApi.GetErrorList();

            Assert.AreEqual(errorList.CachedUntilLocal, cached.CachedUntilLocal);
            Assert.AreEqual(false, errorList.FromCache);
            Assert.AreEqual(true, cached.FromCache);

            Constants.ErrorList = errorListLocation;
        }
    }
}
