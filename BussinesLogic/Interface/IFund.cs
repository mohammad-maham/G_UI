using G_APIs.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace G_APIs.BussinesLogic.Interface
{

    public interface IFund
    {
        WalletCurrency GetWallet(Wallet model);
        Task<List<WalletCurrency>> GetWalletCurrencyAsync(Wallet model);
        List<WalletCurrency> GetWalletCurrency(Wallet model);
        Task<ApiResult> Windrow(WalletCurrency model);
        Task<ApiResult> Deposit(WalletCurrency model);
        Task<ApiResult> Exchange(Xchenger model);
    }
}
