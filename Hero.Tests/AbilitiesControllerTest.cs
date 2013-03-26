using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hero.Configuration;
using Hero.Interfaces;
using Hero.Services.Interfaces;
using Moq;
using NUnit.Framework;

namespace Hero.Tests
{
    public class AbilitiesControllerTest
    {
        private Mock<IAbilityAuthorizationService> _authorizationServiceMock;
        private AbilitiesController _controller;
        private List<Ability> _abilities;
        private List<IRole> _roles;
        private IUser _user;

        [SetUp]
        public void Initialize()
        {
            _Setup();
            _authorizationServiceMock = new Mock<IAbilityAuthorizationService>();
            _controller = new AbilitiesController(_authorizationServiceMock.Object);
        }

        [Test]
        public void TestControllerWithEmptyServiceThrowException()
        {
            Assert.False(true);
            Assert.Throws<ArgumentNullException>(() => new AbilitiesController(null));
        }

        [Test]
        public void TestGetAbilitiesForRole()
        {
            _authorizationServiceMock.Setup(service => service.GetAbilitiesForRole("Role1")).Returns(_abilities);
            IEnumerable<Ability> abilitiesForRole = _controller.GetAbilitiesForRole("Role1");
            Assert.True(abilitiesForRole.SequenceEqual(_abilities));
        }

        [Test]
        public void TestGetAbilitiesForUser()
        {
            _authorizationServiceMock.Setup(service => service.GetAbilitiesForUser("User1")).Returns(_abilities);
            IEnumerable<Ability> abilitiesForUser = _controller.GetAbilitiesForUser("User1");
            Assert.True(abilitiesForUser.SequenceEqual(_abilities));
        }

        [Test]
        public void TestGetRolesForUser()
        {
            _authorizationServiceMock.Setup(service => service.GetRolesForUser("User1")).Returns(_roles);
            IEnumerable<IRole> rolesForUser = _controller.GetRolesForUser("User1");
            Assert.True(rolesForUser.SequenceEqual(_roles));
        }

        private void _Setup()
        {
            Ability ability1 = new Ability("Ability1");
            Ability ability2 = new Ability("Ability2");
            Ability ability3 = new Ability("Ability3");
            _abilities = new List<Ability> {ability1, ability2, ability3};

            IRole role1 = new Role("Role1");
            IRole role2 = new Role("Role2");
            IRole role3 = new Role("Role3");
            _roles = new List<IRole> {role1, role2, role3};

            _user = new User("User1");
        }

    }
}
