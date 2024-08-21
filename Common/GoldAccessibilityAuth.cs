using G_APIs.Models;
using G_APIs.Services;
using Newtonsoft.Json;
using System.Web;
using System.Web.Mvc;
using static G_APIs.Common.Enums;

namespace G_APIs.Common
{
    public class GoldAccessibilityAuth : AuthorizeAttribute
    {
        public int UserStatusPermission { get; set; }

        public GoldAccessibilityAuth()
        {
            UserStatusPermission = 1;
        }

        public GoldAccessibilityAuth(int UserStatus)
        {
            UserStatusPermission = UserStatus;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            User userInfo = null;
            HttpCookie userToken = filterContext.HttpContext.Request.Cookies["gldauth"];
            string userInfoSession = filterContext.HttpContext.Session["UserInfo"] as string;

            if (!string.IsNullOrEmpty(userInfoSession))
            {
                userInfo = JsonConvert.DeserializeObject<User>(userInfoSession.ToString());
            }

            if (userToken != null && userInfo != null && userInfo.Status == UserStatusPermission)
            {
                string token = userToken.Value.Replace("Bearer ", "");
                ApiResult res = new GoldApi(GoldHost.Accounting, "/api/Attributes/GetAuthorize", new { Token = token }).Post();

                if (res.StatusCode != 200)
                {
                    filterContext.Result = new RedirectResult("/Account/Login");
                }
            }
            else if (userToken == null || userInfo == null)
            {
                filterContext.Result = new RedirectResult("/Account/Login");
            }
            else if (userInfo != null && userInfo.Status != UserStatusPermission)
            {
                filterContext.Result = new RedirectResult("/Account/RedirectToErrorMiddleware?error=403");
            }
        }
    }
}