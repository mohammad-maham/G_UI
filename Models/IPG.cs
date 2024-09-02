﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G_APIs.Models
{
    public class PaymentLinkRequest
    {
        public long? UserId { get; set; }
        public long? WalletId { get; set; }
        public long? WallectCurrencyId { get; set; }
        public long? TransactionConfirmId { get; set; }

        public string  Title { get; set; }

        public long? Price { get; set; }

        public int? CallBackType { get; set; }

        public string  OrderId { get; set; }

        public DateTime ExpDate { get; set; }

        public string AccLinkReqConf { get; set; }

        public string Guid { get; set; }

        public string CallBackURL { get; set; }

        public string ClientMobile { get; set; }

        public FactorDataModel  FactorData { get; set; }

    }

    public class FactorDataModel
    {
        public FactorHeader Header { get; set; }
        public List<FactorItem> Items { get; set; }
        public FactorFooter Footer { get; set; }
    }

    public class FactorHeader
    {
        public int FactorId { get; set; }
        public int FactorStatus { get; set; }
        public string FactorTitle { get; set; }
        public string FactorDescription { get; set; }
        public string CutomerName { get; set; }
        public long CustomerId { get; set; }
        public string CustomerTelephone { get; set; }
        public string CustomerMobile { get; set; }
        public DateTime CreateDate { get; set; }
        public PayDetail PaymentDetail { get; set; }

    }
    public class PayDetail
    {
        public string PaymentDescription { get; set; }
        public int PaymentStatus { get; set; }
        public int TransactionId { get; set; }
        public DateTime PaymentDate { get; set; }

    }
    public class FactorItem
    {
        public string ItemTitle { get; set; }
        public long ItemUnitPrice { get; set; }
        public long ItemCount { get; set; }
        public string ItemUnitType { get; set; }
        public long ItemDiscount { get; set; }
        public long ItemSumPrice { get; set; }
        public string ItemDesctription { get; set; }
    }
    public class FactorFooter
    {
        public double FactorVAT { get; set; }
        public long FactorSumPrice { get; set; }
        public long FactorSumPriceByVAT { get; set; }
        public string SellerName { get; set; }
        public List<string> SellerTelephone { get; set; }
        public List<string> SellerAddress { get; set; }
    }
}