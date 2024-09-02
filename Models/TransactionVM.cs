using System;

namespace G_APIs.Models
{

    public class TransactionVM
    {
        public long Id { get; set; }
        public long? TransactionConfirmId { get; set; }
        public long WalletId { get; set; }
        public long WalletCurrencyId { get; set; }
        public short TransactionTypeId { get; set; }
        public short TransactionModeId { get; set; }
        public short? Status { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Info { get; set; }
        public string OrderId { get; set; }
        public string TrackingCode { get; set; }
        public decimal? Amount { get; set; }
        public long ConfirmationUserId { get; set; }
        public DateTime ConfirmationDate { get; set; }
        public string RequestDescription { get; set; }
        public string Description { get; set; }
        public string ResponceDescription { get; set; }
        public string TransactionInfo { get; set; }

    }
}