using G_APIs.BussinesLogic.Interface;
using G_APIs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<ActionResult> BuyIndex()
        {
            string token = Request.Cookies["gldauth"].Value;
            User userInfo = _session.Get<User>("UserInfo");
            double buyPrice = await _store.GetOnlineBuyPrice(new PriceCalcVM()
            {
                GoldCalcType = CalcTypes.buy,
                GoldWeight = 1
            }, token);

            List<WalletCurrency> lstUserWalletCurr = await _wallet.GetWalletCurrencyAsync(new Wallet() { UserId = userInfo.UserId });
            double currencyAmount = lstUserWalletCurr.FirstOrDefault(x => x.CurrencyId == 1)?.Amount ?? 0.0;
            double goldAmount = lstUserWalletCurr.FirstOrDefault(x => x.CurrencyId == 2)?.Amount ?? 0.0;

            BuyVM buyVM = new BuyVM
            {
                CurrentOnlinePrice = buyPrice,
                GoldAmount = goldAmount,
                CurrencyAmount = currencyAmount,
            };
            return View(buyVM);
        }

        [GoldAuthorize]
        public async Task<ActionResult> SubmitBuy(BuyPerformVM buyVM)
        {
            ApiResult response = new ApiResult();
            User userInfo = _session.Get<User>("UserInfo");
            string token = Request.Headers["Authorization"];

            try
            {
                if (userInfo != null && !string.IsNullOrEmpty(token))
                {
                    // Business
                    double buyPrice = await _store.GetOnlineBuyPrice(new PriceCalcVM()
                    {
                        GoldCalcType = CalcTypes.buy,
                        GoldWeight = buyVM.Weight
                    }, token);
                    WalletCurrency walletCurrency = await _wallet.GetWallet(new Wallet() { UserId = userInfo.UserId });

                    // Binding
                    buyVM.WalleId = walletCurrency.Id;
                    buyVM.UserId = userInfo.UserId.Value;
                    buyVM.CurrentCalculatedPrice = buyPrice;

                    // Perform Buy
                    response = await _store.PerformBuy(buyVM, token);

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

        [GoldAuthorize]
        public async Task<double> GetOnlinePrice(string weight = "1")
        {
            try
            {
                string token = Request.Cookies["gldauth"].Value;
                double buyPrice = await _store.GetOnlineBuyPrice(new PriceCalcVM()
                {
                    GoldCalcType = CalcTypes.buy,
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