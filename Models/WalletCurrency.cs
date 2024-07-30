using System;
using System.Collections.Generic;

namespace G_APIs.Models { 

public  class WalletCurrency
{
    public long Id { get; set; }

    public long WalletId { get; set; }

    public long CurrencyId { get; set; }
    public string  CurrencyName { get; set; }

    public short Status { get; set; }

    public DateTime RegDate { get; set; }

    public decimal Amount { get; set; }

    public string  WcAddress { get; set; }

    public short TransactionMode { get; set; }
    public short TransactionType { get; set; }
    public string  Unit { get; set; }

}
}
