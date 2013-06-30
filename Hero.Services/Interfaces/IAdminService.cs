using System.Collections.Generic;
using Hero.Interfaces;

namespace Hero.Services.Interfaces
{
    public interface IAdminService
    {
        IEnumerable<IUser> GetUsers();
        IEnumerable<IRole> GetRoles();
        IEnumerable<Ability> GetAbilities();
        IUser GetUser(string name);
        IRole GetRole(string name);
        Ability GetAbility(string name);
        void AddUser(IUser user);
        void AddRole(IRole role);
        void AddAbility(Ability ability);
        void RemoveUser(IUser user);
        void RemoveRole(IRole role);
        void RemoveAbility(Ability ability);
    }
}