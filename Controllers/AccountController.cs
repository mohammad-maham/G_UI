﻿using G_APIs.BussinesLogic.Interface;
using G_APIs.Common;
using G_APIs.Models;
using G_APIs.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.Security;


namespace G_APIs.Controllers
{

    public class AccountController : Controller
    {
        private readonly IAccount _account;
        private readonly ISession _session;


        public AccountController(IAccount account, ISession session)
        {
            _account = account;
            _session = session;
        }

        public ActionResult Login()
        {

            return View(new User() { Captcha = GetCaptcha() });
        }

        public ActionResult Signup()
        {
            return View(new User() { Captcha = GetCaptcha() });
        }

        [GoldAuthorize]
        public ActionResult UserProfile()
        {
            User user = _session.Get<User>("UserInfo");
            string token = Request.Cookies["gldauth"].Value;
            ApiResult res = _account.GetUserInfo(user, token);
            User model = JsonConvert.DeserializeObject<User>(res.Data);

            //if (model.BirthDay != null)
            //    model.BirthDate = DateTime.Parse(model.BirthDay.ToString() , new CultureInfo("fa-IR")).ToString("yyyy/MM/dd");

            List<SelectListItem> genders = new List<SelectListItem>();
            genders.Add(new SelectListItem() { Text = "مرد", Value = "1" });
            genders.Add(new SelectListItem() { Text = "زن", Value = "0" });
            ViewBag.Genders = genders;
               
            return View(model);
        }

        [GoldAuthorize]

        public ActionResult AddressInfo(User model)
        {
            var token = Request.Cookies["auth"].ToString();
            var res = _account.GetUserInfo(model, token);
            var user = new User();

            if (res != null && res.StatusCode == 200 && res.Data != null)
                user = JsonConvert.DeserializeObject<User>(res.Data);

            return View(user);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [HttpPost]

        public ActionResult Login(User model)
        {
            try
            {

                var captcha = _session.Get<string>("Captcha");

                if (captcha != null && model.Captcha != null && model.Captcha != captcha)
                    return Json(new { result = false, message = "متن تصویر اشتباه است." });


                var res = _account.Login(model);

                if (res != null)
                {

                    if (res.StatusCode == 0)
                        return Json(new { result = false, message = res.Message });

                    if (res.StatusCode == 200 && res.Data != null)
                    {
                        //var user = new GenericPrincipal(new GenericIdentity(model.Username), new string[] { "Role1", "Role2" });
                        FormsAuthentication.SetAuthCookie(model.Name, false);

                        HttpCookie authCookie = new HttpCookie("gldauth", res.Data);
                        //authCookie.Expires = DateTime.Now.AddMinutes(5);
                        Response.Cookies.Add(authCookie);

                        return Json(new { result = true, data = res.Data });

                    }

                }

                return Json(new { result = false, message = res.Message });
            }
            catch (Exception ex)
            {

                return Json(new { result = false, message = ex.Message });
            }

        }


        [HttpPost]

        public ActionResult SignUp(User model)
        {
            try
            {
                var captcha = _session.Get<string>("Captcha");

                if (captcha != null && model.Captcha != null && model.Captcha != captcha)
                    return Json(new { result = false, message = "متن تصویر اشتباه است." });


                model.Mobile = model.Mobile.StartsWith("0") ? model.Mobile.Remove(0, 1) : model.Mobile;


                var res = _account.SignUp(model);

                if (res != null)
                {

                    if (res.StatusCode != 200)
                        return Json(new { result = false, message = res.Message });

                    if (res.StatusCode == 200 && res.Data != null)
                    {
                        var user = JsonConvert.DeserializeObject<User>(res.Data);

                        if (user.Id != null)
                            return Json(new { result = true, message = "لطفا کد ارسال شده به موبایل را وارد کنید", data = JsonConvert.SerializeObject(user) });

                    }

                }

                return Json(new { result = false, message = "بروز خطا لطفا دوباره تلاش کنید." });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    result = false,
                    message = ex.Message
                });
            }

        }

