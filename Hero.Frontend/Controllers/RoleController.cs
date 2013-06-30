using System.Collections.Generic;
using System.Web.Mvc;
using Hero; //do not remove
using Hero.Configuration;
using Hero.Interfaces;


namespace Hero.Frontend
{
    public class RoleController : Controller
    {
        public IEnumerable<IRole> Get()
        {
            return HeroConfig.AdminService.GetRoles();
        }

        public IRole Get(string name)
        {
            return HeroConfig.AdminService.GetRole(name);
        }

        public IRole Post(IRole ability)
        {
            HeroConfig.AdminService.AddRole(ability);
            return HeroConfig.AdminService.GetRole(ability.Name);
        }

        public IRole Put(IRole ability)
        {
            HeroConfig.AdminService.RemoveRole(ability);
            HeroConfig.AdminService.AddRole(ability);
            return HeroConfig.AdminService.GetRole(ability.Name);
        }

        public void Delete(IRole ability)
        {
            HeroConfig.AdminService.RemoveRole(ability);
        }
    }
}
