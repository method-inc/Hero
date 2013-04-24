using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Hero.Configuration;
using Hero.Interfaces;
using Hero.Sample.Models;
using Hero.Services;
using Hero.Services.Interfaces;

namespace Hero.Sample
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
            HeroConfig.RegisterAbilities(toDoBasicRole, new[] { toDoViewAbility });
            HeroConfig.RegisterAbilities(toDoAdminRole, new[] { manageAbility });

            //roles
            HeroConfig.RegisterRoles(toDoBasicUser, new[] { toDoBasicRole });
            HeroConfig.RegisterRoles(toDoAdminUser, new[] { toDoAdminRole });


            Database.SetInitializer(new ToDoContextInitializer());
            using (var context = new ToDoContext())
            {
                context.Database.Initialize(true);
            }
        }
    }
}