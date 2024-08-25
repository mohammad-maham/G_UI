using G_APIs.BussinesLogic.Interface;
using G_APIs.Models;
using System.Web.Mvc;

namespace G_APIs.Controllers
{
    public class SettingsController : Controller
    {
        private readonly IStore _store;
        private readonly IFund _wallet;
        private readonly ISession _session;
        private readonly IAccount _account;

        public SettingsController(IStore store, ISession session, IFund wallet, IAccount account)
        {
            _store = store;
            _session = session;
            _wallet = wallet;
            _account = account;
        }

        [HttpGet]
        [GoldAuthorize]
        public ActionResult ThresholdPriceManagementIndex(ThresholdsVM thresholds)
        {
            string token = Request.Cookies["gldauth"].Value;
            User userInfo = _session.Get<User>("UserInfo");
            double currentOnlinePrice = _store.GetOnlineBuyPrice(new PriceCalcVM()
            {
                GoldCalcType = CalcTypes.none,
                GoldWeight = 1
            }, token);

            double thresholdPrice = _store.GetOnlineBuyPrice(new PriceCalcVM()
            {
                GoldCalcType = CalcTypes.threshold,
                GoldWeight = 1
            }, token);

            thresholds.ThresholdCurrentPrice = thresholdPrice;
            thresholds.GoldOnlinePrice = currentOnlinePrice;

            return View(thresholds);
        }
    }
}