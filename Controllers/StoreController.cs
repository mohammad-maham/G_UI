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

            OrderVM orderVM = new OrderVM
            {
                CurrentOnlinePrice = buyPrice,
            };
            return View(orderVM);
        }

        [GoldAuthorize]
        public ActionResult SubmitBuy(OrderPerformVM buyVM)
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
                    buyVM.Carat = 750;
                    buyVM.GoldType = 2;
                    buyVM.SourceAmount = buyPrice;
                    buyVM.WalleId = walletCurrency.Id;
                    buyVM.UserId = userInfo.UserId.Value;
                    buyVM.DestinationAmount = buyVM.Weight;
                    buyVM.CurrentCalculatedPrice = buyPrice;
                    buyVM.SourceWalletCurrency = 1;
                    buyVM.DestinationWalletCurrency = 2;

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
                double transactionId = !string.IsNullOrEmpty(response.Data) ? long.Parse(response.Data) : 0;
                return View("OrderResult", transactionId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [GoldAuthorize]
        public ActionResult SellIndex()
        {
            string token = Request.Cookies["gldauth"].Value;
            User userInfo = _session.Get<User>("UserInfo");
            double buyPrice = _store.GetOnlineBuyPrice(new PriceCalcVM()
            {
                GoldCalcType = CalcTypes.sell,
                GoldWeight = 2
            }, token);

            OrderVM orderVM = new OrderVM
            {
                CurrentOnlinePrice = buyPrice,
            };
            return View(orderVM);
        }

        [GoldAuthorize]
        public ActionResult SubmitSell(OrderPerformVM sellVM)
        {
            ApiResult response = new ApiResult();
            User userInfo = _session.Get<User>("UserInfo");
            string token = Request.Headers["Authorization"];

            try
            {
                if (userInfo != null && !string.IsNullOrEmpty(token))
                {
                    // Business
                    double sellPrice = _store.GetOnlineBuyPrice(new PriceCalcVM()
                    {
                        GoldCalcType = CalcTypes.sell,
                        GoldWeight = sellVM.Weight
                    }, token);
                    WalletCurrency walletCurrency = _wallet.GetWallet(new Wallet() { UserId = userInfo.UserId });

                    // Binding
                    sellVM.Carat = 750;
                    sellVM.GoldType = 2;
                    sellVM.SourceAmount = sellVM.Weight;
                    sellVM.WalleId = walletCurrency.Id;
                    sellVM.UserId = userInfo.UserId.Value;
                    sellVM.DestinationAmount = sellPrice;
                    sellVM.CurrentCalculatedPrice = sellPrice;
                    sellVM.SourceWalletCurrency = 2;
                    sellVM.DestinationWalletCurrency = 1;

                    // Perform Sell
                    response = _store.PerformSell(sellVM, token);

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
                double transactionId = !string.IsNullOrEmpty(response.Data) ? long.Parse(response.Data) : 0;
                return View("OrderResult", transactionId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult GoldOnlinePrice(bool isBuy = true)
        {
            string token = Request.Cookies["gldauth"].Value;
            CalcTypes priceCalcType = isBuy ? CalcTypes.buy : CalcTypes.sell;
            double price = _store.GetOnlineBuyPrice(new PriceCalcVM()
            {
                GoldCalcType = priceCalcType,
                GoldWeight = 1
            }, token);
            return View(new OnlinePriceVM() { Price = price, CalcTypes = priceCalcType });
        }

        [GoldAuthorize]
        public double GetOnlinePrice(string weight = "1", int calcType = 1)
        {
            try
            {
                string token = Request.Cookies["gldauth"].Value;
                double buyPrice = _store.GetOnlineBuyPrice(new PriceCalcVM()
                {
                    GoldCalcType = calcType == 1 ? CalcTypes.buy : CalcTypes.sell,
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