using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using DotNetStandard.Cache;
using Hero.Configuration;

namespace Hero.Attributes
{
    public class TokenWebApiAuthorizationAttribute : AuthorizeAttribute
    {
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

        public virtual bool AuthorizeRequest(HttpRequestMessage request)
        {
            bool isAuthenticated = false;
            bool isAuthorized = false;

            if (!request.Headers.Contains("Authorization-Token"))
            {
                return false;
            }

            string token = request.Headers.GetValues("Authorization-Token").FirstOrDefault();

            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            isAuthenticated = TokenCache.Instance.Validate(token);

            User user = TokenCache.Instance.GetCachedObject(token) as User;

            if (user == null)
                return false;

            foreach (string role in RolesSplit)
            {
                if (HeroConfig.AuthorizationService.GetRolesForUser(user).Any(r => r.Name == role))
                {
                    isAuthorized = true;
                }
            }

            return isAuthenticated && (isAuthorized || !RolesSplit.Any());
        }

        protected string[] RolesSplit
        {
            get { return SplitStrings(Roles); }
        }

        protected string[] UsersSplit
        {
            get { return SplitStrings(Users); }
        }

        protected static string[] SplitStrings(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return new string[0];
            var result = input.Split(',')
                .Where(s => !String.IsNullOrWhiteSpace(s.Trim()));
            return result.Select(s => s.Trim()).ToArray();
        }
    }
}
