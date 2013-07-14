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
            return HeroConfig.AuthorizationService.GetAbilities();
        }

        public IAbility Get(string id)
        {
            return HeroConfig.AuthorizationService.GetAbility(id);
        }

        public IAbility Post([FromBody]Ability ability)
        {
            return HeroConfig.AuthorizationService.AddAbility(ability);
        }

        public IAbility Put([FromBody]Ability ability)
        {
            return HeroConfig.AuthorizationService.UpdateAbility(ability);
        }

        public void Delete(string id)
        {
            HeroConfig.AuthorizationService.RemoveAbility(id);
        }
    }
}
