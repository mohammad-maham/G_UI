using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace G_APIs.Models
{

    public class WalletCurrency
    {
        public long Id { get; set; }

        public long WalletId { get; set; }

        public long CurrencyId { get; set; }
        public string CurrencyName { get; set; }

        public short Status { get; set; }

        [Display(Name = "تاریخ")]
        public DateTime RegDate { get; set; }

        [Display(Name = "مبلغ")]
        [Required(ErrorMessage = "وارد کردن این فیلد الزامیست  ")]
        public double Amount { get; set; }

        public string WcAddress { get; set; }

        public short TransactionMode { get; set; }
        public short TransactionType { get; set; }
        public string Unit { get; set; }

        public string ErrorMessage { get; set; }

        [UIHint("Textarea")]
        [Display(Name = "توضیحات در صورت لزوم")]
        public string Description { get; set; }

        public long WalletCurrencyId { get; set; }


    }
}
