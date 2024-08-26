using System;
using System.ComponentModel.DataAnnotations;

namespace G_APIs.Models
{
    public class ThresholdsVM
    {
        [Display(Name = "قیمت آنلاین طلا (خام)")]
        public double GoldOnlinePrice { get; set; }
        [Display(Name = "قیمت آنلاین من")]
        public double? ThresholdCurrentPrice { get; set; }
        [Display(Name = "درصد حد خرید")]
        public double? ThresholdBuyPercentage { get; set; }
        [Display(Name = "درصد حد فروش")]
        public double? ThresholdSellPercentage { get; set; }
        [Display(Name = "قیمت حد خرید")]
        public double? ThresholdBuyPrice { get; set; }
        [Display(Name = "قیمت حد فروش")]
        public double? ThresholdSellPrice { get; set; }
        [Display(Name = "درصد/قیمت")]
        public int IsPercentage { get; set; }
        [Display(Name = "ساعت انقضاء قیمت")]
        public string ThresholdExpireDate { get; set; }
        [Display(Name = "قیمت پایه من"), Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public double CurrentPrice { get; set; } = 1;
        public short IsOnlinePrice { get; set; }
    }

    public partial class AmountThresholdVM
    {
        public long Id { get; set; }

        public short Status { get; set; }

        public long RegUserId { get; set; }

        public double BuyThreshold { get; set; }

        public double SelThreshold { get; set; }

        public double? CurrentPrice { get; set; }

        public DateTime RegDate { get; set; }

        public DateTime ExpireEffectDate { get; set; }

        public short IsOnlinePrice { get; set; }
    }

}