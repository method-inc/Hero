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
            bool isAuthenticated = httpContext.User.Identity.IsAuthenticated;
            bool isAuthorized = base.AuthorizeCore(httpContext);

            if (!isAuthorized)
            {
                return false;
            }

            isAuthorized = HeroConfig.AuthorizationService.Authorize(httpContext.User.Identity.Name, Ability);

            return isAuthenticated && isAuthorized;
        }
    }
}
