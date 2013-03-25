using Hero.Interfaces;
using System.Collections.Generic;

namespace Hero.Services.Interfaces
{
    public interface IAbilityAuthorizationService
    {
        bool Authorize(IRole role, Ability ability);
        bool Authorize(IUser user, Ability ability);
        void RegisterAbility(IRole role, Ability ability);
        void UnregisterAbility(IRole role, Ability ability);
        void RegisterRole(IUser user, IRole role);
        void UnregisterRole(IUser user, IRole role);
        IEnumerable<IRole> GetRolesForUser(IUser user);
        IEnumerable<Ability> GetAbilitiesForRole(IRole role);
    }
}