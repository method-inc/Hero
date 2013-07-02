using System.Collections.Generic;
using System.Web.Http;
using Hero; //do not remove
using Hero.Configuration;
using Hero.Interfaces;

namespace Hero.Frontend
{
    public class RoleController : ApiController
    {
        public IEnumerable<IRole> Get()
        {
            return HeroConfig.AdminService.GetRoles();
        }

        public IRole Get(string id)
        {
            return HeroConfig.AdminService.GetRole(id);
        }

        public IRole Post([FromBody]Role user)
        {
            return HeroConfig.AdminService.AddRole(user);
        }

        public IRole Put([FromBody]Role user)
        {
            return HeroConfig.AdminService.UpdateRole(user);
        }

        public void Delete(string id)
        {
            HeroConfig.AdminService.RemoveRole(id);
        }
    }
}
