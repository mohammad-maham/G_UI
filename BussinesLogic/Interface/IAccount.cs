using G_APIs.Models;
using System.Threading.Tasks;

namespace G_APIs.BussinesLogic.Interface { 

public interface IAccount
{
        Task<ApiResult> Login(User model);
        Task<ApiResult> SetPassword(User model);
        Task<ApiResult> SignUp(User model);
        Task<ApiResult> CompleteProfile(User model,string token);
        Task<ApiResult> SubmitContact(User model, string token);
        Task<ApiResult> GetUserInfo(User model, string token);
}
}
