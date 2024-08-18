using G_APIs.BussinesLogic.Interface;
using G_APIs.Models;
using G_APIs.Services;
using System.Web.Mvc;

namespace G_APIs.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISession _session;
        private readonly IDashboard _dashboard;
        private readonly IStore _store;

        public HomeController(ISession session, IDashboard dashboard, IStore store)
        {
            _session = session;
            _dashboard = dashboard;
            _store = store;
        }

        [GoldUserInfo]
        [GoldAuthorize]
        public ActionResult Index(Models.Dashboard model)
        {
            string userInfo = Request.Headers["UserInfo"];

            if (!string.IsNullOrEmpty(userInfo))
            {
                _session.Set("UserInfo", userInfo);
                return View(model);
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
            {
                model = _dashboard.GetDashboard(user);
            }
            else
            {
                AlertMessaging.AddToUserQueue(new MessageContext("اطلاعات کاربر یافت نشد", type: MessageType.Warning));
            }

            return View(model);
        }

        [GoldAuthorize]
        public ActionResult Header(User model)
        {
            string token = Request.Cookies["gldauth"].Value;
            User user = _session.Get<User>("UserInfo");
            if (user != null)
            {
                double buyPrice = _store.GetOnlineBuyPrice(new PriceCalcVM()
                {
                    GoldCalcType = CalcTypes.buy,
                    GoldWeight = 1
                }, token);

                model = _dashboard.GetUserInfo(user, token) ?? new User();
                model.BuyPrice = buyPrice;

                double sellPrice = _store.GetOnlineBuyPrice(new PriceCalcVM()
                {
                    GoldCalcType = CalcTypes.sell,
                    GoldWeight = 1
                }, token);

                model.SellPrice = sellPrice;
            }

            return View(model);
        }

    }
}
