using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<User> GetUsers()
        {
            return _userRepository.Get<User>();
        }

        public IEnumerable<Role> GetRoles()
        {
            return _roleRepository.Get<Role>();
        }

        public IEnumerable<Ability> GetAbilities()
        {
            return _abilityRepository.Get<Ability>();
        }

        public User GetUser(string id)
        {
            return _userRepository.Get<User>().FirstOrDefault(user => user.Id == id);
        }

        public Role GetRole(string id)
        {
            return _roleRepository.Get<Role>().FirstOrDefault(role => role.Id == id);
        }

        public Ability GetAbility(string id)
        {
            return _abilityRepository.Get<Ability>().FirstOrDefault(ability => ability.Id == id);
        }

        public User AddUser(User user)
        {
            foreach (Role role in user.Roles)
                AddRole(role);

            _userRepository.Create(user);
            return GetUser(user.Name);
        }

        public Role AddRole(Role role)
        {
            foreach (Ability ability in role.Abilities)
                AddAbility(ability);

            _roleRepository.Create(role);
            return GetRole(role.Name);
        }

        public Ability AddAbility(Ability ability)
        {
            _abilityRepository.Create(ability);

            foreach (Ability childAbility in ability.Abilities)
                AddAbility(childAbility);

            return GetAbility(ability.Name);
        }

        public void RemoveUser(string id)
        {
            RemoveUser((GetUser(id)));
        }

        public void RemoveUser(User user)
        {
            _userRepository.Delete(user);
        }

        public void RemoveRole(string id)
        {
            Role role = GetRole(id);
            RemoveRole(role);
        }

        public void RemoveRole(Role role)
        {
            foreach (User user in _userRepository.Get<User>())
                user.Roles.Remove(role);
            _roleRepository.Delete(role);
        }

        public void RemoveAbility(string id)
        {
            Ability ability = GetAbility(id);
            RemoveAbility(ability);
        }

        public void RemoveAbility(Ability ability)
        {
            foreach (Role role in _roleRepository.Get<Role>())
                role.Abilities.Remove(ability);
            _abilityRepository.Delete(ability);
        }

        public User UpdateUser(User user)
        {
            RemoveUser(user.Id);
            AddUser(user);
            return GetUser(user.Name);
        }

        public Role UpdateRole(Role role)
        {
            RemoveRole(role.Id);
            AddRole(role);
            return GetRole(role.Name);
        }

        public Ability UpdateAbility(Ability ability)
        {
            RemoveAbility(ability.Id);
            AddAbility(ability);
            return GetAbility(ability.Name);
        }

        public bool Authorize(Role role, Ability ability)
        {
            if (role.Abilities.Contains(ability))
                return true;

            foreach (Ability root in role.Abilities)
                if (_Authorize(root, ability))
                    return true;

            return false;
        }

        public bool Authorize(string userName, string abilityName)
        {
            User user = GetUser(userName);
            Ability ability = GetAbility(abilityName);
            return Authorize(user, ability);
        }

        public bool Authorize(User user, Ability ability)
        {
            if (user == null || ability == null) return false;
            return user.Roles.Any(role => Authorize(role, ability));
        }

        private bool _Authorize(Ability root, Ability query)
        {
            if (root.Abilities.Contains(query))
                return true;

            foreach (Ability childAbility in root.Abilities)
            {
                if (_Authorize(childAbility, query))
                    return true;
            }

            return false;
        }

        public IEnumerable<Ability> GetAbilitiesForUser(string userName)
        {
            return GetAbilitiesForUser(_userRepository.Get<User>().FirstOrDefault(user => user.Name.Equals(userName)));
        }

        public IEnumerable<Ability> GetAbilitiesForUser(User user)
        {
            if(user == null)
                return new List<Ability>();

            var abilities = new HashSet<Ability>();

            foreach (var ability in user.Roles.SelectMany(role => role.Abilities))
            {
                abilities.Add(ability);
                AddChildAbilities(ability, abilities);
            }

            return abilities;
        }

        private void AddChildAbilities(Ability root, HashSet<Ability> abilities)
        {
            foreach (Ability childAbility in root.Abilities)
            {
                abilities.Add(childAbility);
                AddChildAbilities(childAbility, abilities);
            }
        }
    }
}