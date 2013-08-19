using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using libeveapi;

namespace UnitTests
{
    [TestFixture]
    public class MemberTrackingTests
    {
        [SetUp]
        public void Setup()
        {
            Utility.UseLocalUrls();
        }

        [Test]
        public void GetMemberTracking()
        {
            ResponseCache.Clear();
            MemberTracking memberTracking = EveApi.GetMemberTracking(432435, 234523, "fullApiKey");

            Assert.AreEqual(memberTracking.Members.Length, 2);
            // First member
            MemberTracking.Member tracking = memberTracking.Members[0];
            Assert.AreEqual(150336922, tracking.CharacterId);
            Assert.AreEqual("corpexport", tracking.Name);
            Assert.AreEqual(0, tracking.BaseId);
            Assert.AreEqual(string.Empty, tracking.Base);
            Assert.AreEqual("asdf", tracking.Title);
            Assert.AreEqual(60011566, tracking.LocationId);
            Assert.AreEqual("Bourynes VII - Moon 2 - University of Caille School", tracking.Location);
            Assert.AreEqual(606, tracking.ShipTypeId);
            Assert.AreEqual("Velator", tracking.ShipType);
            Assert.AreEqual("0", tracking.RolesMask);
            Assert.AreEqual("0", tracking.GrantableRoles);

            Assert.AreEqual(new DateTime(2007, 6, 13, 14, 39, 00), tracking.StartDateTime);
            Assert.AreEqual(new DateTime(2007, 6, 16, 21, 12, 00), tracking.LogonDateTime);
            Assert.AreEqual(new DateTime(2007, 6, 16, 21, 36, 00), tracking.LogoffDateTime);

            // Second Member
            tracking = memberTracking.Members[1];
            Assert.AreEqual(150337897, tracking.CharacterId);
            Assert.AreEqual("corpslave", tracking.Name);
            Assert.AreEqual(0, tracking.BaseId);
            Assert.AreEqual(string.Empty, tracking.Base);
            Assert.AreEqual(string.Empty, tracking.Title);
            Assert.AreEqual(60011566, tracking.LocationId);
            Assert.AreEqual("Bourynes VII - Moon 2 - University of Caille School", tracking.Location);
            Assert.AreEqual(670, tracking.ShipTypeId);
            Assert.AreEqual("Capsule", tracking.ShipType);
            Assert.AreEqual("22517998271070336", tracking.RolesMask);
            Assert.AreEqual("0", tracking.GrantableRoles);

            Assert.AreEqual(new DateTime(2007, 6, 14, 13, 14, 00), tracking.StartDateTime);
            Assert.AreEqual(new DateTime(2007, 6, 16, 21, 14, 00), tracking.LogonDateTime);
            Assert.AreEqual(new DateTime(2007, 6, 16, 21, 35, 00), tracking.LogoffDateTime);
        }

        [Test]
        public void MemberTrackingPersist()
        {
            ResponseCache.Clear();
            MemberTracking memberTracking = EveApi.GetMemberTracking(432435, 234523, "fullApiKey");

            ResponseCache.Save("ResponseCache.xml");
            ResponseCache.Clear();
            ResponseCache.Load("ResponseCache.xml");
            MemberTracking cachedMemberTracking = EveApi.GetMemberTracking(432435, 234523, "fullApiKey");

            Assert.AreEqual(memberTracking.CachedUntilLocal, cachedMemberTracking.CachedUntilLocal);
            Assert.AreEqual(memberTracking.Members.Length, cachedMemberTracking.Members.Length);

            for (int i = 0; i < memberTracking.Members.Length; i++)
            {
                MemberTracking.Member mti = memberTracking.Members[i];
                MemberTracking.Member cmti = cachedMemberTracking.Members[i];

                Assert.AreEqual(mti.CharacterId, cmti.CharacterId);
                Assert.AreEqual(mti.Name, cmti.Name);
                Assert.AreEqual(mti.BaseId, cmti.BaseId);
                Assert.AreEqual(mti.Base, cmti.Base);
                Assert.AreEqual(mti.Title, cmti.Title);
                Assert.AreEqual(mti.LocationId, cmti.LocationId);
                Assert.AreEqual(mti.Location, cmti.Location);
                Assert.AreEqual(mti.ShipTypeId, cmti.ShipTypeId);
                Assert.AreEqual(mti.ShipType, cmti.ShipType);
                Assert.AreEqual(mti.RolesMask, cmti.RolesMask);
                Assert.AreEqual(mti.GrantableRoles, cmti.GrantableRoles);
                Assert.AreEqual(mti.StartDateTime, cmti.StartDateTime);
                Assert.AreEqual(mti.LogonDateTime, cmti.LogonDateTime);
                Assert.AreEqual(mti.LogoffDateTime, cmti.LogoffDateTime);
                Assert.AreEqual(mti.StartDateTimeLocal, cmti.StartDateTimeLocal);
                Assert.AreEqual(mti.LogonDateTimeLocal, cmti.LogonDateTimeLocal);
                Assert.AreEqual(mti.LogoffDateTimeLocal, cmti.LogoffDateTimeLocal);
            }
        }
    }
}
