using G_APIs.Model;
using G_APIs.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace G_APIs.BussinesLogic.Interface
{

    public interface IFund
    {
        WalletCurrency GetWallet(Wallet model);
        List<WalletCurrency> GetWalletCurrencyAsync(Wallet model);
        List<WalletCurrency> GetWalletCurrency(Wallet model);
        ApiResult AddTransaction(TransactionVM model);
        ApiResult Deposit(PaymentLinkRequest model,string token);
        IEnumerable<WalletBankAccount> GetBankAccounts(Wallet model);
        ApiResult AddBankAccount(WalletBankAccount model);
        ApiResult ToggleBankCard(WalletBankAccount model);
        IEnumerable<ReportVM> GetTransactions(FilterVM model);
        IEnumerable<ReportVM> GetFinancialReport(FilterVM model);
        IEnumerable<ReportVM> GetExchanges(FilterVM model);
        ApiResult  ConfirmTransaction(TransactionVM model);

    }
}
