using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using libeveapi;

namespace UnitTests
{
    [TestFixture]
    public class JournalEntriesTests
    {
        [SetUp]
        public void Setup()
        {
            Utility.UseLocalUrls();
        }

        [Test]
        public void GetCharJournalEntriesTest()
        {
            JournalEntries.VersionCheck = false;
            JournalEntries journal = EveApi.GetJournalEntryList(JournalEntryType.Character, 0, 0, "apiKey");
            JournalEntries.JournalEntryItem journalEntry = journal.JournalEntryItems[0];
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
        public void GetCorpJournalEntriesTest()
        {
            JournalEntries.VersionCheck = false;
            JournalEntries journal = EveApi.GetJournalEntryList(JournalEntryType.Corporation, 0, 0, "apiKey");
            JournalEntries.JournalEntryItem journalEntry = journal.JournalEntryItems[0];
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
        public void JournalEntriesPersist()
        {
            ResponseCache.Clear();

            JournalEntries.VersionCheck = false;
            JournalEntries journalEntry = EveApi.GetJournalEntryList(JournalEntryType.Corporation, 0, 0, "apiKey");
            ResponseCache.Save("ResponseCache.xml");
            ResponseCache.Clear();
            ResponseCache.Load("ResponseCache.xml");
            JournalEntries cachedJournalEntry = EveApi.GetJournalEntryList(JournalEntryType.Corporation, 0, 0, "apiKey");

            Assert.AreEqual(journalEntry.CachedUntilLocal, cachedJournalEntry.CachedUntilLocal);


            for (int i = 0; i < journalEntry.JournalEntryItems.Length; i++)
            {
                Assert.AreEqual(journalEntry.JournalEntryItems[i].Date, cachedJournalEntry.JournalEntryItems[i].Date);
                Assert.AreEqual(journalEntry.JournalEntryItems[i].Amount, cachedJournalEntry.JournalEntryItems[i].Amount);
                Assert.AreEqual(journalEntry.JournalEntryItems[i].Balance, cachedJournalEntry.JournalEntryItems[i].Balance);
            }
             
        }
    }
}

