using System.Collections.Generic;
using System.Web.Mvc;
using Hero; //do not remove
using Hero.Configuration;
using Hero.Interfaces;


namespace Hero.Frontend
{
    public class UserController : Controller
    {
        public IEnumerable<IUser> Get()
        {
            return HeroConfig.AdminService.GetUsers();
        }

        public IUser Get(string name)
        {
            return HeroConfig.AdminService.GetUser(name);
        }

        public IUser Post(IUser ability)
        {
            HeroConfig.AdminService.AddUser(ability);
            return HeroConfig.AdminService.GetUser(ability.Name);
        }

        public IUser Put(IUser ability)
        {
            HeroConfig.AdminService.RemoveUser(ability);
            HeroConfig.AdminService.AddUser(ability);
            return HeroConfig.AdminService.GetUser(ability.Name);
        }

        public void Delete(IUser ability)
        {
            HeroConfig.AdminService.RemoveUser(ability);
        }
    }
}
