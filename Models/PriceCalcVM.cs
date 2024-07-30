using System.ComponentModel.DataAnnotations;

namespace G_APIs.Models
{
    public class PriceCalcVM
    {
        public int GoldCalcType { get; set; }
        public double GoldWeight { get; set; }
        public double GoldCarat { get; set; }
    }
}