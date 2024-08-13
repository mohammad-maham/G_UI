using G_APIs.Models;
using System.Threading.Tasks;

namespace G_APIs.BussinesLogic.Interface
{

    public interface IAccount
    {
        ApiResult  Login(User model);
        ApiResult  SetPassword(User model);
        ApiResult  SignUp(User model);
        ApiResult  CompleteProfile(User model, string token);
        ApiResult  SubmitContact(User model, string token);
        ApiResult  GetUserInfo(User model, string token);
        ApiResult  ForgotPassword(User model);
    }
}
