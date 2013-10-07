using Hero.Interfaces;
using System.Collections.Generic;

namespace Hero.Services.Interfaces
{
    public interface IAbilityAuthorizationService
    {
        bool Authorize(IRole role, IAbility ability);
        bool Authorize(string userName, string abilityName);
        bool Authorize(IUser user, IAbility ability);
        IEnumerable<IAbility> GetAbilitiesForUser(IUser user);
        IEnumerable<IAbility> GetAbilitiesForUser(string userName);

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
        void RemoveUser(IUser user);
        void RemoveRole(IRole role);
        void RemoveAbility(IAbility ability);
        IUser UpdateUser<T>(IUser user) where T : class, IUser;
        IRole UpdateRole(IRole role);
        IAbility UpdateAbility(IAbility ability);
    }
}