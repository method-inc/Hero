using System.Collections.Generic;
using Hero.Interfaces;

namespace Hero.Services.Interfaces
{
    public interface IAdminService
    {
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