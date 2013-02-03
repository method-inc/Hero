using System;
using NUnit.Framework;

namespace Hero.Tests
{
    public class AbilityTests
    {
        private Ability _ability1;
        private Ability _ability2;

        [SetUp]
        public void Initialize()
        {
            _ability1 = new Ability("Ability1");
            _ability2 = new Ability("Ability2");
        }

        [Test]
        public void TestAbilityWithNullNameThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => new Ability(null));
        }

        [Test]
        public void TestAbilityHasNameProperty()
        {
            Assert.AreEqual("Ability1", _ability1.Name);
        }

        [Test]
        public void TestAbilitysAreEqual()
        {
            Ability abilityOne = new Ability("Ability1");
            Assert.AreEqual(abilityOne, _ability1);
        }

        [Test]
        public void TestAbilitysEqualityOperator()
        {
            Ability abilityOne = new Ability("Ability1");
            Assert.True(abilityOne == _ability1);
        }

        [Test]
        public void TestAbilitysAreNotEqual()
        {
            Assert.AreNotEqual(_ability2, _ability1);
        }

        [Test]
        public void TestAbilitysNotEqualOperator()
        {
            Assert.True(_ability2 != _ability1);
        }

        [Test]
        public void TestAbilityReferenceEqualsTrue()
        {
            Assert.AreEqual(_ability1, _ability1);
        }

        [Test]
        public void TestAbilityReferenceEqualsTrueWithEqualsMethod()
        {
            Assert.True(_ability1.Equals(_ability1));
        }

        [Test]
        public void TestAbilityNullReferenceReturnsFalse()
        {
            Assert.False(_ability1.Equals(null));
        }

        [Test]
        public void TestAbilityDoesNotEqualADifferentObject()
        {
            Assert.AreNotEqual("", _ability1);
        }

        [Test]
        public void TestAbilityReferenceEqualsTrueWithEqualsMethodCastObject()
        {
            Assert.True(_ability1.Equals((object)_ability1));
        }

        [Test]
        public void TestAbilityNullReferenceCastObjectReturnsFalse()
        {
            Assert.False(_ability1.Equals((object)null));
        }

        [Test]
        public void TestAbilityDoesNotEqualADifferentObjectCastObject()
        {
            Assert.False(_ability1.Equals((object)""));
        }

        [Test]
        public void TestAbilityHashCodeEquals()
        {
            Assert.AreEqual(_ability1.GetHashCode(), _ability1.GetHashCode());
        }

        [Test]
        public void TestAbilityHashCodeNotEquals()
        {
            Assert.AreNotEqual(_ability1.GetHashCode(), _ability2.GetHashCode());
        }
    }
}
