using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using NUnit.Framework;
using libeveapi;

namespace UnitTests
{
    [TestFixture]
    public class PortraitTests
    {
        [Test]
        public void DownloadTest()
        {
            string backup = Constants.ImageFullURL;
            Constants.ImageFullURL = "http://localhost/eveapi/test.jpg";

            Image image = EveApi.GetCharacterPortrait(1234, PortraitSize.Small);

            Assert.AreNotEqual(null, image);
            Assert.AreEqual(64, image.Width);
            Assert.AreEqual(64, image.Height);

            Constants.ImageFullURL = backup;
        }
    }
}
