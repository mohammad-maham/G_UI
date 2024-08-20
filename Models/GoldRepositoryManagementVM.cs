using System;
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
        public int Carat { get; set; }

        [Display(Name = "نوع طلا")]
        public short GoldType { get; set; }
        public long RegUserId { get; set; }
        public int Decharge { get; set; } = 0;
        public short Status { get; set; } = 1;
        [Display(Name = "از تاریخ"), Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string FromDate { get; set; }
        [Display(Name = "تا تاریخ"), Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ToDate { get; set; }
        [Display(Name = "کاربر")]
        public long UserId { get; set; }
    }

    [Serializable]
    public class GoldRepositoryManagementReportVM
    {
        public long TransactionId { get; set; }
        public string GoldType { get; set; }
        public string GoldMaintenanceType { get; set; }
        public int Carat { get; set; }
        public long RegUserId { get; set; }
        public double LastGoldValue { get; set; }
        public double NewGoldValue { get; set; }
        public string TransactionType { get; set; }
        public double Weight { get; set; }
        public string RegPersianDate { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string ArchiveOperation { get; set; }
    }
}