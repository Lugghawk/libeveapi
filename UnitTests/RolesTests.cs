using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using libeveapi;

namespace UnitTests
{
    [TestFixture]
    public class RolesTests
    {
        [Test]
        public void DecodeMask()
        {
            Roles roles = new Roles("1");
            Assert.IsTrue(roles.HasRole(RoleTypes.Director));
            Assert.IsFalse(roles.HasRole(RoleTypes.FactoryManager));

            roles = new Roles("128");
            Assert.IsFalse(roles.HasRole(RoleTypes.Director));
            Assert.IsTrue(roles.HasRole(RoleTypes.PersonnelManager));

            roles = new Roles("1281");
            Assert.IsTrue(roles.HasRole(RoleTypes.Director));
            Assert.IsFalse(roles.HasRole(RoleTypes.PersonnelManager));
            Assert.IsTrue(roles.HasRole(RoleTypes.Accountant));
            Assert.IsFalse(roles.HasRole(RoleTypes.SecurityManager));
            Assert.IsTrue(roles.HasRole(RoleTypes.FactoryManager));
        }

        [Test]
        public void GetName()
        {
            Assert.AreEqual("Director", Roles.GetRoleName(RoleTypes.Director));
        }

        [Test]
        public void GetDescription()
        {
            Assert.AreEqual("A director is, for most intents and purposes, the same as a CEO in many respects. They can hire and fire members and change job descriptions (assigning both roles and grantable roles).", 
                Roles.GetRoleDescription(RoleTypes.Director));
        }
    }
}
