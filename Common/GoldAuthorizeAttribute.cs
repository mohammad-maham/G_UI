
using G_APIs.Models;
using G_APIs.Services;
using System.Net;
using System.Web;
using System.Web.Mvc;
using static G_APIs.Common.Enums;

public class GoldAuthorizeAttribute : AuthorizeAttribute
{
    public override void OnAuthorization(AuthorizationContext filterContext)
    {
        HttpCookie userToken = filterContext.HttpContext.Request.Cookies["gldauth"];

        if (userToken != null)
        {
            string token = userToken.Value.Replace("Bearer ", "");
            ApiResult res = new GoldApi(GoldHost.Accounting, "/api/Attributes/GetAuthorize", new { Token = token }).Post();
            if (res.StatusCode != 200)
            {
                filterContext.Result = new RedirectResult("/Account/Login");
            }
        }
        else
        {
            filterContext.Result = new RedirectResult("/Account/Login");
        }
    }
}

