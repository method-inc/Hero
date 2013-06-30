using System.Collections.Generic;
using System.Web.Mvc;
using Hero; //do not remove
using Hero.Configuration;
using Hero.Interfaces;


namespace Hero.Frontend
{
    public class AbilityController : Controller
    {
        public IEnumerable<Ability> Get()
        {
            return HeroConfig.AdminService.GetAbilities();
        }

        public Ability Get(string name)
        {
            return HeroConfig.AdminService.GetAbility(name);
        }

        public Ability Post(Ability ability)
        {
            HeroConfig.AdminService.AddAbility(ability);
            return HeroConfig.AdminService.GetAbility(ability.Name);
        }

        public Ability Put(Ability ability)
        {
            HeroConfig.AdminService.RemoveAbility(ability);
            HeroConfig.AdminService.AddAbility(ability);
            return HeroConfig.AdminService.GetAbility(ability.Name);
        }

        public void Delete(Ability ability)
        {
            HeroConfig.AdminService.RemoveAbility(ability);
        }
    }
}
