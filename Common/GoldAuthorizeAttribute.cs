
using G_APIs.Models;
using G_APIs.Services;
using System;
using System.Web;
using System.Web.Mvc;
using static G_APIs.Common.Enums;

public class GoldAuthorizeAttribute : AuthorizeAttribute
{
    protected override bool AuthorizeCore(HttpContextBase httpContext)
    {
        try
        {
            HttpRequestBase request = httpContext.Request;
            if (request.Cookies["gldauth"] != null)
            {
                string token = request.Cookies["gldauth"].Value.Replace("Bearer ", "");
                ApiResult res = new GoldApi(GoldHost.Accounting, "/api/Attributes/GetAuthorize", new { Token = token }).Post();
                return res.StatusCode == 200;
            }
            return false;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}

