using System;
using System.Collections.Generic;
using System.Linq;
using Hero.Interfaces;
using Hero.Repositories;
using Hero.Services.Interfaces;

namespace Hero.Services
{
    public class AbilityAuthorizationService : AuthorizationService, IAbilityAuthorizationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IAbilityRepository _abilityRepository;

        public AbilityAuthorizationService()
        {
            _userRepository = new UserRepository();
            _roleRepository = new RoleRepository();
            _abilityRepository = new AbilityRepository();
        }

        public AbilityAuthorizationService(IUserRepository userRepository, 
                                           IRoleRepository roleRepository, 
                                           IAbilityRepository abilityRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _abilityRepository = abilityRepository;
        }

        public IEnumerable<IUser> GetUsers()
        {
            return _userRepository.Get<IUser>();
        }

        public IEnumerable<IRole> GetRoles()
        {
            return _roleRepository.Get<IRole>();
        }

        public IEnumerable<IAbility> GetAbilities()
        {
            return _abilityRepository.Get<IAbility>();
        }

        public IUser GetUser(string id)
        {
            return _userRepository.Get<IUser>().FirstOrDefault(user => user.Id == id);
        }

        public IRole GetRole(string id)
        {
            return _roleRepository.Get<IRole>().FirstOrDefault(role => role.Id == id);
        }

        public IAbility GetAbility(string id)
        {
            return _abilityRepository.Get<IAbility>().FirstOrDefault(ability => ability.Id == id);
        }

        public IUser AddUser(IUser user)
        {
            foreach (IRole role in user.Roles)
                AddRole(role);

            _userRepository.Create(user);
            return GetUser(user.Name);
        }

        public IRole AddRole(IRole role)
        {
            foreach (IAbility ability in role.Abilities)
                AddAbility(ability);

            _roleRepository.Create(role);
            return GetRole(role.Name);
        }

        public IAbility AddAbility(IAbility ability)
        {
            _abilityRepository.Create(ability);

            foreach (IAbility childAbility in ability.Abilities)
                AddAbility(childAbility);

            return GetAbility(ability.Name);
        }

        public void RemoveUser(string id)
        {
            RemoveUser((GetUser(id)));
        }

        public void RemoveUser(IUser user)
        {
            _userRepository.Delete(user);
        }

        public void RemoveRole(string id)
        {
            IRole role = GetRole(id);
            RemoveRole(role);
        }

        public void RemoveRole(IRole role)
        {
            foreach (IUser user in _userRepository.Get<IUser>())
                user.Roles.Remove((IRole)role);
            _roleRepository.Delete(role);
        }

        public void RemoveAbility(string id)
        {
            IAbility ability = GetAbility(id);
            RemoveAbility(ability);
        }

        public void RemoveAbility(IAbility ability)
        {
            foreach (IRole role in _roleRepository.Get<IRole>())
                role.Abilities.Remove((IAbility)ability);
            _abilityRepository.Delete(ability);
        }

        public IUser UpdateUser<T>(IUser user) where T : class, IUser
        {
            T userToEdit = _userRepository.Get<T>().FirstOrDefault(u => u.Id == user.Id);

            if (userToEdit == null)
            {
                throw new Exception("Could not find user to update.");
            }

            List<string> currentRoleIds = userToEdit.Roles.Select(r => r.Id).ToList();
            List<string> newRoleIds = user.Roles.Select(r => r.Id).ToList();

            List<IRole> rolesToRemove = userToEdit.Roles.Where(r => !newRoleIds.Contains(r.Id)).ToList();
            List<IRole> rolesToAdd = user.Roles.Where(r => !currentRoleIds.Contains(r.Id)).ToList();

            foreach (IRole role in rolesToAdd)
            {
                userToEdit.Roles.Add(role);
            }

            foreach (IRole role in rolesToRemove)
            {
                userToEdit.Roles.Remove(role);
            }

            _userRepository.Update(userToEdit);

            return userToEdit;
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

        public bool Authorize(IRole role, IAbility ability)
        {
            if (role.Abilities.Contains(ability))
                return true;

            foreach (IAbility root in role.Abilities)
                if (_Authorize(root, ability))
                    return true;

            return false;
        }

        public bool Authorize(string userName, string abilityName)
        {
            IUser user = GetUser(userName);
            IAbility ability = GetAbility(abilityName);
            return Authorize(user, ability);
        }

        public bool Authorize(IUser user, IAbility ability)
        {
            if (user == null || ability == null) return false;
            return user.Roles.Any(role => Authorize(role, ability));
        }

        private bool _Authorize(IAbility root, IAbility query)
        {
            if (root.Abilities.Contains(query))
                return true;

            foreach (IAbility childAbility in root.Abilities)
            {
                if (_Authorize(childAbility, query))
                    return true;
            }

            return false;
        }

        public IEnumerable<IAbility> GetAbilitiesForUser(string userName)
        {
            return GetAbilitiesForUser(_userRepository.Get<IUser>().FirstOrDefault(user => user.Name.Equals(userName)));
        }

        public IEnumerable<IAbility> GetAbilitiesForUser(IUser user)
        {
            if(user == null)
                return new List<IAbility>();

            var abilities = new HashSet<IAbility>();

            foreach (var ability in user.Roles.SelectMany(role => role.Abilities))
            {
                abilities.Add(ability);
                AddChildAbilities(ability, abilities);
            }

            return abilities;
        }

        private void AddChildAbilities(IAbility root, HashSet<IAbility> abilities)
        {
            foreach (IAbility childAbility in root.Abilities)
            {
                abilities.Add(childAbility);
                AddChildAbilities(childAbility, abilities);
            }
        }
    }
}