﻿using G_APIs.BussinesLogic.Interface;
using G_APIs.Common;
using G_APIs.Models;
using GoldHelpers.Helpers;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Web;
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
        public ActionResult Profile(User model)
        {
            return View(model ?? new User());
        }

        [GoldAuthorize]
        public ActionResult BankAccount(BankAccount model)
        {
            return View(model ?? new BankAccount());
        }

        [GoldAuthorize]
        public async Task<ActionResult> AddressInfo(User model)
        {
            var token = Request.Cookies["auth"].ToString();
            var res = await _account.GetUserInfo(model, token);
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
        public async Task<ActionResult> Login(User model)
        {
            try
            {

                var captcha = _session.Get<string>("Captcha");

                if (captcha != null && model.Captcha != null && model.Captcha != captcha)
                    return Json(new { result = false, message = "متن تصویر اشتباه است." });

                var res = await _account.Login(model);

                if (res != null)
                {

                    if (res.StatusCode == 0)
                        return Json(new { result = false, message = res.Message });

                    if (res.StatusCode == 200 && res.Data != null)
                    {
                        //var user = new GenericPrincipal(new GenericIdentity(model.Username), new string[] { "Role1", "Role2" });
                        FormsAuthentication.SetAuthCookie(model.Name, false);

                        HttpCookie authCookie = new HttpCookie("gldauth", res.Data);
                        authCookie.Expires = DateTime.Now.AddMinutes(5);
                        Response.Cookies.Add(authCookie);

                        return Json(new { result = true, data = res.Data });

                    }

                }

                return Json(new { result = false, message = " رمز عبور یا نام کاربری اشتباه است " });
            }
            catch (Exception ex)
            {

                return Json(new { result = false, message = ex.Message });
            }

        }


        [HttpPost]
        public async Task<ActionResult> SignUp(User model)
        {
            try
            {
                var captcha = _session.Get<string>("Captcha");

                if (captcha != null && model.Captcha != null && model.Captcha != captcha)
                    return Json(new { result = false, message = "متن تصویر اشتباه است." });


                model.Mobile = model.Mobile.StartsWith("0") ? model.Mobile.Remove(0, 1) : model.Mobile;

                var res = await _account.SignUp(model);

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
        public async Task<ActionResult> SetPassword(User model)
        {
            try
            {
                model.Username = model.NationalCode;
                var res = await _account.SetPassword(model);

                if (res != null)
                {

                    if (res.StatusCode != 200)
                        return Json(new { result = false, message = res.Message });

                    if (res.StatusCode == 200)
                        return Json(new { result = true, message = res.Message });

                }

                return Json(new { result = false, message = "بروز خطا لطفا دوباره تلاش کنید." });
            }
            catch (Exception ex)
            {

                return Json(new { result = false, message = ex.Message });
            }

        }

        private string GetCaptcha()
        {

            var strCaptcha = new Random().Next().ToString().Substring(0, 4);
            _session.Set("Captcha", strCaptcha);

            var savePath = Server.MapPath("~") + @"\Content\Captcha\" + strCaptcha + ".png";
            new Captcha().CreateCaptch(strCaptcha).Save(savePath, System.Drawing.Imaging.ImageFormat.Png);

            return strCaptcha + ".png";
        }

        [HttpPost]
        [GoldAuthorize]
        public async Task<ActionResult> CompleteProfile(User model)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();

                if (token == null || !token.StartsWith("Bearer "))
                {
                    //return Unauthorized("Missing or invalid Authorization header.");
                    return Json(new { result = false, message = "ورود غیر مجاز لطفا دوباره وارد شوید." });
                }

                var res = await _account.CompleteProfile(model, token);

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
        public async Task<ActionResult> SubmitContact(User model)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();

                if (token == null || !token.StartsWith("Bearer "))
                {
                    //return Unauthorized("Missing or invalid Authorization header.");
                    return Json(new { result = false, message = "ورود غیر مجاز لطفا دوباره وارد شوید." });
                }

                var res = await _account.SubmitContact(model, token);

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
    }

}