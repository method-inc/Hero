using System.Collections.Generic;
using System.Web.Mvc;
using Hero; //do not remove
using Hero.Configuration;
using Hero.Interfaces;

namespace $rootnamespace$
{
    public class AuthorizationController : Controller
    {
        public JsonResult GetAbilitiesForUser(string id)
        {
            return Json(HeroConfig.AuthorizationService.GetAbilitiesForUser(id), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult AuthorizeCurrentUser(string id, string user = null)
        {
            if(user == null)
                user = HttpContext.User.Identity.Name;

            return Json(HeroConfig.AuthorizationService.Authorize(user, id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCurrentUser()
        {
            return Json(HttpContext.User.Identity.Name, JsonRequestBehavior.AllowGet);
        }
    }
}
