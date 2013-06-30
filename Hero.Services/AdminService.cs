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
        private IList<Ability> _abilities;
 
        public AdminService()
        {
            _users = new List<IUser>();
            _roles = new List<IRole>();
            _abilities = new List<Ability>();
        }

        public IEnumerable<IUser> GetUsers()
        {
            return _users;
        }

        public IEnumerable<IRole> GetRoles()
        {
            return _roles;
        }

        public IEnumerable<Ability> GetAbilities()
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

        public Ability GetAbility(string name)
        {
            return _abilities.FirstOrDefault(u => u.Name == name);
        }

        public void AddUser(IUser user)
        {
            _users.Add(user);
        }

        public void AddRole(IRole role)
        {
            _roles.Add(role);
        }

        public void AddAbility(Ability ability)
        {
            _abilities.Add(ability);
        }

        public void RemoveUser(IUser user)
        {
            _users.Remove(user);
        }

        public void RemoveRole(IRole role)
        {
            _roles.Remove(role);
        }

        public void RemoveAbility(Ability ability)
        {
            _abilities.Remove(ability);
        }
    }
}
