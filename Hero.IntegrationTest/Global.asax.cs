using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Hero.Attributes;
using Hero.Configuration;
using Hero.Interfaces;
using Hero.Services;
using Hero.Services.Interfaces;

namespace Hero.IntegrationTest
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            //Hero initialization
            IAbilityAuthorizationService service = new AbilityAuthorizationService();

            HeroConfig.Initialize(service);

            //setup users, roles, and abilities
            IRole toDoBasicRole = new Role("ToDoBasic");
            IRole toDoAdminRole = new Role("ToDoAdmin");
            IUser toDoBasicUser = new User("ToDoBasicUser");
            IUser toDoAdminUser = new User("ToDoAdminUser");
            Ability toDoViewAbility = new Ability("View");
            Ability toDoCreateAbility = new Ability("Create");
            Ability toDoDeleteAbility = new Ability("Delete");
            Ability toDoEditAbility = new Ability("Edit");
            Ability manageAbility = new Ability("Manage", new[]{toDoCreateAbility, toDoEditAbility, toDoDeleteAbility, toDoViewAbility});

            //abilitites
            HeroConfig.RegisterAbilities(service, toDoBasicRole, new[] { toDoViewAbility });
            HeroConfig.RegisterAbilities(service, toDoAdminRole, new[] { manageAbility });

            //roles
            HeroConfig.RegisterRoles(service, toDoBasicUser, new[] { toDoBasicRole });
            HeroConfig.RegisterRoles(service, toDoAdminUser, new[] { toDoAdminRole });
        }
    }
}