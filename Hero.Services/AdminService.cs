using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hero.Interfaces;
using Hero.Services.Interfaces;
using Repositories.Interfaces;

namespace Hero.Services
{
    public class AdminService : IAdminService
    {
        private IList<IUser> _users;
        private IList<IRole> _roles;
        private IList<IAbility> _abilities;
 
        public AdminService()
        {
            _users = new List<IUser>();
            _roles = new List<IRole>();
            _abilities = new List<IAbility>();
        }

        public IEnumerable<IUser> GetUsers()
        {
            return _users;
        }

        public IEnumerable<IRole> GetRoles()
        {
            return _roles;
        }

        public IEnumerable<IAbility> GetAbilities()
        {
            return _abilities;
        }

        public IUser GetUser(string name)
        {
            return _users.FirstOrDefault(u => u.Name == name);
        }

        public IRole GetRole(string name)
        {
            return _roles.FirstOrDefault(u => u.Name == name);
        }

        public IAbility GetAbility(string name)
        {
            return _abilities.FirstOrDefault(u => u.Name == name);
        }

        public IUser AddUser(IUser user)
        {
            _users.Add(user);
            return GetUser(user.Name);
        }

        public IRole AddRole(IRole role)
        {
            _roles.Add(role);
            return GetRole(role.Name);
        }

        public IAbility AddAbility(IAbility ability)
        {
            _abilities.Add(ability);
            return GetAbility(ability.Name);
        }

        public void RemoveUser(string id)
        {
            _users.Remove(_users.FirstOrDefault(u => u.Id == id));
        }

        public void RemoveRole(string id)
        {
            _roles.Remove(_roles.FirstOrDefault(r => r.Id == id));
        }

        public void RemoveAbility(string id)
        {
            _abilities.Remove(_abilities.FirstOrDefault(a => a.Id == id));
        }

        public IUser UpdateUser(IUser user)
        {
            RemoveUser(user.Id);
            AddUser(user);
            return GetUser(user.Name);
        }

        public IRole UpdateRole(IRole role)
        {
            RemoveRole(role.Id);
            AddRole(role);
            return GetRole(role.Name);
        }

        public IAbility UpdateAbility(IAbility ability)
        {
            RemoveAbility(ability.Id);
            AddAbility(ability);
            return GetAbility(ability.Name);
        }
    }
}
