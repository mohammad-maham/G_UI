using G_APIs.Models;

namespace G_APIs.BussinesLogic.Interface
{
    public interface IStore
    {
        double GetOnlinePrice();
        double GetOnlineBuyPrice(PriceCalcVM priceCalc, string token);
        ApiResult PerformBuy(BuyPerformVM buyVM, string token);
    }
}
