using NUnit.Framework;

namespace Hero.Tests
{
    public class RoleTests
    {
        private Role _role1;
        private Role _role2;

        [SetUp]
        public void Initialize()
        {
            _role1 = new Role("Role1");
            _role2 = new Role("Role2");
        }

        [Test]
        public void TestRolesAreEqual()
        {
            Role roleOne = new Role("Role1");
            Assert.AreEqual(roleOne, _role1);
        }

        [Test]
        public void TestRolesEqualityOperator()
        {
            Role roleOne = new Role("Role1");
            Assert.True(roleOne == _role1);
        }

        [Test]
        public void TestRolesAreNotEqual()
        {
            Assert.AreNotEqual(_role2, _role1);
        }

        [Test]
        public void TestRolesNotEqualOperator()
        {
            Assert.True(_role2 != _role1);
        }

        [Test]
        public void TestRoleReferenceEqualsTrue()
        {
            Assert.AreEqual(_role1, _role1);
        }

        [Test]
        public void TestRoleReferenceEqualsTrueWithEqualsMethod()
        {
            Assert.True(_role1.Equals(_role1));
        }

        [Test]
        public void TestRoleNullReferenceReturnsFalse()
        {
            Assert.False(_role1.Equals(null));
        }

        [Test]
        public void TestRoleDoesNotEqualADifferentObject()
        {
            Assert.AreNotEqual("", _role1);
        }

        [Test]
        public void TestRoleReferenceEqualsTrueWithEqualsMethodCastObject()
        {
            Assert.True(_role1.Equals((object)_role1));
        }

        [Test]
        public void TestRoleNullReferenceCastObjectReturnsFalse()
        {
            Assert.False(_role1.Equals((object)null));
        }

        [Test]
        public void TestRoleDoesNotEqualADifferentObjectCastObject()
        {
            Assert.False(_role1.Equals((object)""));
        }

        [Test]
        public void TestRoleHashCodeEquals()
        {
            Assert.AreEqual(_role1.GetHashCode(), _role1.GetHashCode());
        }

        [Test]
        public void TestRoleHashCodeNotEquals()
        {
            Assert.AreNotEqual(_role1.GetHashCode(), _role2.GetHashCode());
        }
    }
}
