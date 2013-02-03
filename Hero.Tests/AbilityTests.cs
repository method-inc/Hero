using System;
using NUnit.Framework;

namespace Hero.Tests
{
    public class AbilityTests
    {
        private Ability _ability;

        [SetUp]
        public void Initialize()
        {
            _ability = new Ability("ability");
        }

        [Test]
        public void TestAbilityWithNullNameThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => new Ability(null));
        }

        [Test]
        public void TestAbilityHasNameProperty()
        {
            Assert.AreEqual("ability", _ability.Name);
        }
    }
}
