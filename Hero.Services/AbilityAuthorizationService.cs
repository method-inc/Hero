using System.Collections.Generic;
using Hero.Interfaces;
using Hero.Services.Interfaces;

namespace Hero.Services
{
    public class AbilityAuthorizationService : AuthorizationService, IAbilityAuthorizationService
    {
        // TODO Make this generate an event?
        private readonly RoleAbilityMap _roleAbilityMap;

        public AbilityAuthorizationService()
        {
            _roleAbilityMap = new RoleAbilityMap();
        }

        public bool Authorize(IRole role, Ability ability)
        {
            return _Authorize(role, ability);
        }

        public void RegisterAbility(IRole role, Ability ability)
        {
            if (_roleAbilityMap.ContainsKey(role))
                _roleAbilityMap[role].Add(ability);
            _roleAbilityMap.Add(role, new HashSet<Ability> {ability});
        }

        public void UnregisterAbility(IRole role, Ability ability)
        {
            if (!_roleAbilityMap.ContainsKey(role))
                return;
            _roleAbilityMap.Remove(role);
        }

        private bool _Authorize(IRole role, Ability ability)
        {
            return _roleAbilityMap.ContainsKey(role) &&
                   _roleAbilityMap[role].Contains(ability);
        }
    }
}