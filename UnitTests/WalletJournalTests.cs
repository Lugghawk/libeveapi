using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using libeveapi;

namespace UnitTests
{
    class WalletJournalTests
    {
        [SetUp]
        public void Setup()
        {
            Utility.UseLocalUrls();
        }

        [Test]
        public void GetCharWalletJournalTest()
        {
            WalletJournal walletJournal = EveApi.GetWalletJournal(WalletJournalType.Character, 0, 0, "apiKey");

            WalletJournal.WalletJournalItem journalEntry = walletJournal.WalletJournalItems[0];
            Assert.AreEqual(new DateTime(2008, 08, 22, 03, 36, 00), journalEntry.Date);
            Assert.AreEqual(1578932679, journalEntry.RefId);
            Assert.AreEqual(54, journalEntry.RefTypeId);
            Assert.AreEqual("corpslave", journalEntry.OwnerName1);
            Assert.AreEqual(150337897, journalEntry.OwnerId1);
            Assert.AreEqual("Secure Commerce Commission", journalEntry.OwnerName2);
            Assert.AreEqual(1000132, journalEntry.OwnerId2);
            Assert.AreEqual("", journalEntry.ArgName1);
            Assert.AreEqual(0, journalEntry.ArgId1);
            Assert.AreEqual(-8396.99, journalEntry.Amount);
            Assert.AreEqual(576336941.61, journalEntry.Balance);
            Assert.AreEqual("", journalEntry.Reason);
            Assert.AreEqual(0, journalEntry.TaxRecieverID);
            Assert.AreEqual(0, journalEntry.TaxAmount);
        }

        [Test]
        public void GetCorpWalletJournalTest()
        {
            WalletJournal walletJournal = EveApi.GetWalletJournal(WalletJournalType.Corporation, 0, 0, "apiKey");

            WalletJournal.WalletJournalItem journalEntry = walletJournal.WalletJournalItems[0];
            Assert.AreEqual(new DateTime(2008, 08, 22, 03, 36, 00), journalEntry.Date);
            Assert.AreEqual(1578932679, journalEntry.RefId);
            Assert.AreEqual(54, journalEntry.RefTypeId);
            Assert.AreEqual("corpslave", journalEntry.OwnerName1);
            Assert.AreEqual(150337897, journalEntry.OwnerId1);
            Assert.AreEqual("Secure Commerce Commission", journalEntry.OwnerName2);
            Assert.AreEqual(1000132, journalEntry.OwnerId2);
            Assert.AreEqual("", journalEntry.ArgName1);
            Assert.AreEqual(0, journalEntry.ArgId1);
            Assert.AreEqual(-8396.99, journalEntry.Amount);
            Assert.AreEqual(576336941.61, journalEntry.Balance);
            Assert.AreEqual("", journalEntry.Reason);
        }

        [Test]
        public void WalletJournalPersist()
        {
            ResponseCache.Clear();

            WalletJournal journalEntry = EveApi.GetWalletJournal(WalletJournalType.Corporation, 0, 0, "apiKey");
            ResponseCache.Save("ResponseCache.xml");
            ResponseCache.Clear();
            ResponseCache.Load("ResponseCache.xml");
            WalletJournal cachedJournalEntry = EveApi.GetWalletJournal(WalletJournalType.Corporation, 0, 0, "apiKey");

            Assert.AreEqual(journalEntry.CachedUntilLocal, cachedJournalEntry.CachedUntilLocal);


            for (int i = 0; i < journalEntry.WalletJournalItems.Length; i++)
            {
                Assert.AreEqual(journalEntry.WalletJournalItems[i].Date, cachedJournalEntry.WalletJournalItems[i].Date);
                Assert.AreEqual(journalEntry.WalletJournalItems[i].Amount, cachedJournalEntry.WalletJournalItems[i].Amount);
                Assert.AreEqual(journalEntry.WalletJournalItems[i].Balance, cachedJournalEntry.WalletJournalItems[i].Balance);
            }

        }
    }
}
