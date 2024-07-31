using G_APIs.Models;
using G_APIs.Services;
using System;
using System.Web;
using System.Web.Mvc;
using static G_APIs.Common.Enums;

public class GoldUserInfoAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        try
        {
            HttpRequestBase request = filterContext.HttpContext.Request;
            if (request.Cookies["gldauth"] != null)
            {
                string token = request.Cookies["gldauth"].Value.Replace("Bearer ", "");
                ApiResult res = new GoldApi(GoldHost.Accounting, "/api/Attributes/GetUserInfo", new { Token = token }).Post();
                if (res != null && !string.IsNullOrEmpty(res.Data))
                {
                    request.Headers["UserInfo"] = res.Data;
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
}

