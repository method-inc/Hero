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
            _user = new User("User", "User");
            _adminAbilities = new List<Ability>
                {
                    new Ability("Ability1"),
                    new Ability("Ability2")
                };

            Ability abilityThree = new Ability("Ability3");

            HeroConfig.Initialize(_authorizationService);
            HeroConfig.RegisterAbilities(_authorizationService, _adminRole, _adminAbilities);
            HeroConfig.RegisterAbility(_authorizationService, _adminRole, abilityThree);
            HeroConfig.RegisterRole(_authorizationService, _user, _adminRole);
        }

        [Test]
        public void TestHeroConfigurationThrowsExceptionWithNullAuthorizationServiceForAssignAbility()
        {
            Assert.Throws<ArgumentNullException>(() => HeroConfig.RegisterAbilities(null, _adminRole, _adminAbilities));
        }

        [Test]
        public void TestHeroConfigurationThrowsExceptionWithNullRoleForAssignAbility()
        {
            Assert.Throws<ArgumentNullException>(() => HeroConfig.RegisterAbilities(_authorizationService, null, _adminAbilities));
        }

        [Test]
        public void TestHeroConfigurationThrowsExceptionWithNullAbilitiesForAssignAbility()
        {
            Assert.Throws<ArgumentNullException>(() => HeroConfig.RegisterAbilities(_authorizationService, _adminRole, null));
        }

        [Test]
        public void TestHeroConfigurationThrowsExceptionWithNullAuthorizationServiceForUnassignAbility()
        {
            Assert.Throws<ArgumentNullException>(() => HeroConfig.UnregisterAbilities(null, _adminRole, _adminAbilities));
        }

        [Test]
        public void TestHeroConfigurationThrowsExceptionWithNullRoleForUnassignAbility()
        {
            Assert.Throws<ArgumentNullException>(() => HeroConfig.UnregisterAbilities(_authorizationService, null, _adminAbilities));
        }

        [Test]
        public void TestHeroConfigurationThrowsExceptionWithNullAbilitiesForUnassignAbility()
        {
            Assert.Throws<ArgumentNullException>(() => HeroConfig.UnregisterAbilities(_authorizationService, _adminRole, null));
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
            Assert.Throws<ArgumentNullException>(() => HeroConfig.RegisterRoles(null, _user, new[] { _adminRole }));
        }

        [Test]
        public void TestHeroConfigurationThrowsExceptionWithNullUserForAssignRole()
        {
            Assert.Throws<ArgumentNullException>(() => HeroConfig.RegisterRoles(_authorizationService, null, new[] { _adminRole }));
        }

        [Test]
        public void TestHeroConfigurationThrowsExceptionWithNullRoleForAssignRole()
        {
            Assert.Throws<ArgumentNullException>(() => HeroConfig.RegisterRoles(_authorizationService, _user, null));
        }

        [Test]
        public void TestHeroConfigurationThrowsExceptionWithNullAuthorizationServiceForUnassignRole()
        {
            Assert.Throws<ArgumentNullException>(() => HeroConfig.UnregisterRoles(null, _user, new[] { _adminRole }));
        }

        [Test]
        public void TestHeroConfigurationThrowsExceptionWithNullUserForUnassignRole()
        {
            Assert.Throws<ArgumentNullException>(() => HeroConfig.UnregisterRoles(_authorizationService, null, new[] { _adminRole }));
        }

        [Test]
        public void TestHeroConfigurationThrowsExceptionWithNullRoleForUnassignRole()
        {
            Assert.Throws<ArgumentNullException>(() => HeroConfig.UnregisterRoles(_authorizationService, _user, null));
        }

        [Test]
        public void TestHeroConfigurationProvidesDefaultRoles()
        {
            Assert.True(_authorizationService.GetRolesForUser(_user).Any(r => r.Equals(_adminRole)));
        }

        [Test]
        public void TestHeroConfigurationCanUnregisterAbility()
        {
            HeroConfig.UnregisterAbility(_authorizationService, _adminRole, new Ability("Ability1"));

            foreach (Ability ability in _adminAbilities.Except(new List<Ability>{new Ability("Ability1")}))
                Assert.True(_authorizationService.Authorize(_adminRole, ability));
        }

        [Test]
        public void TestHeroConfigurationCanUnregisterRole()
        {
            HeroConfig.UnregisterRole(_authorizationService, _user, _adminRole);
            Assert.True(!_authorizationService.GetRolesForUser(_user).Any());
        }
    }
}
