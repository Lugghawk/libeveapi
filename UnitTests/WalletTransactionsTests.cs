using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using libeveapi;

namespace UnitTests
{
    [TestFixture]
    public class WalletTransactionsTests
    {
        [SetUp]
        public void Setup()
        {
            Utility.UseLocalUrls();
        }

        [Test]
        public void GetCharWalletTransactionsTest()
        {
            WalletTransactions walletTransactions = EveApi.GetWalletTransactionsList(WalletTransactionListType.Character, 0, 0, "apiKey");

            Assert.AreEqual(7, walletTransactions.WalletTransactionItems.Length);

            WalletTransactions.WalletTransactionItem transaction = walletTransactions.WalletTransactionItems[0];
            Assert.AreEqual(7, walletTransactions.WalletTransactionItems.Length);
            Assert.AreEqual(new DateTime(2010, 02, 07, 03, 34, 00), transaction.TransactionDateTime);
            Assert.AreEqual(1309776438, transaction.TransactionId);
            Assert.AreEqual(1, transaction.Quantity);
            Assert.AreEqual("Information Warfare", transaction.TypeName);
            Assert.AreEqual(20495, transaction.TypeId);
//            Assert.AreEqual(34101.06, transaction.Price);
            Assert.AreEqual(1034922339, transaction.ClientId);
            Assert.AreEqual("Elthana", transaction.ClientName);
            Assert.AreEqual(60003760, transaction.StationId);
            Assert.AreEqual("Jita IV - Moon 4 - Caldari Navy Assembly Plant", transaction.StationName);
            Assert.AreEqual("buy", transaction.TransactionType);
            Assert.AreEqual("personal", transaction.TransactionFor);
        }

        [Test]
        public void GetCorpWalletTransactionsTest()
        {
            WalletTransactions walletTransactions = EveApi.GetWalletTransactionsList(WalletTransactionListType.Corporation, 0, 0, "apiKey");
            
            WalletTransactions.WalletTransactionItem transaction = walletTransactions.WalletTransactionItems[0];
            Assert.AreEqual(4, walletTransactions.WalletTransactionItems.Length);
            Assert.AreEqual(new DateTime(2008, 08, 04, 22, 01, 00), transaction.TransactionDateTime);
            Assert.AreEqual(705664738, transaction.TransactionId);
            Assert.AreEqual(50000, transaction.Quantity);
            Assert.AreEqual("Oxygen Isotopes", transaction.TypeName);
            Assert.AreEqual(17887, transaction.TypeId);
            Assert.AreEqual(250.00, transaction.Price);
            Assert.AreEqual(174312871, transaction.ClientId);
            Assert.AreEqual("ACHAR", transaction.ClientName);
            Assert.AreEqual(000000000, transaction.CharacterId);
            Assert.AreEqual("SELLER", transaction.CharacterName);
            Assert.AreEqual(60004375, transaction.StationId);
            Assert.AreEqual("SYSTEM IV - Moon 10 - Corporate Police Force Testing Facilities", transaction.StationName);
            Assert.AreEqual("buy", transaction.TransactionType);
            Assert.AreEqual("corporation", transaction.TransactionFor);
        }

        [Test]
        public void WalletTransactionsPersist()
        {
            ResponseCache.Clear();

            WalletTransactions walletTransactions = EveApi.GetWalletTransactionsList(WalletTransactionListType.Corporation, 0, 0, "apiKey");
            ResponseCache.Save("ResponseCache.xml");
            ResponseCache.Clear();
            ResponseCache.Load("ResponseCache.xml");
            WalletTransactions cachedwalletTransactions = EveApi.GetWalletTransactionsList(WalletTransactionListType.Corporation, 0, 0, "apiKey");

            Assert.AreEqual(walletTransactions.CachedUntilLocal, cachedwalletTransactions.CachedUntilLocal);

            for (int i = 0; i < walletTransactions.WalletTransactionItems.Length; i++)
            {
                Assert.AreEqual(walletTransactions.WalletTransactionItems[i].CharacterId, cachedwalletTransactions.WalletTransactionItems[i].CharacterId);
                Assert.AreEqual(walletTransactions.WalletTransactionItems[i].ClientName, cachedwalletTransactions.WalletTransactionItems[i].ClientName);
                Assert.AreEqual(walletTransactions.WalletTransactionItems[i].Quantity, cachedwalletTransactions.WalletTransactionItems[i].Quantity);
            }
             
        }
    }
}
