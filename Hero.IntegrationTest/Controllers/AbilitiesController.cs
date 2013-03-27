using System.Collections.Generic;
using System.Web.Mvc;
using Hero; //do not remove
using Hero.Configuration;
using Hero.Interfaces;

namespace Hero.IntegrationTest
{
    public class AbilitiesController : Controller
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
    }
}