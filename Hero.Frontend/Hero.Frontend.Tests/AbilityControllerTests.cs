using System.Collections.Generic;
using System.Linq;
using Hero.Configuration;
using Hero.Frontend.Controllers;
using Hero.Interfaces;
using Hero.Services.Interfaces;
using Moq;
using NUnit.Framework;

namespace Hero.Frontend.Tests
{
    public class AbilityControllerTests
    {
        private AbilityController _abilityController;
        private Mock<IAbilityAuthorizationService> _authorizationServiceMock;
        private List<Ability> _abilities;

        [SetUp]
        public void Init()
        {
            _Setup();
            _abilityController = new AbilityController();
            _authorizationServiceMock = new Mock<IAbilityAuthorizationService>();
            HeroConfig.Initialize(_authorizationServiceMock.Object);
        }

        [Test]
        public void TestGet()
        {
            _authorizationServiceMock.Setup(service => service.GetAbilities()).Returns(_abilities);
            IEnumerable<IAbility> abilities = _abilityController.Get();
            Assert.True(abilities.SequenceEqual(_abilities));
        }

        [Test]
        public void TestGetById()
        {
            _authorizationServiceMock.Setup(service => service.GetAbility("Ability1")).Returns(_abilities.First());
            IAbility ability = _abilityController.Get("Ability1");
            Assert.AreEqual(ability, _abilities.First());
        }

        [Test]
        public void TestPostAbility()
        {
            _authorizationServiceMock.Setup(service => service.AddAbility(_abilities.First())).Returns(_abilities.First());
            IAbility ability = _abilityController.Post(_abilities.First());
            Assert.AreEqual(ability, _abilities.First());
        }

        [Test]
        public void TestPutAbility()
        {
            _authorizationServiceMock.Setup(service => service.UpdateAbility(_abilities.First())).Returns(_abilities.First());
            IAbility ability = _abilityController.Put(_abilities.First());
            Assert.AreEqual(ability, _abilities.First());
        }

        [Test]
        public void TestDeleteAbility()
        {
            _abilityController.Delete("Ability1");
            _authorizationServiceMock.Verify(service => service.RemoveAbility("Ability1"), Times.AtLeastOnce());
        }

        private void _Setup()
        {
            Ability ability1 = new Ability("Ability1");
            Ability ability2 = new Ability("Ability2");
            Ability ability3 = new Ability("Ability3");
            _abilities = new List<Ability> {ability1, ability2, ability3};
        }
    }
}
