using System.Collections.Generic;
using System.Web.Http;
using Hero; //do not remove
using Hero.Configuration;
using Hero.Interfaces;


namespace $rootnamespace$
{
    public class AbilityController : ApiController
    {
        public IEnumerable<IAbility> Get()
        {
            return HeroConfig.AdminService.GetAbilities();
        }

        public IAbility Get(string id)
        {
            return HeroConfig.AdminService.GetAbility(id);
        }

        public IAbility Post([FromBody]Ability user)
        {
            return HeroConfig.AdminService.AddAbility(user);
        }

        public IAbility Put([FromBody]Ability user)
        {
            return HeroConfig.AdminService.UpdateAbility(user);
        }

        public void Delete(string id)
        {
            HeroConfig.AdminService.RemoveAbility(id);
        }
    }
}
