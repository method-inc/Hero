using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Hero.Configuration;
using Hero.Services.Interfaces;
using System.Web.Mvc;
using AuthorizeAttribute = System.Web.Mvc.AuthorizeAttribute;

namespace Hero.Attributes
{
    public class AbilityMvcAuthorizationAttribute : AuthorizeAttribute
    {
        public string Ability { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool isAuthenticated = false;
            bool isAuthorized = base.AuthorizeCore(httpContext);

            if (!isAuthorized)
            {
                return false;
            }

            isAuthenticated = httpContext.User.Identity.IsAuthenticated;

            User user = new User(httpContext.User.Identity.Name.GetHashCode(), httpContext.User.Identity.Name);
            isAuthorized = HeroConfig.AuthorizationService.Authorize(user, new Ability(Ability));

            return isAuthenticated && isAuthorized;
        }
    }
}
