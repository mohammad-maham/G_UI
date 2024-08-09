using System;
using System.Collections.Generic;

namespace G_APIs.Models
{
    public partial class Transaction
    {
        public long Id { get; set; }

        public long WalletId { get; set; }

        public long WalletCurrencyId { get; set; }

        public short TransactionTypeId { get; set; }

        public short TransactionModeId { get; set; }

        public short? Status { get; set; }

        public DateTime TransactionDate { get; set; }

        public string PersianDate
        {
            get
            {
                return new PersianDateTime(TransactionDate).ToString("yyyy/MM/dd HH:mm:ss");
            }
        }
        public string Info { get; set; }

        public string OrderId { get; set; }

        public string TrackingCode { get; set; }
        public decimal Amount { get; set; }

    }
}
