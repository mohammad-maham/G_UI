using System.ComponentModel.DataAnnotations;

namespace G_APIs.Models
{
    public class ThresholdsVM
    {
        [Display(Name = "قیمت آنلاین طلا (خام)")]
        public double GoldOnlinePrice { get; set; }
        [Display(Name = "قیمت آنلاین من")]
        public double ThresholdCurrentPrice { get; set; }
        [Display(Name = "درصد حد خرید")]
        public double ThresholdBuyPercentage { get; set; }
        [Display(Name = "درصد حد فروش")]
        public double ThresholdSellPercentage { get; set; }
        [Display(Name = "قیمت حد خرید")]
        public double ThresholdBuyPrice { get; set; }
        [Display(Name = "قیمت حد فروش")]
        public double ThresholdSellPrice { get; set; }
        [Display(Name = "درصد/قیمت")]
        public int IsPercentage { get; set; }
        [Display(Name = "تاریخ انقضاء قیمت")]
        public string ThresholdExpireDate { get; set; }
    }
}