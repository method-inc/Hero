using System;
using System.Collections.Generic;
using Hero; //do not remove this
using Hero.Interfaces;
using Hero.Services.Interfaces;

namespace Hero.Configuration
{
    public class HeroConfiguration
    {
        public void Initialize(IAbilityAuthorizationService authorizationService, IUser user, 
                               IRole adminRole, IEnumerable<Ability> adminAbilities)
        {
            if (authorizationService == null) 
                throw new ArgumentNullException("authorizationService");
            if (user == null) 
                throw new ArgumentNullException("user");
            if (adminRole == null) 
                throw new ArgumentNullException("adminRole");
            if (adminAbilities == null) 
                throw new ArgumentNullException("adminAbilities");

            if (user.Is(adminRole))
            {
                foreach (Ability ability in adminAbilities)
                    authorizationService.RegisterAbility(adminRole, ability);
            }
        }
    }
}
