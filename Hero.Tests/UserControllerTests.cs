using System.Collections.Generic;
using System.Linq;
using Hero.Configuration;
using Hero.Frontend;
using Hero.Interfaces;
using Hero.Services.Interfaces;
using Moq;
using NUnit.Framework;

namespace Hero.Tests
{
    public class UserControllerTests
    {
        private UserController _userController;
        private Mock<IAbilityAuthorizationService> _authorizationServiceMock;
        private List<User> _users;

        [SetUp]
        public void Init()
        {
            _Setup();
            _userController = new UserController();
            _authorizationServiceMock = new Mock<IAbilityAuthorizationService>();
            HeroConfig.Initialize(_authorizationServiceMock.Object);
        }

        [Test]
        public void TestGet()
        {
            _authorizationServiceMock.Setup(service => service.GetUsers()).Returns(_users);
            IEnumerable<IUser> users = _userController.Get();
            Assert.True(users.SequenceEqual(_users));
        }

        [Test]
        public void TestGetById()
        {
            _authorizationServiceMock.Setup(service => service.GetUser("User1")).Returns(_users.First());
            IUser user = _userController.Get("User1");
            Assert.AreEqual(user, _users.First());
        }

        [Test]
        public void TestPostUser()
        {
            _authorizationServiceMock.Setup(service => service.AddUser(_users.First())).Returns(_users.First());
            IUser user = _userController.Post(_users.First());
            Assert.AreEqual(user, _users.First());
        }

        [Test]
        public void TestPutUser()
        {
            _authorizationServiceMock.Setup(service => service.UpdateUser(_users.First())).Returns(_users.First());
            IUser user = _userController.Put(_users.First());
            Assert.AreEqual(user, _users.First());
        }

        [Test]
        public void TestDeleteUser()
        {
            _userController.Delete("User1");
            _authorizationServiceMock.Verify(service => service.RemoveUser("User1"), Times.AtLeastOnce());
        }

        private void _Setup()
        {
            User user1 = new User("User1");
            User user2 = new User("User2");
            User user3 = new User("User3");
            _users = new List<User> {user1, user2, user3};
        }
    }
}
