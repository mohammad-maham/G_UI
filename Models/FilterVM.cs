using System;
using System.ComponentModel.DataAnnotations;
using System.Resources;

namespace G_APIs.Model
{
    public class FilterVM
    {

        public int UserId { get; set; }

        [Display(Name = "از تاریخ ")]

        public string FromDate { get; set; }

        [Display(Name = "تا تاریخ ")]
        public string ToDate { get; set; }
        public long? WalletId { get; set; }
        public long? WalletCurrencyId { get; set; }
        public short? TransactionTypeId { get; set; }
        public short? TransactionModeId { get; set; }
        public short? CurrencyId { get; set; }

    }
}
