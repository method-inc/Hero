using System.Collections.Generic;
using System.Web.Mvc;
using Hero; //do not remove
using Hero.Configuration;
using Hero.Interfaces;

namespace Hero.Frontend
{
    public class AbilitiesController : Controller
    {
        public IEnumerable<Ability> GetAbilitiesForRole(string roleName)
        {
            return HeroConfig.AuthorizationService.GetAbilitiesForRole(roleName);
        }

        public IEnumerable<Ability> GetAbilitiesForUser(string userName)
        {
            return HeroConfig.AuthorizationService.GetAbilitiesForUser(userName);
        }

        public IEnumerable<IRole> GetRolesForUser(string userName)
        {
            return HeroConfig.AuthorizationService.GetRolesForUser(userName);
        }
    }
}