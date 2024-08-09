using G_APIs.BussinesLogic.Interface;
using G_APIs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
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
                model.UserId = user.UserId;

                if (user == null)
                    return View(new List<WalletCurrency> { new WalletCurrency { CurrencyName = "بروز خطا در دریافت اطلاعات" } });

                var res = _fund.GetWalletCurrency(model);

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

                var res = _fund.GetWalletCurrency(new Wallet { UserId = user.UserId })
                    .FirstOrDefault(x => x.CurrencyId == 1);

                res.Amount = 0;
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

                var res = _fund.GetBankAccounts(user).OrderBy(x => x.Id).ToList();

                return View(res);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [GoldAuthorize]
        public ActionResult GetTransactions()
        {
            try
            {
                var user = _session.Get<User>("UserInfo");

                if (user == null)
                    return View(new List<Transaction>());

                var res = _fund.GetTransactions(new Wallet { UserId = user.UserId }).OrderBy(x => x.Id).ToList();

                return View(res);
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPost]
        [GoldAuthorize]
        public ActionResult Deposit(WalletCurrency model)
        {
            try
            {
                var user = _session.Get<User>("UserInfo");

                if (user == null)
                    return Json(new { result = false, message = "بروز خطا :  لطفا دوباره وارد شوید." });

                var wc = _fund.GetWalletCurrency(new Wallet { UserId = user.UserId })
                    .Where(x => x.CurrencyId == 1).FirstOrDefault();

                if (wc == null)
                    return Json(new { result = false, message = "بروز خطا : کیف پول پیدا نشد." });


                wc.Amount = model.Amount;
                wc.WalletCurrencyId = 1;

                var res = _fund.Deposit(wc);

                if (res.StatusCode == 200)
                    return Json(new { result = true, message = res.Message });

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
                var user = _session.Get<User>("UserInfo");

                if (user == null)
                    return Json(new { result = false, message = "بروز خطا :  لطفا دوباره وارد شوید." });

                var wallet = _fund.GetWallet(new Wallet { UserId = user.UserId });
                model.BankAccountNumber = model.BankAccountNumber.Replace("-", "");
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
                if (model.Amount <= 0)
                    return Json(new { result = false, message = " لطفا مبلغ درخواستی را وارد نمایید." });

                var user = _session.Get<User>("UserInfo");

                if (user == null)
                    return View(new List<WalletCurrency> { new WalletCurrency { CurrencyName = "بروز خطا در دریافت اطلاعات" } });

                var wc = _fund.GetWalletCurrency(new Wallet { UserId = user.UserId })
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
