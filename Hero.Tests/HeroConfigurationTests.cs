using System;
using System.Collections.Generic;
using Hero.Configuration;
using Hero.Interfaces;
using Hero.Services;
using Hero.Services.Interfaces;
using NUnit.Framework;

namespace Hero.Tests
{
    public class HeroConfigurationTests
    {
        private HeroConfiguration _configuration;
        private IAbilityAuthorizationService _authorizationService;
        private IUser _user;
        private IRole _adminRole;
        private IList<Ability> _adminAbilities;

        [SetUp]
        public void Initialize()
        {
            _authorizationService = new AbilityAuthorizationService();
            _adminRole = new Role(1, "Administrator");
            _user = new User(1, "User", new List<IRole> {_adminRole});
            _adminAbilities = new List<Ability>
                {
                    new Ability("Ability1"),
                    new Ability("Ability2"),
                    new Ability("Ability3")
                };

            _configuration = new HeroConfiguration();
            _configuration.Initialize(_authorizationService, _user, _adminRole, _adminAbilities);
        }

        [Test]
        public void TestHeroConfigurationThrowsExceptionWithNullAuthorizationService()
        {
            Assert.Throws<ArgumentNullException>(() => _configuration.Initialize(null, _user, _adminRole, _adminAbilities));
        }

        [Test]
        public void TestHeroConfigurationThrowsExceptionWithNullUser()
        {
            Assert.Throws<ArgumentNullException>(() => _configuration.Initialize(_authorizationService, null, _adminRole, _adminAbilities));
        }

        [Test]
        public void TestHeroConfigurationThrowsExceptionWithNullRole()
        {
            Assert.Throws<ArgumentNullException>(() => _configuration.Initialize(_authorizationService, _user, null, _adminAbilities));
        }

        [Test]
        public void TestHeroConfigurationThrowsExceptionWithNullAbilities()
        {
            Assert.Throws<ArgumentNullException>(() => _configuration.Initialize(_authorizationService, _user, _adminRole, null));
        }

        [Test]
        public void TestHeroConfigurationProvidesDefaultAbilities()
        {
            foreach (Ability ability in _adminAbilities)
                Assert.True(_authorizationService.Authorize(_adminRole, ability));
        }
    }
}
