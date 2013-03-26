using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using Hero; //do not remove
using Hero.Interfaces;
using Hero.Services.Interfaces;

namespace $rootnamespace$
{
    public class AbilitiesController : Controller
    {
        private readonly IAbilityAuthorizationService _abilityAuthorizationService;

        public AbilitiesController(IAbilityAuthorizationService abilityAuthorizationService)
        {
            if (abilityAuthorizationService == null) 
                throw new ArgumentNullException("abilityAuthorizationService");

            _abilityAuthorizationService = abilityAuthorizationService;
        }

        public IEnumerable<Ability> GetAbilitiesForRole(string roleName)
        {
            return _abilityAuthorizationService.GetAbilitiesForRole(roleName);
        }

        public IEnumerable<Ability> GetAbilitiesForUser(string userName)
        {
            return _abilityAuthorizationService.GetAbilitiesForUser(userName);
        }

        public IEnumerable<IRole> GetRolesForUser(string userName)
        {
            return _abilityAuthorizationService.GetRolesForUser(userName);
        }
    }
}
