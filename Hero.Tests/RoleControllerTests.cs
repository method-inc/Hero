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
    public class RoleControllerTests
    {
        private RoleController _roleController;
        private Mock<IAbilityAuthorizationService> _authorizationServiceMock;
        private List<Role> _roles;

        [SetUp]
        public void Init()
        {
            _Setup();
            _roleController = new RoleController();
            _authorizationServiceMock = new Mock<IAbilityAuthorizationService>();
            HeroConfig.Initialize(_authorizationServiceMock.Object);
        }

        [Test]
        public void TestGet()
        {
            _authorizationServiceMock.Setup(service => service.GetRoles()).Returns(_roles);
            IEnumerable<IRole> roles = _roleController.Get();
            Assert.True(roles.SequenceEqual(_roles));
        }

        [Test]
        public void TestGetById()
        {
            _authorizationServiceMock.Setup(service => service.GetRole("Role1")).Returns(_roles.First());
            IRole role = _roleController.Get("Role1");
            Assert.AreEqual(role, _roles.First());
        }

        [Test]
        public void TestPostRole()
        {
            _authorizationServiceMock.Setup(service => service.AddRole(_roles.First())).Returns(_roles.First());
            IRole role = _roleController.Post(_roles.First());
            Assert.AreEqual(role, _roles.First());
        }

        [Test]
        public void TestPutRole()
        {
            _authorizationServiceMock.Setup(service => service.UpdateRole(_roles.First())).Returns(_roles.First());
            IRole role = _roleController.Put(_roles.First());
            Assert.AreEqual(role, _roles.First());
        }

        [Test]
        public void TestDeleteRole()
        {
            _roleController.Delete("Role1");
            _authorizationServiceMock.Verify(service => service.RemoveRole("Role1"), Times.AtLeastOnce());
        }

        private void _Setup()
        {
            Role role1 = new Role("Role1");
            Role role2 = new Role("Role2");
            Role role3 = new Role("Role3");
            _roles = new List<Role> {role1, role2, role3};
        }
    }
}
