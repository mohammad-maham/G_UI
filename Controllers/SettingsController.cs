using G_APIs.BussinesLogic.Interface;
using G_APIs.Models;
using G_APIs.Services;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Web.Mvc;

namespace G_APIs.Controllers
{
    public class SettingsController : Controller
    {
        private readonly IStore _store;
        private readonly IFund _wallet;
        private readonly ISession _session;
        private readonly IAccount _account;
        private readonly ISettings _settings;

        public SettingsController(IStore store, ISession session, IFund wallet, IAccount account, ISettings settings)
        {
            _store = store;
            _session = session;
            _wallet = wallet;
            _account = account;
            _settings = settings;
        }

        #region ThresholdsManagement
        [HttpGet]
        [GoldAuthorize]
        public ActionResult ThresholdPriceManagementIndex(ThresholdsVM thresholds)
        {
            string token = Request.Cookies["gldauth"].Value;
            User userInfo = _session.Get<User>("UserInfo");
            double currentOnlinePrice = _store.GetOnlineBuyPrice(new PriceCalcVM()
            {
                GoldCalcType = CalcTypes.none,
                GoldWeight = 1
            }, token);

            double thresholdPrice = _store.GetOnlineBuyPrice(new PriceCalcVM()
            {
                GoldCalcType = CalcTypes.threshold,
                GoldWeight = 1
            }, token);

            thresholds.ThresholdCurrentPrice = thresholdPrice;
            thresholds.GoldOnlinePrice = currentOnlinePrice;

            return View(thresholds);
        }

        [HttpPost]
        [GoldAuthorize]
        public ActionResult SubmitThreshold(ThresholdsVM thresholds)
        {
            AmountThresholdVM amount = new AmountThresholdVM();
            string token = Request.Cookies["gldauth"].Value;
            User userInfo = _session.Get<User>("UserInfo");

            // Conditions
            if (userInfo != null && userInfo.Id != 0)
            {
                if (thresholds != null
                    && !string.IsNullOrEmpty(thresholds.ThresholdExpireDate)
                    && (thresholds.IsPercentage == 0 && thresholds.ThresholdSellPrice != null && thresholds.ThresholdSellPrice >= 1
                    && thresholds.ThresholdBuyPrice != null && thresholds.ThresholdBuyPrice >= 1)
                     || (thresholds.IsPercentage == 1 && thresholds.ThresholdBuyPercentage > 0 && thresholds.ThresholdSellPercentage > 0))
                {
                    // Convert To Percentage
                    if (thresholds.IsPercentage == 1 && thresholds.ThresholdBuyPercentage > 0 && thresholds.ThresholdSellPercentage > 0)
                    {
                        thresholds.ThresholdBuyPrice = (thresholds.ThresholdBuyPercentage / 100d);
                        thresholds.ThresholdSellPrice = (thresholds.ThresholdSellPercentage / 100d);
                    }
                    string convertedDate = DateTime.Now.ToString($"yyyy-MM-ddT{thresholds.ThresholdExpireDate}:00Z");
                    // Filling API Model
                    amount.BuyThreshold = thresholds.ThresholdBuyPrice.Value;
                    amount.SelThreshold = thresholds.ThresholdSellPrice.Value;
                    amount.CurrentPrice = thresholds.CurrentPrice;
                    amount.ExpireEffectDate = DateTime.Parse(convertedDate).ToUniversalTime();
                    amount.RegUserId = userInfo.Id.Value;
                    amount.Status = 1;
                    amount.IsOnlinePrice = thresholds.IsOnlinePrice;

                    // Send Request
                    ApiResult response = _settings.SubmitThreshold(amount, token);

                    // Parse Response
                    if (response.StatusCode != 200)
                        AlertMessaging.AddToUserQueue(new MessageContext(response.Message, type: MessageType.Error));
                    else
                        amount = JsonConvert.DeserializeObject<AmountThresholdVM>(response.Data) ?? new AmountThresholdVM();
                    return View(amount);
                }
                return Json(new { result = false, message = "لطفا مقادیر فرم را بررسی و دوباره تکمیل نمائید" });
            }
            return Json(new { result = false, message = "لطفا دوباره به سامانه وارد شوید" });
        }
        #endregion ThresholdsManagement
    }
}