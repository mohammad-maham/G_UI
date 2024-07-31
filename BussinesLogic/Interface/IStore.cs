using G_APIs.Models;
using System.Threading.Tasks;

namespace G_APIs.BussinesLogic.Interface
{
    public interface IStore
    {
        Task<double> GetOnlinePrice();
        Task<long> PerformBuy(BuyPerformVM buyVM,string token);
    }
}
