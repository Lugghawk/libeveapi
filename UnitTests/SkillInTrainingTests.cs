using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using libeveapi;

namespace UnitTests
{
    [TestFixture]
    public class SkillInTrainingTests
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
            SkillInTraining skillintraining = EveApi.GetSkillInTraining(456456,456436,"asdf");

            //Attributes of the currently training skill
            Assert.AreEqual(new DateTime(2008, 01, 27, 01, 42, 38), skillintraining.TrainingEndTime);
            Assert.AreEqual(new DateTime(2008, 01, 25, 01, 04, 23), skillintraining.TrainingStartTime);
            Assert.AreEqual(3412, skillintraining.TrainingTypeId);
            Assert.AreEqual(36253, skillintraining.TrainingStartSP);
            Assert.AreEqual(135765, skillintraining.TrainingDestinationSP);
            Assert.AreEqual(4, skillintraining.TrainingToLevel);
            Assert.AreEqual(true, skillintraining.SkillCurrentlyInTraining);
        }

        [Test]
        public void PersistSkillInTraining()
        {
            ResponseCache.Clear();
            SkillInTraining skillintraining = EveApi.GetSkillInTraining(56456, 4564356, "asdf");
            ResponseCache.Save("ResponseCache.xml");
            ResponseCache.Clear();
            ResponseCache.Load("ResponseCache.xml");
            SkillInTraining cached = EveApi.GetSkillInTraining(4563456, 36456, "asdf");

            //Test the Cache Time
            Assert.AreEqual(skillintraining.CachedUntilLocal, cached.CachedUntilLocal);

            //Attributes of the currently training skill
            Assert.AreEqual(cached.TrainingEndTime, skillintraining.TrainingEndTime);
            Assert.AreEqual(cached.TrainingStartTime, skillintraining.TrainingStartTime);
            Assert.AreEqual(cached.TrainingTypeId, skillintraining.TrainingTypeId);
            Assert.AreEqual(cached.TrainingStartSP, skillintraining.TrainingStartSP);
            Assert.AreEqual(cached.TrainingDestinationSP, skillintraining.TrainingDestinationSP);
            Assert.AreEqual(cached.TrainingToLevel, skillintraining.TrainingToLevel);
            Assert.AreEqual(cached.SkillCurrentlyInTraining, skillintraining.SkillCurrentlyInTraining);
        }
    }
}
