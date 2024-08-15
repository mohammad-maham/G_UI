using G_APIs.BussinesLogic.Interface;
using G_APIs.Models;
using G_APIs.Services;
using Newtonsoft.Json;
using System;
using static G_APIs.Common.Enums;

namespace G_APIs.BussinesLogic
{
    public class Dashboard : IDashboard
    {
        public Menu GetDashboard(User user)
        {
            Menu menu = new Menu();
            try
            {
                user.UserId = user.Id;
                ApiResult response = new GoldApi(GoldHost.Accounting, "/api/Dashboard/GetDashboard", user).Post();
                if (response != null && response.StatusCode == 200 && !string.IsNullOrEmpty(response.Data))
                {
                    menu = JsonConvert.DeserializeObject<Menu>(response.Data);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return menu;
        }

        public User GetUserInfo(User user, string token)
        {
            User userResponse = new User();
            try
            {
                user.UserId = user.Id;
                ApiResult response = new GoldApi(GoldHost.Accounting, "/api/User/GetUserInfo", user, authorization: token).Post();
                if (response != null && response.StatusCode == 200 && !string.IsNullOrEmpty(response.Data))
                {
                    userResponse = JsonConvert.DeserializeObject<User>(response.Data);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return userResponse;
        }
    }
}
