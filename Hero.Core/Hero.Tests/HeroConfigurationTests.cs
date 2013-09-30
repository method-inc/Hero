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
            _adminRole = new Role("Administrator");
            _user = new User("User");
            _adminAbilities = new List<Ability>
                {
                    new Ability("Ability1"),
                    new Ability("Ability2")
                };

            Ability abilityThree = new Ability("Ability3");

            HeroConfig.Initialize(_authorizationService);
            HeroConfig.RegisterAbilities(_adminRole, _adminAbilities);
            HeroConfig.RegisterAbility(_adminRole, abilityThree);
            HeroConfig.RegisterRole(_user, _adminRole);
        }

        [Test]
        public void TestCan()
        {
            bool can = HeroConfig.Can("User", "Ability1");
            Assert.True(can);
        }

        [Test]
        public void TestCannot()
        {
            bool cannot = HeroConfig.Cannot("User", "Ability4");
            Assert.True(cannot);
        }

        [Test]
        public void TestHeroConfigurationThrowsExceptionWithNullRoleForAssignAbility()
        {
            Assert.Throws<ArgumentNullException>(() => HeroConfig.RegisterAbilities(null, _adminAbilities));
        }

        [Test]
        public void TestHeroConfigurationThrowsExceptionWithNullAbilitiesForAssignAbility()
        {
            Assert.Throws<ArgumentNullException>(() => HeroConfig.RegisterAbilities(_adminRole, null));
        }

        [Test]
        public void TestHeroConfigurationThrowsExceptionWithNullRoleForUnassignAbility()
        {
            Assert.Throws<ArgumentNullException>(() => HeroConfig.UnregisterAbilities(null, _adminAbilities));
        }

        [Test]
        public void TestHeroConfigurationThrowsExceptionWithNullAbilitiesForUnassignAbility()
        {
            Assert.Throws<ArgumentNullException>(() => HeroConfig.UnregisterAbilities(_adminRole, null));
        }

        [Test]
        public void TestHeroConfigurationProvidesDefaultAbilities()
        {
            foreach (Ability ability in _adminAbilities)
                Assert.True(_authorizationService.Authorize(_adminRole, ability));
        }

        [Test]
        public void TestHeroConfigurationThrowsExceptionWithNullUserForAssignRole()
        {
            Assert.Throws<ArgumentNullException>(() => HeroConfig.RegisterRoles(null, new[] { _adminRole }));
        }

        [Test]
        public void TestHeroConfigurationThrowsExceptionWithNullRoleForAssignRole()
        {
            Assert.Throws<ArgumentNullException>(() => HeroConfig.RegisterRoles(_user, null));
        }

        [Test]
        public void TestHeroConfigurationThrowsExceptionWithNullUserForUnassignRole()
        {
            Assert.Throws<ArgumentNullException>(() => HeroConfig.UnregisterRoles(null, new[] { _adminRole }));
        }

        [Test]
        public void TestHeroConfigurationThrowsExceptionWithNullRoleForUnassignRole()
        {
            Assert.Throws<ArgumentNullException>(() => HeroConfig.UnregisterRoles(_user, null));
        }

        //[Test]
        //public void TestHeroConfigurationProvidesDefaultRoles()
        //{
        //    Assert.True(_authorizationService.GetRolesForUser(_user).Any(r => r.Equals(_adminRole)));
        //}

        [Test]
        public void TestHeroConfigurationCanUnregisterAbility()
        {
            HeroConfig.UnregisterAbility(_adminRole, new Ability("Ability1"));

            foreach (Ability ability in _adminAbilities.Except(new List<Ability>{new Ability("Ability1")}))
                Assert.True(_authorizationService.Authorize(_adminRole, ability));
        }

        //[Test]
        //public void TestHeroConfigurationCanUnregisterRole()
        //{
        //    HeroConfig.UnregisterRole(_user, _adminRole);
        //    Assert.True(!_authorizationService.GetRolesForUser(_user).Any());
        //}
    }
}
