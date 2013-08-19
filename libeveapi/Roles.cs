using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace libeveapi
{
    /// <summary>
    /// Roles of a Corporation Member
    /// </summary>
    public class Roles
    {
        /// <summary>
        /// Hashtable containing roles for the Corporation Member
        /// </summary>
        protected Hashtable rolesTable = new Hashtable();
        public List<RoleTypes> RoleList = new List<RoleTypes>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="roleMask">String representing Roles</param>
        public Roles(string roleMask):this(Convert.ToUInt64(roleMask))
        {
            //ulong mask = Convert.ToUInt64(roleMask);
            //decodeRoles(mask);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="roleMask">64bit Unsigned Integer representing Roles</param>
        public Roles(ulong roleMask)
        {
            decodeRoles(roleMask);
        }

        /// <summary>
        /// This loops through each role and checks to see if it has been assigned
        /// </summary>
        /// <param name="mask">64bit Unsigned Integer representing Roles</param>
        private void decodeRoles(ulong mask)
        {
            foreach (RoleTypes roleType in Enum.GetValues(typeof(RoleTypes)))
            {
                if (((ulong)roleType & mask) > 0)
                {
                    rolesTable.Add(roleType, 1);
                    RoleList.Add(roleType);
                }
            }
        }

        /// <summary>
        /// Return true if the specified role is present
        /// </summary>
        /// <param name="roleType">Type of role</param>
        /// <returns>True/False</returns>
        public bool HasRole(RoleTypes roleType)
        {
            if (rolesTable.Contains(roleType))
            {
                return true;
            }

            return false;
        }

        

        /// <summary>
        /// Get the name of a role
        /// </summary>
        /// <param name="en">Type of role</param>
        /// <returns>Name of the role</returns>
        public static string GetRoleName(RoleTypes en)
        {
            Type type = en.GetType();
            MemberInfo[] memberInfo = type.GetMember(en.ToString());

            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attributes = memberInfo[0].GetCustomAttributes(typeof(Name), false);
                if (attributes != null && attributes.Length > 0)
                {
                    return ((Name)attributes[0]).Text;
                }
            }
            return en.ToString();
        }

        /// <summary>
        /// Get the description of a role
        /// </summary>
        /// <param name="en">Type of role</param>
        /// <returns>Description of the RoleType</returns>
        public static string GetRoleDescription(RoleTypes en)
        {
            Type type = en.GetType();
            MemberInfo[] memberInfo = type.GetMember(en.ToString());

            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attributes = memberInfo[0].GetCustomAttributes(typeof(Description), false);
                if (attributes != null && attributes.Length > 0)
                {
                    return ((Description)attributes[0]).Text;
                }
            }

            return en.ToString();
        }
    }

    /// <summary>
    /// The name of a role
    /// </summary>
    class Name : Attribute
    {
        public string Text;

        public Name(string text)
        {
            this.Text = text;
        }
    }

    /// <summary>
    /// The description of a role
    /// </summary>
    class Description : Attribute
    {
        public string Text;

        public Description(string text)
        {
            this.Text = text;
        }
    }

    /// <summary>
    /// Character Roles
    /// </summary>
    public enum RoleTypes : ulong
    {
        /// <summary>
        /// 
        /// </summary>
        [Name("Director")]
        [Description("A director is, for most intents and purposes, the same as a CEO in many respects. They can hire and fire members and change job descriptions (assigning both roles and grantable roles).")]
        Director = 1,

        /// <summary>
        /// 
        /// </summary>
        [Name("Config Equipment")]
        [Description("This role allows the holder to anchor, unanchor, rename, and configure various types of objects in space.")]
        ConfigEquipment = 1 << 41,

        /// <summary>
        /// 
        /// </summary>
        [Name("Config Starbase")]
        [Description("This role allows the holder to perform star base configuration.")]
        ConfigStarbase = 1 << 53,

        /// <summary>
        /// 
        /// </summary>
        [Name("Junior Accountant")]
        [Description("The junior accountant is a cut down version of the accountant role. It essentially allows the holder to view the data that an accountant views. However it does not allow the holder to perform the actions that an accountant can.")]
        JuniorAccountant = 1 << 52,

        /// <summary>
        /// 
        /// </summary>
        [Name("Rent Factory")]
        [Description("This role allows the holder to rent factory slots for the corporation. ")]
        RentFactory = 1 << 50,

        /// <summary>
        /// 
        /// </summary>
        [Name("Rent Office")]
        [Description("The role Rent Office allows the holder to rent offices for the corporation. ")]
        RentOffice = 1 << 49,

        /// <summary>
        /// 
        /// </summary>
        [Name("Rent Research Facility")]
        [Description("This role allows the holder to rent research facilities for the corporation. ")]
        RentResearchFacility = 1 << 51,


        /// <summary>
        /// 
        /// </summary>
        [Name("Personel Manager")]
        [Description("A personnel manager can process applications and sign up new members.")]
        PersonnelManager = 1 << 7,

        /// <summary>
        /// 
        /// </summary>
        [Name("Accountant")]
        [Description("Accountants can monitor and manage the financial affairs of the corporation. They can view corporation bills in the NeoCom “Bills” panel.")]
        Accountant = 1 << 8,

        /// <summary>
        /// 
        /// </summary>
        [Name("Security Manager")]
        [Description("The role security officer allows the holder to view the contents of the deliveries window in station. This is the window where items bought by the corporation at a station are placed. If the corporation has a corporate hangar at a station, the role Security Officer grants view access to the contents of the members' hangars. This access also facilitates the placing of items into the members' hangars.")]
        SecurityManager = 1 << 9,

        /// <summary>
        /// 
        /// </summary>
        [Name("Factory Manager")]
        [Description("The factory manager can eject blueprints from corporation-owned factories and research slots that belong to members. Additionally, they can see what is going on in any corporate factory and research slot. They have the ability to manufacture items for the corporation using blueprints and minerals from the corporation’s hangar floor and research blueprints in research slots. ")]
        FactoryManager = 1 << 10,

        /// <summary>
        /// 
        /// </summary>
        [Name("Station Manager")]
        [Description("As the name implies, station managers can perform station management tasks for corporation-owned facilities. These tasks are varied and include adjusting the standing-based modifiers for allowing docking and the charges for it, as well as permission to access factories, the reprocessing plant, the repair shop, and also how modifiers should be applied for costs based upon standings for these tasks.")]
        StationManager = 1 << 11,

        /// <summary>
        /// 
        /// </summary>
        [Name("Auditor")]
        [Description("The Auditor role allows the holder to check the role history of members of the corporation. ")]
        Auditor = 1 << 12,

        /* Corp Hangers */
        /// <summary>
        /// This allows a person to take from the Division 1 Hanger
        /// </summary>
        TakeFromDivision1Hangar = (ulong)1 << 13,
        /// <summary>
        /// This allows a person to take from the Division 2 Hanger
        /// </summary>
        TakeFromDivision2Hangar = (ulong)1 << 14,
        /// <summary>
        /// This allows a person to take from the Division 3 Hanger
        /// </summary>
        TakeFromDivision3Hangar = (ulong)1 << 15,
        /// <summary>
        /// This allows a person to take from the Division 4 Hanger
        /// </summary>
        TakeFromDivision4Hangar = (ulong)1 << 16,
        /// <summary>
        /// This allows a person to take from the Division 5 Hanger
        /// </summary>
        TakeFromDivision5Hangar = (ulong)1 << 17,
        /// <summary>
        /// This allows a person to take from the Division 6 Hanger
        /// </summary>
        TakeFromDivision6Hangar = (ulong)1 << 18,
        /// <summary>
        /// This allows a person to take from the Division 7 Hanger
        /// </summary>
        TakeFromDivision7Hangar = (ulong)1 << 19,

        /// <summary>
        /// This allows a person to query from the Division 1 Hanger
        /// </summary>
        QueryDivision1Hangar = (ulong)1 << 20,
        /// <summary>
        /// This allows a person to query from the Division 2 Hanger
        /// </summary>
        QueryDivision2Hangar = (ulong)1 << 21,
        /// <summary>
        /// This allows a person to query from the Division 3 Hanger
        /// </summary>
        QueryDivision3Hangar = (ulong)1 << 22,
        /// <summary>
        /// This allows a person to query from the Division 4 Hanger
        /// </summary>
        QueryDivision4Hangar = (ulong)1 << 23,
        /// <summary>
        /// This allows a person to query from the Division 5 Hanger
        /// </summary>
        QueryDivision5Hangar = (ulong)1 << 24,
        /// <summary>
        /// This allows a person to query from the Division 6 Hanger
        /// </summary>
        QueryDivision6Hangar = (ulong)1 << 25,
        /// <summary>
        /// This allows a person to query from the Division 7 Hanger
        /// </summary>
        QueryDivision7Hangar = (ulong)1 << 26,

        /* Corp Wallets */
        /// <summary>
        /// This allows a person to take ISK from the Division 1 Account
        /// </summary>
        TakeFromDivision1Accounts = (ulong)1 << 27,
        /// <summary>
        /// This allows a person to take ISK from the Division 2 Account
        /// </summary>
        TakeFromDivision2Accounts = (ulong)1 << 28,
        /// <summary>
        /// This allows a person to take ISK from the Division 3 Account
        /// </summary>
        TakeFromDivision3Accounts = (ulong)1 << 29,
        /// <summary>
        /// This allows a person to take ISK from the Division 4 Account
        /// </summary>
        TakeFromDivision4Accounts = (ulong)1 << 30,
        /// <summary>
        /// This allows a person to take ISK from the Division 5 Account
        /// </summary>
        TakeFromDivision5Accounts = (ulong)1 << 31,
        /// <summary>
        /// This allows a person to take ISK from the Division 6 Account
        /// </summary>
        TakeFromDivision6Accounts = (ulong)1 << 32,
        /// <summary>
        /// This allows a person to take ISK from the Division 7 Account
        /// </summary>
        TakeFromDivision7Accounts = (ulong)1 << 33,

        /// <summary>
        /// This allows a person to look at how much ISK is in Division 1 Account
        /// </summary>
        QueryDivision1Accounts = (ulong)1 << 34,
        /// <summary>
        /// This allows a person to look at how much ISK is in Division 2 Account
        /// </summary>
        QueryDivision2Accounts = (ulong)1 << 35,
        /// <summary>
        /// This allows a person to look at how much ISK is in Division 3 Account
        /// </summary>
        QueryDivision3Accounts = (ulong)1 << 36,
        /// <summary>
        /// This allows a person to look at how much ISK is in Division 4 Account
        /// </summary>
        QueryDivision4Accounts = (ulong)1 << 37,
        /// <summary>
        /// This allows a person to look at how much ISK is in Division 5 Account
        /// </summary>
        QueryDivision5Accounts = (ulong)1 << 38,
        /// <summary>
        /// This allows a person to look at how much ISK is in Division 6 Account
        /// </summary>
        QueryDivision6Accounts = (ulong)1 << 39,
        /// <summary>
        /// This allows a person to look at how much ISK is in Division 7 Account
        /// </summary>
        QueryDivision7Accounts = (ulong)1 << 40,

        /* Container Access in Hangers */
        /// <summary>
        /// This allows a person to take a container from the Division 1 Hanger
        /// </summary>
        ContainerCanTakeDivision1 = (ulong)1 << 42,
        /// <summary>
        /// This allows a person to take a container from the Division 2 Hanger
        /// </summary>
        ContainerCanTakeDivision2 = (ulong)1 << 43,
        /// <summary>
        /// This allows a person to take a container from the Division 3 Hanger
        /// </summary>
        ContainerCanTakeDivision3 = (ulong)1 << 44,
        /// <summary>
        /// This allows a person to take a container from the Division 4 Hanger
        /// </summary>
        ContainerCanTakeDivision4 = (ulong)1 << 45,
        /// <summary>
        /// This allows a person to take a container from the Division 5 Hanger
        /// </summary>
        ContainerCanTakeDivision5 = (ulong)1 << 46,
        /// <summary>
        /// This allows a person to take a container from the Division 6 Hanger
        /// </summary>
        ContainerCanTakeDivision6 = (ulong)1 << 47,
        /// <summary>
        /// This allows a person to take a container from the Division 7 Hanger
        /// </summary>
        ContainerCanTakeDivision7 = (ulong)1 << 48,
    }
}
