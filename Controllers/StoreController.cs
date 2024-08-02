using G_APIs.BussinesLogic.Interface;
using G_APIs.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace G_APIs.Controllers
{
    public class StoreController : Controller
    {
        private readonly IStore _store;
        private readonly ISession _session;

        public StoreController(IStore store, ISession session)
        {
            _store = store;
            _session = session;
        }

        [HttpGet]
        [GoldAuthorize]
        public async Task<ActionResult> BuyIndex()
        {
            string token = Request.Cookies["gldauth"].Value;
            double buyPrice = await _store.GetOnlineBuyPrice(new PriceCalcVM()
            {
                GoldCalcType = CalcTypes.buy,
                GoldWeight = 1
            }, token);

            BuyVM buyVM = new BuyVM
            {
                CurrentOnlinePrice = buyPrice,
                /*Carat = new List<SelectListItem>() {
                new SelectListItem() { Text="18 عیار",Value="750"},
                new SelectListItem() { Text="22 عیار",Value="900"},
                new SelectListItem() { Text="24 عیار",Value="1000"}
            }*/
            };
            return View(buyVM);
        }

        [GoldAuthorize]
        public async Task<ActionResult> SubmitBuy(BuyPerformVM buyVM)
        {
            ApiResult response = new ApiResult();
            string token = Request.Headers["Authorization"];
            User userInfo = _session.Get<User>("UserInfo");
            try
            {
                if (userInfo != null)
                {
                    buyVM.UserId = userInfo.Id.Value;

                    if (string.IsNullOrEmpty(token))
                    {
                        return Json(new { result = false, message = "ورود غیر مجاز لطفا دوباره وارد شوید." });
                    }

                    double buyPrice = await _store.GetOnlineBuyPrice(new PriceCalcVM()
                    {
                        GoldCalcType = CalcTypes.buy,
                        GoldWeight = buyVM.Weight
                    }, token);

                    buyVM.CurrentCalculatedPrice = buyPrice;
                    buyVM.WalleId = 4;
                    response = await _store.PerformBuy(buyVM, token);

                    if (response.StatusCode != 200)
                    {
                        return Json(new { result = false, message = response.Message });
                    }
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