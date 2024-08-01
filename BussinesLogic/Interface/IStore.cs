using G_APIs.Models;
using System.Threading.Tasks;

namespace G_APIs.BussinesLogic.Interface
{
    public interface IStore
    {
        Task<double> GetOnlinePrice();
        Task<double> GetOnlineBuyPrice(PriceCalcVM priceCalc, string token);
        Task<ApiResult> PerformBuy(BuyPerformVM buyVM, string token);
    }
}
