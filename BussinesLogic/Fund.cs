﻿using G_APIs.BussinesLogic.Interface;
using G_APIs.Model;
using G_APIs.Models;
using G_APIs.Services;
using G_Wallet_API.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using static G_APIs.Common.Enums;

namespace G_APIs.BussinesLogic
{

    public class Fund : IFund
    {
        public WalletCurrency GetWallet(Wallet model)
        {
            //var res = new GoldApi("http://localhost:5017/api/Fund/GetWallet", model).Post();
            var res = new GoldApi(GoldHost.Wallet, "/api/Fund/GetWallet", model).Post();
            var w = JsonConvert.DeserializeObject<WalletCurrency>(res.Data);

            return w;
        }

        public List<WalletCurrency> GetWalletCurrency(Wallet model)
        {
            //var res = new GoldApi("http://localhost:5017/api/Fund/GetWalletCurrency", model).Post();
            var res = new GoldApi(GoldHost.Wallet, "/api/Fund/GetWalletCurrency", model).Post();

            var w = JsonConvert.DeserializeObject<List<WalletCurrency>>(res.Data);

            return w;
        }

        public List<WalletCurrency> GetWalletCurrencyAsync(Wallet model)
        {
            var res = new GoldApi(GoldHost.Wallet, "/api/Fund/GetWalletCurrency", model).Post();

            var w = JsonConvert.DeserializeObject<List<WalletCurrency>>(res.Data);

            return w;
        }

        public ApiResult Deposit(PaymentLinkRequest model, string token)
        {
            // var res = new GoldApi("http://localhost:5171/IPG/AddPaymentData", model).Post();
            var res = new GoldApi(GoldHost.IPG, "/IPG/AddPaymentData", model, authorization: token).Post();
            return res;
        }

        public ApiResult AddTransaction(TransactionVM model)
        {
            //var res = new GoldApi("http://localhost:5017/api/Fund/AddTransaction", model).Post();
            var res = new GoldApi(GoldHost.Wallet, "/api/Fund/AddTransaction", model).Post();
            return res;
        }

        public IEnumerable<WalletBankAccount> GetBankAccounts(Wallet model)
        {
            var t = new GoldApi(GoldHost.Wallet, "/api/Fund/GetBankAccounts", model).Post();
            var res = JsonConvert.DeserializeObject<List<WalletBankAccount>>(t.Data);
            return res;
        }

        public ApiResult AddBankAccount(WalletBankAccount model)
        {
            var res = new GoldApi(GoldHost.Wallet, "/api/Fund/AddBankAccount", model).Post();
            return res;
        }

        public ApiResult ToggleBankCard(WalletBankAccount model)
        {
            var res = new GoldApi(GoldHost.Wallet, "/api/Fund/ToggleBankCard", model).Post();
            return res;
        }
        public IEnumerable<ReportVM> GetTransactions(FilterVM model)
        {
            //var t = new GoldApi("http://localhost:5017/api/Fund/GetTransactions", model).Post();
            var t = new GoldApi(GoldHost.Wallet, "/api/Fund/GetTransactions", model).Post();
            var res = JsonConvert.DeserializeObject<List<ReportVM>>(t.Data);
            return res;
        }

        public IEnumerable<ReportVM> GetFinancialReport(FilterVM model)
        {
            // var t = new GoldApi("http://localhost:5017/api/Fund/GetFinancialReport", model).Post();
            var t = new GoldApi(GoldHost.Wallet, "/api/Fund/GetFinancialReport", model).Post();
            var res = JsonConvert.DeserializeObject<List<ReportVM>>(t.Data);
            return res;
        }

        public IEnumerable<ReportVM> GetExchanges(FilterVM model)
        {
            //var t = new GoldApi("http://localhost:5017/api/Fund/GetExchanges", model).Post();
            var t = new GoldApi(GoldHost.Wallet, "/api/Fund/GetExchanges", model).Post();
            var res = JsonConvert.DeserializeObject<List<ReportVM>>(t.Data);
            return res;
        }

        public ApiResult ConfirmTransaction(TransactionVM model)
        {
            //var t = new GoldApi("http://localhost:5017/api/Fund/ConfirmTransaction", model).Post();
            var t = new GoldApi(GoldHost.Wallet, "/api/Fund/ConfirmTransaction", model).Post();
            //var res = JsonConvert.DeserializeObject<WalletCurrency>(t.Data);
            return t;
        }
    }
}
