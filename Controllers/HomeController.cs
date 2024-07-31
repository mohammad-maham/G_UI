using G_APIs.BussinesLogic.Interface;
using G_APIs.Models;
using G_APIs.Services;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using static G_APIs.Common.Enums;

namespace G_APIs.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISession _session;

        public HomeController(ISession session)
        {
            _session = session;
        }

        [GoldUserInfo]
        [GoldAuthorize]
        public ActionResult Index(Dashboard model)
        {
            string userInfo = Request.Headers["UserInfo"];
            if (!string.IsNullOrEmpty(userInfo))
                _session.Set("UserInfo", userInfo);
            return (ActionResult)View(model);

        }

        [GoldAuthorize]
        public ActionResult Chart1()
        {
            return View(new Chart());
        }

        [GoldAuthorize]
        public ActionResult Chart2()
        {
            return View(new Chart());
        }

        [GoldAuthorize]
        public ActionResult Chart3()
        {
            return View(new Chart());
        }

        [GoldAuthorize]
        public ActionResult Sidebar(Menu model)
        {
            return View(model);
        }

        [GoldAuthorize]
        public ActionResult Header(User model)
        {
            return View(model);
        }

    }
}
