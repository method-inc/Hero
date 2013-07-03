using System;
using System.Collections.Generic;
using System.Linq;
using Hero.Interfaces;
using Hero.Repositories;
using Hero.Services;
using Hero.Tests.Models;
using NUnit.Framework;

namespace Hero.Tests
{
    public class AbilityAuthorizationServiceTests
    {
        private Role _role1;
        private Role _role2;
        private User _user1;
        private User _user2;
        private Ability _ability1;
        private Ability _ability2;
        private Ability _ability3;
        private AbilityConsumer _abilityConsumer;
        private RoleConsumer _roleConsumer;
        private AbilityAuthorizationService _authorizationService;

        [SetUp]
        public void Initialize()
        {
            _role1 = new Role("Role1");
            _role2 = new Role("Role2");
            _user1 = new User("User1");
            _user2 = new User("User2");
            _ability1 = new Ability("Ability1");
            _ability2 = new Ability("Ability2");
            _ability3 = new Ability("Ability3");
            _ability3.Abilities.Add(_ability1);
            _ability3.Abilities.Add(_ability2);
            _abilityConsumer = new AbilityConsumer();
            _roleConsumer = new RoleConsumer();
            _authorizationService = new AbilityAuthorizationService();
        }

        [Test]
        public void TestGetAbility()
        {
            _authorizationService = new AbilityAuthorizationService(new UserRepository(), new RoleRepository(), new AbilityRepository());
            _authorizationService.AddAbility(_ability1);
            _authorizationService.AddAbility(_ability2);
            IAbility ability = _authorizationService.GetAbility("Ability1");
            Assert.AreEqual(ability, _ability1);
        }

        [Test]
        public void TestGetAbilities()
        {
            _authorizationService = new AbilityAuthorizationService(new UserRepository(), new RoleRepository(), new AbilityRepository());
            _authorizationService.AddAbility(_ability1);
            _authorizationService.AddAbility(_ability2);
            IEnumerable<IAbility> abilitys = _authorizationService.GetAbilities();
            Assert.AreEqual(2, abilitys.Count());
            Assert.AreEqual(_ability1, abilitys.First());
            Assert.AreEqual(_ability2, abilitys.Last());
        }

        [Test]
        public void TestRemoveAbility()
        {
            _authorizationService = new AbilityAuthorizationService(new UserRepository(), new RoleRepository(), new AbilityRepository());
            _authorizationService.AddAbility(_ability1);
            _authorizationService.AddAbility(_ability2);
            IEnumerable<IAbility> abilitys = _authorizationService.GetAbilities();
            Assert.AreEqual(2, abilitys.Count());
            Assert.AreEqual(_ability1, abilitys.First());
            Assert.AreEqual(_ability2, abilitys.Last());

            _authorizationService.RemoveAbility("Ability1");
            abilitys = _authorizationService.GetAbilities();
            Assert.AreEqual(1, abilitys.Count());
            Assert.AreEqual(_ability2, abilitys.First());
        }

        [Test]
        public void TestRemoveAbilityAlsoRemovesFromRoles()
        {
            _authorizationService = new AbilityAuthorizationService(new UserRepository(), new RoleRepository(), new AbilityRepository());
            _role1.Abilities.Add(_ability1);
            _role1.Abilities.Add(_ability2);
            _authorizationService.AddRole(_role1);

            IEnumerable<IAbility> abilities = _authorizationService.GetAbilities();
            Assert.AreEqual(2, abilities.Count());
            Assert.AreEqual(_ability1, abilities.First());
            Assert.AreEqual(_ability2, abilities.Last());

            _authorizationService.RemoveAbility("Ability1");
            abilities = _authorizationService.GetAbilities();
            Assert.AreEqual(1, abilities.Count());
            Assert.AreEqual(_ability2, abilities.First());

            IEnumerable<IRole> roles = _authorizationService.GetRoles();
            Assert.AreEqual(1, roles.Count());
            Assert.AreEqual(1, roles.FirstOrDefault().Abilities.Count);
            Assert.AreEqual(_ability2, roles.FirstOrDefault().Abilities.FirstOrDefault());
        }

        [Test]
        public void TestUpdateAbility()
        {
            _authorizationService = new AbilityAuthorizationService(new UserRepository(), new RoleRepository(), new AbilityRepository());
            _authorizationService.AddAbility(_ability1);
            _authorizationService.AddAbility(_ability2);
            IEnumerable<IAbility> abilitys = _authorizationService.GetAbilities();
            Assert.AreEqual(2, abilitys.Count());
            Assert.AreEqual(_ability1, abilitys.First());
            Assert.AreEqual(_ability2, abilitys.Last());

            _ability2.Name = "AbilityUpdate";
            _authorizationService.UpdateAbility(_ability2);
            abilitys = _authorizationService.GetAbilities();
            Assert.AreEqual(2, abilitys.Count());
            Assert.AreEqual(_ability1, abilitys.First());
            Assert.AreEqual(_ability2, abilitys.Last());
            Assert.AreEqual("AbilityUpdate", abilitys.Last().Name);
        }

        [Test]
        public void TestAddAbilityWithChildAbilities()
        {
            _authorizationService = new AbilityAuthorizationService(new UserRepository(), new RoleRepository(), new AbilityRepository());
            _authorizationService.AddAbility(_ability3);
            IAbility ability = _authorizationService.GetAbility("Ability3");
            Assert.AreEqual(ability, _ability3);
            Assert.AreEqual(2, _ability3.Abilities.Count);
            Assert.True(ability.Abilities.SequenceEqual(_ability3.Abilities));
        }

