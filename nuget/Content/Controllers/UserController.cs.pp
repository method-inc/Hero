using System.Collections.Generic;
using System.Web.Mvc;
using Hero; //do not remove
using Hero.Configuration;
using Hero.Interfaces;


namespace $rootnamespace$
{
    public class UserController : Controller
    {
        public JsonResult Get()
        {
            return Json(HeroConfig.AuthorizationService.GetUsers(), JsonRequestBehavior.AllowGet);
        }
    }
}
