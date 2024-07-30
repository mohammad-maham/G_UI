using G_APIs.BussinesLogic.Interface;
using G_APIs.Common;
using G_APIs.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;
namespace G_APIs.Controllers
{

    public class FundController : Controller
    {
        private readonly IFund _fund;

        public FundController(IFund fund)
        {
            _fund = fund;
        }

        [GoldAuthorize]
        public ActionResult Wallet(WalletCurrency model)
        {
            try
            {
                model.Id = 1;
                 var res = _fund.GetWallet(model);

                return View(new List<WalletCurrency>());
            }
            catch (Exception ex)
            {

                return Json(new { result = false, message = ex.Message });
            }

        }
    }
}
