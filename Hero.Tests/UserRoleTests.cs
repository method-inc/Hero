using Hero.Interfaces;
using NUnit.Framework;

namespace Hero.Tests
{
    public class UserRoleTests
    {
        private IUser _user1;
        private IUser _user2;
        private Role _role1;
        private Role _role2;
        private UserRole _userRole1;
        private UserRole _userRole2;

        [SetUp]
        public void Initialize()
        {
            _user1 = new User(1, "User1");
            _user2 = new User(2, "User2");
            _role1 = new Role(1, "Role1");
            _role2 = new Role(2, "Role2");
            _userRole1 = new UserRole(_user1, _role1);
            _userRole2 = new UserRole(_user2, _role2);
        }

        [Test]
        public void TestUserRolesAreEqual()
        {
            User userOne = new User(1, "User1");
            Role roleOne = new Role(1, "Role1");
            UserRole userRoleOne = new UserRole(userOne, roleOne);
            Assert.AreEqual(userRoleOne, _userRole1);
        }

        [Test]
        public void TestUserRolesEqualityOperator()
        {
            User userOne = new User(1, "User1");
            Role roleOne = new Role(1, "Role1");
            UserRole userRoleOne = new UserRole(userOne, roleOne);
            Assert.True(userRoleOne == _userRole1);
        }

        [Test]
        public void TestUserRolesAreNotEqual()
        {
            Assert.AreNotEqual(_userRole2, _userRole1);
        }

        [Test]
        public void TestUserRolesNotEqualOperator()
        {
            Assert.True(_userRole2 != _userRole1);
        }

        [Test]
        public void TestUserRoleReferenceEqualsTrue()
        {
            Assert.AreEqual(_userRole1, _userRole1);
        }

        [Test]
        public void TestUserRoleReferenceEqualsTrueWithEqualsMethod()
        {
            Assert.True(_userRole1.Equals(_userRole1));
        }

        [Test]
        public void TestUserRoleNullReferenceReturnsFalse()
        {
            Assert.False(_userRole1.Equals(null));
        }

        [Test]
        public void TestUserRoleDoesNotEqualADifferentObject()
        {
            Assert.AreNotEqual("", _userRole1);
        }

        [Test]
        public void TestUserRoleReferenceEqualsTrueWithEqualsMethodCastObject()
        {
            Assert.True(_userRole1.Equals((object)_userRole1));
        }

        [Test]
        public void TestUserRoleNullReferenceCastObjectReturnsFalse()
        {
            Assert.False(_userRole1.Equals((object)null));
        }

        [Test]
        public void TestUserRoleDoesNotEqualADifferentObjectCastObject()
        {
            Assert.False(_userRole1.Equals((object)""));
        }

        [Test]
        public void TestUserRoleHashCodeEquals()
        {
            Assert.AreEqual(_userRole1.GetHashCode(), _userRole1.GetHashCode());
        }

        [Test]
        public void TestUserRoleHashCodeNotEquals()
        {
            Assert.AreNotEqual(_userRole1.GetHashCode(), _userRole2.GetHashCode());
        }
    }
}

