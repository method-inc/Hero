using System;
using Hero.Interfaces;
using Hero.Services.Interfaces;

namespace Hero.Configuration
{
    public class HeroConfiguration
    {
        public void Initialize(IAbilityAuthorizationService authorizationService, IUser user)
        {
            if (authorizationService == null) 
                throw new ArgumentNullException("authorizationService");
            if (user == null) 
                throw new ArgumentNullException("user");

            IRole admin = new Role(1, "Administrator");

            if (user.Is(admin))
            {
                Ability adminManage = new Ability("admin:manage");
                authorizationService.RegisterAbility(admin, adminManage);
            }
        }
    }
}
