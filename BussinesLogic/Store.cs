using G_APIs.BussinesLogic.Interface;
using G_APIs.Models;
using G_APIs.Services;
using Newtonsoft.Json;
using System;
using static G_APIs.Common.Enums;

namespace G_APIs.BussinesLogic
{
    public class Store : IStore
    {
        public GoldRepositoryStatusVM GetGoldRepositoryStatus(string token)
        {
            string result = string.Empty;
            GoldRepositoryStatusVM repositoryStatusVM = new GoldRepositoryStatusVM();
            try
            {
                ApiResult response = new GoldApi(GoldHost.Store, "/api/Shopping/GetGoldRepositoryStatistics", new { }, authorization: token).Post();

                if (response != null && !string.IsNullOrEmpty(response.Data))
                {
                    result = response.Data;
                    repositoryStatusVM = JsonConvert.DeserializeObject<GoldRepositoryStatusVM>(result);
                }

                return repositoryStatusVM;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public GoldTypesVM GetGoldTypes(string token)
        {
            string result = string.Empty;
            GoldTypesVM goldTypesVM = new GoldTypesVM();
            try
            {
                ApiResult response = new GoldApi(GoldHost.Store, "/api/Shopping/GetGoldTypes", new { }, authorization: token).Post();

                if (response != null && !string.IsNullOrEmpty(response.Data))
                {
                    result = response.Data;
                    goldTypesVM = JsonConvert.DeserializeObject<GoldTypesVM>(result);
                }

                return goldTypesVM;
            }
            catch (Exception)
            {
                throw;
            }
        }

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