        [HttpPost]

        public ActionResult SetPassword(User model)
        {
            try
            {
                if (model.Password != model.PasswordRepeat)
                    return Json(new { result = false, message = "رمز عبور با تکرار آن مطابقت ندارد" });

                if (!string.IsNullOrEmpty(model.ForgotUsername))
                    model.NationalCode = model.ForgotUsername;

                ApiResult result = _account.SetPassword(model);

                if (result != null)
                    return Json(new { result = result.StatusCode = 200, message = result.Message });

                return Json(new { result = false, message = "بروز خطا، لطفا دوباره تلاش کنید." });
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message });
            }

        }

        public string GetCaptcha()
        {

            var strCaptcha = new Random().Next().ToString().Substring(0, 4);
            _session.Set("Captcha", strCaptcha);

            var fileName = Guid.NewGuid().ToString().Substring(0, 4);
            var savePath = Server.MapPath("~") + @"\Content\Captcha\" + fileName + ".png";
            new Captcha().CreateCaptch(strCaptcha).Save(savePath, System.Drawing.Imaging.ImageFormat.Png);

            return fileName + ".png";
        }



        [HttpPost]
        [GoldAuthorize]

        public ActionResult CompleteProfile([System.Web.Http.FromBody] User model)
        {
            try
            {
                string token = Request.Cookies["gldauth"].Value;
                if (token == null)
                    return Json(new { result = false, message = "ورود غیر مجاز لطفا دوباره وارد شوید." });

                User user = _session.Get<User>("UserInfo");

                //model.BirthDay = DateTime.Parse(model.BirthDate, new CultureInfo("fa-IR"));
                model.UserId = user.Id;

                var files=Request.Files;

                var uploadList = new UploadFile().Upload(files);
                model.NationalCardImage=JsonConvert.SerializeObject(uploadList);
                var res = _account.CompleteProfile(model, token);

                if (res != null)
                {

                    if (res.StatusCode != 200)
                        return Json(new { result = false, message = res.Message });

                    if (res.StatusCode == 200 && res.Data != null)
                        return Json(new { result = true, message = res.Message });


                }

                return Json(new { result = false, message = "بروز خطا لطفا دوباره تلاش کنید." });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    result = false,
                    message = ex.Message
                });
            }

        }

        [HttpPost]
        [GoldAuthorize]

        public ActionResult SubmitContact(User model)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();

                if (token == null || !token.StartsWith("Bearer "))
                {
                    //return Unauthorized("Missing or invalid Authorization header.");
                    return Json(new { result = false, message = "ورود غیر مجاز لطفا دوباره وارد شوید." });
                }


                var res = _account.SubmitContact(model, token);

                if (res != null)
                {

                    if (res.StatusCode != 200)
                        return Json(new { result = false, message = res.Message });

                    if (res.StatusCode == 200 && res.Data != null)
                        return Json(new { result = true, message = res.Message });


                }

                return Json(new { result = false, message = "بروز خطا لطفا دوباره تلاش کنید." });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    result = false,
                    message = ex.Message
                });
            }
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]

        public ActionResult ForgotPassword(User user)
        {
            try
            {
                if (user != null && !string.IsNullOrEmpty(user.ForgotUsername))
                {
                    user.Username = user.ForgotUsername;

                    ApiResult result = _account.ForgotPassword(user);
                    if (result != null)
                    {
                        return Json(new { result = result.StatusCode == 200, message = result.Message });
                    }
                }
                return Json(new { result = false, message = "بروز خطا، لطفا دوباره تلاش کنید." });
            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message });
            }
        }

        public ActionResult RedirectToErrorMiddleware(int error = 403)
        {
            PartialViewResult view = PartialView("");
            switch (error)
            {
                case 403:
                    view = PartialView("_AuthMiddleware");
                    break;
                default:
                    break;
            }
            return view;
        }

        #region UserManagement
        [GoldAuthorize]
        public ActionResult UsersManagementIndex(UsersList users)
        {
            List<UserRole> roles = new List<UserRole>();
            List<SelectListItem> lstRolesSelect = new List<SelectListItem>();
            string token = Request.Cookies["gldauth"].Value;

            roles = _account.GetUserRoles(token);

            if (roles.Count > 0)
            {
                foreach (UserRole role in roles)
                    lstRolesSelect.Add(new SelectListItem() { Text = role.Description, Value = role.Id.ToString() });
            }
            ViewBag.Roles = lstRolesSelect;
            return View(users);
        }

        public ActionResult UsersManagementIndexData(UsersList users)
        {
            UsersReportFilterVM filterVM = new UsersReportFilterVM();
            string token = Request.Headers["Authorization"].ToString();

            List<UsersList> usersLists = new List<UsersList>();
            if (users != null)
            {
                if (!string.IsNullOrEmpty(users.FromDate))
                {
                    string from = DateTime.Parse(users.FromDate, new CultureInfo("fa-IR")).ToString("yyyy-MM-ddT00:00:00Z");
                    users.FromRegDate = DateTime.Parse(from).ToUniversalTime();
                }
                if (!string.IsNullOrEmpty(users.ToDate))
                {
                    string to = DateTime.Parse(users.ToDate, new CultureInfo("fa-IR")).ToString("yyyy-MM-ddT23:59:59Z");
                    users.ToRegDate = DateTime.Parse(to).ToUniversalTime();
                }

                filterVM.RoleId = users.Roles;
                filterVM.FromRegDate = users.FromRegDate;
                filterVM.ToRegDate = users.ToRegDate;
                filterVM.NationalCode = users.NationalCode;
                usersLists = _account.GetUsersList(filterVM, token).OrderBy(x => x.RegDate).ToList();
            }
            return View(usersLists);
        }

        public ActionResult UpdateUserStatusIndex(long userId)
        {
            List<UserStatus> statuses = new List<UserStatus>();
            List<SelectListItem> lstStatusesSelect = new List<SelectListItem>();
            string token = Request.Cookies["gldauth"].Value;

            statuses = _account.GetUserStatuses(token);
            if (statuses.Count > 0)
            {
                foreach (UserStatus status in statuses)
                    lstStatusesSelect.Add(new SelectListItem() { Text = status.Caption, Value = status.Id.ToString() });
            }
            ViewBag.Statuses = lstStatusesSelect;
            return View();
        }

        [HttpPost]
        public ActionResult UpdateUserStatus(UsersList users)
        {
            string token = Request.Cookies["gldauth"].Value;
            if (users != null && users.UserId != 0 && users.Statuses != 0)
            {
                users.Status = users.Statuses.ToString();
                ApiResult result = _account.UpdateUserStatus(users, token);
                if (result != null)
                {
                    return Json(new { result = result.StatusCode, message = result.Message });
                }
            }
            return Json(new { result = false, message = "عملیات با مشکل مواجه شد" });
        }

        public ActionResult ChangeUserRoleIndex(long userId)
        {
            List<UserRole> roles = new List<UserRole>();
            List<SelectListItem> lstRolesSelect = new List<SelectListItem>();
            string token = Request.Cookies["gldauth"].Value;

            roles = _account.GetUserRoles(token);
            if (roles.Count > 0)
            {
                foreach (UserRole role in roles)
                    lstRolesSelect.Add(new SelectListItem() { Text = role.Description, Value = role.Id.ToString() });
            }
            ViewBag.Roles = lstRolesSelect;
            return View();
        }

        [HttpPost]
        public ActionResult ChangeUserRole(UsersList users)
        {
            string token = Request.Cookies["gldauth"].Value;
            if (users != null && users.UserId != 0 && users.Statuses != 0)
            {
                users.RoleId = users.Roles;
                ApiResult result = _account.ChangeUserRole(users, token);
                if (result != null)
                {
                    return Json(new { result = result.StatusCode, message = result.Message ?? "عملیات با مشکل مواجه شد" });
                }
            }
            return Json(new { result = false, message = "عملیات با مشکل مواجه شد" });
        }
        #endregion UserManagement
    }
}