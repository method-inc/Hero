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
        IEnumerable<IRole> GetRolesForUser(string userName);
        IEnumerable<Ability> GetAbilitiesForRole(IRole role);
        IEnumerable<Ability> GetAbilitiesForRole(string roleName);
        IEnumerable<Ability> GetAbilitiesForUser(IUser user);
        IEnumerable<Ability> GetAbilitiesForUser(string userName);


        IEnumerable<IUser> GetUsers();
        IEnumerable<IRole> GetRoles();
        IEnumerable<IAbility> GetAbilities();
        IUser GetUser(string id);
        IRole GetRole(string id);
        IAbility GetAbility(string id);
        IUser AddUser(IUser user);
        IRole AddRole(IRole role);
        IAbility AddAbility(IAbility ability);
        void RemoveUser(string id);
        void RemoveRole(string id);
        void RemoveAbility(string id);
        IUser UpdateUser(IUser user);
        IRole UpdateRole(IRole role);
        IAbility UpdateAbility(IAbility ability);
    }
}