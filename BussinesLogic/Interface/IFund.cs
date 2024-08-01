using G_APIs.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace G_APIs.BussinesLogic.Interface
{

    public interface IFund
    {
        Task<WalletCurrency> GetWallet(int userId);
        Task<List<WalletCurrency>> GetWalletCurrency(int userId);
        Task<ApiResult> Windrow(WalletCurrency model);
        Task<ApiResult> Deposit(WalletCurrency model);
        Task<ApiResult> Exchange(Xchenger model);
    }
}
