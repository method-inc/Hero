using System.Collections.Generic;

namespace Hero.Services.Interfaces
{
    public interface IAbilityAuthorizationService
    {
        bool Authorize(Role role, Ability ability);
        bool Authorize(string userName, string abilityName);
        bool Authorize(User user, Ability ability);
        IEnumerable<Ability> GetAbilitiesForUser(User user);
        IEnumerable<Ability> GetAbilitiesForUser(string userName);

        IEnumerable<User> GetUsers();
        IEnumerable<Role> GetRoles();
        IEnumerable<Ability> GetAbilities();
        User GetUser(string id);
        Role GetRole(string id);
        Ability GetAbility(string id);
        User AddUser(User user);
        Role AddRole(Role role);
        Ability AddAbility(Ability ability);
        void RemoveUser(string id);
        void RemoveRole(string id);
        void RemoveAbility(string id);
        void RemoveUser(User user);
        void RemoveRole(Role role);
        void RemoveAbility(Ability ability);
        User UpdateUser(User user);
        Role UpdateRole(Role role);
        Ability UpdateAbility(Ability ability);
    }
}