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
        ApiResult Windrow(WalletCurrency model);
        ApiResult Deposit(PaymentLinkRequest model);
        IEnumerable<WalletBankAccount> GetBankAccounts(Wallet model);
        ApiResult AddBankAccount(WalletBankAccount model);
        ApiResult ToggleBankCard(WalletBankAccount model);
        IEnumerable<Transaction> GetTransactions(Wallet model);
        IEnumerable<FinancialVM> GetFinancialReport(Wallet model);
        IEnumerable<Xchenger> GetExchanges(Wallet model);

    }
}
