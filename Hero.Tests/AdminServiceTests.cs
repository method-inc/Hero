using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Hero.Configuration;
using Hero.Frontend;
using Hero.Interfaces;
using Hero.Services;
using Hero.Services.Interfaces;
using Moq;
using NUnit.Framework;

namespace Hero.Tests
{
    public class AdminServiceTests
    {
        private IAdminService _adminService;
        private List<Ability> _abilities;
        private List<IRole> _roles;
        private List<IUser> _users;

        [SetUp]
        public void Initialize()
        {
            _Setup();
            _adminService = new AdminService();
        }

        [Test]
        public void TestAddUsers()
        {
            _users.ForEach(u => _adminService.AddUser(u));
            Assert.AreEqual(3, _adminService.GetUsers().Count());
        }

        [Test]
        public void TestGetUser()
        {
            _users.ForEach(u => _adminService.AddUser(u));
            Assert.AreEqual("User1", _adminService.GetUser("User1").Name);
        }

        [Test]
        public void TestGetUserFail()
        {
            _users.ForEach(u => _adminService.AddUser(u));
            Assert.AreEqual(null, _adminService.GetUser("NoUser"));
        }

        [Test]
        public void TestRemoveUser()
        {
            _users.ForEach(u => _adminService.AddUser(u));
            _adminService.RemoveUser("User1");
            Assert.AreEqual(null, _adminService.GetUser("User1"));
            Assert.AreEqual(2, _adminService.GetUsers().Count());
        }

        [Test]
        public void TestRemoveInvalidUser()
        {
            _users.ForEach(u => _adminService.AddUser(u));
            _adminService.RemoveUser("NoUser");
            Assert.AreEqual(null, _adminService.GetUser("NoUser"));
            Assert.AreEqual(3, _adminService.GetUsers().Count());
        }

        [Test]
        public void TestAddRoles()
        {
            _roles.ForEach(u => _adminService.AddRole(u));
            Assert.AreEqual(3, _adminService.GetRoles().Count());
        }

        [Test]
        public void TestGetRole()
        {
            _roles.ForEach(u => _adminService.AddRole(u));
            Assert.AreEqual("Role1", _adminService.GetRole("Role1").Name);
        }

        [Test]
        public void TestGetRoleFail()
        {
            _roles.ForEach(u => _adminService.AddRole(u));
            Assert.AreEqual(null, _adminService.GetRole("NoRole"));
        }

        [Test]
        public void TestRemoveRole()
        {
            _roles.ForEach(u => _adminService.AddRole(u));
            _adminService.RemoveRole("Role1");
            Assert.AreEqual(null, _adminService.GetRole("Role1"));
            Assert.AreEqual(2, _adminService.GetRoles().Count());
        }

        [Test]
        public void TestRemoveInvalidRole()
        {
            _roles.ForEach(u => _adminService.AddRole(u));
            _adminService.RemoveRole("NoRole");
            Assert.AreEqual(null, _adminService.GetRole("NoRole"));
            Assert.AreEqual(3, _adminService.GetRoles().Count());
        }

        [Test]
        public void TestAddAbilities()
        {
            _abilities.ForEach(u => _adminService.AddAbility(u));
            Assert.AreEqual(3, _adminService.GetAbilities().Count());
        }

        [Test]
        public void TestGetAbility()
        {
            _abilities.ForEach(u => _adminService.AddAbility(u));
            Assert.AreEqual("Ability1", _adminService.GetAbility("Ability1").Name);
        }

        [Test]
        public void TestGetAbilityFail()
        {
            _abilities.ForEach(u => _adminService.AddAbility(u));
            Assert.AreEqual(null, _adminService.GetAbility("NoAbility"));
        }

        [Test]
        public void TestRemoveAbility()
        {
            _abilities.ForEach(u => _adminService.AddAbility(u));
            _adminService.RemoveAbility("Ability1");
            Assert.AreEqual(null, _adminService.GetAbility("Ability1"));
            Assert.AreEqual(2, _adminService.GetAbilities().Count());
        }

        [Test]
        public void TestRemoveInvalidAbility()
        {
            _abilities.ForEach(u => _adminService.AddAbility(u));
            _adminService.RemoveAbility("NoAbility");
            Assert.AreEqual(null, _adminService.GetAbility("NoAbility"));
            Assert.AreEqual(3, _adminService.GetAbilities().Count());
        }

        private void _Setup()
        {
            Ability ability1 = new Ability("Ability1");
            Ability ability2 = new Ability("Ability2");
            Ability ability3 = new Ability("Ability3");
            _abilities = new List<Ability> { ability1, ability2, ability3 };

            IRole role1 = new Role("Role1");
            IRole role2 = new Role("Role2");
            IRole role3 = new Role("Role3");
            _roles = new List<IRole> { role1, role2, role3 };

            IUser user1 = new User("User1");
            IUser user2 = new User("User2");
            IUser user3 = new User("User3");
            _users = new List<IUser> { user1, user2, user3 };
        }
    }
}
