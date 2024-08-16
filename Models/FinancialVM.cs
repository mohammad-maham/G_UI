using System;

namespace G_APIs.Models
{
    public class FinancialVM
    {

        public long Id { get; set; }
        public long WalletId { get; set; }
        public short TransactionTypeId { get; set; }
        public DateTime TransactionDate { get; set; }
        public long SourceWalletCurrency { get; set; }
        public long DestinationWalletCurrency { get; set; }
        public decimal SourceAmount { get; set; }
        public decimal DestinationAmout { get; set; }
        public string PersianDate
        {
            get
            {
                if (TransactionDate is DateTime pdate)
                    return new PersianDateTime(pdate).ToString("yyyy/MM/dd HH:mm:ss");

                return string.Empty;
            }
        }
    }
}
