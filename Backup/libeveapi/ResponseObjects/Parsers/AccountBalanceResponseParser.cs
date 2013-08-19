using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace libeveapi.ResponseObjects.Parsers
{
    ///<summary>
    /// A parser which converts a given <see cref="XmlDocument"/> to a <see cref="AccountBalance"/>.
    ///</summary>
    internal class AccountBalanceResponseParser : IApiResponseParser<AccountBalance>
    {
        public AccountBalance Parse(XmlDocument xmlDocument)
        {
            this.CheckVersion(xmlDocument);
            AccountBalance accountBalance = new AccountBalance();
            accountBalance.ParseCommonElements(xmlDocument);

            List<AccountBalance.AccountBalanceItem> accountList = new List<AccountBalance.AccountBalanceItem>();
            foreach (XmlNode accountRow in xmlDocument.SelectNodes("//rowset[@name='accounts']/row"))
            {
                AccountBalance.AccountBalanceItem account = new AccountBalance.AccountBalanceItem();
                account.AccountId = Convert.ToInt32(accountRow.Attributes["accountID"].InnerText, CultureInfo.InvariantCulture);
                account.AccountKey = Convert.ToInt32(accountRow.Attributes["accountKey"].InnerText, CultureInfo.InvariantCulture);
                account.Balance = Convert.ToDouble(accountRow.Attributes["balance"].InnerText, CultureInfo.InvariantCulture);
                accountList.Add(account);
            }
            accountBalance.AccountBalanceItems = accountList.ToArray();

            return accountBalance;
        }

        public void CheckVersion(XmlDocument xmlDocument)
        {
            if (AccountBalance.VersionCheck)
            {
                string version = xmlDocument.SelectSingleNode("//eveapi").Attributes["version"].InnerText;
                if (version.CompareTo(AccountBalance.API_VERSION) != 0)
                {
                    throw new ApiVersionException(version, AccountBalance.API_VERSION);
                }
            }
        }
    }
}
