using System;
using System.Collections.Generic;
using System.Linq;
using Hero.Configuration;
using Hero.Interfaces;
using Hero.Services;
using Hero.Services.Interfaces;
using NUnit.Framework;

namespace Hero.Tests
{
    public class HeroConfigurationTests
    {
        private IAbilityAuthorizationService _authorizationService;
        private IUser _user;
        private IRole _adminRole;
        private IList<Ability> _adminAbilities;

        [SetUp]
        public void Initialize()
        {
            _authorizationService = new AbilityAuthorizationService();
            _adminRole = new Role(1, "Administrator");
            _user = new User(1, "User");
            _adminAbilities = new List<Ability>
                {
                    new Ability("Ability1"),
                    new Ability("Ability2"),
                    new Ability("Ability3")
                };

            HeroConfig.AssignAbilitiesToRole(_authorizationService, _adminRole, _adminAbilities);
            HeroConfig.AssignRolesToUser(_authorizationService, _user, new[] { _adminRole });
        }

        [Test]
        public void TestHeroConfigurationThrowsExceptionWithNullAuthorizationServiceForAssignAbility()
        {
            Assert.Throws<ArgumentNullException>(() => HeroConfig.AssignAbilitiesToRole(null, _adminRole, _adminAbilities));
        }

        [Test]
        public void TestHeroConfigurationThrowsExceptionWithNullRoleForAssignAbility()
        {
            Assert.Throws<ArgumentNullException>(() => HeroConfig.AssignAbilitiesToRole(_authorizationService, null, _adminAbilities));
        }

        [Test]
        public void TestHeroConfigurationThrowsExceptionWithNullAbilitiesForAssignAbility()
        {
            Assert.Throws<ArgumentNullException>(() => HeroConfig.AssignAbilitiesToRole(_authorizationService, _adminRole, null));
        }

        [Test]
        public void TestHeroConfigurationProvidesDefaultAbilities()
        {
            foreach (Ability ability in _adminAbilities)
                Assert.True(_authorizationService.Authorize(_adminRole, ability));
        }

        [Test]
        public void TestHeroConfigurationThrowsExceptionWithNullAuthorizationServiceForAssignRole()
        {
            Assert.Throws<ArgumentNullException>(() => HeroConfig.AssignRolesToUser(null, _user, new[] { _adminRole }));
        }

        [Test]
        public void TestHeroConfigurationThrowsExceptionWithNullUserForAssignRole()
        {
            Assert.Throws<ArgumentNullException>(() => HeroConfig.AssignRolesToUser(_authorizationService, null, new[] { _adminRole }));
        }

        [Test]
        public void TestHeroConfigurationThrowsExceptionWithNullRoleForAssignRole()
        {
            Assert.Throws<ArgumentNullException>(() => HeroConfig.AssignRolesToUser(_authorizationService, _user, null));
        }

        [Test]
        public void TestHeroConfigurationProvidesDefaultRoles()
        {
            Assert.True(_authorizationService.GetRolesForUser(_user).Any(r => r.Equals(_adminRole)));
        }
    }
}
