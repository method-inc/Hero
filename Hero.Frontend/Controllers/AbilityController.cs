using System.Collections.Generic;
using System.Web.Http;
using Hero; //do not remove
using Hero.Configuration;
using Hero.Interfaces;


namespace Hero.Frontend
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

        public IAbility Post([FromBody]Ability ability)
        {
            return HeroConfig.AdminService.AddAbility(ability);
        }

        public IAbility Put([FromBody]Ability ability)
        {
            return HeroConfig.AdminService.UpdateAbility(ability);
        }

        public void Delete(string id)
        {
            HeroConfig.AdminService.RemoveAbility(id);
        }
    }
}
