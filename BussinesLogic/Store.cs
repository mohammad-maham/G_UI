using G_APIs.BussinesLogic.Interface;
using G_APIs.Models;
using G_APIs.Services;
using System;
using static G_APIs.Common.Enums;

namespace G_APIs.BussinesLogic
{
    public class Store : IStore
    {
        public double GetOnlineBuyPrice(PriceCalcVM priceCalc, string token)
        {
            try
            {
                ApiResult response = new GoldApi(GoldHost.Store, "/api/Shopping/GetPrices", priceCalc, authorization: token).Post();
                string result = response.Data;
                return !string.IsNullOrEmpty(result) ? double.Parse(result) : 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public double GetOnlinePrice()
        {
            try
            {
                ApiResult response = new GoldApi(GoldHost.Gateway, "/api/Prices/GetGoldOnlinePrice", new { }).Post();
                string result = response.Data;
                return !string.IsNullOrEmpty(result) ? double.Parse(result) : 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ApiResult PerformBuy(OrderPerformVM buyVM, string token)
        {
            try
            {
                ApiResult response = new GoldApi(GoldHost.Store, "/api/Shopping/Buy", buyVM, authorization: token).Post();
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ApiResult PerformSell(OrderPerformVM sellVM, string token)
        {
            try
            {
                ApiResult response = new GoldApi(GoldHost.Store, "/api/Shopping/Sell", sellVM, authorization: token).Post();
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}