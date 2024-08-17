using System.ComponentModel.DataAnnotations;

namespace G_APIs.Models
{
    public class GoldRepositoryManagementVM
    {
        public int SubmitType { get; set; } // Increase/Decrease
        public GoldRepositoryStatusVM GoldRepositoryStatus { get; set; }

        [Display(Name = "مقدار طلا")]
        public int Weight { get; set; }

        [Display(Name = "عیار")]
        public short Carat { get; set; }

        [Display(Name = "نوع طلا")]
        public short GoldType { get; set; }
        public long RegUserId { get; set; }
        public int Decharge { get; set; } = 0;
        public short Status { get; set; } = 1;
    }
}