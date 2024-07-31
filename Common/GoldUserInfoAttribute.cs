
using G_APIs.BussinesLogic.Interface;
using G_APIs.Models;
using G_APIs.Services;
using System;
using System.Configuration;
using System.Web.Http.Filters;
using System.Web.Mvc;
using static G_APIs.Common.Enums;
using IActionFilter = System.Web.Mvc.IActionFilter;

public class GoldUserInfoAttribute : Attribute, IActionFilter
{
    private readonly string authEndpoint = ConfigurationManager.AppSettings["Accounting"] + "/api/Attributes/GetAuthorize";
    private ISession _session;
    public GoldUserInfoAttribute(ISession session)
    {
        _session = session;
    }
    public GoldUserInfoAttribute()
    {
            
    }

    public void OnActionExecuted(ActionExecutedContext filterContext)
    {
        return;
    }

    public void OnActionExecuting(ActionExecutingContext filterContext)
    {
        try
        {
            /* if (httpContext.User.Identity.IsAuthenticated)
                 return true;*/
            if (filterContext.HttpContext.Request.Cookies["gldauth"] != null)
            {
                string token = filterContext.HttpContext.Request.Cookies["gldauth"].Value.Replace("Bearer ", "");
                G_APIs.Models.ApiResult res = new GoldApi(GoldHost.Accounting, "/api/Attributes/GetUserInfo", new { Token = token }).Post();
                _session.Set("UserInfo", res.Data);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

}

