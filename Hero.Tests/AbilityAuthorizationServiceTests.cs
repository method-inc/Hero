using System;
using Hero.Interfaces;
using Hero.Services;
using Hero.Tests.Models;
using NUnit.Framework;

namespace Hero.Tests
{
    public class AbilityAuthorizationServiceTests
    {
        private IRole _role1;
        private IRole _role2;
        private Ability _ability1;
        private Ability _ability2;
        private Ability _ability3;
        private Consumer _consumer;
        private AbilityAuthorizationService _authorizationService;

        [SetUp]
        public void Initialize()
        {
            _role1 = new Role(1, "Role1");
            _role2 = new Role(2, "Role2");
            _ability1 = new Ability("Ability1");
            _ability2 = new Ability("Ability2");
            _ability3 = new Ability("Ability3");
            _consumer = new Consumer();
            _authorizationService = new AbilityAuthorizationService();
        }

        [Test]
        public void TestAuthorizionWithEmptyRoleThrowException()
        {
            Assert.Throws<NotImplementedException>(() => _authorizationService.Authorize(""));
        }

        [Test]
        public void TestAuthorizationServiceCanRegisterAbilityWithEmptyMap()
        {
            Assert.DoesNotThrow(() => _authorizationService.RegisterAbility(_role1, _ability1));
        }

        [Test]
        public void TestAuthorizationServiceCanRegisterMultipleAbilitiesWithEmptyMap()
        {
            Assert.DoesNotThrow(() => _authorizationService.RegisterAbility(_role1, _ability1));
            Assert.DoesNotThrow(() => _authorizationService.RegisterAbility(_role1, _ability2));
        }

        [Test]
        public void TestAuthorizationServiceCanRegisterMultipleAbilitiesAndRolesWithEmptyMap()
        {
            Assert.DoesNotThrow(() => _authorizationService.RegisterAbility(_role1, _ability1));
            Assert.DoesNotThrow(() => _authorizationService.RegisterAbility(_role1, _ability2));
            Assert.DoesNotThrow(() => _authorizationService.RegisterAbility(_role2, _ability1));
            Assert.DoesNotThrow(() => _authorizationService.RegisterAbility(_role2, _ability2));
        }

        [Test]
        public void TestAuthorizationServiceUnregisterDoesNothingWithEmptyMap()
        {
            Assert.DoesNotThrow(() => _authorizationService.UnregisterAbility(_role1, _ability1));
        }

        [Test]
        public void TestAuthorizationServiceAuthorizeWithOneRoleOneAbility()
        {
            _authorizationService.RegisterAbility(_role1, _ability1);
            Assert.True(_authorizationService.Authorize(_role1, _ability1));
        }

        [Test]
        public void TestAuthorizationServiceAuthorizeWithOneRoleMultipleAbilities()
        {
            _authorizationService.RegisterAbility(_role1, _ability1);
            _authorizationService.RegisterAbility(_role1, _ability2);
            _authorizationService.RegisterAbility(_role1, _ability3);
            Assert.True(_authorizationService.Authorize(_role1, _ability1));
            Assert.True(_authorizationService.Authorize(_role1, _ability2));
            Assert.True(_authorizationService.Authorize(_role1, _ability3));
        }

        [Test]
        public void TestAuthorizationServiceAuthorizeWithMultipleRolesMultipleAbilities()
        {
            _authorizationService.RegisterAbility(_role1, _ability1);
            _authorizationService.RegisterAbility(_role1, _ability2);
            _authorizationService.RegisterAbility(_role1, _ability3);
            _authorizationService.RegisterAbility(_role2, _ability1);
            _authorizationService.RegisterAbility(_role2, _ability2);
            _authorizationService.RegisterAbility(_role2, _ability3);
            Assert.True(_authorizationService.Authorize(_role1, _ability1));
            Assert.True(_authorizationService.Authorize(_role1, _ability2));
            Assert.True(_authorizationService.Authorize(_role1, _ability3));
            Assert.True(_authorizationService.Authorize(_role2, _ability1));
            Assert.True(_authorizationService.Authorize(_role2, _ability2));
            Assert.True(_authorizationService.Authorize(_role2, _ability3));
        }

        [Test]
        public void TestAuthorizationServiceAuthorizeWithOneRoleOneAbilityAfterUnregister()
        {
            _authorizationService.RegisterAbility(_role1, _ability1);
            Assert.True(_authorizationService.Authorize(_role1, _ability1));
            _authorizationService.UnregisterAbility(_role1, _ability1);
            Assert.False(_authorizationService.Authorize(_role1, _ability1));
        }

        [Test]
        public void TestAuthorizationServiceAuthorizeWithMultipleRolesMultipleAbilitiesAfterUnregister()
        {
            _authorizationService.RegisterAbility(_role1, _ability1);
            _authorizationService.RegisterAbility(_role1, _ability2);
            _authorizationService.RegisterAbility(_role1, _ability3);
            _authorizationService.RegisterAbility(_role2, _ability1);
            _authorizationService.RegisterAbility(_role2, _ability2);
            _authorizationService.RegisterAbility(_role2, _ability3);
            Assert.True(_authorizationService.Authorize(_role1, _ability1));
            Assert.True(_authorizationService.Authorize(_role1, _ability2));
            Assert.True(_authorizationService.Authorize(_role2, _ability1));
            Assert.True(_authorizationService.Authorize(_role2, _ability2));
            _authorizationService.UnregisterAbility(_role1, _ability1);
            Assert.False(_authorizationService.Authorize(_role1, _ability1));
            Assert.True(_authorizationService.Authorize(_role1, _ability2));
            Assert.True(_authorizationService.Authorize(_role2, _ability1));
            Assert.True(_authorizationService.Authorize(_role2, _ability2));
        }

        [Test]
        public void TestAuthorizationServiceEvents()
        {
            _authorizationService.RegisterAbility(_role1, _ability1);
            Assert.True(_authorizationService.Authorize(_role1, _ability1));
            Assert.AreEqual(1, _consumer.Counter);
            Assert.AreEqual(new RoleAbility(_role1, _ability1), _consumer.Param[0]);
            _authorizationService.UnregisterAbility(_role1, _ability1);
            Assert.AreEqual(0, _consumer.Counter);
            Assert.AreEqual(new RoleAbility(_role1, _ability1), _consumer.Param[0]);
        }
    }
}
