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
        public short Carat { get; set; }

        [Display(Name = "نوع طلا")]
        public short GoldType { get; set; }
        public long RegUserId { get; set; }
        public int Decharge { get; set; } = 0;
        public short Status { get; set; } = 1;
        [Display(Name = "از تاریخ")]
        public string FromDate { get; set; }
        [Display(Name = "تا تاریخ")]
        public string ToDate { get; set; }
        [Display(Name = "کاربر")]
        public string Username { get; set; }
    }

    [Serializable]
    public class GoldRepositoryManagementReportVM
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public string GoldType { get; set; }
        public string Weight { get; set; }
        public string Carat { get; set; }
        public string RegUser { get; set; }
        public string RegDate { get; set; }
        public string ArchiveDate { get; set; }
        public string ArchiveOperation { get; set; }
        public string GoldMaintenanceType { get; set; }
    }
}