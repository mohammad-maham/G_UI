using G_APIs.BussinesLogic.Interface;
using G_APIs.Models;
using G_APIs.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace G_APIs.Controllers
{
    public class StoreController : Controller
    {
        private readonly IStore _store;
        private readonly IFund _wallet;
        private readonly ISession _session;
        private readonly IAccount _account;

        public StoreController(IStore store, ISession session, IFund wallet, IAccount account)
        {
            _store = store;
            _session = session;
            _wallet = wallet;
            _account = account;
        }

        #region GoldShopping
        [HttpGet]
        [GoldAuthorize]
        public ActionResult BuyIndex()
        {
            string token = Request.Cookies["gldauth"].Value;
            User userInfo = _session.Get<User>("UserInfo");
            double buyPrice = _store.GetOnlineBuyPrice(new PriceCalcVM()
            {
                GoldCalcType = CalcTypes.buy,
                GoldWeight = 1
            }, token);

            OrderVM orderVM = new OrderVM
            {
                CurrentOnlinePrice = buyPrice,
            };
            return View(orderVM);
        }

        [GoldAuthorize]
        public ActionResult SubmitBuy(OrderPerformVM buyVM)
        {
            ApiResult response = new ApiResult();
            User userInfo = _session.Get<User>("UserInfo");
            string token = Request.Headers["Authorization"];

            try
            {
                if (userInfo != null && !string.IsNullOrEmpty(token))
                {
                    // Business
                    double buyPrice = _store.GetOnlineBuyPrice(new PriceCalcVM()
                    {
                        GoldCalcType = CalcTypes.buy,
                        GoldWeight = buyVM.Weight
                    }, token);
                    WalletCurrency walletCurrency = _wallet.GetWallet(new Wallet() { UserId = userInfo.Id });

                    // Binding
                    buyVM.Carat = 750;
                    buyVM.GoldType = 2;
                    buyVM.SourceAmount = buyPrice;
                    buyVM.WalleId = walletCurrency.Id;
                    buyVM.UserId = userInfo.Id.Value;
                    buyVM.DestinationAmount = buyVM.Weight;
                    buyVM.CurrentCalculatedPrice = buyPrice;
                    buyVM.SourceWalletCurrency = 1;
                    buyVM.DestinationWalletCurrency = 2;

                    // Perform Buy
                    response = _store.PerformBuy(buyVM, token);

                    // Response Reaction
                    if (response.StatusCode != 200)
                    {
                        return Json(new { result = false, message = response.Message });
                    }
                }
                else
                {
                    return Json(new { result = false, message = "ورود غیر مجاز لطفا دوباره وارد شوید." });
                }

                AlertMessaging.AddToUserQueue(new MessageContext(response.Message, type: MessageType.Success));
                double transactionId = !string.IsNullOrEmpty(response.Data) ? long.Parse(response.Data) : 0;
                return View("OrderResult", transactionId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [GoldAuthorize]
        public ActionResult SellIndex()
        {
            string token = Request.Cookies["gldauth"].Value;
            User userInfo = _session.Get<User>("UserInfo");
            double buyPrice = _store.GetOnlineBuyPrice(new PriceCalcVM()
            {
                GoldCalcType = CalcTypes.sell,
                GoldWeight = 2
            }, token);

            OrderVM orderVM = new OrderVM
            {
                CurrentOnlinePrice = buyPrice,
            };
            return View(orderVM);
        }

        [GoldAuthorize]
        public ActionResult SubmitSell(OrderPerformVM sellVM)
        {
            ApiResult response = new ApiResult();
            User userInfo = _session.Get<User>("UserInfo");
            string token = Request.Headers["Authorization"];

            try
            {
                if (userInfo != null && !string.IsNullOrEmpty(token))
                {
                    // Business
                    double sellPrice = _store.GetOnlineBuyPrice(new PriceCalcVM()
                    {
                        GoldCalcType = CalcTypes.sell,
                        GoldWeight = sellVM.Weight
                    }, token);
                    WalletCurrency walletCurrency = _wallet.GetWallet(new Wallet() { UserId = userInfo.Id });

                    // Binding
                    sellVM.Carat = 750;
                    sellVM.GoldType = 2;
                    sellVM.SourceAmount = sellVM.Weight;
                    sellVM.WalleId = walletCurrency.Id;
                    sellVM.UserId = userInfo.Id.Value;
                    sellVM.DestinationAmount = sellPrice;
                    sellVM.CurrentCalculatedPrice = sellPrice;
                    sellVM.SourceWalletCurrency = 2;
                    sellVM.DestinationWalletCurrency = 1;

                    // Perform Sell
                    response = _store.PerformSell(sellVM, token);

                    // Response Reaction
                    if (response.StatusCode != 200)
                    {
                        return Json(new { result = false, message = response.Message });
                    }
                }
                else
                {
                    return Json(new { result = false, message = "ورود غیر مجاز لطفا دوباره وارد شوید." });
                }

                AlertMessaging.AddToUserQueue(new MessageContext(response.Message, type: MessageType.Success));
                double transactionId = !string.IsNullOrEmpty(response.Data) ? long.Parse(response.Data) : 0;
                return View("OrderResult", transactionId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion GoldShopping
        #region GoldOnlinePrices
        public ActionResult GoldOnlinePrice(bool isBuy = true)
        {
            string token = Request.Cookies["gldauth"].Value;
            CalcTypes priceCalcType = isBuy ? CalcTypes.buy : CalcTypes.sell;
            double price = _store.GetOnlineBuyPrice(new PriceCalcVM()
            {
                GoldCalcType = priceCalcType,
                GoldWeight = 1
            }, token);
            return View(new OnlinePriceVM() { Price = price, CalcTypes = priceCalcType });
        }

        [GoldAuthorize]
        public double GetOnlinePrice(string weight = "1", int calcType = 1)
        {
            try
            {
                string token = Request.Cookies["gldauth"].Value;
                double buyPrice = _store.GetOnlineBuyPrice(new PriceCalcVM()
                {
                    GoldCalcType = calcType == 1 ? CalcTypes.buy : CalcTypes.sell,
                    GoldWeight = double.Parse(weight)
                }, token);
                return buyPrice;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion GoldOnlinePrices
        #region GoldRepositoryManagement
        public ActionResult RepositoryManagementIndex()
        {
            string token = Request.Cookies["gldauth"].Value;
            GoldTypesVM goldTypes = new GoldTypesVM();
            List<SelectListItem> lstGoldTypesSelect = new List<SelectListItem>();
            List<SelectListItem> lstGoldCaratsSelect = new List<SelectListItem>();
            GoldRepositoryStatusVM goldRepositoryStatus = new GoldRepositoryStatusVM();
            GoldRepositoryManagementVM managementVM = new GoldRepositoryManagementVM();

            if (!string.IsNullOrEmpty(token))
            {
                goldRepositoryStatus = _store.GetGoldRepositoryStatus(token);
                goldTypes = _store.GetGoldTypes(token);
                if (goldTypes != null)
                {
                    if (goldTypes.GoldTypes.Count > 0)
                    {
                        foreach (GoldType goldType in goldTypes.GoldTypes)
                        {
                            lstGoldTypesSelect.Add(new SelectListItem() { Text = goldType.Name, Value = goldType.Id.ToString() });
                        }
                    }
                    if (goldTypes.GoldCarats.Count > 0)
                    {
                        foreach (GoldCarat goldCarat in goldTypes.GoldCarats)
                        {
                            lstGoldCaratsSelect.Add(new SelectListItem() { Text = goldCarat.Name, Value = goldCarat.Value.ToString() });
                        }
                    }
                }
            }
            foreach (var item in goldRepositoryStatus.GoldRepositoryVM)
                item.MaintenanceType = item.GoldMaintenanceType == 10 ? "مالکیتی" : "وکالتی/امانتی";
            ViewBag.Carats = lstGoldCaratsSelect;
            ViewBag.Types = lstGoldTypesSelect;
            managementVM.GoldRepositoryStatus = goldRepositoryStatus;
            return View(managementVM);
        }

        [HttpPost]
        public ActionResult SubmitRepositoryCharge(GoldRepositoryManagementVM managementVM)
        {
            string token = Request.Cookies["gldauth"].Value;
            User userInfo = _session.Get<User>("UserInfo");

            if (managementVM != null && !string.IsNullOrEmpty(token))
            {
                managementVM.Decharge = managementVM.SubmitType == 2 ? 1 : 0;
                managementVM.RegUserId = userInfo.Id.Value;

                ApiResult response = _store.ChargeRepository(managementVM, token);
                if (response.StatusCode != 200)
                {
                    return Json(new { result = false, message = response.Message });
                }
                AlertMessaging.AddToUserQueue(new MessageContext(response.Message, type: MessageType.Success));
            }
            return View(managementVM);
        }

        public ActionResult RepositoryReportIndex()
        {
            string token = Request.Cookies["gldauth"].Value;

            GoldTypesVM goldTypes = new GoldTypesVM();
            List<GetUsersVM> users = new List<GetUsersVM>();
            List<SelectListItem> lstGoldTypesSelect = new List<SelectListItem>();
            List<SelectListItem> lstGoldCaratsSelect = new List<SelectListItem>();
            List<SelectListItem> lstUsersSelect = new List<SelectListItem>();

            goldTypes = _store.GetGoldTypes(token);
            users = _account.GetUsers(token);

            if (goldTypes != null)
            {
                if (goldTypes.GoldTypes.Count > 0)
                {
                    foreach (GoldType goldType in goldTypes.GoldTypes)
                    {
                        lstGoldTypesSelect.Add(new SelectListItem() { Text = goldType.Name, Value = goldType.Id.ToString() });
                    }
                }
                if (goldTypes.GoldCarats.Count > 0)
                {
                    foreach (GoldCarat goldCarat in goldTypes.GoldCarats)
                    {
                        lstGoldCaratsSelect.Add(new SelectListItem() { Text = goldCarat.Name, Value = goldCarat.Value.ToString() });
                    }
                }
            }
            if (users != null && users.Count > 0)
            {
                foreach (GetUsersVM user in users)
                {
                    lstUsersSelect.Add(new SelectListItem() { Text = user.Username, Value = user.UserId.Value.ToString() });
                }
            }

            ViewBag.Carats = lstGoldCaratsSelect;
            ViewBag.Types = lstGoldTypesSelect;
            ViewBag.Users = lstUsersSelect;
            return View(new GoldRepositoryManagementVM());
        }

        public ActionResult RepositoryReportIndexData(GoldRepositoryManagementVM managementVM)
        {
            string token = Request.Cookies["gldauth"].Value;

            IEnumerable<GoldRepositoryManagementReportVM> reportVM = Enumerable.Empty<GoldRepositoryManagementReportVM>();
            if (managementVM != null && !string.IsNullOrEmpty(managementVM.FromDate) && !string.IsNullOrEmpty(managementVM.ToDate))
            {
                reportVM = _store.GetGoldRepositoryReports(managementVM, token);
            }
            return View(reportVM);
        }
        #endregion GoldRepositoryManagement
    }
}