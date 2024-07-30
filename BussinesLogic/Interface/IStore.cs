using System.Threading.Tasks;

namespace G_APIs.BussinesLogic.Interface
{
    public interface IStore
    {
        Task<double> GetOnlinePrice();
    }
}
