using G_APIs.Models;
using System.Threading.Tasks;

namespace G_APIs.BussinesLogic.Interface { 

public interface IFund
{
    Task<ApiResult> GetWallet(WalletCurrency model);

}
}
