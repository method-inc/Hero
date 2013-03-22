using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hero.Attributes;
using Hero.Configuration;

namespace Hero.IntegrationTest.Controllers
{
    public class ToDoController : Controller
    {
        //
        // GET: /ToDo/

        [AbilityMvcAuthorization(Ability = "ToDoIndex")]
        public ActionResult Index()
        {
            return View();
        }

    }
}
