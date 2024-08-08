using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace G_APIs.Models
{
    public class OrderVM
    {
        [Display(Name = "وزن(گرم)"), Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Weight { get; set; }
        [Display(Name = "شناسه کاربری"), Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long UserId { get; set; }
        [Display(Name = "شناسه کیف پول"), Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long WalleId { get; set; }
        [Display(Name = "آدرس مبدأ"), Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long? SourceAddress { get; set; } = 1; // Currency
        [Display(Name = "آدرس مقصد"), Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long? DestinationAddress { get; set; } = 2; // Gold
        [Display(Name = "مقدار درخواستی"), Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public double SourceAmount { get; set; }
        public double DestinationAmount { get; set; }
        [Display(Name = "قیمت لحظه ای"), Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public double CurrentOnlinePrice { get; set; }
        public double CurrentCalculatedPrice { get; set; }
        [Display(Name = "عیار")]
        public List<SelectListItem> Carat { get; set; }
    }

    public class OrderPerformVM
    {
        public int Weight { get; set; }
        public long UserId { get; set; }
        public long WalleId { get; set; }
        public string SourceAddress { get; set; } = "1"; // Currency
        public string DestinationAddress { get; set; } = "2"; // Gold
        public double SourceAmount { get; set; }
        public double DestinationAmount { get; set; }
        public double CurrentCalculatedPrice { get; set; }
        public int Carat { get; set; } = 750;
        public int GoldType { get; set; } = 2; // Remittance
        public int? SourceWalletCurrency { get; set; } = 1; // Currency
        public int? DestinationWalletCurrency { get; set; } = 2; // Gold
    }

    public enum CalcTypes
    {
        none = 0,
        buy = 1,
        sell = 2
    }

    public class PriceCalcVM
    {
        public CalcTypes GoldCalcType { get; set; }
        public double GoldWeight { get; set; }
        public double GoldCarat { get; set; }
    }
}