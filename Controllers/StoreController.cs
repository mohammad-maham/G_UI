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

        [GoldAuthorize]
        public async Task<ActionResult> BuyIndex()
        {
            BuyVM buyVM = new BuyVM
            {
                CurrentCalculatedPrice = await _store.GetOnlinePrice(),
                Carat = new List<SelectListItem>() {
                new SelectListItem() { Text="18 عیار",Value="750"},
                new SelectListItem() { Text="22 عیار",Value="900"},
                new SelectListItem() { Text="24 عیار",Value="1000"}
            }
            };
            return View(buyVM);
        }

        [GoldAuthorize]
        public async Task<ActionResult> SubmitBuy(BuyPerformVM buyVM)
        {
            string token = Request.Headers["Authorization"];
            User userInfo = _session.Get<User>("UserInfo");
            buyVM.UserId = userInfo.Id.Value;

            if (string.IsNullOrEmpty(token))
                return Json(new { result = false, message = "ورود غیر مجاز لطفا دوباره وارد شوید." });

            long transactionId = await _store.PerformBuy(buyVM, token);
            return View(transactionId);
        }

        public ActionResult SellIndex()
        {
            return View();
        }

        [GoldAuthorize]
        public async Task<double> GetOnlinePrice()
        {
            try
            {
                double price = await _store.GetOnlinePrice();
                return price;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}