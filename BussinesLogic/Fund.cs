using G_APIs.BussinesLogic.Interface;
using G_APIs.Models;
using G_APIs.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using static G_APIs.Common.Enums;

namespace G_APIs.BussinesLogic
{

    public class Fund : IFund
    {
        public async Task<WalletCurrency> GetWallet(Wallet model)
        {
            var res = await new GoldApi(GoldHost.Wallet, "/api/Fund/GetWallet", model).PostAsync();
            var w = JsonConvert.DeserializeObject<WalletCurrency>(res.Data);

            return w;
        }

        public List<WalletCurrency>  GetWalletCurrency(Wallet model)
        {
            var res =   new GoldApi(GoldHost.Wallet, "/api/Fund/GetWalletCurrency",model).Post();
            
            var w = JsonConvert.DeserializeObject<List<WalletCurrency>>(res.Data);

            return w;
        }

        public async Task<List<WalletCurrency>> GetWalletCurrencyAsync(Wallet model)
        {
            var res = await    new GoldApi(GoldHost.Wallet, "/api/Fund/GetWalletCurrency", model).PostAsync();

            var w = JsonConvert.DeserializeObject<List<WalletCurrency>>(res.Data);

            return w;
        }

        public async Task<ApiResult> Deposit(WalletCurrency model)
        {
            var res = await new GoldApi(GoldHost.Wallet, "/api/Fund/Deposit", model).PostAsync();
            return res;
        }

        public async Task<ApiResult> Windrow(WalletCurrency model)
        {
            var res = await new GoldApi(GoldHost.Wallet, "/api/Fund/Windrow", model).PostAsync();
            return res;
        }

        public async Task<ApiResult> Exchange(Xchenger model)
        {
            var res = await new GoldApi(GoldHost.Wallet, "/api/Fund/ExChange", model).PostAsync();
            return res;
        }


    }
}
