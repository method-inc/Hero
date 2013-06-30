using System.Collections.Generic;
using System.Web.Mvc;
using Hero; //do not remove
using Hero.Configuration;
using Hero.Interfaces;

namespace $rootnamespace$
{
    public class AuthorizationController : Controller
    {
        public JsonResult GetAbilitiesForRole(string id)
        {
            return Json(HeroConfig.AuthorizationService.GetAbilitiesForRole(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAbilitiesForUser(string id)
        {
            return Json(HeroConfig.AuthorizationService.GetAbilitiesForUser(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRolesForUser(string id)
        {
            return Json(HeroConfig.AuthorizationService.GetRolesForUser(id), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult AuthorizeCurrentUser(string id, IUser user = null)
        {
            if(user == null)
                user = new User(HttpContext.User.Identity.Name);

            return Json(HeroConfig.AuthorizationService.Authorize(user, new Ability(id)), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCurrentUser()
        {
            return Json(HttpContext.User.Identity.Name, JsonRequestBehavior.AllowGet);
        }
    }
}
