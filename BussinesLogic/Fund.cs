using G_APIs.BussinesLogic.Interface;
using G_APIs.Models;
using G_APIs.Services;
using System.Threading.Tasks;
using static G_APIs.Common.Enums;

namespace G_APIs.BussinesLogic
{

    public class Fund : IFund
    {
        public Fund()
        {
        }

        public async Task<ApiResult> GetWallet(WalletCurrency model)
        {

            var res = await new GoldApi(GoldHost.Wallet, "/api/Fund/GetWalletCurrency", model).PostAsync();

            return res;
        }
    }
}
