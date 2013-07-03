using System.Collections.Generic;
using System.Linq;
using DotNetStandard.Vent;
using Hero.Interfaces;
using Hero.Repositories;
using Hero.Services.Events;
using Hero.Services.Interfaces;
using NGenerics.DataStructures.Trees;

namespace Hero.Services
{
    public class AbilityAuthorizationService : AuthorizationService, IAbilityAuthorizationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IAbilityRepository _abilityRepository;
        private readonly RoleAbilityMap _roleAbilityMap;
        private readonly UserRoleMap _userRoleMap;

        public AbilityAuthorizationService()
        {
            _roleAbilityMap = new RoleAbilityMap();
            _userRoleMap = new UserRoleMap();
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
            return _userRepository.Get<IUser>().FirstOrDefault(u => u.Id == id);
        }

        public IRole GetRole(string id)
        {
            return _roleRepository.Get<IRole>().FirstOrDefault(u => u.Id == id);
        }

        public IAbility GetAbility(string id)
        {
            return _abilityRepository.Get<IAbility>().FirstOrDefault(u => u.Id == id);
        }

        public IUser AddUser(IUser user)
        {
            _userRepository.Create(user);
            return GetUser(user.Name);
        }

        public IRole AddRole(IRole role)
        {
            _roleRepository.Create(role);
            return GetRole(role.Name);
        }

        public IAbility AddAbility(IAbility ability)
        {
            _abilityRepository.Create(ability);
            return GetAbility(ability.Name);
        }

        public void RemoveUser(string id)
        {
            _userRepository.Delete(GetUser(id));
        }

        public void RemoveRole(string id)
        {
            _roleRepository.Delete(GetRole(id));
        }

        public void RemoveAbility(string id)
        {
            _abilityRepository.Delete(GetAbility(id));
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

        public bool Authorize(IRole role, Ability ability)
        {
            return _Authorize(role, ability);
        }

        public bool Authorize(IUser user, Ability ability)
        {
            return GetRolesForUser(user).Any(role => _Authorize(role, ability));
        }

        public void RegisterAbility(IRole role, Ability ability)
        {
            if (_roleAbilityMap.ContainsKey(role))
                _roleAbilityMap[role].Add(new GeneralTree<Ability>(ability));
            else
                _roleAbilityMap.Add(role, new List<GeneralTree<Ability>> {new GeneralTree<Ability>(ability)});

            EventAggregator.Instance.Trigger(
                new RegisterAbilityEvent(),
                new object[] { new RoleAbility(role, ability) }
                );
        }

        public void UnregisterAbility(IRole role, Ability ability)
        {
            if (!_roleAbilityMap.ContainsKey(role))
                return;

            for (int i = 0; i < _roleAbilityMap[role].Count; i++)
            {
                if (!_roleAbilityMap[role][i].Data.Equals(ability))
                    continue;
                _roleAbilityMap[role].RemoveAt(i);
            }

            EventAggregator.Instance.Trigger(
                new UnregisterAbilityEvent(),
                new object[] {new RoleAbility(role, ability)}
                );
        }

        public void RegisterChildAbility(Ability parent, Ability child)
        {
            foreach (KeyValuePair<IRole, List<GeneralTree<Ability>>> roleList in _roleAbilityMap)
                foreach (GeneralTree<Ability> tree in roleList.Value)
                    _Visit(tree, parent, child, roleList.Key);
        }

        public void _Visit(GeneralTree<Ability> tree, Ability parent, Ability child, IRole role)
        {
            if (tree.Data.Equals(parent))
            {
                tree.Add(child);
                EventAggregator.Instance.Trigger(
                    new RegisterAbilityEvent(),
                    new object[] {new RoleAbility(role, child)}
                    );
                return;
            }

            foreach (var childTree in tree.ChildNodes)
                _Visit(childTree, parent, child, role);
        }

        public void RegisterRole(IUser user, IRole role)
        {
            if (_userRoleMap.ContainsKey(user))
                _userRoleMap[user].Add(role);
            else
                _userRoleMap.Add(user, new HashSet<IRole> {role});

            EventAggregator.Instance.Trigger(
                new RegisterUserWithRoleEvent(),
                new object[] {new UserRole(user, role)}
                );
        }

        public void UnregisterRole(IUser user, IRole role)
        {
            if (!_userRoleMap.ContainsKey(user))
                return;
            _userRoleMap[user].Remove(role);

            EventAggregator.Instance.Trigger(
                new UnregisterUserWithRoleEvent(),
                new object[] {new UserRole(user, role)}
                );
        }

        private bool _Authorize(IRole role, Ability ability)
        {
            if (!_roleAbilityMap.ContainsKey(role))
                return false;

            foreach (GeneralTree<Ability> tree in _roleAbilityMap[role])
                foreach (var node in tree.AsEnumerable())
                    if (node == ability)
                        return true;

            return false;
        }

        public IEnumerable<IRole> GetRolesForUser(string userName)
        {
            return GetRolesForUser(new User(userName));
        }

        public IEnumerable<IRole> GetRolesForUser(IUser user)
        {
            var retVal = new HashSet<IRole>();
            if (_userRoleMap.TryGetValue(user, out retVal))
                return retVal;

            return new List<IRole>();
        }

        public IEnumerable<Ability> GetAbilitiesForRole(string roleName)
        {
            return GetAbilitiesForRole(new Role(roleName));
        }

        public IEnumerable<Ability> GetAbilitiesForRole(IRole role)
        {
            List<GeneralTree<Ability>> retVal;
            if (_roleAbilityMap.TryGetValue(role, out retVal))
                return retVal.Select(tree => tree.Data);

            return new List<Ability>();
        }

        public IEnumerable<Ability> GetAbilitiesForUser(string userName)
        {
            return GetAbilitiesForUser(new User(userName));
        }

        public IEnumerable<Ability> GetAbilitiesForUser(IUser user)
        {
            var abilities = new HashSet<Ability>();
            var roles = new HashSet<IRole>();

            if (!_userRoleMap.TryGetValue(user, out roles))
                return new List<Ability>();

            foreach (var role in roles)
            {
                List<GeneralTree<Ability>> roleAbilities;
                if (_roleAbilityMap.TryGetValue(role, out roleAbilities))
                {
                    foreach (var ability in roleAbilities.Select(tree => tree.Data))
                        abilities.Add(ability);
                }
            }

            return abilities;
        }
    }
}