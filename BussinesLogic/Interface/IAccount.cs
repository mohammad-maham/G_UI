using G_APIs.Models;
using System.Collections.Generic;

namespace G_APIs.BussinesLogic.Interface
{
    public interface IAccount
    {
        ApiResult Login(User model);
        ApiResult SetPassword(User model);
        ApiResult SignUp(User model);
        ApiResult CompleteProfile(User model, string token);
        ApiResult SubmitContact(User model, string token);
        ApiResult GetUserInfo(User model, string token);
        ApiResult ForgotPassword(User model);
        List<GetUsersVM> GetUsers(string token);
        List<UsersList> GetUsersList(UsersReportFilterVM users, string token);
        List<UserRole> GetUserRoles(string token);
    }
}