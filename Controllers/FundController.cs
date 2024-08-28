﻿using G_APIs.BussinesLogic.Interface;
using G_APIs.Model;
using G_APIs.Models;
using G_APIs.Services;
using Microsoft.Owin.Security.Provider;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using static G_APIs.Common.Enums;
namespace G_APIs.Controllers
{

    public class FundController : Controller
    {
        private readonly ISession _session;
        private readonly IFund _fund;

        public FundController(IFund fund, ISession session = null)
        {
            _fund = fund;
            _session = session;
        }

        [GoldAuthorize]
        public ActionResult BankAccount()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {
                throw;
            }

        }

        [GoldAuthorize]
        public ActionResult Wallet(Wallet model)
        {
            try
            {
                var user = _session.Get<User>("UserInfo");
                model.UserId = user.Id;

                if (user == null)
                    return View(new List<WalletCurrency> { new WalletCurrency { CurrencyName = "بروز خطا در دریافت اطلاعات" } });

                var res = _fund.GetWalletCurrency(model).OrderBy(x => x.CurrencyId).ToList();

                ViewBag.ShowButtons = model.ShowButtons;
                return View(res);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [GoldAuthorize]
        public ActionResult Deposit()
        {
            try
            {
                var user = _session.Get<User>("UserInfo");

                if (user == null)
                    return View(new WalletCurrency());

                var res = _fund.GetWalletCurrency(new Wallet { UserId = user.Id })
                    .FirstOrDefault(x => x.CurrencyId == 1);

                ViewBag.BankCards = _fund.GetBankAccounts(new Wallet { UserId = user.Id }).OrderBy(x => x.Id).ToList();

                res.Amount = 100000;
                return View(res);
            }
            catch (Exception)
            {
                throw;
            }
        }


        [GoldAuthorize]
        public ActionResult GetBankAccounts()
        {
            try
            {
                var user = _session.Get<User>("UserInfo");

                if (user == null)
                    return View(new List<WalletBankAccount> { new WalletBankAccount { Shaba = "بروز خطا در دریافت اطلاعات" } });

                var res = _fund.GetBankAccounts(new Wallet { UserId = user.Id }).OrderBy(x => x.Id).ToList();

                return View(res);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [GoldAuthorize]
        public ActionResult WindrowReport()
        {
            return View();
        }

        [GoldAuthorize]
        public ActionResult DepositReport()
        {
            return View();
        }

        [GoldAuthorize]
        public ActionResult FinanceMinister()
        {
            return View();
        }

        [GoldAuthorize]
        public ActionResult FinanceMinisterReport(FilterVM model)
        {
            try
            {
                var user = _session.Get<User>("UserInfo");

                if (user == null)
                    return View(new List<FinancialVM>());

                var res = _fund.GetFinancialReport(model).OrderBy(x => x.Id).ToList();

                return View(res);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [GoldAuthorize]
        public ActionResult ExchangeReport(FilterVM model)
        {
            try
            {
                var user = _session.Get<User>("UserInfo");

                if (user == null)
                    return View(new List<Xchenger>());

                var res = _fund.GetExchanges(model).OrderBy(x => x.Id).ToList();

                return View(res);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [GoldAuthorize]
        public ActionResult GetTransactions(FilterVM model)
        {
            try
            {
                var user = _session.Get<User>("UserInfo");

                if (user == null)
                    return View(new List<ReportVM>());

                model.UserId = (int)user.Id;

                if (model.FromDate != null)
                    model.FromDate = DateTime.Parse(model.FromDate, new CultureInfo("fa-IR")).ToString("yyyy-MM-ddT00:00:00");

                if (model.ToDate!= null)
                    model.ToDate = DateTime.Parse(model.ToDate, new CultureInfo("fa-IR")).ToString("yyyy-MM-ddT23:59:59");

                var res = _fund.GetTransactions(model).OrderBy(x => x.Id).ToList();

                return View(res);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [GoldAuthorize]
        public ActionResult Deposit(string amount, string description)
        {
            try
            {

                var er = ValidationHelper.Validate(ModelState);
                if (!string.IsNullOrEmpty(er))
                    return Json(new { result = false, message = er });

                //if (String.IsNullOrEmpty(model.BankCard))
                //    return Json(new { result = false, message = "بروز خطا : لطفا شماره کارت را وارد نمایید." });

                var user = _session.Get<User>("UserInfo");

                if (user == null)
                    return Json(new { result = false, message = "بروز خطا :  لطفا دوباره وارد شوید." });

                var model = new PaymentLinkRequest();
                var factorHeader = new FactorHeader
                {
                    CustomerId = (long)user.Id,
                    CustomerMobile = model.ClientMobile = user.Mobile,
                    CutomerName = model.ClientMobile = user.Name,
                    FactorTitle = model.Title = "واریز پول",
                    CreateDate = DateTime.Now,
                };

                var factorItems = new List<FactorItem>
                 {
                    new FactorItem
                    {
                        ItemTitle="واریز پول",
                        ItemUnitPrice=long.Parse(amount),
                        ItemUnitType="ریال",
                        ItemCount=1,
                        ItemDiscount=0,
                    }
                 };

                var wc = _fund.GetWallet(new Wallet { UserId = user.Id });

                model.UserId = user.Id;
                model.WalletId = wc.WalletId;
                model.WallectCurrencyId = wc.WalletCurrencyId;
                model.Price = factorItems.Sum(x => x.ItemUnitPrice * x.ItemCount);
                model.ExpDate = DateTime.Now.AddMinutes(15);
                model.OrderId = Guid.NewGuid().ToString().Substring(1, 10);

                var factorFooter = new FactorFooter
                {
                    FactorSumPrice = factorItems.Sum(x => x.ItemUnitPrice),
                    SellerAddress = new List<string> { "تبریز ابوریحان" },
                    SellerName = "  مهام ",
                    SellerTelephone = new List<string> { "04135520000" },
                    FactorVAT = 1

                };
                model.FactorData = new FactorDataModel
                {
                    Header = factorHeader,
                    Items = factorItems,
                    Footer = factorFooter
                };

                //model.CallBackURL = ConfigurationManager.AppSettings["CallBackURL"];
                //return  Json(JsonConvert.SerializeObject(model));
                var res = _fund.Deposit(model);

                if (res.StatusCode == 200)
                    return Json(new { result = true, message = res.Message, data = res.Data });

                return Json(new { result = false, message = res.Message });

            }
            catch (Exception)
            {
                throw;
            }
        }

        [GoldAuthorize]
        public ActionResult ToggleBankCard(int id)
        {
            try
            {
                var user = _session.Get<User>("UserInfo");

                if (user == null)
                    return View(new List<WalletBankAccount> { new WalletBankAccount { Shaba = "بروز خطا در دریافت اطلاعات" } });

                var res = _fund.ToggleBankCard(new WalletBankAccount { Id = id });

                return View("BankAccount");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [GoldAuthorize]
        public ActionResult AddBankAccount(WalletBankAccount model)
        {
            try
            {
                var er = ValidationHelper.Validate(ModelState);
                if (!string.IsNullOrEmpty(er))
                    return Json(new { result = false, message = er });

                var user = _session.Get<User>("UserInfo");

                if (user == null)
                    return Json(new { result = false, message = "بروز خطا :  لطفا دوباره وارد شوید." });

                var wallet = _fund.GetWallet(new Wallet { UserId = user.Id });
                model.BankAccountNumber = model.BankAccountNumber.Replace("-", "").Replace("_", "");

                model.WalletId = wallet.Id;

                var res = _fund.AddBankAccount(model);

                if (res != null)
                {

                    if (res.StatusCode != 200)
                        return Json(new { result = false, message = res.Message });

                    if (res.StatusCode == 200)
                        return Json(new { result = true, message = res.Message });

                }

                return Json(new { result = false, message = "بروز خطا لطفا دوباره تلاش کنید." });

            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult DepoResult()
        {
            return View();

        }

        [GoldAuthorize]
        public ActionResult Windrow()
        {
            try
            {
                var user = _session.Get<User>("UserInfo");

                if (user == null)
                    return View(new List<WalletCurrency> { new WalletCurrency { CurrencyName = "بروز خطا در دریافت اطلاعات" } });

                return View(new WalletCurrency());
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [GoldAuthorize]
        public ActionResult Windrow(WalletCurrency model)
        {
            try
            {
                var er = ValidationHelper.Validate(ModelState);
                if (!string.IsNullOrEmpty(er))
                    return Json(new { result = false, message = er });

                if (model.Amount <= 0)
                    return Json(new { result = false, message = " لطفا مبلغ درخواستی را وارد نمایید." });

                var user = _session.Get<User>("UserInfo");

                if (user == null)
                    return View(new List<WalletCurrency> { new WalletCurrency { CurrencyName = "بروز خطا در دریافت اطلاعات" } });

                var wc = _fund.GetWalletCurrency(new Wallet { UserId = user.Id })
                  .Where(x => x.CurrencyId == 1).FirstOrDefault();

                if (wc == null)
                    return Json(new { result = false, message = "بروز خطا : کیف پول پیدا نشد." });


                if (wc.Amount < model.Amount)
                    return Json(new { result = false, message = "بروز خطا :مقدار درخواستی بیش از موجودی میباشد." });

                wc.Amount = model.Amount;
                var res = _fund.Windrow(wc);

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
                return Json(new
                {
                    result = false,
                    message = ex.Message
                });
            }
        }

    }
}
