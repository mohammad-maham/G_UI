using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace G_APIs.Models
{
    public partial class Transaction
    {
        public long Id { get; set; }

        public long WalletId { get; set; }


        [Display(Name = " نوع ارز  ")]
        public long WalletCurrencyId { get; set; }

        [Display(Name = " مدل تراکنش   ")]
        public short TransactionTypeId { get; set; }

        [Display(Name = " نوع تراکنش  ")]
        public short TransactionModeId { get; set; }

        public short? Status { get; set; }

        public DateTime TransactionDate { get; set; }

        [Display(Name = " تاریح    ")]
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
        [Display(Name = " مبلغ    ")]
        public decimal Amount { get; set; }

    }
}
