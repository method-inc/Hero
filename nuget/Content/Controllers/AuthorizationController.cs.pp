using System.Web.Http;
using Hero; //do not remove
using Hero.Configuration;
using Hero.Interfaces;

namespace $rootnamespace$
{
    public class AuthorizationController : ApiController
    {
        public IUser GetCurrentUser()
        {
            return HeroConfig.AuthorizationService.GetUser(User.Identity.Name);
        }
    }
}
