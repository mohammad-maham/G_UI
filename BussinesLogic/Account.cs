using G_APIs.BussinesLogic.Interface;
using G_APIs.Models;
using G_APIs.Services;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using static G_APIs.Common.Enums;

namespace G_APIs.BussinesLogic
{
    public class Account : IAccount
    {
        public ApiResult Login(User model)
        {
            ApiResult res = new GoldApi(GoldHost.Accounting, "/api/User/SignIn", model).Post();
            return res;
        }

        public ApiResult SignUp(User model)
        {
            ApiResult res = new GoldApi(GoldHost.Accounting, "/api/User/SignUp", model).Post();
            return res;
        }

        public ApiResult SetPassword(User model)
        {
            ApiResult res = new GoldApi(GoldHost.Accounting, "/api/User/SetPassword", model).Post();
            return res;
        }

        public ApiResult CompleteProfile(User model, string token)
        {
            ApiResult res = new GoldApi(GoldHost.Accounting, "/api/User/CompleteProfile", model, authorization: token).Post();
            return res;
        }

        public ApiResult SubmitContact(User model, string token)
        {
            ApiResult res = new GoldApi(GoldHost.Accounting, "/api/User/SubmitContact", model, authorization: token).Post();
            return res;
        }

        public ApiResult GetUserInfo(User model, string token)
        {
            ApiResult res = new GoldApi(GoldHost.Accounting, "/api/User/GetUserInfo", model, authorization: token).Post();
            return res;
        }

        public ApiResult ForgotPassword(User model)
        {
            ApiResult res = new GoldApi(GoldHost.Accounting, "/api/User/ForgotPassword", model).Post();
            return res;
        }

        public List<GetUsersVM> GetUsers(string token)
        {
            List<GetUsersVM> users = new List<GetUsersVM>();
            try
            {
                ApiResult response = new GoldApi(GoldHost.Accounting, "/api/User/GetUsers", new { }, authorization: token).Post();
                if (response != null && response.StatusCode == 200 && !string.IsNullOrEmpty(response.Data))
                {
                    users = JsonConvert.DeserializeObject<List<GetUsersVM>>(response.Data);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return users;
        }

        public List<UsersList> GetUsersList(UsersReportFilterVM users, string token)
        {
            List<UsersList> usersList = new List<UsersList>();
            try
            {
                ApiResult response = new GoldApi(GoldHost.Accounting, "/api/Reports/GetUsers", users, authorization: token).Post();
                if (response != null && response.StatusCode == 200 && !string.IsNullOrEmpty(response.Data))
                {
                    usersList = JsonConvert.DeserializeObject<List<UsersList>>(response.Data);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return usersList;
        }

        public List<UserRole> GetUserRoles(string token)
        {
            List<UserRole> userRoles = new List<UserRole>();
            try
            {
                ApiResult response = new GoldApi(GoldHost.Accounting, "/api/User/GetRoles", new { }, authorization: token).Post();
                if (response != null && response.StatusCode == 200 && !string.IsNullOrEmpty(response.Data))
                {
                    userRoles = JsonConvert.DeserializeObject<List<UserRole>>(response.Data);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return userRoles;
        }

        public ApiResult UpdateUserStatus(UsersList users, string token)
        {
            ApiResult response = new GoldApi(GoldHost.Accounting, "/api/User/UpdateUserStatus", users, authorization: token).Post();
            return response;
        }

        public ApiResult ChangeUserRole(UsersList users, string token)
        {
            ApiResult response = new GoldApi(GoldHost.Accounting, "/api/User/ChangeUserRole", users, authorization: token).Post();
            return response;
        }

        public List<UserStatus> GetUserStatuses(string token)
        {
            List<UserStatus> userStatuses = new List<UserStatus>();
            try
            {
                ApiResult response = new GoldApi(GoldHost.Accounting, "/api/User/GetStatuses", new { }, authorization: token).Post();
                if (response != null && response.StatusCode == 200 && !string.IsNullOrEmpty(response.Data))
                {
                    userStatuses = JsonConvert.DeserializeObject<List<UserStatus>>(response.Data);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return userStatuses;
        }

        public ApiResult UserInfo(string token)
        {
            ApiResult response = new GoldApi(GoldHost.Accounting, "/api/Attributes/GetUserInfo", new { token }, authorization: token).Post();
            return response;
        }
    }
}