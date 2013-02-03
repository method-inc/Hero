using Hero.Interfaces;
using NUnit.Framework;

namespace Hero.Tests
{
    public class RoleAbilityTests
    {
        private IRole _role1;
        private IRole _role2;
        private Ability _ability1;
        private Ability _ability2;
        private RoleAbility _roleAbility1;
        private RoleAbility _roleAbility2;

        [SetUp]
        public void Initialize()
        {
            _role1 = new Role(1, "Role1");
            _role2 = new Role(2, "Role2");
            _ability1 = new Ability("Ability1");
            _ability2 = new Ability("Ability2");
            _roleAbility1 = new RoleAbility(_role1, _ability1);
            _roleAbility2 = new RoleAbility(_role2, _ability2);
        }

        [Test]
        public void TestRoleAbilitysAreEqual()
        {
            Role roleOne = new Role(1, "Role1");
            Ability abilityOne = new Ability("Ability1");
            RoleAbility roleAbilityOne = new RoleAbility(roleOne, abilityOne);
            Assert.AreEqual(roleAbilityOne, _roleAbility1);
        }

        [Test]
        public void TestRoleAbilitysEqualityOperator()
        {
            Role roleOne = new Role(1, "Role1");
            Ability abilityOne = new Ability("Ability1");
            RoleAbility roleAbilityOne = new RoleAbility(roleOne, abilityOne);
            Assert.True(roleAbilityOne == _roleAbility1);
        }

        [Test]
        public void TestRoleAbilitysAreNotEqual()
        {
            Assert.AreNotEqual(_roleAbility2, _roleAbility1);
        }

        [Test]
        public void TestRoleAbilitysNotEqualOperator()
        {
            Assert.True(_roleAbility2 != _roleAbility1);
        }

        [Test]
        public void TestRoleAbilityReferenceEqualsTrue()
        {
            Assert.AreEqual(_roleAbility1, _roleAbility1);
        }

        [Test]
        public void TestRoleAbilityReferenceEqualsTrueWithEqualsMethod()
        {
            Assert.True(_roleAbility1.Equals(_roleAbility1));
        }

        [Test]
        public void TestRoleAbilityNullReferenceReturnsFalse()
        {
            Assert.False(_roleAbility1.Equals(null));
        }

        [Test]
        public void TestRoleAbilityDoesNotEqualADifferentObject()
        {
            Assert.AreNotEqual("", _roleAbility1);
        }

        [Test]
        public void TestRoleAbilityReferenceEqualsTrueWithEqualsMethodCastObject()
        {
            Assert.True(_roleAbility1.Equals((object)_roleAbility1));
        }

        [Test]
        public void TestRoleAbilityNullReferenceCastObjectReturnsFalse()
        {
            Assert.False(_roleAbility1.Equals((object)null));
        }

        [Test]
        public void TestRoleAbilityDoesNotEqualADifferentObjectCastObject()
        {
            Assert.False(_roleAbility1.Equals((object)""));
        }

        [Test]
        public void TestRoleAbilityHashCodeEquals()
        {
            Assert.AreEqual(_roleAbility1.GetHashCode(), _roleAbility1.GetHashCode());
        }

        [Test]
        public void TestRoleAbilityHashCodeNotEquals()
        {
            Assert.AreNotEqual(_roleAbility1.GetHashCode(), _roleAbility2.GetHashCode());
        }
    }
}

