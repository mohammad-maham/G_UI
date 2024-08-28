using System;
using System.ComponentModel.DataAnnotations;

namespace G_APIs.Model
{
    public class FilterVM
    {

        public int UserId { get; set; }

        [Display(Name = "از تاریخ"), Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string FromDate { get; set; }
        [Display(Name = "تا تاریخ"), Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ToDate { get; set; }
        public long? WalletId { get; set; }
        public long? WalletCurrencyId { get; set; }
        public short? TransactionTypeId { get; set; }
        public short? TransactionModeId { get; set; }
        public short? CurrencyId { get; set; }

    }
}
