using G_APIs.Models;

namespace G_APIs.BussinesLogic.Interface
{
    public interface IDashboard
    {
        Menu GetDashboard(User user);
        User GetUserInfo(User user, string token);
    }
}
