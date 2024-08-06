using G_APIs.BussinesLogic.Interface;
using G_APIs.Models;
using System;
using System.Web.Mvc;

namespace G_APIs.Controllers
{
    public class StoreController : Controller
    {
        private readonly IStore _store;
        private readonly IFund _wallet;
        private readonly ISession _session;

        public StoreController(IStore store, ISession session, IFund wallet)
        {
            _store = store;
            _session = session;
            _wallet = wallet;
        }

        [HttpGet]
        [GoldAuthorize]
        public ActionResult BuyIndex()
        {
            string token = Request.Cookies["gldauth"].Value;
            User userInfo = _session.Get<User>("UserInfo");
            double buyPrice = _store.GetOnlineBuyPrice(new PriceCalcVM()
            {
                GoldCalcType = CalcTypes.buy,
                GoldWeight = 1
            }, token);

            BuyVM buyVM = new BuyVM
            {
                CurrentOnlinePrice = buyPrice,
            };
            return View(buyVM);
        }

        [GoldAuthorize]
        public ActionResult SubmitBuy(BuyPerformVM buyVM)
        {
            ApiResult response = new ApiResult();
            User userInfo = _session.Get<User>("UserInfo");
            string token = Request.Headers["Authorization"];

            try
            {
                if (userInfo != null && !string.IsNullOrEmpty(token))
                {
                    // Business
                    double buyPrice = _store.GetOnlineBuyPrice(new PriceCalcVM()
                    {
                        GoldCalcType = CalcTypes.buy,
                        GoldWeight = buyVM.Weight
                    }, token);
                    WalletCurrency walletCurrency = _wallet.GetWallet(new Wallet() { UserId = userInfo.UserId });

                    // Binding
                    buyVM.WalleId = walletCurrency.Id;
                    buyVM.UserId = userInfo.UserId.Value;
                    buyVM.CurrentCalculatedPrice = buyPrice;

                    // Perform Buy
                    response = _store.PerformBuy(buyVM, token);

                    // Response Reaction
                    if (response.StatusCode != 200)
                    {
                        return Json(new { result = false, message = response.Message });
                    }
                }
                else
                {
                    return Json(new { result = false, message = "ورود غیر مجاز لطفا دوباره وارد شوید." });
                }
                return View(!string.IsNullOrEmpty(response.Data) ? long.Parse(response.Data) : 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult SellIndex()
        {
            return View();
        }

        public ActionResult GoldOnlinePrice(bool isBuy = true)
        {
            string token = Request.Cookies["gldauth"].Value;
            double price = _store.GetOnlineBuyPrice(new PriceCalcVM()
            {
                GoldCalcType = isBuy ? CalcTypes.buy : CalcTypes.sell,
                GoldWeight = 1
            }, token);
            return View(price);
        }

        [GoldAuthorize]
        public double GetOnlinePrice(string weight = "1", bool isBuy = true)
        {
            try
            {
                string token = Request.Cookies["gldauth"].Value;
                double buyPrice = _store.GetOnlineBuyPrice(new PriceCalcVM()
                {
                    GoldCalcType = isBuy ? CalcTypes.buy : CalcTypes.sell,
                    GoldWeight = double.Parse(weight)
                }, token);
                return buyPrice;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}