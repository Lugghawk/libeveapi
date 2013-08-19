using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using libeveapi;

namespace UnitTests
{
    [TestFixture]    
    public class ErrorListTests
    {
        [SetUp]
        public void Setup()
        {
            Utility.UseLocalUrls();
        }

        [Test]
        public void GetErrorList()
        {
            ResponseCache.Clear();
            ErrorList errorList = EveApi.GetErrorList();

            Assert.AreEqual("Expected before ref/trans ID = 0: wallet not previously loaded.", errorList.GetMessageForErrorCode("100"));
            Assert.AreEqual("Current security level not high enough.", errorList.GetMessageForErrorCode("200"));
            Assert.AreEqual("User forced test error condition.", errorList.GetMessageForErrorCode("999"));
        }

        [Test]
        public void ErrorListPersist()
        {
            ResponseCache.Clear();
            ErrorList errorList = EveApi.GetErrorList();

            ResponseCache.Save("ResponseCache.xml");
            ResponseCache.Clear();
            ResponseCache.Load("ResponseCache.xml");
            ErrorList cachedErrorList = EveApi.GetErrorList();

            Assert.AreEqual(errorList.CachedUntilLocal, cachedErrorList.CachedUntilLocal);

            Assert.AreEqual(errorList.GetMessageForErrorCode("100"), cachedErrorList.GetMessageForErrorCode("100"));
            Assert.AreEqual(errorList.GetMessageForErrorCode("200"), cachedErrorList.GetMessageForErrorCode("200"));
            Assert.AreEqual(errorList.GetMessageForErrorCode("999"), cachedErrorList.GetMessageForErrorCode("999"));
        }
    }
}
