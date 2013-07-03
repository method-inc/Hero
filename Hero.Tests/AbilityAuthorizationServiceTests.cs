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
        public void TestAuthorizationServiceCanRegisterAbilityWithEmptyMap()
        {
            Assert.DoesNotThrow(() => _authorizationService.RegisterAbility(_role1, _ability1));
        }

        [Test]
        public void TestAuthorizationServiceCanRegisterRoleWithEmptyMap()
        {
            Assert.DoesNotThrow(() => _authorizationService.RegisterRole(_user1, _role1));
        }

        [Test]
        public void TestAuthorizationServiceCanRegisterMultipleAbilitiesWithEmptyMap()
        {
            Assert.DoesNotThrow(() => _authorizationService.RegisterAbility(_role1, _ability1));
            Assert.DoesNotThrow(() => _authorizationService.RegisterAbility(_role1, _ability2));
        }

        [Test]
        public void TestAuthorizationServiceCanRegisterMultipleRolesWithEmptyMap()
        {
            Assert.DoesNotThrow(() => _authorizationService.RegisterRole(_user1, _role1));
            Assert.DoesNotThrow(() => _authorizationService.RegisterRole(_user1, _role2));
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
        public void TestAuthorizationServiceCanRegisterMultipleRolesAndUserssWithEmptyMap()
        {
            Assert.DoesNotThrow(() => _authorizationService.RegisterRole(_user1, _role1));
            Assert.DoesNotThrow(() => _authorizationService.RegisterRole(_user1, _role2));
            Assert.DoesNotThrow(() => _authorizationService.RegisterRole(_user2, _role1));
            Assert.DoesNotThrow(() => _authorizationService.RegisterRole(_user2, _role2));
        }

        [Test]
        public void TestAuthorizationServiceUnregisterAbilityDoesNothingWithEmptyMap()
        {
            Assert.DoesNotThrow(() => _authorizationService.UnregisterAbility(_role1, _ability1));
        }

        [Test]
        public void TestAuthorizationServiceUnregisterRoleDoesNothingWithEmptyMap()
        {
            Assert.DoesNotThrow(() => _authorizationService.UnregisterRole(_user1, _role1));
        }

        [Test]
        public void TestAuthorizationServiceAuthorizeWithOneRoleOneAbility()
        {
            _authorizationService.RegisterAbility(_role1, _ability1);
            Assert.True(_authorizationService.Authorize(_role1, _ability1));
        }

        [Test]
        public void TestAuthorizationServiceAuthorizeWithOneUserOneRole()
        {
            _authorizationService.RegisterRole(_user1, _role1);
            Assert.True(_authorizationService.GetRolesForUser(_user1).Any(r => r.Equals(_role1)));
        }

        [Test]
        public void TestAuthorizationServiceAuthorizeWithOneUserOneAbility()
        {
            _authorizationService.RegisterAbility(_role1, _ability1);
            _authorizationService.RegisterRole(_user1, _role1);
            Assert.True(_authorizationService.Authorize(_user1, _ability1));
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
        public void TestAuthorizationServiceAuthorizeWithOneUserMultipleRoles()
        {
            _authorizationService.RegisterRole(_user1, _role1);
            _authorizationService.RegisterRole(_user1, _role2);

            Assert.True(_authorizationService.GetRolesForUser(_user1).Any(r => r.Equals(_role1)));
            Assert.True(_authorizationService.GetRolesForUser(_user1).Any(r => r.Equals(_role2)));
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
        public void TestAuthorizationServiceAuthorizeWithMultipleUsersMultipleRoles()
        {
            _authorizationService.RegisterRole(_user1, _role1);
            _authorizationService.RegisterRole(_user1, _role2);
            _authorizationService.RegisterRole(_user2, _role1);
            _authorizationService.RegisterRole(_user2, _role2);

            Assert.True(_authorizationService.GetRolesForUser(_user1).Any(r => r.Equals(_role1)));
            Assert.True(_authorizationService.GetRolesForUser(_user1).Any(r => r.Equals(_role2)));
            Assert.True(_authorizationService.GetRolesForUser(_user2).Any(r => r.Equals(_role1)));
            Assert.True(_authorizationService.GetRolesForUser(_user2).Any(r => r.Equals(_role2)));
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
        public void TestAuthorizationServiceAuthorizeWithOneUserOneRoleAfterUnregister()
        {
            _authorizationService.RegisterRole(_user1, _role1);
            Assert.True(_authorizationService.GetRolesForUser(_user1).Any(r => r.Equals(_role1)));
            _authorizationService.UnregisterRole(_user1, _role1);
            Assert.False(_authorizationService.GetRolesForUser(_user1).Any(r => r.Equals(_role1)));
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
        public void TestAuthorizationServiceAuthorizeWithMultipleUsersMultipleRolesAfterUnregister()
        {
            _authorizationService.RegisterRole(_user1, _role1);
            _authorizationService.RegisterRole(_user1, _role2);
            _authorizationService.RegisterRole(_user2, _role1);
            _authorizationService.RegisterRole(_user2, _role2);
            Assert.True(_authorizationService.GetRolesForUser(_user1).Any(r => r.Equals(_role1)));
            Assert.True(_authorizationService.GetRolesForUser(_user1).Any(r => r.Equals(_role2)));
            Assert.True(_authorizationService.GetRolesForUser(_user2).Any(r => r.Equals(_role1)));
            Assert.True(_authorizationService.GetRolesForUser(_user2).Any(r => r.Equals(_role2)));


            _authorizationService.UnregisterRole(_user1, _role1);
            Assert.False(_authorizationService.GetRolesForUser(_user1).Any(r => r.Equals(_role1)));
            Assert.True(_authorizationService.GetRolesForUser(_user1).Any(r => r.Equals(_role2)));
            Assert.True(_authorizationService.GetRolesForUser(_user2).Any(r => r.Equals(_role1)));
            Assert.True(_authorizationService.GetRolesForUser(_user2).Any(r => r.Equals(_role2)));
        }

        [Test]
        public void TestAuthorizationServiceNotAuthorizeWithAbilityHierarchyNoRole()
        {
            _authorizationService.RegisterAbility(_role1, _ability1);
            _authorizationService.RegisterChildAbility(_ability1, _ability2);
            _authorizationService.RegisterChildAbility(_ability2, _ability3);
            Assert.True(_authorizationService.Authorize(_role1, _ability1));
            Assert.True(_authorizationService.Authorize(_role1, _ability2));
            Assert.True(_authorizationService.Authorize(_role1, _ability3));
            Assert.False(_authorizationService.Authorize(_role2, _ability1));
            Assert.False(_authorizationService.Authorize(_role2, _ability2));
            Assert.False(_authorizationService.Authorize(_role2, _ability3));
        }

        [Test]
        public void TestAuthorizationServiceAuthorizeWithAbilityHierarchy()
        {
            _authorizationService.RegisterAbility(_role1, _ability1);
            _authorizationService.RegisterAbility(_role2, _ability1);
            _authorizationService.RegisterChildAbility(_ability1, _ability2);
            _authorizationService.RegisterChildAbility(_ability2, _ability3);
            Assert.True(_authorizationService.Authorize(_role1, _ability1));
            Assert.True(_authorizationService.Authorize(_role1, _ability2));
            Assert.True(_authorizationService.Authorize(_role1, _ability3));
            Assert.True(_authorizationService.Authorize(_role2, _ability1));
            Assert.True(_authorizationService.Authorize(_role2, _ability2));
            Assert.True(_authorizationService.Authorize(_role2, _ability3));
        }

        [Test]
        public void TestAuthorizationServiceAuthorizeWithComplexAbilityHierarchy()
        {
            Ability _ability4 = new Ability("Ability4");
            Ability _ability5 = new Ability("Ability5");
            Ability _ability6 = new Ability("Ability6");
            Ability _ability7 = new Ability("Ability7");
            Ability _ability8 = new Ability("Ability8");
            Ability _ability9 = new Ability("Ability9");
            Ability _ability10 = new Ability("Ability10");

            _authorizationService.RegisterAbility(_role1, _ability1);

            _authorizationService.RegisterChildAbility(_ability1, _ability2);
            _authorizationService.RegisterChildAbility(_ability1, _ability4);
            _authorizationService.RegisterChildAbility(_ability1, _ability8);
            _authorizationService.RegisterChildAbility(_ability2, _ability3);
            _authorizationService.RegisterChildAbility(_ability2, _ability6);
            _authorizationService.RegisterChildAbility(_ability4, _ability5);
            _authorizationService.RegisterChildAbility(_ability5, _ability9);

            Assert.True(_authorizationService.Authorize(_role1, _ability1));
            Assert.True(_authorizationService.Authorize(_role1, _ability2));
            Assert.True(_authorizationService.Authorize(_role1, _ability3));
            Assert.True(_authorizationService.Authorize(_role1, _ability4));
            Assert.True(_authorizationService.Authorize(_role1, _ability5));
            Assert.True(_authorizationService.Authorize(_role1, _ability6));
            Assert.False(_authorizationService.Authorize(_role1, _ability7));
            Assert.True(_authorizationService.Authorize(_role1, _ability8));
            Assert.True(_authorizationService.Authorize(_role1, _ability9));
            Assert.False(_authorizationService.Authorize(_role1, _ability10));
        }

        [Test]
        public void TestAuthorizationServiceNotAuthorizeWithAbilityHierarchy()
        {
            _authorizationService.RegisterAbility(_role1, _ability1);
            _authorizationService.RegisterAbility(_role2, _ability1);
            _authorizationService.RegisterChildAbility(_ability1, _ability2);
            Assert.True(_authorizationService.Authorize(_role1, _ability1));
            Assert.True(_authorizationService.Authorize(_role1, _ability2));
            Assert.False(_authorizationService.Authorize(_role1, _ability3));
            Assert.True(_authorizationService.Authorize(_role2, _ability1));
            Assert.True(_authorizationService.Authorize(_role2, _ability2));
            Assert.False(_authorizationService.Authorize(_role2, _ability3));
        }

        [Test]
        public void TestAuthorizationServiceAbilityEvents()
        {
            _authorizationService.RegisterAbility(_role1, _ability1);
            Assert.True(_authorizationService.Authorize(_role1, _ability1));
            Assert.AreEqual(1, _abilityConsumer.Counter);
            Assert.AreEqual(new RoleAbility(_role1, _ability1), _abilityConsumer.Param[0]);
            _authorizationService.UnregisterAbility(_role1, _ability1);
            Assert.AreEqual(0, _abilityConsumer.Counter);
            Assert.AreEqual(new RoleAbility(_role1, _ability1), _abilityConsumer.Param[0]);
        }

        [Test]
        public void TestAuthorizationServiceRoleEvents()
        {
            _authorizationService.RegisterRole(_user1, _role1);
            Assert.True(_authorizationService.GetRolesForUser(_user1).Any(r => r.Equals(_role1)));
            Assert.AreEqual(1, _roleConsumer.Counter);
            Assert.AreEqual(new UserRole(_user1, _role1), _roleConsumer.Param[0]);
            _authorizationService.UnregisterRole(_user1, _role1);
            Assert.AreEqual(0, _roleConsumer.Counter);
            Assert.AreEqual(new UserRole(_user1, _role1), _roleConsumer.Param[0]);
        }

        [Test]
        public void TestGetAbilitiesForRoleThatExists()
        {
            _authorizationService.RegisterAbility(_role1, _ability1);
            _authorizationService.RegisterAbility(_role1, _ability2);
            IEnumerable<Ability> abilitiesForRole = _authorizationService.GetAbilitiesForRole(_role1);
            Assert.AreEqual(2, abilitiesForRole.Count());
            Assert.True(new List<Ability> {_ability1, _ability2}.SequenceEqual(abilitiesForRole));
        }

        [Test]
        public void TestGetAbilitiesForRoleThatDoesNotExist()
        {
            _authorizationService.RegisterAbility(_role1, _ability1);
            _authorizationService.RegisterAbility(_role1, _ability2);
            IEnumerable<Ability> abilitiesForRole = _authorizationService.GetAbilitiesForRole(_role2);
            Assert.AreEqual(0, abilitiesForRole.Count());
        }

        [Test]
        public void TestGetAbilitiesForRoleNameThatExists()
        {
            _authorizationService.RegisterAbility(_role1, _ability1);
            _authorizationService.RegisterAbility(_role1, _ability2);
            IEnumerable<Ability> abilitiesForRole = _authorizationService.GetAbilitiesForRole("Role1");
            Assert.AreEqual(2, abilitiesForRole.Count());
            Assert.True(new List<Ability> {_ability1, _ability2}.SequenceEqual(abilitiesForRole));
        }

        [Test]
        public void TestGetAbilitiesForRoleNameThatDoesNotExist()
        {
            _authorizationService.RegisterAbility(_role1, _ability1);
            _authorizationService.RegisterAbility(_role1, _ability2);
            IEnumerable<Ability> abilitiesForRole = _authorizationService.GetAbilitiesForRole("Role2");
            Assert.AreEqual(0, abilitiesForRole.Count());
        }

        [Test]
        public void TestGetRolesForUserThatDoesExist()
        {
            _authorizationService.RegisterRole(_user1, _role1);
            _authorizationService.RegisterRole(_user1, _role2);
            IEnumerable<IRole> rolesForUser = _authorizationService.GetRolesForUser(_user1);
            Assert.AreEqual(2, rolesForUser.Count());
            Assert.True(new List<IRole> {_role1, _role2}.SequenceEqual(rolesForUser));
        }

        [Test]
        public void TestGetRolesForUserThatDoesNotExist()
        {
            _authorizationService.RegisterRole(_user1, _role1);
            _authorizationService.RegisterRole(_user1, _role2);
            IEnumerable<IRole> rolesForUser = _authorizationService.GetRolesForUser(_user2);
            Assert.AreEqual(0, rolesForUser.Count());
        }

        [Test]
        public void TestGetRolesForUserNameThatDoesExist()
        {
            _authorizationService.RegisterRole(_user1, _role1);
            _authorizationService.RegisterRole(_user1, _role2);
            IEnumerable<IRole> rolesForUser = _authorizationService.GetRolesForUser("User1");
            Assert.AreEqual(2, rolesForUser.Count());
            Assert.True(new List<IRole> {_role1, _role2}.SequenceEqual(rolesForUser));
        }

        [Test]
        public void TestGetAbilitiesForUserThatDoesExist()
        {
            _authorizationService.RegisterRole(_user1, _role1);
            _authorizationService.RegisterRole(_user1, _role2);
            _authorizationService.RegisterAbility(_role1, _ability1);
            _authorizationService.RegisterAbility(_role1, _ability2);
            _authorizationService.RegisterAbility(_role2, _ability3);
            IEnumerable<Ability> abilitiesForUser = _authorizationService.GetAbilitiesForUser(_user1);
            Assert.AreEqual(3, abilitiesForUser.Count());
            Assert.True(new List<Ability> {_ability1, _ability2, _ability3}.SequenceEqual(abilitiesForUser));
        }

        [Test]
        public void TestGetAbilitiesForUserThatDoesNotExist()
        {
            _authorizationService.RegisterRole(_user1, _role1);
            _authorizationService.RegisterRole(_user1, _role2);
            _authorizationService.RegisterAbility(_role1, _ability1);
            _authorizationService.RegisterAbility(_role1, _ability2);
            _authorizationService.RegisterAbility(_role2, _ability3);
            IEnumerable<Ability> abilitiesForUser = _authorizationService.GetAbilitiesForUser(_user2);
            Assert.AreEqual(0, abilitiesForUser.Count());
        }

        [Test]
        public void TestGetAbilitiesForUserNameThatDoesExist()
        {
            _authorizationService.RegisterRole(_user1, _role1);
            _authorizationService.RegisterRole(_user1, _role2);
            _authorizationService.RegisterAbility(_role1, _ability1);
            _authorizationService.RegisterAbility(_role1, _ability2);
            _authorizationService.RegisterAbility(_role2, _ability3);
            IEnumerable<Ability> abilitiesForUser = _authorizationService.GetAbilitiesForUser("User1");
            Assert.AreEqual(3, abilitiesForUser.Count());
            Assert.True(new List<Ability> {_ability1, _ability2, _ability3}.SequenceEqual(abilitiesForUser));
        }

        [Test]
        public void TestGetAbilitiesForUserNameThatDoesNotExist()
        {
            _authorizationService.RegisterRole(_user1, _role1);
            _authorizationService.RegisterRole(_user1, _role2);
            _authorizationService.RegisterAbility(_role1, _ability1);
            _authorizationService.RegisterAbility(_role1, _ability2);
            _authorizationService.RegisterAbility(_role2, _ability3);
            IEnumerable<Ability> abilitiesForUser = _authorizationService.GetAbilitiesForUser("User2");
            Assert.AreEqual(0, abilitiesForUser.Count());
        }

        [Test]
        public void TestGetRolesForUserNameThatDoesNotExist()
        {
            _authorizationService.RegisterRole(_user1, _role1);
            _authorizationService.RegisterRole(_user1, _role2);
            IEnumerable<IRole> rolesForUser = _authorizationService.GetRolesForUser("User2");
            Assert.AreEqual(0, rolesForUser.Count());
        }
    }
}
