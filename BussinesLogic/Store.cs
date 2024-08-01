using G_APIs.BussinesLogic.Interface;
using G_APIs.Models;
using G_APIs.Services;
using System.Threading.Tasks;
using static G_APIs.Common.Enums;

namespace G_APIs.BussinesLogic
{
    public class Store : IStore
    {
        public async Task<double> GetOnlineBuyPrice(PriceCalcVM priceCalc, string token)
        {
            ApiResult response = await new GoldApi(GoldHost.Store, "/api/Shopping/GetPrices", priceCalc, authorization: token).PostAsync();
            string result = response.Data;
            return !string.IsNullOrEmpty(result) ? double.Parse(result) : 0;
        }

        public async Task<double> GetOnlinePrice()
        {
            ApiResult response = await new GoldApi(GoldHost.Gateway, "/api/Prices/GetGoldOnlinePrice", new { }).PostAsync();
            string result = response.Data;
            return !string.IsNullOrEmpty(result) ? double.Parse(result) : 0;
        }

        public async Task<ApiResult> PerformBuy(BuyPerformVM buyVM, string token)
        {
            ApiResult response = await new GoldApi(GoldHost.Store, "/api/Shopping/Buy", buyVM, authorization: token).PostAsync();
            return response;
        }
    }
}