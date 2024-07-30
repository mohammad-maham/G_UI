using G_APIs.Models;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace G_APIs.Controllers
{
    public class HomeController : Controller
    {
  
        [GoldAuthorize]
        public ActionResult Index(Dashboard model)
        {
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
