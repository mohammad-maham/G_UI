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
        public string PersianDate
        {
            get
            {
                if (RegDate is DateTime pdate)
                    return new PersianDateTime(pdate).ToString("yyyy/MM/dd HH:mm:ss");

                return string.Empty;
            }
        }

        [Display(Name = "مبلغ")]
        [Required(ErrorMessage = "وارد کردن این فیلد الزامیست  ")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "لطفا رقم وارد کنید")]
        [Range(10000, double.MaxValue,ErrorMessage="حداقل مبلغ 100000 ریال میباشد.")]
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

        public string BankCard { get; set; }

    }
}
