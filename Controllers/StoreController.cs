using G_APIs.BussinesLogic.Interface;
using G_APIs.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace G_APIs.Controllers
{
    public class StoreController : Controller
    {
        private readonly IStore _store;
        private readonly ISession _session;
        // private readonly ILogger<StoreController> _logger;

        public StoreController(IStore store/*, ILogger<StoreController> logger*/, ISession session)
        {
            _store = store;
            _session = session;
            /*_logger = logger;*/
        }

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
        //[GoldUserInfo]
        public async Task<ActionResult> SubmitBuy(BuyPerformVM buyVM)
        {
            User user = _session.Get<User>("UserInfo");
            string token = Request.Headers["gldauth"];
            
            buyVM.UserId = user.Id.Value;

            if (token == null || !token.StartsWith("Bearer "))
            {
                return Json(new { result = false, message = "ورود غیر مجاز لطفا دوباره وارد شوید." });
            }
            long transactionId = await _store.PerformBuy(buyVM, token);
            return View(transactionId);
        }

        public ActionResult SellIndex()
        {
            return View();
        }

        public async Task<string> GetOnlinePrice()
        {
            double price = await _store.GetOnlinePrice();
            return price.ToString("N0");
        }
    }
}