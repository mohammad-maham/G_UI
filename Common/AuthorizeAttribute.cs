
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

public class AuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
{
    protected override bool AuthorizeCore(HttpContextBase httpContext)
    {
        // Implement your custom authorization logic here
        var isAuthorized = base.AuthorizeCore(httpContext);
        if (!isAuthorized)
        {
            return false;
        }

        // Additional custom logic
        var user = httpContext.User;
        return user.IsInRole("CustomRole");
    }

    protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
    {
        // Custom handling for unauthorized requests
        filterContext.Result = new RedirectToRouteResult(
            new RouteValueDictionary
            {
                { "controller", "Account" },
                { "action", "Login" }
            });
    }
}