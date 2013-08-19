using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using libeveapi;

namespace UnitTests
{
    [TestFixture]
    class SkillQueueTests
    {
        [SetUp]
        public void Setup()
        {
            Utility.UseLocalUrls();
        }

        [Test]
        public void GetSkillInTraining()
        {
            ResponseCache.Clear();
            SkillQueue skillqueue = EveApi.GetSkillQueue(456456,456436,"asdf");
            SkillQueue.Skill skill0 = skillqueue.SkillList[0];
            SkillQueue.Skill skill1 = skillqueue.SkillList[1];

            //Attributes of the first skill in the queue
            Assert.AreEqual(1, skill0.QueuePosition);
            Assert.AreEqual(11441, skill0.TrainingTypeId);
            Assert.AreEqual(3, skill0.TrainingToLevel);
            Assert.AreEqual(7072, skill0.TrainingStartSP);
            Assert.AreEqual(40000, skill0.TrainingEndSP);
            Assert.AreEqual(new DateTime(2009, 03, 18, 02, 01, 06), skill0.TrainingStartTime);
            Assert.AreEqual(new DateTime(2009, 03, 18, 15, 19, 21), skill0.TrainingEndTime);
            Assert.AreEqual(new DateTime(2009, 03, 17, 21, 01, 06), skill0.TrainingStartTimeLocal);
            Assert.AreEqual(new DateTime(2009, 03, 18, 10, 19, 21), skill0.TrainingEndTimeLocal);

            //Attributes of the second skill in the queue
            Assert.AreEqual(2, skill1.QueuePosition);
            Assert.AreEqual(20533, skill1.TrainingTypeId);
            Assert.AreEqual(4, skill1.TrainingToLevel);
            Assert.AreEqual(112000, skill1.TrainingStartSP);
            Assert.AreEqual(633542, skill1.TrainingEndSP);
            Assert.AreEqual(new DateTime(2009, 03, 18, 15, 19, 21), skill1.TrainingStartTime);
            Assert.AreEqual(new DateTime(2009, 03, 30, 03, 16, 14), skill1.TrainingEndTime);
            Assert.AreEqual(new DateTime(2009, 03, 18, 10, 19, 21), skill1.TrainingStartTimeLocal);
            Assert.AreEqual(new DateTime(2009, 03, 29, 22, 16, 14), skill1.TrainingEndTimeLocal);
        }

        [Test]
        public void PersistSkillInTraining()
        {
            ResponseCache.Clear();
            SkillQueue skillqueue = EveApi.GetSkillQueue(56456, 4564356, "asdf");
            ResponseCache.Save("ResponseCache.xml");
            ResponseCache.Clear();
            ResponseCache.Load("ResponseCache.xml");
            SkillQueue cached = EveApi.GetSkillQueue(4563456, 36456, "asdf");

            //Test the Cache Time
            Assert.AreEqual(skillqueue.CachedUntilLocal, cached.CachedUntilLocal);

            //Attributes of the first skill in the queue
            Assert.AreEqual(cached.SkillList[0].QueuePosition, skillqueue.SkillList[0].QueuePosition);
            Assert.AreEqual(cached.SkillList[0].TrainingTypeId, skillqueue.SkillList[0].TrainingTypeId);
            Assert.AreEqual(cached.SkillList[0].TrainingToLevel, skillqueue.SkillList[0].TrainingToLevel);
            Assert.AreEqual(cached.SkillList[0].TrainingStartSP, skillqueue.SkillList[0].TrainingStartSP);
            Assert.AreEqual(cached.SkillList[0].TrainingEndSP, skillqueue.SkillList[0].TrainingEndSP);
            Assert.AreEqual(cached.SkillList[0].TrainingStartTime, skillqueue.SkillList[0].TrainingStartTime);
            Assert.AreEqual(cached.SkillList[0].TrainingEndTime, skillqueue.SkillList[0].TrainingEndTime);
            Assert.AreEqual(cached.SkillList[0].TrainingStartTimeLocal, skillqueue.SkillList[0].TrainingStartTimeLocal);
            Assert.AreEqual(cached.SkillList[0].TrainingEndTimeLocal, skillqueue.SkillList[0].TrainingEndTimeLocal);

            //Attributes of the second skill in the queue
            Assert.AreEqual(cached.SkillList[1].QueuePosition, skillqueue.SkillList[1].QueuePosition);
            Assert.AreEqual(cached.SkillList[1].TrainingTypeId, skillqueue.SkillList[1].TrainingTypeId);
            Assert.AreEqual(cached.SkillList[1].TrainingToLevel, skillqueue.SkillList[1].TrainingToLevel);
            Assert.AreEqual(cached.SkillList[1].TrainingStartSP, skillqueue.SkillList[1].TrainingStartSP);
            Assert.AreEqual(cached.SkillList[1].TrainingEndSP, skillqueue.SkillList[1].TrainingEndSP);
            Assert.AreEqual(cached.SkillList[1].TrainingStartTime, skillqueue.SkillList[1].TrainingStartTime);
            Assert.AreEqual(cached.SkillList[1].TrainingEndTime, skillqueue.SkillList[1].TrainingEndTime);
            Assert.AreEqual(cached.SkillList[1].TrainingStartTimeLocal, skillqueue.SkillList[1].TrainingStartTimeLocal);
            Assert.AreEqual(cached.SkillList[1].TrainingEndTimeLocal, skillqueue.SkillList[1].TrainingEndTimeLocal);
        }
    }
}
