using System.Linq;
using NUnit.Framework;

namespace Hero.Tests
{
    public class UserTests
    {
        private User _user1;
        private User _user2;

        [SetUp]
        public void Initialize()
        {
            _user1 = new User("User1");
            _user2 = new User("User2");
        }

        [Test]
        public void TestUserCanSetProperties()
        {
            User user3 = new User();
            user3.Name = "User3";
            user3.Id = "User3";
            User userThree = new User("User3");
            Assert.AreEqual(userThree, user3);
        }

        [Test]
        public void TestUserIdAndNameSetInConstructor()
        {
            User user4 = new User("User4", "User4");
            User userFour = new User("User4");
            Assert.AreEqual(userFour, user4);
        }

        [Test]
        public void TestUsersAreEqual()
        {
            User userOne = new User("User1");
            Assert.AreEqual(userOne, _user1);
        }

        [Test]
        public void TestUsersEqualityOperator()
        {
            User userOne = new User("User1");
            Assert.True(userOne == _user1);
        }

        [Test]
        public void TestUsersAreNotEqual()
        {
            Assert.AreNotEqual(_user2, _user1);
        }

        [Test]
        public void TestUsersNotEqualOperator()
        {
            Assert.True(_user2 != _user1);
        }

        [Test]
        public void TestUserReferenceEqualsTrue()
        {
            Assert.AreEqual(_user1, _user1);
        }

        [Test]
        public void TestUserReferenceEqualsTrueWithEqualsMethod()
        {
            Assert.True(_user1.Equals(_user1));
        }

        [Test]
        public void TestUserNullReferenceReturnsFalse()
        {
            Assert.False(_user1.Equals(null));
        }

        [Test]
        public void TestUserDoesNotEqualADifferentObject()
        {
            Assert.AreNotEqual("", _user1);
        }

        [Test]
        public void TestUserReferenceEqualsTrueWithEqualsMethodCastObject()
        {
            Assert.True(_user1.Equals((object)_user1));
        }

        [Test]
        public void TestUserNullReferenceCastObjectReturnsFalse()
        {
            Assert.False(_user1.Equals((object)null));
        }

        [Test]
        public void TestUserDoesNotEqualADifferentObjectCastObject()
        {
            Assert.False(_user1.Equals((object)""));
        }

        [Test]
        public void TestUserHashCodeEquals()
        {
            Assert.AreEqual(_user1.GetHashCode(), _user1.GetHashCode());
        }

        [Test]
        public void TestUserHasAbilitiesFromRoles()
        {
            Role role1 = new Role("Role1");
            Ability ability1 = new Ability("Ability1");
            role1.Abilities.Add(ability1);
            Role role2 = new Role("Role2");
            Ability ability2 = new Ability("Ability2");
            role2.Abilities.Add(ability2);

            _user1.Roles.Add(role1);
            _user1.Roles.Add(role2);

            Assert.AreEqual(2, _user1.Abilities.Count);
            Assert.AreEqual(ability1, _user1.Abilities.First());
            Assert.AreEqual(ability2, _user1.Abilities.Last());
        }
    }
}

