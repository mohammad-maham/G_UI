using G_APIs.BussinesLogic.Interface;
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
                return View(new WalletBankAccount());
            }
            catch (Exception)
            {
                throw;
            }

        }

        [GoldAuthorize]
        public ActionResult GetWallet()
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
                    return View(new List<WalletCurrency> { new WalletCurrency { CurrencyName = "بروز خطا در دریافت اطلاعات کاربر" } });

                WalletCurrency wallet = _fund.GetWallet(new Wallet { UserId = user.Id });
                if (wallet == null)
                    return View(new List<WalletCurrency> { new WalletCurrency { CurrencyName = "بروز خطا در دریافت   کیف پول" } });


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

                ViewBag.BankCards = _fund.GetBankAccounts(new Wallet { UserId = user.Id })
                    .Where(x=>x.Status==1).OrderBy(x => x.Id).ToList();

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

        public ActionResult ExchangeReport()
        {
            return View();
        }

        [GoldAuthorize]
        public ActionResult FinanceMinisterReport()
        {
            return View();
        }

        [GoldAuthorize]
        public ActionResult AdminFinance()
        {
            return View();
        }

        [GoldAuthorize]
        public ActionResult GetFinances(FilterVM model)
        {
            try
            {
                var user = _session.Get<User>("UserInfo");

                if (user == null)
                    return View(new List<ReportVM>());

                if (model.FromDate != null)
                    model.FromDate = DateTime.Parse(model.FromDate, new CultureInfo("fa-IR")).ToString("yyyy-MM-ddT00:00:00");

                if (model.ToDate != null)
                    model.ToDate = DateTime.Parse(model.ToDate, new CultureInfo("fa-IR")).ToString("yyyy-MM-ddT23:59:59");

                model.UserId = (int)user.Id;
                var res = _fund.GetFinancialReport(model).OrderBy(x => x.Id).ToList();

                return View(res);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [GoldAuthorize]
        public ActionResult AdminFinanceReport(FilterVM model)
        {
            try
            {
                var user = _session.Get<User>("UserInfo");

                if (user == null)
                    return View();

                if (model.FromDate != null)
                    model.FromDate = DateTime.Parse(model.FromDate, new CultureInfo("fa-IR")).ToString("yyyy-MM-ddT00:00:00");

                if (model.ToDate != null)
                    model.ToDate = DateTime.Parse(model.ToDate, new CultureInfo("fa-IR")).ToString("yyyy-MM-ddT23:59:59");

                var res = _fund.GetFinancialReport(model)
                    .Where(x => x.ConfirmationUserId == 0 && x.TransactionTypeId == (short)TransactionType.Windrow)
                    .OrderBy(x => x.Id).ToList();

                return View(res);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [GoldAuthorize]
        public ActionResult GetExchanges(FilterVM model)
        {
            try
            {
                var user = _session.Get<User>("UserInfo");

                if (user == null)
                    return View(new List<ReportVM>());

                model.UserId = (int)user.Id;

                if (model.FromDate != null)
                    model.FromDate = DateTime.Parse(model.FromDate, new CultureInfo("fa-IR")).ToString("yyyy-MM-ddT00:00:00");

                if (model.ToDate != null)
                    model.ToDate = DateTime.Parse(model.ToDate, new CultureInfo("fa-IR")).ToString("yyyy-MM-ddT23:59:59");

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
        public ActionResult ConfirmTransactions(TransactionVM model)
        {
            try
            {
                var user = _session.Get<User>("UserInfo");

                if (user == null)
                    return View(new List<ReportVM>());

                model.ConfirmationUserId = (int)user.Id;
                model.Amount = (-1 * model.Amount);
                model.TransactionInfo = JsonConvert.SerializeObject(new { TrackingCode = model.TrackingCode });
                var res = _fund.ConfirmTransaction(model);

                if (res.StatusCode == 200)
                    return Json(new { result = true, message = res.Message, data = res.Data });

                return Json(new { result = false, message = res.Message });

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

                if (model.ToDate != null)
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
        public ActionResult Deposit(WalletCurrency m)
        {
            try
            {

                var er = ValidationHelper.Validate(ModelState);
                if (!string.IsNullOrEmpty(er))
                    return Json(new { result = false, message = er });

                string token = Request.Cookies["gldauth"].Value;
                User user = _session.Get<User>("UserInfo");

                if (user == null)
                    return Json(new { result = false, message = "بروز خطا :  لطفا دوباره وارد شوید." });

                var wc = _fund.GetWalletCurrency(new Wallet { UserId = user.Id }).FirstOrDefault(x => x.CurrencyId == 1);
                var t = new TransactionVM
                {
                    TransactionModeId = (short)TransactionMode.Online,
                    WalletCurrencyId = wc.CurrencyId,
                    WalletId = wc.WalletId,
                    TransactionTypeId = (short)TransactionType.Deposit,
                    Amount = (decimal)m.Amount,
                    RequestDescription=m.Description
                };

                var resTrans = _fund.AddTransaction(t);
                if (resTrans == null)
                    return Json(new { result = false, message = "بروز خطا در ثبت تراکنش " });

                var trans = JsonConvert.DeserializeObject<TransactionVM>(resTrans.Data);

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
                        ItemUnitPrice=(long) m.Amount,
                        ItemUnitType="ریال",
                        ItemCount=1,
                        ItemDiscount=0,
                    }
                 };

                model.UserId = user.Id;
                model.WalletId = wc.WalletId;
                model.WallectCurrencyId = wc.CurrencyId;
                model.Price = factorItems.Sum(x => x.ItemUnitPrice * x.ItemCount);
                model.ExpDate = DateTime.Now.AddMinutes(15);
                model.OrderId = Guid.NewGuid().ToString().Substring(1, 10);
                model.TransactionConfirmId = trans.TransactionConfirmId;

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

                var res = _fund.Deposit(model,token);

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

                return RedirectToAction("BankAccount");
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
                model.Name = model.BankName;
                model.WalletId = wallet.Id;
                model.Name = model.BankName;
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

                ViewBag.BankCards = _fund.GetBankAccounts(new Wallet { UserId = user.Id })
                   .Where(x => x.Status == 1).OrderBy(x => x.Id).ToList();

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


                if (wc.Amount < (double)model.Amount)
                    return Json(new { result = false, message = "بروز خطا :مقدار درخواستی بیش از موجودی میباشد." });

                var t = new TransactionVM
                {
                    TransactionModeId = (short)TransactionMode.Online,
                    WalletCurrencyId = wc.CurrencyId,
                    WalletId = wc.WalletId,
                    TransactionTypeId = (short)TransactionType.Windrow,
                    Amount = (decimal)model.Amount,
                    RequestDescription = model.Description,
                    Info=JsonConvert.SerializeObject(new TransInfo { BankCard=model.BankCard})
                };

                var res = _fund.AddTransaction(t);

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
