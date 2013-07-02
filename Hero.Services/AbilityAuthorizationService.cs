using System.Collections.Generic;
using System.Linq;
using DotNetStandard.Vent;
using Hero.Interfaces;
using Hero.Services.Events;
using Hero.Services.Interfaces;
using Repositories.Interfaces;
using NGenerics.DataStructures.Trees;
using NGenerics.Patterns.Visitor;

namespace Hero.Services
{
    public class AbilityAuthorizationService : AuthorizationService, IAbilityAuthorizationService
    {
        private readonly RoleAbilityMap _roleAbilityMap;
        private readonly UserRoleMap _userRoleMap;

        public AbilityAuthorizationService()
        {
            _roleAbilityMap = new RoleAbilityMap();
            _userRoleMap = new UserRoleMap();
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