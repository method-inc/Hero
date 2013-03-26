using System.Collections.Generic;
using System.Linq;
using DotNetStandard.Vent;
using Hero.Interfaces;
using Hero.Services.Events;
using Hero.Services.Interfaces;

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
                _roleAbilityMap[role].Add(ability);
            else
                _roleAbilityMap.Add(role, new HashSet<Ability> {ability});

            EventAggregator.Instance.Trigger(
                new RegisterAbilityEvent(),
                new object[] { new RoleAbility(role, ability) }
                );

            foreach (Ability childAbility in ability.Children)
            {
                _roleAbilityMap[role].Add(childAbility);

                EventAggregator.Instance.Trigger(
                new RegisterAbilityEvent(),
                new object[] { new RoleAbility(role, childAbility) }
                );
            }
        }

        public void UnregisterAbility(IRole role, Ability ability)
        {
            if (!_roleAbilityMap.ContainsKey(role))
                return;
            _roleAbilityMap[role].Remove(ability);

            EventAggregator.Instance.Trigger(
                new UnregisterAbilityEvent(),
                new object[] {new RoleAbility(role, ability)}
                );

            foreach (Ability childAbility in ability.Children)
            {
                _roleAbilityMap[role].Remove(childAbility);

                EventAggregator.Instance.Trigger(
                new UnregisterAbilityEvent(),
                new object[] { new RoleAbility(role, childAbility) }
                );
            }
        }

        public void RegisterRole(IUser user, IRole role)
        {
            if (_userRoleMap.ContainsKey(user))
                _userRoleMap[user].Add(role);
            else
                _userRoleMap.Add(user, new HashSet<IRole> {role});

            EventAggregator.Instance.Trigger(
                new RegisterRoleEvent(),
                new object[] {new UserRole(user, role)}
                );
        }

        public void UnregisterRole(IUser user, IRole role)
        {
            if (!_userRoleMap.ContainsKey(user))
                return;
            _userRoleMap[user].Remove(role);

            EventAggregator.Instance.Trigger(
                new UnregisterRoleEvent(),
                new object[] {new UserRole(user, role)}
                );
        }

        private bool _Authorize(IRole role, Ability ability)
        {
            return _roleAbilityMap.ContainsKey(role) &&
                   _roleAbilityMap[role].Contains(ability);
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
            var retVal = new HashSet<Ability>();
            if (_roleAbilityMap.TryGetValue(role, out retVal))
                return retVal;

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
                var roleAbilities = new HashSet<Ability>();
                if (_roleAbilityMap.TryGetValue(role, out roleAbilities))
                {
                    foreach (var ability in roleAbilities)
                        abilities.Add(ability);
                }
            }

            return abilities;
        }
    }
}