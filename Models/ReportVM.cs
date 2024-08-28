﻿
namespace G_APIs.Models
{
    public partial class ReportVM
    {
        public long? Id { get; set; }

        public long? WalletId { get; set; }
        public long? WalletCurrencyId { get; set; }

        public int TransactionTypeId { get; set; }
        public string TransactionType { get; set; }

        public string TransactionMode { get; set; }
        public short? TransactionModeId { get; set; }

        public short? Status { get; set; }

        public string RepDate { get; set; }

        public string Info { get; set; }

        public string OrderId { get; set; }

        public string TrackingCode { get; set; }

        public decimal Amount { get; set; }

        public string SourceAddress { get; set; }

        public string DestinationAddress { get; set; }

        public long? SourceWalletCurrencyId { get; set; }
        public string SourceWalletCurrency { get; set; }

        public long? DestinationWalletCurrencyId { get; set; }
        public string DestinationWalletCurrency { get; set; }

        public decimal? SourceAmount { get; set; }

        public long? UserId { get; set; }

        public decimal? DestinationAmout { get; set; }


    }

}

