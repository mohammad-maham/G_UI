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
        // private readonly ILogger<StoreController> _logger;

        public StoreController(IStore store/*, ILogger<StoreController> logger*/)
        {
            _store = store;
            /*_logger = logger;*/
        }

        public async Task<ActionResult> BuyIndex()
        {
            BuyVM buyVM = new BuyVM
            {
                CurrentCalculatedPrice = await _store.GetOnlinePrice(),
                Carat = new List<SelectListItem>() {
                new SelectListItem() { Text="18 عیار",Value="750",Selected=true},
                new SelectListItem() { Text="22 عیار",Value="900"},
                new SelectListItem() { Text="24 عیار",Value="1000"}
            }
            };
            return View(buyVM);
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