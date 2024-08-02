using G_APIs.BussinesLogic.Interface;
using G_APIs.Common;
using G_APIs.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
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
        public    ActionResult  Wallet(Wallet model)
        {
            try
            {
                var user = _session.Get<User>("UserInfo");
                model.UserId = user.Id;

                if (user == null)
                    return View(new List<WalletCurrency> { new WalletCurrency { CurrencyName = "بروز خطا در دریافت اطلاعات" } });

                var res =    _fund.GetWalletCurrency(model);

                return View(res);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [GoldAuthorize]
        public async Task<ActionResult> Deposit()
        {
            try
            {
                var user = _session.Get<User>("UserInfo");

                if (user == null)
                    return View(new List<WalletCurrency> { new WalletCurrency { CurrencyName = "بروز خطا در دریافت اطلاعات" } });

                var res = await _fund.GetWalletCurrencyAsync(new Wallet { UserId = user.Id });

                return View(res);
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
        public async Task<ActionResult> Windrow()
        {
            try
            {
                var user = _session.Get<User>("UserInfo");

                if (user == null)
                    return View(new List<WalletCurrency> { new WalletCurrency { CurrencyName = "بروز خطا در دریافت اطلاعات" } });

                var res = await _fund.GetWalletCurrencyAsync(new Wallet { UserId = user.Id });

                return View(res);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [GoldAuthorize]
        public async Task<ActionResult> Exchange()
        {
            try
            {
                var user = _session.Get<User>("UserInfo");

                if (user == null)
                    return View(new List<WalletCurrency> { new WalletCurrency { CurrencyName = "بروز خطا در دریافت اطلاعات" } });

                var res = await _fund.GetWalletCurrencyAsync(new Wallet { UserId = user.Id });

                return View(res);
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPatch]
        [GoldAuthorize]
        public async Task<ActionResult> Windrow(WalletCurrency model)
        {
            try
            {
                var res = await _fund.Windrow(model);

                if (res != null)
                {

                    if (res.StatusCode != 200)
                        return Json(new { result = false, message = "عملیات ناموفق بود لطفا دوباره تلاش نمایید." });

                    if (res.StatusCode == 200 && res.Data != null)
                        return Json(new ApiResult(200));
                }

                return Json(new { result = false, message = "بروز خطا لطفا دوباره تلاش کنید." });
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPatch]
        [GoldAuthorize]
        public async Task<ActionResult> Exchange(Xchenger model)
        {
            try
            {
                var res = await _fund.Exchange(model);

                if (res != null)
                {

                    if (res.StatusCode != 200)
                        return Json(new { result = false, message = "عملیات ناموفق بود لطفا دوباره تلاش نمایید." });

                    if (res.StatusCode == 200 && res.Data != null)
                        return Json(new ApiResult(200));
                }

                return Json(new { result = false, message = "بروز خطا لطفا دوباره تلاش کنید." });
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
