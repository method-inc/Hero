using System.Collections.Generic;
using System.Web.Http;
using Hero; //do not remove
using Hero.Configuration;
using Hero.Interfaces;

namespace $rootnamespace$
{
    public class RoleController : ApiController
    {
        public IEnumerable<IRole> Get()
        {
            return HeroConfig.AuthorizationService.GetRoles();
        }

        public IRole Get(string id)
        {
            return HeroConfig.AuthorizationService.GetRole(id);
        }

        public IRole Post([FromBody]Role role)
        {
            return HeroConfig.AuthorizationService.AddRole(role);
        }

        public IRole Put([FromBody]Role role)
        {
            return HeroConfig.AuthorizationService.UpdateRole(role);
        }

        public void Delete(string id)
        {
            HeroConfig.AuthorizationService.RemoveRole(id);
        }
    }
}
