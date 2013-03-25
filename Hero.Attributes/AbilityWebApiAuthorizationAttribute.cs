using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Hero.Configuration;
using Hero.Services;

namespace Hero.Attributes
{
    public class AbilityWebApiAuthorizationAttribute : AuthorizeAttribute
    {
        public string Ability { get; set; }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            
            if (actionContext == null)
                throw new ArgumentNullException("actionContext");

            if (AuthorizationDisabled(actionContext)
                || AuthorizeRequest(actionContext.ControllerContext.Request))
                return;

            HandleUnauthorizedRequest(actionContext);
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            
            if (actionContext == null)
                throw new ArgumentNullException("actionContext");

            actionContext.Response = CreateUnauthorizedResponse(actionContext.ControllerContext.Request);
        }

        private HttpResponseMessage CreateUnauthorizedResponse(HttpRequestMessage request)
        {
            
            var result = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Unauthorized,
                RequestMessage = request
            };

            return result;
        }

        private static bool AuthorizationDisabled(HttpActionContext actionContext)
        {
            
            //support new AllowAnonymousAttribute
            if (!actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
            {
                return actionContext.ControllerContext
                    .ControllerDescriptor
                    .GetCustomAttributes<AllowAnonymousAttribute>().Any();
            }

            return true;
        }

        public bool AuthorizeRequest(HttpRequestMessage request)
        {
            
            bool isAuthenticated = false;
            bool isAuthorized = false;

            var currentUser = HttpContext.Current.User.Identity;

            if (currentUser == null)
                return false;

            isAuthenticated = currentUser.IsAuthenticated;

            User user = new User(currentUser.Name, currentUser.Name);
            isAuthorized = HeroConfig.AuthorizationService.Authorize(user, new Ability(Ability));

            return isAuthenticated && isAuthorized;
        }
    }
}