        [Test]
        public void TestGetUser()
        {
            _authorizationService = new AbilityAuthorizationService(new UserRepository(), new RoleRepository(), new AbilityRepository());
            _authorizationService.AddUser(_user1);
            _authorizationService.AddUser(_user2);
            IUser user = _authorizationService.GetUser("User1");
            Assert.AreEqual(user, _user1);
        }

        [Test]
        public void TestGetUsers()
        {
            _authorizationService = new AbilityAuthorizationService(new UserRepository(), new RoleRepository(), new AbilityRepository());
            _authorizationService.AddUser(_user1);
            _authorizationService.AddUser(_user2);
            IEnumerable<IUser> users = _authorizationService.GetUsers();
            Assert.AreEqual(2, users.Count());
            Assert.AreEqual(_user1, users.First());
            Assert.AreEqual(_user2, users.Last());
        }

        [Test]
        public void TestRemoveUser()
        {
            _authorizationService = new AbilityAuthorizationService(new UserRepository(), new RoleRepository(), new AbilityRepository());
            _authorizationService.AddUser(_user1);
            _authorizationService.AddUser(_user2);
            IEnumerable<IUser> users = _authorizationService.GetUsers();
            Assert.AreEqual(2, users.Count());
            Assert.AreEqual(_user1, users.First());
            Assert.AreEqual(_user2, users.Last());

            _authorizationService.RemoveUser("User1");
            users = _authorizationService.GetUsers();
            Assert.AreEqual(1, users.Count());
            Assert.AreEqual(_user2, users.First());
        }

        [Test]
        public void TestUpdateUser()
        {
            _authorizationService = new AbilityAuthorizationService(new UserRepository(), new RoleRepository(), new AbilityRepository());
            _authorizationService.AddUser(_user1);
            _authorizationService.AddUser(_user2);
            IEnumerable<IUser> users = _authorizationService.GetUsers();
            Assert.AreEqual(2, users.Count());
            Assert.AreEqual(_user1, users.First());
            Assert.AreEqual(_user2, users.Last());

            _user2.Name = "UserUpdate";
            _authorizationService.UpdateUser(_user2);
            users = _authorizationService.GetUsers();
            Assert.AreEqual(2, users.Count());
            Assert.AreEqual(_user1, users.First());
            Assert.AreEqual(_user2, users.Last());
            Assert.AreEqual("UserUpdate", users.Last().Name);
        }

        [Test]
        public void TestGetRole()
        {
            _authorizationService = new AbilityAuthorizationService(new UserRepository(), new RoleRepository(), new AbilityRepository());
            _authorizationService.AddRole(_role1);
            _authorizationService.AddRole(_role2);
            IRole role = _authorizationService.GetRole("Role1");
            Assert.AreEqual(role, _role1);
        }

        [Test]
        public void TestGetRoles()
        {
            _authorizationService = new AbilityAuthorizationService(new UserRepository(), new RoleRepository(), new AbilityRepository());
            _authorizationService.AddRole(_role1);
            _authorizationService.AddRole(_role2);
            IEnumerable<IRole> roles = _authorizationService.GetRoles();
            Assert.AreEqual(2, roles.Count());
            Assert.AreEqual(_role1, roles.First());
            Assert.AreEqual(_role2, roles.Last());
        }

        [Test]
        public void TestRemoveRole()
        {
            _authorizationService = new AbilityAuthorizationService(new UserRepository(), new RoleRepository(), new AbilityRepository());
            _authorizationService.AddRole(_role1);
            _authorizationService.AddRole(_role2);
            IEnumerable<IRole> roles = _authorizationService.GetRoles();
            Assert.AreEqual(2, roles.Count());
            Assert.AreEqual(_role1, roles.First());
            Assert.AreEqual(_role2, roles.Last());

            _authorizationService.RemoveRole("Role1");
            roles = _authorizationService.GetRoles();
            Assert.AreEqual(1, roles.Count());
            Assert.AreEqual(_role2, roles.First());
        }

        [Test]
        public void TestUpdateRole()
        {
            _authorizationService = new AbilityAuthorizationService(new UserRepository(), new RoleRepository(), new AbilityRepository());
            _authorizationService.AddRole(_role1);
            _authorizationService.AddRole(_role2);
            IEnumerable<IRole> roles = _authorizationService.GetRoles();
            Assert.AreEqual(2, roles.Count());
            Assert.AreEqual(_role1, roles.First());
            Assert.AreEqual(_role2, roles.Last());

            _role2.Name = "RoleUpdate";
            _authorizationService.UpdateRole(_role2);
            roles = _authorizationService.GetRoles();
            Assert.AreEqual(2, roles.Count());
            Assert.AreEqual(_role1, roles.First());
            Assert.AreEqual(_role2, roles.Last());
            Assert.AreEqual("RoleUpdate", roles.Last().Name);
        }

        [Test]
        public void TestAuthorizionWithEmptyRoleThrowException()
        {
            Assert.Throws<NotImplementedException>(() => _authorizationService.Authorize(""));
        }

        [Test]
        public void TestGetAbilitiesForUser()
        {
            _authorizationService = new AbilityAuthorizationService(new UserRepository(), new RoleRepository(), new AbilityRepository());
            _role1.Abilities.Add(_ability1);
            _role2.Abilities.Add(_ability2);
            _user1.Roles.Add(_role1);
            _user1.Roles.Add(_role2);
            _authorizationService.AddUser(_user1);

            IEnumerable<IAbility> abilities = _authorizationService.GetAbilitiesForUser(_user1.Name);
            Assert.AreEqual(2, abilities.Count());
            Assert.AreEqual(_ability1, abilities.First());
            Assert.AreEqual(_ability2, abilities.Last());
        }
    }
}
