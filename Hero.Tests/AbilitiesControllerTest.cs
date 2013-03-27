using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web.Mvc;
using Hero.Configuration;
using Hero.Frontend;
using Hero.Interfaces;
using Hero.Services.Interfaces;
using Moq;
using NUnit.Framework;
using System.Web;

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
            _controller = new AbilitiesController();
            HeroConfig.Initialize(_authorizationServiceMock.Object);
        }

        [Test]
        public void TestGetAbilitiesForRole()
        {
            _authorizationServiceMock.Setup(service => service.GetAbilitiesForRole("Role1")).Returns(_abilities);
            JsonResult result = _controller.GetAbilitiesForRole("Role1");
            IEnumerable<Ability> abilitiesForRole = (IEnumerable<Ability>)result.Data;
            Assert.True(abilitiesForRole.SequenceEqual(_abilities));
        }

        [Test]
        public void TestGetAbilitiesForUser()
        {
            _authorizationServiceMock.Setup(service => service.GetAbilitiesForUser("User1")).Returns(_abilities);
            JsonResult result = _controller.GetAbilitiesForUser("User1");
            IEnumerable<Ability> abilitiesForUser = (IEnumerable<Ability>)result.Data;
            Assert.True(abilitiesForUser.SequenceEqual(_abilities));
        }

        [Test]
        public void TestGetRolesForUser()
        {
            _authorizationServiceMock.Setup(service => service.GetRolesForUser("User1")).Returns(_roles);
            JsonResult result = _controller.GetRolesForUser("User1");
            IEnumerable<IRole> rolesForUser = (IEnumerable<IRole>)result.Data;
            Assert.True(rolesForUser.SequenceEqual(_roles));
        }

        [Test]
        public void TestAuthorizeCurrentUser()
        {
            _authorizationServiceMock.Setup(service => service.Authorize(new User("User1"), new Ability("Ability1"))).Returns(true);
            JsonResult result = _controller.AuthorizeCurrentUser("Ability1", new User("User1"));
            bool authorized = (bool) result.Data;
            Assert.True(authorized);
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
