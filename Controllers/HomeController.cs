using G_APIs.BussinesLogic.Interface;
using G_APIs.Models;
using G_APIs.Services;
using Newtonsoft.Json;
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
        private readonly IDashboard _dashboard;

        public HomeController(ISession session, IDashboard dashboard)
        {
            _session = session;
            _dashboard = dashboard;
        }

        [GoldUserInfo]
        [GoldAuthorize]
        public ActionResult Index(Dashboard model)
        {
            string userInfo = Request.Headers["UserInfo"];

            if (!string.IsNullOrEmpty(userInfo))
            {
                _session.Set("UserInfo", userInfo);
                return (ActionResult)View(model);
            }
            return RedirectToAction("Login", "Account");
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
            User user = _session.Get<User>("UserInfo");

            if (user != null)
                model = _dashboard.GetDashboard(user);
            else
                AlertMessaging.AddToUserQueue(new MessageContext("??????? ????? ???? ???", type: MessageType.Warning));

            return View(model);
        }

        [GoldAuthorize]
        public ActionResult Header(User model)
        {
            return View(model);
        }

    }
}
