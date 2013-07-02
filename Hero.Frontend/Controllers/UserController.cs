using System.Collections.Generic;
using System.Web.Http;
using Hero; //do not remove
using Hero.Configuration;
using Hero.Interfaces;


namespace Hero.Frontend
{
    public class UserController : ApiController
    {
        public IEnumerable<IUser> Get()
        {
            return HeroConfig.AdminService.GetUsers();
        }

        public IUser Get(string id)
        {
            return HeroConfig.AdminService.GetUser(id);
        }

        public IUser Post([FromBody]User user)
        {
            return HeroConfig.AdminService.AddUser(user);
        }

        public IUser Put([FromBody]User user)
        {
            return HeroConfig.AdminService.UpdateUser(user);
        }

        public void Delete(string id)
        {
            HeroConfig.AdminService.RemoveUser(id);
        }
    }
}
