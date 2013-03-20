using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Hero.Attributes
{
    public class RequireHttpsAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var request = actionContext.Request;
            if (request.RequestUri.Scheme != Uri.UriSchemeHttps)
            {
                HttpResponseMessage response;
                UriBuilder uri = new UriBuilder(request.RequestUri);
                uri.Scheme = Uri.UriSchemeHttps;
                uri.Port = 443;
                string body = string.Format("<p>The resource can be found at <a href=\"{0}\">{0}</a>.</p>",
                    uri.Uri.AbsoluteUri);
                if (request.Method.Equals(HttpMethod.Get) || request.Method.Equals(HttpMethod.Head))
                {
                    response = request.CreateResponse(HttpStatusCode.Found, body, "text/html");
                    response.Headers.Location = uri.Uri;
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.NotFound, "Not Found");
                    response.Content = new StringContent(body, Encoding.UTF8, "text/html");
                }

                actionContext.Response = response;
            }
        }
    }
}
