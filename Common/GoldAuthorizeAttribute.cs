
using G_APIs.Services;
using System;
using System.Configuration;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.WebPages;
using static G_APIs.Common.Enums;

public class GoldAuthorizeAttribute : AuthorizeAttribute
{
    private readonly string authEndpoint = ConfigurationManager.AppSettings["Accounting"] + "/api/Attributes/GetAuthorize";

    public GoldAuthorizeAttribute()
    {
    }

    protected override bool AuthorizeCore(HttpContextBase httpContext)
    {
        try
        {
            if (httpContext.User.Identity.IsAuthenticated)
                return true;

            if (httpContext.Request.Cookies["gldauth"] != null)
            {
                var token = httpContext.Request.Cookies["gldauth"].Value.Replace("Bearer ", "");

                var res = new GoldApi(GoldHost.Accounting, "/api/Attributes/GetAuthorize", new { Token = token }).PostAsync();

                 
                 return res.GetAwaiter().GetResult().StatusCode == 200 ? true : false;

            }

            return false;
        }
        catch (Exception ex)
        {
            throw;
        }

    }
}

