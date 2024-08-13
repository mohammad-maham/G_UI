using G_APIs.BussinesLogic.Interface;
using G_APIs.Models;
using G_APIs.Services;
using System.Threading.Tasks;
using static G_APIs.Common.Enums;

namespace G_APIs.BussinesLogic
{

    public class Account : IAccount
    {

        public Account()
        {
        }

        public  ApiResult Login(User model)
        {

            var res =   new GoldApi(GoldHost.Accounting, "/api/User/SignIn", model).Post();

            //var user = JsonConvert.DeserializeObject<User>(res.Message!);

            return res;
        }

        public  ApiResult SignUp(User model)
        {

            var res =   new GoldApi(GoldHost.Accounting, "/api/User/SignUp", model).Post();

            //var user = JsonConvert.DeserializeObject<User>(res.Data);

            return res;
        }

        public  ApiResult SetPassword(User model)
        {
            var res =   new GoldApi(GoldHost.Accounting, "/api/User/SetPassword", model).Post();

            return res;
        }

        public  ApiResult CompleteProfile(User model, string token)
        {
            var res =   new GoldApi(GoldHost.Accounting, "/api/User/CompleteProfile", model, authorization: token).Post();

            return res;
        }

        public  ApiResult SubmitContact(User model, string token)
        {
            var res =   new GoldApi(GoldHost.Accounting, "/api/User/SubmitContact", model, authorization: token).Post();

            return res;
        }

        public  ApiResult GetUserInfo(User model, string token)
        {
            var res =   new GoldApi(GoldHost.Accounting, "/api/User/GetUserInfo", model, authorization: token).Post();

            return res;
        }

        public  ApiResult ForgotPassword(User model)
        {
            ApiResult res =   new GoldApi(GoldHost.Accounting, "/api/User/ForgotPassword", model).Post();
            return res;
        }
    }
}
